using System;
using System.Threading;
using Tobii.Build.Robot.Core.Commands;
using Tobii.Build.Robot.Core.Pipeline;

namespace Tobii.Build.Robot.Core
{
    public class RunLooper
    {
        private readonly InputPipeline _inputPipeline;
        private readonly CommandsExecutor _context;
        private readonly ConsoleCommandProducer _consoleCommandProducer;
        private readonly Output _output;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public RunLooper(InputPipeline inputPipeline, CommandsExecutor context, ConsoleCommandProducer consoleCommandProducer, Output output, CancellationTokenSource cancellationTokenSource)
        {
            _inputPipeline = inputPipeline;
            _context = context;
            _consoleCommandProducer = consoleCommandProducer;
            _output = output;
            _cancellationTokenSource = cancellationTokenSource;
        }

        public void Run()
        {
            _consoleCommandProducer.ListenCommands(_cancellationTokenSource.Token);
            while (true)
            {
                try
                {
                    if(_inputPipeline.TryGetMessage(out Message message))
                    {
                        _context.Execute(message.Content, message.CustomizedOutput ?? _output)
                           .ConfigureAwait(false)
                           .GetAwaiter()
                           .GetResult();
                        _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                        Thread.Sleep(50);
                    }
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