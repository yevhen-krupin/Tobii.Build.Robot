using System;
using System.Threading;

namespace Tobii.Build.Robot
{
    public class RunLooper
    {
        private readonly ExecutionContext _context;
        private readonly Output _output;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public RunLooper(ExecutionContext context, Output output, CancellationTokenSource cancellationTokenSource)
        {
            _context = context;
            _output = output;
            _cancellationTokenSource = cancellationTokenSource;
        }

        public void Run()
        {
            try
            {
                while (true)
                {
                    var message = Console.ReadLine();
                    _context.Execute(message);
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                }
            }
            catch (OperationCanceledException e)
            {
                _output.Write("ill be back baby");
            }
        }
    }
}