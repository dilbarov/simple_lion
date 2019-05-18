using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimpleLion.Bot.Repositories.StateRepository;
using SimpleLion.Bot.Services.MessageConstants;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimpleLion.Bot.Commands
{
    public class SetDateCommand : ICommand
    {
        private readonly ITelegramBotClient _bot;
        private readonly IStateRepository _states;
        private readonly MessageConstants _constants;

        public SetDateCommand(ITelegramBotClient bot, IStateRepository states, MessageConstants constants)
        {
            _bot = bot;
            _states = states;
            _constants = constants;
        }
        public static string Name => "date";
        public string NextName => SetTimeCommand.Name;
        public async Task ExecuteAsync(Message message)
        {
            if (message.Text.ToLower() == "сегодня")
            {
                _states.SetDate(message.Chat.Id, DateTime.Now);
                _states.AddState(message.Chat.Id, Name, NextName);
                await _bot.SendTextMessageAsync(message.Chat, _constants.Messages.SendTime);
            }
            else if(DateTime.TryParse(message.Text, out DateTime date))
            {
                _states.SetDate(message.Chat.Id, date);
                _states.AddState(message.Chat.Id, Name,NextName);
                await _bot.SendTextMessageAsync(message.Chat, _constants.Messages.SendTime);
            }
            else
            {
                await _bot.SendTextMessageAsync(message.Chat, _constants.Messages.SendDate);
            }
        }
    }
}
