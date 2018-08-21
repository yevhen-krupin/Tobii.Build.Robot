using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tobii.Build.Robot.Model;

namespace Tobii.Build.Robot.Rest.TeamCity
{
    public class TeamCityApi : ICiCdServerApi
    {
        public IRestClient RestClient { get; }

        public TeamCityApi(IRestClient restClient)
        {
            RestClient = restClient;
        }
        
        public async Task<Branches> GetBranchesAsync(string projectId)
        {
            var request = $"/httpAuth/app/rest/projects/{projectId}/branches";
            return await RestClient.Get<Branches>(request);
        }

        public async Task<Builds> GetQueue()
        {
            var request = $"/httpAuth/app/rest/buildQueue";
            return await RestClient.Get<Builds>(request);
        }

        public async Task<Model.Build> GetBuild(string buildId)
        {
            var request = $"/httpAuth/app/rest/builds/id:{buildId}";
            return await RestClient.Get<Model.Build>(request);
        }

        public async Task<Builds> GetBuilds(string projectId)
        {
            var request = $"/httpAuth/app/rest/builds?locator=affectedProject:{projectId}";
            return await RestClient.Get<Builds>(request);
        }

        public async Task<BuildTypes> GetBuildTypes(string projectId)
        {
            var request = $"/httpAuth/app/rest/buildTypes?locator=affectedProject:{projectId}";
            return await RestClient.Get<BuildTypes>(request);
        }

        public async Task<Project> GetProjectAsync(string projectId)
        {
            var request = $"/httpAuth/app/rest/projects/{projectId}";
            return await RestClient.Get<Project>(request);
        }


        public async Task<Projects> GetProjectsAsync()
        {
            var request = $"/httpAuth/app/rest/projects";
            return await RestClient.Get<Projects>(request);
        }
    }
}