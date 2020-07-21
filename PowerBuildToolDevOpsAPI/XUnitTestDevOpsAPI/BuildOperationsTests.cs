using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using PowerBuildToolDevOpsAPI.DevOpsBuilds;
using PowerBuildToolDevOpsAPI.DevOpsBuilds.Manager.BuildManager;
using System.Threading.Tasks;
using Microsoft.Extensions.ObjectPool;
using PowerBuildToolDevOpsAPI.ObjectPooling;
using Autofac.Extras.Moq;
using PowerBuildToolDevOpsAPI.Models;

namespace XUnitTestDevOpsAPI
{
    public class BuildOperationsTests
    {
        

        public BuildOperationsTests()
        {
           
        } 


        [Fact]
        public async Task GetProjectBuilds_Test()
        {
            string projName = "Test";
            var expected = GetBuilds();

            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IBuildOperations>()
                    .Setup(expression: x => x.GetProjectBuilds(projName))
                    .ReturnsAsync(GetBuilds());

                var tocomapre = mock.Create<BuilderOperationUnitTests>();
                var actual =  await tocomapre.GetProjectBuilds(projName);

                Assert.True(actual != null);               
                Assert.Equal(expected.Builds.Count, actual.Builds.Count);
              

            }
           

        }


        #region Private Helper
        BuildModel GetBuilds()
        {
            BuildModel buildModel = new BuildModel();
            buildModel.Builds = new List<BuildModelValue>();

            BuildModelValue buildModelValue = new BuildModelValue();
            buildModelValue.BuildID = 111;
            buildModelValue.ProjectName = "Test";

            return buildModel;
        }

        #endregion

    }
}
