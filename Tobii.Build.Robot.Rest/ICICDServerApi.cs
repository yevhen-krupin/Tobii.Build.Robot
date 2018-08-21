using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tobii.Build.Robot.Model;

namespace Tobii.Build.Robot.Rest
{
    public interface ICiCdServerApi
    {
        IRestClient RestClient { get; }

        Task<Projects> GetProjectsAsync();
        
        Task<Branches> GetBranchesAsync(string projectId);
        
        Task<BuildTypes> GetBuildTypes(string projectId);
        
        Task<Builds> GetBuilds(string projectId);

        Task<Builds> GetQueue();

        Task<Model.Build> GetBuild(string buildId);
        
        Task<Project> GetProjectAsync(string projectId);
    }
}