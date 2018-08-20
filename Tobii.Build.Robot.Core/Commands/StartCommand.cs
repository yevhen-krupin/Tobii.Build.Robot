using System.Threading;
using System.Threading.Tasks;

namespace Tobii.Build.Robot.Core.Commands
{
    public class StartCommand : CommandBase
    {
        public StartCommand(CancellationTokenSource cancellationTokenSource) : base(cancellationTokenSource)
        {
        }

        public override string Name { get { return "start"; } }

        public override async Task Do(Output output, string[] parameters)
        {
            var clickables = new Clickable[]
            {
                new Clickable("Projects", "", "", "projects", ""),
                new Clickable("Queue", "", "", "queue", ""),
                new Clickable("Help", "", "", "help", "")
            };

            output.Ask("Starting from", clickables);
        }
    }
}