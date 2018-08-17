using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tobii.Build.Robot.Model;

namespace Tobii.Build.Robot.Rest
{
    public class TeamCityApi : ITeamCityRest
    {
        public TeamCityApi(string baseUrl, string userName, string password)
        {
            credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{userName}:{password}"));
            BaseUrl = new Uri(baseUrl);
        }

        private readonly string credentials;

        public Uri BaseUrl { get; }
        
        public async Task<Branches> GetBranchesAsync(string projectId)
        {
            var request = $"/httpAuth/app/rest/projects/{projectId}/branches";
            return await RequestFor<Branches>(request);
        }

        public async Task<Model.Build> GetBuild(string buildId)
        {
            var request = $"/httpAuth/app/rest/builds/id:{buildId}";
            return await RequestFor<Model.Build>(request);
        }

        public async Task<Builds> GetBuilds(string projectId)
        {
            var request = $"/httpAuth/app/rest/builds?locator=affectedProject:{projectId}";
            return await RequestFor<Builds>(request);
        }

        public async Task<BuildTypes> GetBuildTypes(string projectId)
        {
            var request = $"/httpAuth/app/rest/buildTypes?locator=affectedProject:{projectId}";
            return await RequestFor<BuildTypes>(request);
        }

        public async Task<Project> GetProjectAsync(string projectId)
        {
            var request = $"/httpAuth/app/rest/projects/{projectId}";
            return await RequestFor<Project>(request);
        }

        public async Task<Projects> GetProjectsAsync()
        {
            var request = $"/httpAuth/app/rest/projects";
            return await RequestFor<Projects>(request);
        }

        private async Task<T> RequestFor<T>(string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseUrl;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
                using (var result = await client.GetAsync(url).ConfigureAwait(false))
                {
                    var response = await result.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<T>(response);
                    return obj;
                }
                    
            }
        }
    }
}