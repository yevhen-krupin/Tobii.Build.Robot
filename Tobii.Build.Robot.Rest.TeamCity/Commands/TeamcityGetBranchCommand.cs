using System.Threading;
using System.Threading.Tasks;
using Tobii.Build.Robot.Core;
using Tobii.Build.Robot.Core.Route;

namespace Tobii.Build.Robot.Rest.TeamCity.Commands
{
    public class TeamcityGetBranchCommand : AbstractTeamcityBuildsCommand
    {
        private readonly ITeamCity _teamCity;

        public TeamcityGetBranchCommand(ITeamCity teamCity, CancellationTokenSource cancellationTokenSource) : base(cancellationTokenSource)
        {
            _teamCity = teamCity;
        }

        public override string Name { get { return "branch"; } }

        public override async Task Do(Output output, string[] parameters)
        {
            Assert.Count(parameters, 2);
            var branchname = parameters[0];
            var projectId = parameters[1];

            var buildTypes = await _teamCity.GetBuildTypes(projectId);
            foreach (var buildType in buildTypes.BuildType)
            {
                output.Ask(string.Format("Branch {0}", branchname), new Clickable[]
                {
                    new Clickable("Enqueue " + buildType.Name, "", "", "enqueue", branchname + " " +buildType.Id),
                    new Clickable("Chose agent " + buildType.Name, "", "", "on-agent", branchname + " " +buildType.Id),
                });
            }
        }
    }
}