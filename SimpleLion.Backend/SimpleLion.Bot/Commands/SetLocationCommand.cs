using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimpleLion.Bot.Repositories.StateRepository;
using SimpleLion.Bot.Services.MessageConstants;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SimpleLion.Bot.Commands
{
    public class SetLocationCommand : ICommand
    {
        private readonly ITelegramBotClient _bot;
        private readonly IStateRepository _states;
        private readonly MessageConstants _constants;

        public SetLocationCommand(ITelegramBotClient bot, IStateRepository states, MessageConstants constants)
        {
            _bot = bot;
            _states = states;
            _constants = constants;
        }
        public static string Name => "location";
        public string NextName => IsNewCommand.Name;
        public async Task ExecuteAsync(Message message)
        {
            if (message.Location == null)
            {
                await _bot.SendTextMessageAsync(message.Chat.Id, _constants.Messages.SendLocation);
                return;
            }

            _states.SetLocation(message.Chat.Id, message.Location.Latitude, message.Location.Longitude);

            var rkm = new ReplyKeyboardMarkup();
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

            await _bot.SendTextMessageAsync(message.Chat.Id, _constants.Messages.IsNew, replyMarkup: rkm);
            _states.AddState(message.Chat.Id, Name,NextName);
        }
    }
}
