using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tobii.Build.Robot.Model;
using Tobii.Build.Robot.Rest.Core;

namespace Tobii.Build.Robot.Rest
{
    public interface ICiCdServerApi : IGateway
    {
        IRestClient RestClient { get; }

        Task<Projects> GetProjectsAsync();
        
        Task<Branches> GetBranchesAsync(string projectId);
        
        Task<BuildTypes> GetBuildTypes(string projectId);

        Task<BuildType> GetBuildType(string buildTypeId);

        Task<Builds> GetBuildsFromProject(string projectId, int count = 50, int start = 0);

        Task<Builds> GetBuildsFromBuildType(string projectId, int count = 50, int start = 0);

        Task<Builds> GetRunningBuilds();

        Task<Builds> GetQueue();

        Task<Model.Build> GetBuild(string buildId);
        
        Task<Project> GetProjectAsync(string projectId);

        Task<Agents> GetAgentsAsync();

        Task<Agent> GetAgentAsync(string agentId);

        Task<Agents> GetCompatibleAgents(string buildTypeId);


        Task<Model.Build> Enqueue(string branchName, string buildTypeId, string agentId, string comment, bool cleanSource, bool rebuildAllDependencies, bool queueTop, string modificationId);
    }
}