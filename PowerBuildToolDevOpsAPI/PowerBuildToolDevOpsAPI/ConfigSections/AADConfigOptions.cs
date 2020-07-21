
using System;
using System.Collections.Generic;
using System.Text;

namespace PowerBuildToolDevOpsAPI.ConfigSections
{
    public class AADConfigOptions
    {
        public const string AADConfigOption = "Values";
        public int MyProperty { get; set; }
        public  string UserName { get; set; }
        public  string Pasword { get; set; }
        public  string ClientId { get; set; }
        public  string AADInstance { get; set; }
        public  string TenantId { get; set; }
        public  string ResourceToCrm { get; set; }
        public  string ClientSecret { get; set; }
        public  string AppUserEmail { get; set; }
        public  bool HealthCheck { get; set; }

        public  string ScheduleCornExpression { get; set; }

        public  string glnApiUrl { get; set; }
        public  string glnApiCode { get; set; }

        public  string WebHookUrlBaseUrl { get; set; }

        public  string WebHookAPI { get; set; }

    }
}
