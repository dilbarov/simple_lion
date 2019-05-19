using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleLion.Bot.Models;

namespace SimpleLion.Bot.Repositories.EventsStateRepository
{
    public class EventsStateRepository : IEventsStateRepository
    {
        private readonly BotContext _context;

        public EventsStateRepository(BotContext context)
        {
            _context = context;
        }
        public void AddState(long chatId, string currentCommand = null, string nextCommand = null)
        {
            var item = _context.EventsStates.FirstOrDefault(c => c.ChatId.Equals(chatId));
            if (item != null)
            {
                item.CurrentCommand = currentCommand;
                item.NextCommand = nextCommand;
                _context.Update(item);
            }
            else
            {
                _context.EventsStates.Add(new EventsState()
                {
                    ChatId = chatId,
                    CurrentCommand = currentCommand,
                    NextCommand = nextCommand
                });
            }

            _context.SaveChanges();
        }

        public EventsState GetState(long chatId)
        {
            return _context.EventsStates?.FirstOrDefault(c => c.ChatId.Equals(chatId));
        }

        public void ClearState(long chatId)
        {
            var state = GetState(chatId);
            if (state == null) return;
            _context.Remove(state);
            _context.SaveChanges();
        }
    }
}
