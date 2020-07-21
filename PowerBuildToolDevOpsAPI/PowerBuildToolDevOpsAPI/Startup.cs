using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;
using PowerBuildToolDevOpsAPI.ConfigSections;
using PowerBuildToolDevOpsAPI.CRMOperations;
using PowerBuildToolDevOpsAPI.DevOpsBuilds;
using PowerBuildToolDevOpsAPI.ObjectPooling;

namespace PowerBuildToolDevOpsAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            DevOpsConnectionPoolPolicy DevOpsPolicy = null;//default

            services.AddControllers();
            services.AddSingleton<IBuildOperations, BuildOperations>();
            services.AddSingleton<IWorkItemOperation, WorkItemOperation>();

            //Poling objects
            var DevOpsAuths = new DevOpsAuths();
            Configuration.GetSection(DevOpsAuths.DevOpsAuth).Bind(DevOpsAuths);

            if (DevOpsAuths != null) //must have PAT and URL Project
            {
                DevOpsPolicy = new DevOpsConnectionPoolPolicy(DevOpsAuths.PAT, DevOpsAuths.CollUrl);
                services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();

                services.AddSingleton<ObjectPool<DevOpsConnectionPool>>(serviceProvider =>
                {
                    var provider = serviceProvider.GetRequiredService<ObjectPoolProvider>();
                    return provider.Create(DevOpsPolicy);

                });
            }
         

           // //CRM Operations             
           // services.AddSingleton<ICrmTokenService, CrmTokenService>();//Not fo rnow
           // services.AddSingleton<ISolutionManager, Soluitonmanager>();//Not fo rnow
           // services.AddTransient<DynamicCrmTokenDelegateHandler>(); //Not for now

           // services.AddHttpClient("simpleClient", c =>
           //     {
           //         c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           //         c.DefaultRequestHeaders.Add("OData-MaxVersion", "4.0");
           //         c.DefaultRequestHeaders.Add("OData-Version", "4.0");
           //         c.DefaultRequestHeaders.Add("Prefer", "odata.include-annotations=\"*\"");
           //     }
           //).AddHttpMessageHandler<DynamicCrmTokenDelegateHandler>()
           //.SetHandlerLifetime(TimeSpan.FromMinutes(5))
           //.AddPolicyHandler(GetRetryPolicy());



            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DevOps API", Version = "v1" });
            });

        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevOps API V1");
            });

        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            //return HttpPolicyExtensions
            //    .HandleTransientHttpError()
            //    .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            //    .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            Random jitterer = new Random();
            var retryWithJitterPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(3,    // exponential back-off plus some jitter
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                                  + TimeSpan.FromMilliseconds(jitterer.Next(0, 100))
                );
            return retryWithJitterPolicy;


            //var delay = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(1), retryCount: 5);

            //var retryPolicy = Policy
            //    .Handle<Exception>()
            //    .WaitAndRetryAsync(delay);

            //return retryPolicy;
        }
    }
}
//ObjectPooling
//https://docs.microsoft.com/en-us/aspnet/core/performance/objectpool?view=aspnetcore-3.1
//https://developpaper.com/detailed-explanation-of-object-pools-various-usages-in-net-core/