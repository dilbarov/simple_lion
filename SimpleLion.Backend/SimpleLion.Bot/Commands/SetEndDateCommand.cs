using System;
using System.Threading.Tasks;
using SimpleLion.Bot.StateRepository;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimpleLion.Bot.Commands
{
    public class SetEndDateCommand : ICommand
    {
        private readonly ITelegramBotClient _bot;
        private readonly IStateRepository _states;

        public SetEndDateCommand(ITelegramBotClient bot, IStateRepository states)
        {
            _bot = bot;
            _states = states;
        }
        public static string Name => "enddate";
        public static string NextName => SetEndTimeCommand.Name;
        public async Task ExecuteAsync(Message message)
        {
            if (DateTime.TryParse(message.Text, out DateTime date))
            {
                _states.SetEndDate(message.Chat.Id, date);
                _states.AddState(message.Chat.Id, Name, NextName);
                await _bot.SendTextMessageAsync(message.Chat, "Введите время окончания {часы:минуты}");
            }
            else
            {
                await _bot.SendTextMessageAsync(message.Chat, "Введите дату окончания {год-месяц-число}");
            }
        }
    }
}
