using System.Threading;
using System.Threading.Tasks;

namespace Tobii.Build.Robot.Core
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

        public override async Task Do(Output output, string[] parameters)
        {
            output.Write("Closing...");
            CancellationTokenSource.Cancel();
        }
    }
}