using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimpleLion.Bot.Commands
{
    public class StartCommand : ICommand
    {
        private readonly ITelegramBotClient _bot;

        public StartCommand(ITelegramBotClient bot)
        {
            _bot = bot;
        }
        public static string Name => "/start";
        
        public async Task ExecuteAsync(Message message)
        {
            await _bot.SendTextMessageAsync(message.Chat,
                "/create - создать мероприятие \n" +
                "/help - помощь \n" +
                "/cancel - отменить заполнение формы");
        }
    }
}
