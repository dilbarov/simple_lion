using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimpleLion.Bot.StateRepository;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimpleLion.Bot.Commands
{
    public class SetTitleCommand : ICommand
    {
        private readonly ITelegramBotClient _bot;
        private readonly IStateRepository _states;

        public SetTitleCommand(ITelegramBotClient bot, IStateRepository states)
        {
            _bot = bot;
            _states = states;
        }

        public static string Name => "title";
        public string NextName => SetDateCommand.Name;
        public async Task ExecuteAsync(Message message)
        {
            if (!string.IsNullOrEmpty(message.Text))
            {
                await _bot.SendTextMessageAsync(message.Chat, "Введите дату {год-месяц-число}");
                _states.AddState(message.Chat.Id, Name,NextName);
            }
            else
            {
                await _bot.SendTextMessageAsync(message.Chat, "Введите название");
            }
        }
    }
}
