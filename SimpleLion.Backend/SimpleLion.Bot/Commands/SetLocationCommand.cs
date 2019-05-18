using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimpleLion.Bot.StateRepository;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SimpleLion.Bot.Commands
{
    public class SetLocationCommand : ICommand
    {
        private readonly ITelegramBotClient _bot;
        private readonly IStateRepository _states;

        public SetLocationCommand(ITelegramBotClient bot, IStateRepository states)
        {
            _bot = bot;
            _states = states;
        }
        public static string Name => "location";
        public string NextName => IsNewCommand.Name;
        public async Task ExecuteAsync(Message message)
        {
            if (message.Location == null)
            {
                await _bot.SendTextMessageAsync(message.Chat.Id, "Пришлите геопозицию");
                return;
            }

            _states.SetLocation(message.Chat.Id, message.Location.Latitude, message.Location.Longitude);

            var rkm = new ReplyKeyboardMarkup();
            rkm.Selective = true;
            rkm.ResizeKeyboard = true;
            rkm.Keyboard =
                new[]
                {
                    new[]
                    {
                        new KeyboardButton("Да"),
                        new KeyboardButton("Нет")
                    }
                };

            await _bot.SendTextMessageAsync(message.Chat.Id, "Это новое мероприятие?", replyMarkup: rkm);
            _states.AddState(message.Chat.Id, Name,NextName);
        }
    }
}
