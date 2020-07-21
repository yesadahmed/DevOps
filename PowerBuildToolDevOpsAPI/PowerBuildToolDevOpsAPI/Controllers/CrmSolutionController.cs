using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PowerBuildToolDevOpsAPI.CRMOperations;

namespace PowerBuildToolDevOpsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrmSolutionController : ControllerBase
    {
        //private readonly IHttpClientFactory _cli*/entFactory;
        private readonly ISolutionManager _solutionManager;
        public CrmSolutionController(ISolutionManager solutionManager)
        {
            _solutionManager = solutionManager;
        }

        [HttpGet]
        [Route("getsolcomponentcount/{comtype}/{solid}")]
        public async Task<IActionResult> GetSolcomponentCount(int comtype, Guid solid)
        {
            var result = await _solutionManager.GetSolutionComponentCount(comtype, solid);
            return Ok(result);
        }


        [HttpGet]
        [Route("getsolcomponents/{solid}")]
        public async Task<IActionResult> getsolcomponents(Guid solid)
        {
            var result = await _solutionManager.GetSolutionComponents(solid);
            return Ok(result);
        }

    }
}
