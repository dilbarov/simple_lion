using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimpleLion.Bot.Repositories.EventsStateRepository;
using SimpleLion.Bot.Services.MessageConstants;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SimpleLion.Bot.Commands
{
    public class GetEventsCommand : ICommand
    {
        private readonly ITelegramBotClient _bot;
        private readonly IEventsStateRepository _events;
        private readonly MessageConstants _constants;

        public GetEventsCommand(ITelegramBotClient bot, IEventsStateRepository events, MessageConstants constants)
        {
            _bot = bot;
            _events = events;
            _constants = constants;
        }

        public static string Name => "/events";
        public string NextName => EventsApiCommand.Name;
        //public string NextName => 
        public async Task ExecuteAsync(Message message)
        {
            _events.ClearState(message.Chat.Id);

            _events.AddState(message.Chat.Id, Name, NextName);
            var rkm = new ReplyKeyboardMarkup();
            rkm.ResizeKeyboard = true;
            rkm.Keyboard =
                new[]
                {
                    new[]
                    {
                        new KeyboardButton(_constants.Messages.SendLocation){RequestLocation = true}
                    }
                };

            await _bot.SendTextMessageAsync(
                message.Chat,
                _constants.Messages.SendLocation, replyMarkup: rkm);
        }
    }
}
