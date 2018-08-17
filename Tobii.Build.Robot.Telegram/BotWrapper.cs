using System;
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
        private readonly InputStream inputStream;
        private readonly IPresenterFactory presenterFactory;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly CommandsExecutor _commandsExecutor;
        private readonly Output _output;

        public BotWrapper(TelegramBotClient client, InputStream inputStream, IPresenterFactory presenterFactory, CancellationTokenSource cancellationTokenSource, CommandsExecutor commandsExecutor, Output output)
        {
            _client = client;
            this.inputStream = inputStream;
            this.presenterFactory = presenterFactory;
            _cancellationTokenSource = cancellationTokenSource;
            _commandsExecutor = commandsExecutor;
            _output = output;

            _client.OnMessage += BotOnOnMessage;
            _client.OnCallbackQuery += _client_OnCallbackQuery;
        }
        
        public void Start()
        {
            Task.Run(() =>
            {
                _client.StartReceiving(new[] { UpdateType.All }, _cancellationTokenSource.Token);
            });
        }

        private async void _client_OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            inputStream.Enqueue(new Message()
            {
                Content = e.CallbackQuery.Data,
                Source = e.CallbackQuery.Message.Chat.Id.ToString(),
                CustomizedOutput = WrapOutputFor(e.CallbackQuery.Message.Chat.Id)
            });
        }

        private void BotOnOnMessage(object sender, MessageEventArgs messageEventArgs)
        {
            // todo: looks like here we should decide about when to create wrapped output and when cache chat id.
            inputStream.Enqueue(new Message()
            {
                Content = messageEventArgs.Message.Text,
                Source = messageEventArgs.Message.Chat.Id.ToString(),
                CustomizedOutput = WrapOutputFor(messageEventArgs.Message.Chat.Id)
            });
        }

        private Output WrapOutputFor(long chatId)
        {
            return new Output(
                _output.PresenterFactory,
                new IOutputStream[] {
                    _output,
                    new BotCallbackOutputStream(_client, chatId)});
        }

        public void Dispose()
        {
            _client.OnCallbackQuery -= _client_OnCallbackQuery;
            _client.OnMessage -= BotOnOnMessage;
            _cancellationTokenSource?.Dispose();
        }
    }
}