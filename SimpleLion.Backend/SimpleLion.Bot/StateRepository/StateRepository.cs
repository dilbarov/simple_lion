using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;
using SimpleLion.Bot.Models;

namespace SimpleLion.Bot.StateRepository
{
    public class StateRepository : IStateRepository
    {
        private readonly BotContext _context;

        public StateRepository(BotContext context)
        {
            _context = context;
        }
        public void AddState(long chatId, string currentCommand, string nextCommand)
        {
            var item = _context.CommandStates.FirstOrDefault(c => c.ChatId.Equals(chatId));
            if (item !=null)
            {
                item.CurrentCommand = currentCommand;
                item.NextCommand = nextCommand;
                _context.Update(item);
            }
            else
            {
                _context.CommandStates.Add(new CommandState
                {
                    ChatId = chatId,
                    CurrentCommand = currentCommand,
                    NextCommand = nextCommand
                });
            }

            _context.SaveChanges();

        }

        public CommandState GetState(long chatId)
        {
            return _context.CommandStates?.FirstOrDefault(c => c.ChatId.Equals(chatId));
        }

        public void SetLocation(long chatId, double lat, double lng)
        {
            var state = GetState(chatId);
            state.Latitude = lat;
            state.Longitude = lng;
            _context.CommandStates.Update(state);
            _context.SaveChanges();
        }

        public void ClearState(long chatId)
        {
            var state = GetState(chatId);
            _context.Remove(state);
            _context.SaveChanges();
        }

        public void SetTitle(long chatId, string title)
        {
            var state = GetState(chatId);
            state.Title = title;
            _context.Update(state);
            _context.SaveChanges();
        }

        public void SetDate(long chatId, DateTime date)
        {
            var state = GetState(chatId);
            state.DateTime = date;
            _context.Update(state);
            _context.SaveChanges();
        }

        public void SetTime(long chatId, TimeSpan time)
        {
            var state = GetState(chatId);
            var newDateTime = new DateTime(state.DateTime.Year,state.DateTime.Month,state.DateTime.Day, time.Hours,time.Minutes, time.Seconds);
            state.DateTime = newDateTime;
            _context.Update(state);
            _context.SaveChanges();
        }

        public void SetEndDate(long chatId, DateTime date)
        {
            var state = GetState(chatId);
            state.DateEnd = date;
            _context.Update(state);
            _context.SaveChanges();
        }

        public void SetEndTime(long chatId, TimeSpan time)
        {
            var state = GetState(chatId);
            var newDateTime = new DateTime(state.DateTime.Year, state.DateTime.Month, state.DateTime.Day, time.Hours, time.Minutes, time.Seconds);
            state.DateEnd = newDateTime;
            _context.Update(state);
            _context.SaveChanges();
        }

        public void Finish(long chatId)
        {
            var state = GetState(chatId);
            state.IsFinished = true;
            _context.Update(state);
            _context.SaveChanges();
        }
    }
}
