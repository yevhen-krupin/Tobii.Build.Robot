using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RestEase;
using Tobii.Build.Robot.Model;

namespace Tobii.Build.Robot.Rest
{
    [Header("Accept", "application/json")]
    public interface ITeamCityRest
    {
        [Header("Authorization")]
        AuthenticationHeaderValue Authorization { get; set; }

        [Get(RestConstants.GetProjectsPath)]
        Task<Projects> GetProjectsAsync();

        [Get(RestConstants.GetBranchesPath)]
        Task<Branches> GetBranchesAsync([Path]string projectId);
        
        [Get(RestConstants.GetBuildTypesPath)]
        Task<BuildTypes> GetBuildTypes([Path]string projectId);

        [Get(RestConstants.GetBuildsPath)]
        Task<Builds> GetBuilds([Path]string projectId);

        [Get(RestConstants.GetBuildPath)]
        Task<Model.Build> GetBuild([Path]string buildId);
    }
}