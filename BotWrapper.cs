using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace Tobii.Build.Robot
{
    public class BotWrapper : IDisposable
    {
        private readonly TelegramBotClient _client;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly Output _output;

        public BotWrapper(TelegramBotClient client, CancellationTokenSource cancellationTokenSource, Output output)
        {
            _client = client;
            _cancellationTokenSource = cancellationTokenSource;
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
            _client.SendTextMessageAsync(messageEventArgs.Message.Chat.Id, "Hello " + messageEventArgs.Message.Chat.FirstName);
        }

        public void Dispose()
        {
            _client.OnMessage -= BotOnOnMessage;
            _cancellationTokenSource?.Dispose();
        }
    }
}