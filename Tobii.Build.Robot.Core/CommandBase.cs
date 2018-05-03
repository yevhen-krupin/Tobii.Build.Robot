using System.Threading;

namespace Tobii.Build.Robot.Core
{
    public abstract class CommandBase
    {
        public CancellationTokenSource CancellationTokenSource { get; }

        protected CommandBase(CancellationTokenSource cancellationTokenSource)
        {
            CancellationTokenSource = cancellationTokenSource;
        }

        public abstract string Name { get; }

        public abstract void Do(Output output, string[] parameters);
    }
}