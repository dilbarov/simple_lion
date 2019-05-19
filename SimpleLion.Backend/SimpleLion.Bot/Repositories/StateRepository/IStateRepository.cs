using System;
using SimpleLion.Bot.Models;

namespace SimpleLion.Bot.Repositories.StateRepository
{
    public interface IStateRepository
    {
        void AddState(long chatId, string currentCommand = null, string nextCommand = null);

        CommandState GetState(long chatId);

        void SetLocation(long chatId, double lat, double lng);

        void ClearState(long chatId);

        void SetTitle(long chatId, string title);
        void SetDate(long chatId, DateTime date);

        void SetTime(long chatId, TimeSpan time);

        void SetEndDate(long chatId, DateTime date);

        void SetEndTime(long chatId, TimeSpan time);

        void SetCategory(long chatId, string category);

        void Finish(long chatId);
        void SetComment(long chatId, string messageText);
    }
}
