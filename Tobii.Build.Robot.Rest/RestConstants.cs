namespace Tobii.Build.Robot.Rest
{
    public class RestConstants
    {
        public const string GetProjectsPath = "/httpAuth/app/rest/projects";
        public const string GetBuildPath = "/httpAuth/app/rest/builds/id:{buildId}";
        public const string GetBuildsPath = "/httpAuth/app/rest/builds?locator=affectedProject:{projectId}";
        public const string GetBuildTypesPath = "/httpAuth/app/rest/buildTypes?locator=affectedProject:{projectId}";
        public const string GetBranchesPath = "/httpAuth/app/rest/projects/{projectId}/branches";
    }
}