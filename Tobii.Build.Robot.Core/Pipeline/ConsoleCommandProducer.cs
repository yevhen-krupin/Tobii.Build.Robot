using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tobii.Build.Robot.Core.Pipeline
{
    public class ConsoleCommandProducer
    {
        private readonly InputPipeline _pipeline;

        public ConsoleCommandProducer(InputPipeline pipeline)
        {
            _pipeline = pipeline;
        }

        public async void ListenCommands(CancellationToken token)
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        var line = Console.ReadLine();
                        token.ThrowIfCancellationRequested();
                        _pipeline.Enqueue(new Message()
                        {
                            Content = line,
                            Source = this.GetType().Name
                        });
                        Thread.Sleep(50);
                    }
                    catch (OperationCanceledException e)
                    {
                    }
                }

            }, token).ConfigureAwait(false);
        }
    }
}