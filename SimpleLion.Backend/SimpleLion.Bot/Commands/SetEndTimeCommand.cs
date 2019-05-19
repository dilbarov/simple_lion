using System;
using System.Threading.Tasks;
using SimpleLion.Bot.Repositories.StateRepository;
using SimpleLion.Bot.Services.ApiService;
using SimpleLion.Bot.Services.ApiService.Models;
using SimpleLion.Bot.Services.MessageConstants;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SimpleLion.Bot.Commands
{
    public class SetEndTimeCommand : ICommand
    {

        private readonly ITelegramBotClient _bot;
        private readonly IStateRepository _states;
        private readonly MessageConstants _constants;
        private readonly IApiService _api;

        public SetEndTimeCommand(ITelegramBotClient bot, IStateRepository states, MessageConstants constants, IApiService api)
        {
            _bot = bot;
            _states = states;
            _constants = constants;
            _api = api;
        }
        public static string Name => "endtime";
        public async Task ExecuteAsync(Message message)
        {
            if (TimeSpan.TryParse(message.Text, out TimeSpan time))
            {
                _states.SetEndTime(message.Chat.Id, time);
                _states.Finish(message.Chat.Id);
                var state = _states.GetState(message.Chat.Id);
                _api.Create(new EventDto
                {
                    Longitude = state.Longitude,
                    Latitude = state.Latitude,
                    StartTime = state.DateTime,
                    EndTime = state.DateEnd,
                    Rubric = state.Category,
                    Title = state.Title,
                    Comment = state.Comment
                });
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
                await _bot.SendTextMessageAsync(message.Chat, _constants.Messages.CreateSuccess, replyMarkup: rkm);

            }
            else
            {
                await _bot.SendTextMessageAsync(message.Chat, _constants.Messages.SendTime);
            }
        }
    }
}
