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
            while (true)
            {
                try
                {
                    var message = _inputStream.GetMessage();
                    _context.Execute(message, _output)
                        .ConfigureAwait(false)
                        .GetAwaiter()
                        .GetResult();
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                }
                catch (OperationCanceledException)
                {
                    _output.Write("ill be back baby");
                    return;
                }
                catch (ArgumentException)
                {
                    _output.Write("command received unexpected arguments count");
                }
                catch(Exception ex)
                {
                    _output.Write("error: " + ex.Message); 
                }
            }
        }
    }
}