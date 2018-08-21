using System.Threading;
using System.Threading.Tasks;
using Tobii.Build.Robot.Core;

namespace Tobii.Build.Robot.Rest.TeamCity.Commands
{
    public class TeamcityGetBuildQueueCommand : AbstractTeamcityBuildsCommand
    {
        private readonly ITeamCity _teamCity;

        public TeamcityGetBuildQueueCommand(ITeamCity teamCity, CancellationTokenSource cancellationTokenSource) : base(cancellationTokenSource)
        {
            _teamCity = teamCity;
        }

        public override string Name { get { return "queue"; } }

        public override async Task Do(Output output, string[] parameters)
        {
            output.Write("queue command executing");
            var builds = await _teamCity.GetQueue();
            if (builds.Build.Length == 0)
            {
                output.Write("The build queue is empty");
            }
            else
            {
                await RunViaBuilds(_teamCity, output, builds).ConfigureAwait(false);

            }
        }
    }
}