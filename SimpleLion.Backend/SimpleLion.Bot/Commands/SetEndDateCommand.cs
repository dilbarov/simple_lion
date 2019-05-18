using System;
using System.Threading.Tasks;
using SimpleLion.Bot.Repositories.StateRepository;
using SimpleLion.Bot.Services.MessageConstants;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimpleLion.Bot.Commands
{
    public class SetEndDateCommand : ICommand
    {
        private readonly ITelegramBotClient _bot;
        private readonly IStateRepository _states;
        private readonly MessageConstants _constants;

        public SetEndDateCommand(ITelegramBotClient bot, IStateRepository states, MessageConstants constants)
        {
            _bot = bot;
            _states = states;
            _constants = constants;
        }
        public static string Name => "enddate";
        public static string NextName => SetEndTimeCommand.Name;
        public async Task ExecuteAsync(Message message)
        {
            if (message.Text.ToLower() == "сегодня")
            {
                _states.SetEndDate(message.Chat.Id, DateTime.Now);
                _states.AddState(message.Chat.Id, Name, NextName);
                await _bot.SendTextMessageAsync(message.Chat, _constants.Messages.SendTime);
            }
            else if (DateTime.TryParse(message.Text, out DateTime date))
            {
                _states.SetEndDate(message.Chat.Id, date);
                _states.AddState(message.Chat.Id, Name, NextName);
                await _bot.SendTextMessageAsync(message.Chat, _constants.Messages.SendTime);
            }
            else
            {
                await _bot.SendTextMessageAsync(message.Chat, _constants.Messages.SendEndDate);
            }
        }
    }
}
