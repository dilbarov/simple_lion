using System;
using System.Threading.Tasks;
using SimpleLion.Bot.StateRepository;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimpleLion.Bot.Commands
{
    public class SetEndTimeCommand : ICommand
    {

        private readonly ITelegramBotClient _bot;
        private readonly IStateRepository _states;

        public SetEndTimeCommand(ITelegramBotClient bot, IStateRepository states)
        {
            _bot = bot;
            _states = states;
        }
        public static string Name => "endtime";
        public async Task ExecuteAsync(Message message)
        {
            if (TimeSpan.TryParse(message.Text, out TimeSpan time))
            {
                _states.SetEndTime(message.Chat.Id, time);
                _states.Finish(message.Chat.Id);
                await _bot.SendTextMessageAsync(message.Chat, "Событие создано");
            }
            else
            {
                await _bot.SendTextMessageAsync(message.Chat, "Введите время окончания {часы:минуты}");
            }
        }
    }
}
