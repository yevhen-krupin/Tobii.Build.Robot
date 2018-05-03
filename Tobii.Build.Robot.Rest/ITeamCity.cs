using System.Threading.Tasks;
using RestEase;
using Tobii.Build.Robot.Model;

namespace Tobii.Build.Robot.Rest
{
    public interface ITeamCity
    {
        Task<Projects> GetProjectsAsync();
        Task<Branches> GetBranchesAsync([Path]string projectId);
        Task<Builds> GetBuildsWithStatusesAsync([Path]string projectId);
    }
}