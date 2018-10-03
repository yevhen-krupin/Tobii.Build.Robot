using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.ReplyMarkups;
using Tobii.Build.Robot.Core;
using Tobii.Build.Robot.Core.Commands;
using Tobii.Build.Robot.Core.Route;

namespace Tobii.Build.Robot.Telegram
{
    public class BotCallbackOutputStream : IUIStream, IOutputStream
    {
        private readonly IStore _store;
        private readonly TelegramBotClient _client;
        private readonly long chatId;

        public BotCallbackOutputStream(IStore store, TelegramBotClient client, long chatId)
        {
            _store = store;
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
                .Select(x =>
                {
                    _store.Put(chatId.ToString(), x.Id, x.To);
                    return new [] {InlineKeyboardButton.WithCallbackData(x.Name, x.To + x.Id)};
                })
                .ToArray();
            
            var rkm = new InlineKeyboardMarkup();
            rkm.InlineKeyboard = buttons;
            await _client.SendTextMessageAsync(chatId, title, replyMarkup: rkm);
        }
    }
}