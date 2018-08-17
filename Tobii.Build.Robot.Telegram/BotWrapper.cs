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
        private readonly IPresenterFactory presenterFactory;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly CommandsExecutor _commandsExecutor;
        private readonly Output _output;

        public BotWrapper(TelegramBotClient client, IPresenterFactory presenterFactory, CancellationTokenSource cancellationTokenSource, CommandsExecutor commandsExecutor, Output output)
        {
            _client = client;
            this.presenterFactory = presenterFactory;
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

        private async void BotOnOnMessage(object sender, MessageEventArgs messageEventArgs)
        {
            // todo: looks like here we should decide about when to create wrapped output and when cache chat id.
            _output.Show(
                presenterFactory.Text(messageEventArgs.Message.Chat.FirstName + " said: " + messageEventArgs.Message.Text));
            var wrappedOutput = new Output(
                _output.PresenterFactory, 
                new IOutputStream[] {
                    _output,
                    new BotCallbackOutputStream(_client, messageEventArgs.Message.Chat.Id)});
            await _commandsExecutor.Execute(messageEventArgs.Message.Text, wrappedOutput);
        }

        public void Dispose()
        {
            _client.OnMessage -= BotOnOnMessage;
            _cancellationTokenSource?.Dispose();
        }
    }
}