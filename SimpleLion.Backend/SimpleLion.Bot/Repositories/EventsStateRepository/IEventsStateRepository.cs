using System;
using System.Collections.Generic;
using System.Text;
using SimpleLion.Bot.Models;

namespace SimpleLion.Bot.Repositories.EventsStateRepository
{
    public interface IEventsStateRepository
    {
        void AddState(long chatId, string currentCommand = null, string nextCommand = null);

        EventsState GetState(long chatId);

        void ClearState(long chatId);
    }
}
