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
    public class CancelCommand : ICommand
    {
        private readonly ITelegramBotClient _bot;
        private readonly IStateRepository _states;
        private readonly MessageConstants _constants;

        public CancelCommand(ITelegramBotClient bot, IStateRepository states, MessageConstants constants)
        {
            _bot = bot;
            _states = states;
            _constants = constants;
        }
        public static string Name => "/cancel";
        public async Task ExecuteAsync(Message message)
        {
            var state = _states.GetState(message.Chat.Id);
            if (state!=null)
            {
                if (!state.IsFinished)
                {
                    _states.ClearState(message.Chat.Id);
                    await _bot.SendTextMessageAsync(message.Chat.Id, _constants.Messages.CancelFillng);
                }
                else
                {
                    await _bot.SendTextMessageAsync(message.Chat.Id, _constants.Messages.NothingCancel);
                }
            }
            else
            {
                await _bot.SendTextMessageAsync(message.Chat.Id, _constants.Messages.NothingCancel);
            }
        }
    }
}
