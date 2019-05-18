using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimpleLion.Bot.StateRepository;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SimpleLion.Bot.Commands
{
    public class IsNewCommand : ICommand
    {
        private readonly ITelegramBotClient _bot;
        private readonly IStateRepository _states;

        public IsNewCommand(ITelegramBotClient bot, IStateRepository states)
        {
            _bot = bot;
            _states = states;
        }
        public static string Name => "isnew";
        public static string NextName => SetTitleCommand.Name;
        public async Task ExecuteAsync(Message message)
        {
            switch (message.Text.ToLower())
            {
                case "да": 
                    _states.AddState(message.Chat.Id, Name, NextName);
                    await _bot.SendTextMessageAsync(message.Chat.Id, "Введите название");
                    break;
                case "нет":
                    _states.ClearState(message.Chat.Id);
                    await _bot.SendTextMessageAsync(message.Chat.Id,
                        "Тогда лан");
                    break;
            }
        }
    }
}
