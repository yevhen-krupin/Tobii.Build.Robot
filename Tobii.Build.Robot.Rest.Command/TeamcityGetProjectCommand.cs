﻿using System.Threading;
using System.Threading.Tasks;
using Tobii.Build.Robot.Core;
using System.Linq;
using Tobii.Build.Robot.Core.Commands;
namespace Tobii.Build.Robot.Rest.Command
{
    public class TeamcityGetProjectCommand : CommandBase
    {
        public const string Project = "project";

        private readonly ITeamCity _teamCity;

        public TeamcityGetProjectCommand(ITeamCity teamCity, CancellationTokenSource cancellationTokenSource) : base(cancellationTokenSource)
        {
            _teamCity = teamCity;
        }

        public override string Name { get { return Project; } }

        public override async Task Do(Output output, string[] parameters)
        {
            var project = await _teamCity.GetProjectAsync(parameters[0]);
            var clickables = new Clickable[]
            {
                new Clickable("Build types", Project, project.Id, "buildtypes", project.Id),
                new Clickable("Builds", Project, project.Id, "builds", project.Id),
                new Clickable("Back", "", "", "projects", "")
            };

            output.Ask(
               string.Format("Project {0} {1} {2}", project.Id, project.Name, project.WebUrl),
               clickables);
        }
    }
}