using System.Threading;

namespace Tobii.Build.Robot
{
    public class ExitCommand : CommandBase
    {
        public ExitCommand(CancellationTokenSource cancellationTokenSource, Output output) : base(cancellationTokenSource, output)
        {
        }

        public override string Name
        {
            get => "exit";
        }

        public override void Do()
        {
            CancellationTokenSource.Cancel();
        }
    }

    public class GetAllBranches : CommandBase
    {
        public GetAllBranches(CancellationTokenSource cancellationTokenSource, Output output) : base(cancellationTokenSource, output)
        {
        }

        public override string Name
        {
            get => "branches";
        }

        public override void Do()
        {
            Output.Write("looking for active branches");
            Output.Write("looking for active branches");
        }
    }
}