using System.Threading;
using System.Threading.Tasks;
using Tobii.Build.Robot.Core;
using Tobii.Build.Robot.Core.Commands;

namespace Tobii.Build.Robot.Rest.TeamCity.Commands
{
    public class TeamcityGetBranchesCommand : CommandBase
    {
        private readonly ITeamCity _teamCity;

        public TeamcityGetBranchesCommand(ITeamCity teamCity, CancellationTokenSource cancellationTokenSource) : base(cancellationTokenSource)
        {
            _teamCity = teamCity;
        }

        public override string Name { get { return "branches"; } }
        
        public override async Task Do(Output output, string[] parameters)
        {
            Assert.Count(parameters, 1);
            output.Write("branches command executing");
            
            var branches = await _teamCity.GetBranchesAsync(parameters[0]);
            foreach (var branch in branches.branch)
            {
                output.Write(string.Format("Branch {0}, default: {1}", branch.Name, branch.Default));
            }
        }
    }
}