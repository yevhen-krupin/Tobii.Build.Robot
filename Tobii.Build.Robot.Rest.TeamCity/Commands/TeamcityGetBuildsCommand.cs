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

            var builds = await _teamCity.GetBuilds(parameters[0]);
            foreach (var build in builds.Build)
            {
                var info = await _teamCity.GetBuildFullInfo(build.Id);

                Ask(output, info);
            }
        }

       
    }
}