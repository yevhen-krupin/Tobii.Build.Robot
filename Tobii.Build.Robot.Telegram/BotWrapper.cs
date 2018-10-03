using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Tobii.Build.Robot.Core;
using Tobii.Build.Robot.Core.Commands;
using Tobii.Build.Robot.Core.Pipeline;

namespace Tobii.Build.Robot.Telegram
{
    public class BotWrapper : IDisposable
    {
        private readonly TelegramBotClient _client;
        private readonly InputPipeline _inputPipeline;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly Output _output;
        private readonly IStore _store;

        public BotWrapper(TelegramBotClient client, InputPipeline inputPipeline, IPresenterFactory presenterFactory, CancellationTokenSource cancellationTokenSource, CommandsExecutor commandsExecutor, Output output, IStore store)
        {
            _client = client;
            this._inputPipeline = inputPipeline;
            _cancellationTokenSource = cancellationTokenSource;
            _output = output;
            _store = store;

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

        private void _client_OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            _inputPipeline.Enqueue(new Message()
            {
                Content = e.CallbackQuery.Data,
                Source = e.CallbackQuery.Message.Chat.Id.ToString(),
                CustomizedOutput = RespondTo(e.CallbackQuery.Message.Chat.Id)
            });
        }

        private void BotOnOnMessage(object sender, MessageEventArgs messageEventArgs)
        {
            var id = messageEventArgs.Message.Text;
            var data = _store.Get<string>(messageEventArgs.Message.Chat.Id.ToString(), id) ?? id;
            // todo: looks like here we should decide about when to create wrapped output and when cache chat id.
            _inputPipeline.Enqueue(new Message()
            {
                Content = data,
                Source = messageEventArgs.Message.Chat.Id.ToString(),
                CustomizedOutput = RespondTo(messageEventArgs.Message.Chat.Id)
            });
        }

        private Output RespondTo(long chatId)
        {
            return new Output(
                _output.PresenterFactory,
                new IOutputStream[] {
                    _output,
                    new BotCallbackOutputStream(_store, _client, chatId)});
        }

        public void Dispose()
        {
            _client.OnCallbackQuery -= _client_OnCallbackQuery;
            _client.OnMessage -= BotOnOnMessage;
            _cancellationTokenSource?.Dispose();
        }
    }
}