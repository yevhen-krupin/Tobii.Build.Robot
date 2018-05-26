using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Tobii.Build.Robot.Core;

namespace Tobii.Build.Robot.Telegram
{
    public class BotWrapper : IBotWrapper
    {
        private readonly TelegramBotClient _client;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly CommandsExecutor _commandsExecutor;
        private readonly Output _output;

        public BotWrapper(TelegramBotClient client, CancellationTokenSource cancellationTokenSource, CommandsExecutor commandsExecutor, Output output)
        {
            _client = client;
            _cancellationTokenSource = cancellationTokenSource;
            _commandsExecutor = commandsExecutor;
            _output = output;
        }
        
        public void Start()
        {
            Task.Run(() =>
            {
                _client.OnMessage += BotOnOnMessage;
                _client.StartReceiving(new[] { UpdateType.All }, _cancellationTokenSource.Token);
            });
        }

        private void BotOnOnMessage(object sender, MessageEventArgs messageEventArgs)
        {
            _output.Write(messageEventArgs.Message.Chat.FirstName + " said: " + messageEventArgs.Message.Text);
            var wrappedOutput = new Output(new IOutputStream[] {
                _output,
                new BotCallbackOutputStream(_client, messageEventArgs.Message)});
            _commandsExecutor.Execute(messageEventArgs.Message.Text, wrappedOutput);
        }

        public void Dispose()
        {
            _client.OnMessage -= BotOnOnMessage;
            _cancellationTokenSource?.Dispose();
        }
    }
}