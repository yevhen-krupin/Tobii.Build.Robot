using System.Collections;
using System.Threading;
using System.Threading.Tasks;

namespace Tobii.Build.Robot.Core.Commands
{
    public abstract class CommandBase
    {
        public CancellationTokenSource CancellationTokenSource { get; }

        protected CommandBase(CancellationTokenSource cancellationTokenSource)
        {
            CancellationTokenSource = cancellationTokenSource;
        }

        public abstract string Name { get; }

        public abstract Task Do(Output output, string[] parameters);
    }
}