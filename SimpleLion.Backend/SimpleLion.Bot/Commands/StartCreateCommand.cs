using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimpleLion.Bot.Models;
using SimpleLion.Bot.StateRepository;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimpleLion.Bot.Commands
{
    public class StartCreateCommand : ICommand
    {
        private readonly ITelegramBotClient _bot;
        private readonly IStateRepository _repository;

        public StartCreateCommand(ITelegramBotClient bot, IStateRepository repository)
        {
            _bot = bot;
            _repository = repository;
        }
        public static string Name => "/create";
        public string NextName => SetLocationCommand.Name;
        public async Task ExecuteAsync(Message message)
        {
            _repository.ClearState(message.Chat.Id);
            await _bot.SendTextMessageAsync(
                message.Chat,
                "Пришлите геоопозицию");
            _repository.AddState(message.Chat.Id, Name, NextName);
        }
    }
}
