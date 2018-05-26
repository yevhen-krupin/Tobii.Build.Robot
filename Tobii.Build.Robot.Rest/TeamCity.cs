using System;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using RestEase;
using Tobii.Build.Robot.Model;

namespace Tobii.Build.Robot.Rest
{
    public class TeamCity : ITeamCity
    {
        private const string RootProject = "_Root";
        private ITeamCityRest _api;

        public string BaseUrl { get; }

        public TeamCity(string url, string userName, string password)
        {
            BaseUrl = url;
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{userName}:{password}"));
            _api = RestClient.For<ITeamCityRest>(BaseUrl);
            _api.Authorization = new AuthenticationHeaderValue("Basic", credentials);
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

        public Task<Model.Build> GetBuildFullInfo([Path] string buildId)
        {
            return _api.GetBuild(buildId);
        }

        public Task<BuildTypes> GetBuildTypes([Path] string projectId)
        {
            return _api.GetBuildTypes(projectId);
        }

        public Task<Builds> GetBuilds([Path] string projectId)
        {
            return _api.GetBuilds(projectId);
        }
    }
}
