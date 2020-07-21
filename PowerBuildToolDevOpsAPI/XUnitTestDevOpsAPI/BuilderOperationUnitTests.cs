using PowerBuildToolDevOpsAPI.DevOpsBuilds;
using PowerBuildToolDevOpsAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XUnitTestDevOpsAPI
{
    public class BuilderOperationUnitTests : IBuildOperations
    {
        public Task<int> CreateBuildForSolution(string ProjetcName, string solname, string pipelinevariablename, int definitionId)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateCRMBuild(string solname)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProjectModel>> GetAllProjetcs()
        {
            throw new NotImplementedException();
        }

        public Task<BuildHtml> GetBuildFinalReport(string projectName, int BuildId)
        {
            throw new NotImplementedException();
        }

        public Task<BuildLogs> GetBuildLogs(string projectName, int BuildId)
        {
            throw new NotImplementedException();
        }

        public Task<BuildStatusModel> GetBuildStatus(string projentname, int buildId)
        {
            throw new NotImplementedException();
        }

        public Task<List<VariableModel>> GetPipeLineVariables(string projectname, int definitionid)
        {
            throw new NotImplementedException();
        }

        public async Task<BuildModel> GetProjectBuilds(string ProjetcName)
        {
            BuildModel buildModel = new BuildModel();
            buildModel.Builds = new List<BuildModelValue>();

            BuildModelValue buildModelValue = new BuildModelValue();
            buildModelValue.BuildID = 111;
            buildModelValue.ProjectName = "Test";
            return buildModel;
            
        }

        public Task<List<PipeLineModel>> GetProjectPipeLines(string projectname)
        {
            throw new NotImplementedException();
        }

        public Task<List<TeamMemberModel>> GetProjectTeamMembers(string projectName)
        {
            throw new NotImplementedException();
        }

        public Task<List<TeamModel>> GetProjectTeams(string projectName)
        {
            throw new NotImplementedException();
        }

        public Task<VariableModel> GetSpecificPipeLineVariable(string projectname, int definitionid, string variablekey)
        {
            throw new NotImplementedException();
        }
    }
}
