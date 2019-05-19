using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleLion.Bot.Repositories.EventsStateRepository;
using SimpleLion.Bot.Services.ApiService;
using SimpleLion.Bot.Services.MessageConstants;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SimpleLion.Bot.Commands
{
    public class EventsApiCommand :ICommand
    {
        private readonly ITelegramBotClient _bot;
        private readonly IEventsStateRepository _events;
        private readonly MessageConstants _constants;
        private readonly IApiService _api;

        public EventsApiCommand(ITelegramBotClient bot, IEventsStateRepository events, MessageConstants constants, IApiService api)
        {
            _bot = bot;
            _events = events;
            _constants = constants;
            _api = api;
        }

        public static string Name => "eventsapi";

        public async Task ExecuteAsync(Message message)
        {
            if (message.Location == null)
            {
                await _bot.SendTextMessageAsync(message.Chat.Id, _constants.Messages.SendLocation);
                return;
            }
            

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

            var events = _api.GetEvents(message.Location);

            if (!events.Any())
            {
                await _bot.SendTextMessageAsync(message.Chat.Id, _constants.Messages.EmptyEvents, replyMarkup: rkm);
                return;;
            }
                

            foreach (var ev in events)
            {
                await _bot.SendTextMessageAsync(message.Chat.Id, $"{ev.Title} \n" +
                                                                 $"{ev.StartTime.ToShortDateString()} \n" +
                                                                 $"{ev.Comment}");
            }

            _events.ClearState(message.Chat.Id);

           
            await _bot.SendTextMessageAsync(message.Chat,
                _constants.Messages.Start, replyMarkup: rkm);

        }
    }
}
