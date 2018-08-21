using System.Threading;
using System.Threading.Tasks;
using Tobii.Build.Robot.Core;
using Tobii.Build.Robot.Core.Commands;

namespace Tobii.Build.Robot.Rest.TeamCity.Commands
{
    public class TeamcityGetBuildTypesCommand : CommandBase
    {
        private readonly ITeamCity _teamCity;

        public TeamcityGetBuildTypesCommand(ITeamCity server, CancellationTokenSource cancellationTokenSource) : base(cancellationTokenSource)
        {
            _teamCity = server;
        }

        public override string Name { get { return "buildtypes"; } }

        public override async Task Do(Output output, string[] parameters)
        {
            Assert.Count(parameters, 1);
            output.Write("get build types command executing");

            var buildTypes = await _teamCity.GetBuildTypes(parameters[0]);
            foreach (var item in buildTypes.BuildType)
            {
                var info = await _teamCity.GetBuildType(item.Id);
                output.Write(string.Format("buildtype: {0} {1}", info.Id, info.Name));
            }
        }
    }
}