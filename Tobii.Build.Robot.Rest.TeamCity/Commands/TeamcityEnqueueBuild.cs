using System.Threading;
using System.Threading.Tasks;
using Tobii.Build.Robot.Core;
using Tobii.Build.Robot.Core.Commands;
using Tobii.Build.Robot.Core.Route;

namespace Tobii.Build.Robot.Rest.TeamCity.Commands
{
    public class TeamcityEnqueueBuild : CommandBase
    {
        private readonly ITeamCity _teamCity;

        public TeamcityEnqueueBuild(ITeamCity teamCity, CancellationTokenSource cancellationTokenSource) : base(cancellationTokenSource)
        {
            _teamCity = teamCity;
        }

        public override string Name
        {
            get => "enqueue";
        }

        public override async Task Do(Output output, string[] parameters)
        {
            var branchName = GetStringAt(parameters, 0);
            var buildTypeId = GetStringAt(parameters, 1);
            var agentId = GetStringAt(parameters, 2);
            var modificationId = GetStringAt(parameters, 3);
            var comment = GetStringAt(parameters, 4, "Build requested from bot");
            var cleanSource = GetBoolAt(parameters, 5);
            var rebuildAllDependencies = GetBoolAt(parameters, 6);
            var queueTop = GetBoolAt(parameters, 7);

            var result = await _teamCity.Enqueue(branchName, buildTypeId, agentId, comment, cleanSource, rebuildAllDependencies, queueTop, modificationId);
            output.Ask($"Build triggered at branch {branchName} with buildtype {buildTypeId}" + (result.Agent==null?"":$" on agent {result.Agent.Name}"), new Clickable[]
            {
                new Clickable("Back to start", "", "", "start", ""),
                new Clickable("To running builds", "", "", "running", ""),
            });
        }

        private string GetStringAt(string[] parameters, int index, string defaultValue = null)
        {
            return parameters.Length > index ? parameters[index] : defaultValue;
        }
        private bool GetBoolAt(string[] parameters, int index, bool defaultValue = false)
        {
            return parameters.Length > index && bool.TryParse(parameters[index], out bool value) && value || defaultValue;
        }
    }
}