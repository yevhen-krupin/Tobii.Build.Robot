using System.Threading;
using System.Threading.Tasks;
using Tobii.Build.Robot.Core;

using Tobii.Build.Robot.Core.Commands;
namespace Tobii.Build.Robot.Rest.Command
{
    public class TeamcityGetBuildCommand : CommandBase
    {
        private readonly ITeamCity _teamCity;

        public TeamcityGetBuildCommand(ITeamCity teamCity, CancellationTokenSource cancellationTokenSource) : base(cancellationTokenSource)
        {
            _teamCity = teamCity;
        }

        public override string Name { get { return "build"; } }

        public override async Task Do(Output output, string[] parameters)
        {
            Assert.Count(parameters, 1);
            output.Write("get build command executing");

            var build = await _teamCity.GetBuildFullInfo(parameters[0]);
            output.Write("full build info received:");
            output.Write(ObjectDumper.Dump(build));
        }
    }
}