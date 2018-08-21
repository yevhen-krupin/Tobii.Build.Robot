using System.Threading;
using System.Threading.Tasks;
using Tobii.Build.Robot.Core;
using Tobii.Build.Robot.Core.Route;

namespace Tobii.Build.Robot.Rest.TeamCity.Commands
{
    public class TeamcityEnqueueAgentCommand : AbstractTeamcityBuildsCommand
    {
        private readonly ITeamCity _teamCity;

        public TeamcityEnqueueAgentCommand(ITeamCity teamCity, CancellationTokenSource cancellationTokenSource) : base(cancellationTokenSource)
        {
            _teamCity = teamCity;
        }

        public override string Name { get { return "on-agent"; } }

        public override async Task Do(Output output, string[] parameters)
        {
            Assert.Count(parameters, 2);
            var branchname = parameters[0];
            var buildType = parameters[1];

            var agents = await _teamCity.GetCompatibleAgents(buildType);
            foreach (var agent in agents.Agent)
            {
                output.Ask(string.Format("Enqueue {0} with {1} on: ", branchname, buildType), new Clickable[]
                {
                    new Clickable(agent.Name, "", "", "enqueue", branchname + " " +buildType + " " + agent.Id),
                });
            }
        }
    }
}