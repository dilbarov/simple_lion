using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimpleLion.Bot.Services.MessageConstants;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SimpleLion.Bot.Commands
{
    public class StartCommand : ICommand
    {
        private readonly ITelegramBotClient _bot;
        private readonly MessageConstants _constants;

        public StartCommand(ITelegramBotClient bot, MessageConstants constants)
        {
            _bot = bot;
            _constants = constants;
        }
        public static string Name => "/start";
        
        public async Task ExecuteAsync(Message message)
        {
            var rkm = new ReplyKeyboardMarkup();
            rkm.ResizeKeyboard = true;
            rkm.Keyboard =
                new[]
                {
                    new[]
                    {
                        new KeyboardButton("/create"),
                        new KeyboardButton("/events")
                    }
                };
            await _bot.SendTextMessageAsync(message.Chat,
                _constants.Messages.Start,replyMarkup:rkm);
        }
    }
}
