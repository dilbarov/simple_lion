using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SimpleLion.Bot.Commands
{
    public class GetEventsCommand : ICommand
    {
        private readonly ITelegramBotClient _bot;

        public GetEventsCommand(ITelegramBotClient bot)
        {
            _bot = bot;
        }

        public string Name => "/events";
        //public string NextName => 
        public async Task ExecuteAsync(Message message)
        {
        }
    }
}
