using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimpleLion.Bot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimpleLion.Bot.Commands
{
    public class HelloCommand : ICommand
    {
        private readonly BotContext _context;
        private readonly ITelegramBotClient _bot;

        public HelloCommand(BotContext context, ITelegramBotClient bot)
        {
            _context = context;
            _bot = bot;
        }

        public string Name => "hello";
        public string NextName => "none";

        public async Task ExecuteAsync(Message message)
        {
            await _bot.SendTextMessageAsync(
                message.Chat,
                "Hello");
        }
    }
}
