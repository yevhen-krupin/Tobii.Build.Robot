using System.Threading;
using Tobii.Build.Robot.Core;

namespace Tobii.Build.Robot.Rest
{
    public class TeamcityGetProjectsCommand : CommandBase
    {
        private readonly ITeamCity _teamCity;

        public TeamcityGetProjectsCommand(ITeamCity teamCity, CancellationTokenSource cancellationTokenSource) : base(cancellationTokenSource)
        {
            _teamCity = teamCity;
        }

        public override string Name { get { return "projects"; }}

        public override async void Do(Output output, string[] parameters)
        {
            output.Write("projects command executing");
            var projects = await _teamCity.GetProjectsAsync();
            foreach (var project in projects.project)
            {
                output.Write(string.Format("{0} {1} {2} {3}", project.Id, project.Name, project.WebUrl, project.Href));
            }
        }
    }
}