using System.Threading;
using System.Threading.Tasks;
using Tobii.Build.Robot.Core;
using Tobii.Build.Robot.Core.Route;

namespace Tobii.Build.Robot.Rest.TeamCity.Commands
{
    public class TeamcityGetRunningBuildsCommand : AbstractTeamcityBuildsCommand
    {
        private readonly ITeamCity _teamCity;

        public TeamcityGetRunningBuildsCommand(ITeamCity teamCity, CancellationTokenSource cancellationTokenSource) : base(cancellationTokenSource)
        {
            _teamCity = teamCity;
        }

        public override string Name { get { return "running"; } }

        public override async Task Do(Output output, string[] parameters)
        {
            var builds = await _teamCity.GetRunningBuilds();
            if (builds.Build.Length == 0)
            {
                output.Write("No running builds");
            }
            else
            {
                await RunViaBuilds(_teamCity, output, builds).ConfigureAwait(false);
            }
            output.Ask("You can", new Clickable[]
            {
                new Clickable("Refresh", "", "", "running", ""),
                new Clickable("Go to start", "", "", "start", ""),
            });
        }
    }
}