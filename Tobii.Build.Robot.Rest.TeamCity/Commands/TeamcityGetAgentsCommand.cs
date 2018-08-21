using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tobii.Build.Robot.Core;
using Tobii.Build.Robot.Core.Commands;

namespace Tobii.Build.Robot.Rest.TeamCity.Commands
{
    public class TeamcityGetAgentsCommand : CommandBase
    {
        private readonly ITeamCity _teamCity;

        public TeamcityGetAgentsCommand(ITeamCity teamCity, CancellationTokenSource cancellationTokenSource) : base(cancellationTokenSource)
        {
            _teamCity = teamCity;
        }

        public override string Name { get { return "agents"; } }

        public override async Task Do(Output output, string[] parameters)
        {
            var agents = await _teamCity.GetAgentsAsync();
            if (agents.Count == 0)
            {
                output.Write("Agents not found");
            }
            foreach (var agent in agents.Agent)
            {
                var agentFullInfo = await _teamCity.GetAgentAsync(agent.Id);
                //var propertiesJoin = string.Join(Environment.NewLine,
                //    agentFullInfo.Properties.Property.Select(x => $"{x.Name} = {x.Value}").ToArray());
                output.Write(
                    string.Format("Agent {0}, enabled: {1}, ip: {2}, id: {3}",
                        agentFullInfo.Name, 
                        agentFullInfo.Enabled, 
                        agentFullInfo.Ip,
                        agentFullInfo.Id));
            }
        }
    }
}