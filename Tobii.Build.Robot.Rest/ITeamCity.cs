using System.Threading.Tasks;
using Tobii.Build.Robot.Model;
using Tobii.Build.Robot.Rest.Core;

namespace Tobii.Build.Robot.Rest
{
    public interface ITeamCity : IGateway
    {
        Task<Project> GetProjectAsync(string projectId);
        Task<Projects> GetProjectsAsync();
        Task<Branches> GetBranchesAsync(string projectId);
        Task<Model.Build> GetBuildFullInfo(string buildId);
        Task<BuildTypes> GetBuildTypes(string projectId);
        Task<Builds> GetBuilds(string projectId);
        Task<Builds> GetQueue();
    }
}