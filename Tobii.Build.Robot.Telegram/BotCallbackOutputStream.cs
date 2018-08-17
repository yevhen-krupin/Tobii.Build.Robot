using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.ReplyMarkups;
using Tobii.Build.Robot.Core;

namespace Tobii.Build.Robot.Telegram
{
    public class BotCallbackOutputStream : IUIStream, IOutputStream
    {
        private readonly TelegramBotClient _client;
        private readonly long chatId;

        public BotCallbackOutputStream(TelegramBotClient client, long chatId)
        {
            _client = client;
            this.chatId = chatId;
        }

        public void Show(IOutputView view)
        {
            view.Present(this);
        }

        public void ShowButton(Clickable button)
        {
            throw new System.NotImplementedException();
        }

        public void ShowMessage(string message)
        {
            _client.SendTextMessageAsync(chatId, message);
        }

        public async void ShowOptions(string title, Clickable[] options)
        {
            var buttons = options
                .Select(x => new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData(x.Name, x.To) } )
                .ToArray();
            var rkm = new InlineKeyboardMarkup();
            rkm.InlineKeyboard = buttons;
            await _client.SendTextMessageAsync(chatId, title, replyMarkup: rkm);
        }
    }
}