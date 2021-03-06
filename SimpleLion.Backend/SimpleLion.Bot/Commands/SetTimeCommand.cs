﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimpleLion.Bot.Repositories.StateRepository;
using SimpleLion.Bot.Services.MessageConstants;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimpleLion.Bot.Commands
{
    public class SetTimeCommand : ICommand
    {
        private readonly ITelegramBotClient _bot;
        private readonly IStateRepository _states;
        private readonly MessageConstants _constants;

        public SetTimeCommand(ITelegramBotClient bot, IStateRepository states, MessageConstants constants)
        {
            _bot = bot;
            _states = states;
            _constants = constants;
        }
        public static string Name => "time";
        public string NextName => SetEndDateCommand.Name;
        public async Task ExecuteAsync(Message message)
        {
            if (TimeSpan.TryParse(message.Text, out TimeSpan time))
            {
                _states.SetTime(message.Chat.Id, time);
                _states.AddState(message.Chat.Id, Name, NextName);
                await _bot.SendTextMessageAsync(message.Chat, _constants.Messages.SendEndDate);
            }
            else
            {
                await _bot.SendTextMessageAsync(message.Chat, _constants.Messages.SendTime);
            }
        }
    }
}
