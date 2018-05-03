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

        public TeamCity(string path, string userName, string password)
        {
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{userName}:{password}"));
            _api = RestClient.For<ITeamCityRest>(path);
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

        public Task<Builds> GetBuildsWithStatusesAsync(string projectId)
        {
            return _api.GetBuildsWithStatusesAsync(projectId);
        }
    }
}
