using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimpleLion.Bot.StateRepository;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimpleLion.Bot.Commands
{
    public class SetDateCommand : ICommand
    {
        private readonly ITelegramBotClient _bot;
        private readonly IStateRepository _states;

        public SetDateCommand(ITelegramBotClient bot, IStateRepository states)
        {
            _bot = bot;
            _states = states;
        }
        public static string Name => "date";
        public string NextName => SetTimeCommand.Name;
        public async Task ExecuteAsync(Message message)
        {
            if(DateTime.TryParse(message.Text, out DateTime date))
            {
                _states.SetDate(message.Chat.Id, date);
                _states.AddState(message.Chat.Id, Name,NextName);
                await _bot.SendTextMessageAsync(message.Chat, "Введите время {часы:минуты}");
            }
            else
            {
                await _bot.SendTextMessageAsync(message.Chat, "Введите дату {год-месяц-число}");
            }
        }
    }
}
