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
    public class SetTitleCommand : ICommand
    {
        private readonly ITelegramBotClient _bot;
        private readonly IStateRepository _states;
        private readonly MessageConstants _constants;

        public SetTitleCommand(ITelegramBotClient bot, IStateRepository states, MessageConstants constants)
        {
            _bot = bot;
            _states = states;
            _constants = constants;
        }

        public static string Name => "title";
        public string NextName => SetDateCommand.Name;
        public async Task ExecuteAsync(Message message)
        {
            if (!string.IsNullOrEmpty(message.Text))
            {
                await _bot.SendTextMessageAsync(message.Chat, _constants.Messages.SendDate);
                _states.SetTitle(message.Chat.Id, message.Text);
                _states.AddState(message.Chat.Id, Name,NextName);
            }
            else
            {
                await _bot.SendTextMessageAsync(message.Chat, _constants.Messages.SendTitle);
            }
        }
    }
}
