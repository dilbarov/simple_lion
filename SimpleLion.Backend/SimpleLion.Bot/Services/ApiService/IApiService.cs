using System;
using System.Collections.Generic;
using System.Text;
using SimpleLion.Bot.Services.ApiService.Models;
using Telegram.Bot.Types;

namespace SimpleLion.Bot.Services.ApiService
{
    public interface IApiService
    {
        IEnumerable<EventDto> GetEvents(Location location, string rubric = null, int distance = 500);

        void Create(EventDto ev);
    }
}
