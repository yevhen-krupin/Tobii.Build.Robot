using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tobii.Build.Robot.Core;

namespace Tobii.Build.Robot.Rest.TeamCity.Commands
{
    public class TeamcityGetBuildsCommand : AbstractTeamcityBuildsCommand
    {
        private readonly ITeamCity _teamCity;

        public TeamcityGetBuildsCommand(ITeamCity teamCity, CancellationTokenSource cancellationTokenSource) : base(cancellationTokenSource)
        {
            _teamCity = teamCity;
        }

        public override string Name { get { return "builds"; } }

        public override async Task Do(Output output, string[] parameters)
        {
            Assert.Count(parameters, 1);

            output.Write("builds command executing");

            var builds = await _teamCity.GetBuildsFromProject(parameters[0]);
            if (!builds.Build.Any())
            {
                output.Write("Builds not found for project " + parameters[0]);
            }
            else
            {
                await RunViaBuilds(_teamCity, output, builds).ConfigureAwait(false);
            }
        }
    }
}