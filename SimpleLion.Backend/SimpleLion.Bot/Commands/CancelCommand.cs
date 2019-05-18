using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimpleLion.Bot.StateRepository;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimpleLion.Bot.Commands
{
    public class CancelCommand : ICommand
    {
        private readonly ITelegramBotClient _bot;
        private readonly IStateRepository _states;

        public CancelCommand(ITelegramBotClient bot, IStateRepository states)
        {
            _bot = bot;
            _states = states;
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
                    await _bot.SendTextMessageAsync(message.Chat.Id, "Заполнение отменено");
                }
                else
                {
                    await _bot.SendTextMessageAsync(message.Chat.Id, "Отменять нечего");
                }
            }
            else
            {
                await _bot.SendTextMessageAsync(message.Chat.Id, "Отменять нечего");
            }
        }
    }
}
