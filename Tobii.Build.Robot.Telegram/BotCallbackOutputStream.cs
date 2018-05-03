using Telegram.Bot;
using Telegram.Bot.Types;
using Tobii.Build.Robot.Core;

namespace Tobii.Build.Robot.Telegram
{
    public class BotCallbackOutputStream : IOutputStream
    {
        private readonly TelegramBotClient _client;
        private readonly Message _message;

        public BotCallbackOutputStream(TelegramBotClient client, Message message)
        {
            _client = client;
            _message = message;
        }

        public void Write(string message)
        {
            _client.SendTextMessageAsync(_message.Chat.Id, message);
        }
    }
}