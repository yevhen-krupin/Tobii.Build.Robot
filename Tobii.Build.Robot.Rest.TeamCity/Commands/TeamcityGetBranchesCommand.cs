using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tobii.Build.Robot.Core;
using Tobii.Build.Robot.Core.Commands;
using Tobii.Build.Robot.Core.Route;

namespace Tobii.Build.Robot.Rest.TeamCity.Commands
{
    public class TeamcityGetBranchesCommand : AbstractTeamcityBuildsCommand
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
            output.Ask("Branches available: ",
                branches.Branch.Select(branch => new Clickable(branch.Name, "", "", "branch", branch.Name + " " + parameters[0])).ToArray());
          
        }
    }
}