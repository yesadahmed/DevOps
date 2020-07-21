using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerBuildToolDevOpsAPI.ConfigSections
{
    public class DevOpsAuths
    {
        public const string DevOpsAuth = "DevOpsAuth";
        public string PAT { get; set; }
        public string CollUrl { get; set; }
    
    }
}
