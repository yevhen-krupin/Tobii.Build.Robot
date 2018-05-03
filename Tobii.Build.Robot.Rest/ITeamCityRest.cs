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

        [Get("/httpAuth/app/rest/projects")]
        Task<Projects> GetProjectsAsync();

        [Get("/httpAuth/app/rest/projects/{projectId}/branches")]
        Task<Branches> GetBranchesAsync([Path]string projectId);

        [Get("/httpAuth/app/rest/buildTypes?locator=affectedProject:(id:{projectId})&fields=buildType(id,name,project,builds($locator(running:false,canceled:false,count:1),build(number,status,statusText)))")]
        Task<Builds> GetBuildsWithStatusesAsync([Path]string projectId);
    }
}