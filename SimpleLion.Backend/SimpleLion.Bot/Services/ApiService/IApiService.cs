using System;
using System.Collections.Generic;
using System.Text;
using SimpleLion.Bot.Services.ApiService.Models;
using Telegram.Bot.Types;

namespace SimpleLion.Bot.Services.ApiService
{
    public interface IApiService
    {
        IEnumerable<EventDto> GetEvents(Location location, string rubric = "any", int distance = 500);

        bool Create(EventDto ev);
    }
}
