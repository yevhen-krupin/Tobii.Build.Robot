using System.Threading;
using System.Threading.Tasks;

namespace Tobii.Build.Robot.Core.Commands
{
    public class ExitCommand : CommandBase
    {
        public ExitCommand(CancellationTokenSource cancellationTokenSource) : base(cancellationTokenSource)
        {
        }

        public override string Name
        {
            get => "exit";
        }

        public override Task Do(Output output, string[] parameters)
        {
            return Task.Run(() =>
            {
                output.Write("Closing...");
                CancellationTokenSource.Cancel();
            });
        }
    }
}