using System;
using System.Threading;

namespace Tobii.Build.Robot.Core
{
    public class RunLooper
    {
        private readonly InputStream _inputStream;
        private readonly CommandsExecutor _context;
        private readonly Output _output;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public RunLooper(InputStream inputStream, CommandsExecutor context, Output output, CancellationTokenSource cancellationTokenSource)
        {
            _inputStream = inputStream;
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
                    var message = _inputStream.GetMessage();
                    _context.Execute(message, _output);
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                }
            }
            catch (OperationCanceledException)
            {
                _output.Write("ill be back baby");
            }
        }
    }
}