using System.Threading.Tasks;
using RestEase;
using Tobii.Build.Robot.Model;
using Tobii.Build.Robot.Rest.Core;

namespace Tobii.Build.Robot.Rest
{
    public interface ITeamCity : IGateway
    {
        Task<Projects> GetProjectsAsync();
        Task<Branches> GetBranchesAsync([Path]string projectId);
        Task<Model.Build> GetBuildFullInfo([Path]string buildId);
        Task<BuildTypes> GetBuildTypes([Path]string projectId);
        Task<Builds> GetBuilds([Path]string projectId);
    }
}