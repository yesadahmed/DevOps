using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;
using PowerBuildToolDevOpsAPI.DevOpsBuilds;
using PowerBuildToolDevOpsAPI.DevOpsBuilds.Manager.BuildManager;
using PowerBuildToolDevOpsAPI.Models;
using PowerBuildToolDevOpsAPI.ObjectPooling;

namespace PowerBuildToolDevOpsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkItemController : ControllerBase
    {
        private readonly IWorkItemOperation _workItemManager;
        private readonly ObjectPool<DevOpsConnectionPool> _builderPool;

        public WorkItemController(IWorkItemOperation workItemManager, ObjectPool<DevOpsConnectionPool> builderPool)
        {
            this._workItemManager = workItemManager;
            _builderPool = builderPool;
        }


        [HttpPost]
        [Route("createworkitem")]
        public async Task<IActionResult> CreateWorkItem(CreateWorkItem workItem)
        {

            var result = await _workItemManager.CreateWorkitem(workItem);
            return Ok(result);
        }

        [HttpPost]
        [Route("createlinkedworkitem")]
        public async Task<IActionResult> CreateLinkedWorkitem(CreateWorkItem workItem)
        {
            var result = await _workItemManager.CreateLinkedWorkitem(workItem);
            return Ok(result);
        }


        [HttpGet]
        [Route("getallworkitems/{projectname}/{searchtext}")]
        public async Task<IActionResult> GetAllWorkItems(string projectname, string searchtext)
        {
            var result = await _workItemManager.GetAllworkItems(projectname, searchtext);
            return Ok(result);
        }

        [HttpPost]
        [Route("createattachmenttoworkitem")]
        public async Task<IActionResult> CreateAttachmentToWorkitem(CreateWorkItem wit)
        {
            BuildManager buildManager = new BuildManager(_builderPool);
            var buildReport = await buildManager.GetBuildFinalReport(wit.projectname, wit.buildId);
            var result = await _workItemManager.CreateAttachmentToWorkitem(wit.WitId, wit.crmsolutioname,
               wit.projectid, wit.crmorgurl, buildReport.Html);
            return Ok(result);
        }


    }
}
