using System;
using System.Threading;
using Telegram.Bot;

namespace Tobii.Build.Robot
{
    class Program
    {
        static void Main(string[] args)
        {
            var output = new Output();
            var config = new ConfigurationProvider();
            var cancellationSource = new CancellationTokenSource();
            output.Write("build bot greatings you");
            output.Write("type exit to leave bot");
            var client = new TelegramBotClient(config.ApiKey);
            var commandsExecutor = new ExecutionContext(new CommandBase[1] { new ExitCommand(cancellationSource, output)}, output);
            var runLooper = new RunLooper(commandsExecutor, output, cancellationSource);
            using (var botWrapper = new BotWrapper(client, cancellationSource, output))
            {
                botWrapper.Start();
                runLooper.Run();
            }
        }
    }
}
