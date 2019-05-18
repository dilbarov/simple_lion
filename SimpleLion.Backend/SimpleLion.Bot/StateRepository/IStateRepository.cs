using System;
using System.Collections.Generic;
using System.Text;
using SimpleLion.Bot.Models;
using Telegram.Bot.Types;

namespace SimpleLion.Bot.StateRepository
{
    public interface IStateRepository
    {
        void AddState(long chatId, string currentCommand, string nextCommand);

        CommandState GetState(long chatId);

        void SetLocation(long chatId, double lat, double lng);

        void ClearState(long chatId);

        void SetTitle(long chatId, string title);
        void SetDate(long chatId, DateTime date);

        void SetTime(long chatId, TimeSpan time);

        void SetEndDate(long chatId, DateTime date);

        void SetEndTime(long chatId, TimeSpan time);

        void Finish(long chatId);
    }
}
