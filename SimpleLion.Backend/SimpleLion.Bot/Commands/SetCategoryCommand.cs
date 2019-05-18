using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimpleLion.Bot.StateRepository;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimpleLion.Bot.Commands
{
    public class SetCategoryCommand : ICommand
    {
        private readonly ITelegramBotClient _bot;
        private readonly IStateRepository _states;

        public SetCategoryCommand(ITelegramBotClient bot, IStateRepository states)
        {
            _bot = bot;
            _states = states;
        }

        public static string Name => "category";
        public string NextName => SetTitleCommand.Name;
        public async Task ExecuteAsync(Message message)
        {
            if (!string.IsNullOrEmpty(message.Text))
            {
                await _bot.SendTextMessageAsync(message.Chat, "Введите название");
                _states.SetCategory(message.Chat.Id, message.Text);
                _states.AddState(message.Chat.Id, Name, NextName);
            }
            else
            {
                await _bot.SendTextMessageAsync(message.Chat, "Введите категорию");
            }
        }
    }
}
