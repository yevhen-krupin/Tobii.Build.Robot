using System.Threading;
using System.Threading.Tasks;
using Tobii.Build.Robot.Core;

using Tobii.Build.Robot.Core.Commands;
namespace Tobii.Build.Robot.Rest.Command
{
    public class TeamcityGetBuildTypesCommand : CommandBase
    {
        private readonly ITeamCity _teamCity;

        public TeamcityGetBuildTypesCommand(ITeamCity teamCity, CancellationTokenSource cancellationTokenSource) : base(cancellationTokenSource)
        {
            _teamCity = teamCity;
        }

        public override string Name { get { return "buildtypes"; } }

        public override async Task Do(Output output, string[] parameters)
        {
            Assert.Count(parameters, 1);
            output.Write("get build types command executing");

            var buildTypes = await _teamCity.GetBuildTypes(parameters[0]);
            foreach (var item in buildTypes.BuildType)
            {
                output.Write(string.Format("buildtype: {0} {1}", item.Id, item.Name));
            }
        }
    }
}