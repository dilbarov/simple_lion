using System;
using System.Threading.Tasks;
using SimpleLion.Bot.Repositories.StateRepository;
using SimpleLion.Bot.Services.MessageConstants;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimpleLion.Bot.Commands
{
    public class SetEndTimeCommand : ICommand
    {

        private readonly ITelegramBotClient _bot;
        private readonly IStateRepository _states;
        private readonly MessageConstants _constants;

        public SetEndTimeCommand(ITelegramBotClient bot, IStateRepository states, MessageConstants constants)
        {
            _bot = bot;
            _states = states;
            _constants = constants;
        }
        public static string Name => "endtime";
        public async Task ExecuteAsync(Message message)
        {
            if (TimeSpan.TryParse(message.Text, out TimeSpan time))
            {
                _states.SetEndTime(message.Chat.Id, time);
                _states.Finish(message.Chat.Id);
                await _bot.SendTextMessageAsync(message.Chat, _constants.Messages.CreateSuccess);
            }
            else
            {
                await _bot.SendTextMessageAsync(message.Chat, _constants.Messages.SendTime);
            }
        }
    }
}
