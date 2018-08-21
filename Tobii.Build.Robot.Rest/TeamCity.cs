using System;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Tobii.Build.Robot.Model;

namespace Tobii.Build.Robot.Rest
{
    public class TeamCity : ITeamCity
    {
        private const string RootProject = "_Root";
        private readonly ICiCdServerApi _api;
        
        public TeamCity(IRestClient restClient)
        {
            _api = new TeamCityApi(restClient);
        }

        public async Task<Projects> GetProjectsAsync()
        {
            var projects = await _api.GetProjectsAsync();
            return projects;
        }

        public async Task<Branches> GetBranchesAsync(string projectId)
        {
            var branches = await _api.GetBranchesAsync(projectId);
            return branches;
        }

        public Task<Model.Build> GetBuildFullInfo(string buildId)
        {
            return _api.GetBuild(buildId);
        }

        public Task<BuildTypes> GetBuildTypes(string projectId)
        {
            return _api.GetBuildTypes(projectId);
        }

        public Task<Builds> GetBuilds(string projectId)
        {
            return _api.GetBuilds(projectId);
        }

        public Task<Builds> GetQueue()
        {
            return _api.GetQueue();
        }

        public Task<Project> GetProjectAsync(string projectId)
        {
            return _api.GetProjectAsync(projectId);
        }
    }
}
