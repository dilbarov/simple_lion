using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimpleLion.Bot.Repositories.StateRepository;
using SimpleLion.Bot.Services.MessageConstants;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SimpleLion.Bot.Commands
{
    public class IsNewCommand : ICommand
    {
        private readonly ITelegramBotClient _bot;
        private readonly IStateRepository _states;
        private readonly MessageConstants _constants;

        public IsNewCommand(ITelegramBotClient bot, IStateRepository states, MessageConstants constants)
        {
            _bot = bot;
            _states = states;
            _constants = constants;
        }
        public static string Name => "isnew";
        public static string NextName => SetCategoryCommand.Name;
        public async Task ExecuteAsync(Message message)
        {
            switch (message.Text.ToLower())
            {
                case "да": 
                    _states.AddState(message.Chat.Id, Name, NextName);
                    await _bot.SendTextMessageAsync(message.Chat.Id, _constants.Messages.ChooseCategory,replyMarkup: _constants.GetReplyKeyboardMarkupByCategories());
                    break;
                case "нет":
                    _states.ClearState(message.Chat.Id);
                    await _bot.SendTextMessageAsync(message.Chat.Id,
                        _constants.Messages.IfIsNotNew);
                    break;
            }
        }
    }
}
