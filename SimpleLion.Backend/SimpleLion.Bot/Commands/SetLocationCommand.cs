using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimpleLion.Bot.Extensions;
using SimpleLion.Bot.Repositories.StateRepository;
using SimpleLion.Bot.Services.ApiService;
using SimpleLion.Bot.Services.ApiService.Models;
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
        private readonly IApiService _api;

        public SetLocationCommand(ITelegramBotClient bot, IStateRepository states, MessageConstants constants, IApiService api)
        {
            _bot = bot;
            _states = states;
            _constants = constants;
            _api = api;
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

            var events = _api.GetEvents(message.Location);

            foreach (var ev in events)
            {
                await _bot.SendTextMessageAsync(message.Chat.Id, $"{ev.Title} \n" +
                                                                 $"{ev.StartTime} \n" +
                                                                 $"{ev.Comment} \n" +
                                                                 $"https://www.google.com/maps/search/?api=1&query={ev.Latitude.ReplaceDot()},{ev.Longitude.ReplaceDot()}");
            }

            _states.AddState(message.Chat.Id, Name, NextName);

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
         
        }
    }
}
