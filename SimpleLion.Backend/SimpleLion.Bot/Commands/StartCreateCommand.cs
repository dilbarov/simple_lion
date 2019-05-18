using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimpleLion.Bot.Models;
using SimpleLion.Bot.Repositories.StateRepository;
using SimpleLion.Bot.Services.MessageConstants;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SimpleLion.Bot.Commands
{
    public class StartCreateCommand : ICommand
    {
        private readonly ITelegramBotClient _bot;
        private readonly IStateRepository _repository;
        private readonly MessageConstants _constants;

        public StartCreateCommand(ITelegramBotClient bot, IStateRepository repository, MessageConstants constants)
        {
            _bot = bot;
            _repository = repository;
            _constants = constants;
        }
        public static string Name => "/create";
        public string NextName => SetLocationCommand.Name;
        public async Task ExecuteAsync(Message message)
        {
            _repository.ClearState(message.Chat.Id);
            
            _repository.AddState(message.Chat.Id, Name, NextName);
            var rkm = new ReplyKeyboardMarkup();
            rkm.ResizeKeyboard = true;
            rkm.Keyboard =
                new[]
                {
                    new[]
                    {
                        new KeyboardButton(_constants.Messages.SendLocation){RequestLocation = true}
                    }
                };

            await _bot.SendTextMessageAsync(
                message.Chat,
                _constants.Messages.SendLocation,replyMarkup:rkm);
        }
    }
}
