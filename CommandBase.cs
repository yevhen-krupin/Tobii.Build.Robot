using System.Threading;

namespace Tobii.Build.Robot
{
    public abstract class CommandBase
    {
        public CancellationTokenSource CancellationTokenSource { get; }
        public Output Output { get; }

        protected CommandBase(CancellationTokenSource cancellationTokenSource, Output output)
        {
            CancellationTokenSource = cancellationTokenSource;
            Output = output;
        }

        public abstract string Name { get; }

        public abstract void Do();
    }
}