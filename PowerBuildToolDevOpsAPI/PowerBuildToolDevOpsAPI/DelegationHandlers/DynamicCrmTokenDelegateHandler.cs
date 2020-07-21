using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PowerBuildToolDevOpsAPI
{
    public class DynamicCrmTokenDelegateHandler : DelegatingHandler
    {
        private readonly ICrmTokenService _tokenService;

        public DynamicCrmTokenDelegateHandler(ICrmTokenService tokenService)
        {
            _tokenService = tokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //Genrate CRM token for each Dynamic365 CRUD operation

            var token = await _tokenService.GetAccessToken();//once for session
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return await base.SendAsync(request, cancellationToken);
        }


        public DynamicCrmTokenDelegateHandler()
            : base(new HttpClientHandler()) { }
        public DynamicCrmTokenDelegateHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        { }



    }
}
