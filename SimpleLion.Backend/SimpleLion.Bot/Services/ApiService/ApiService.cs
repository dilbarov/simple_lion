using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using SimpleLion.Bot.Services.ApiService.Models;
using Telegram.Bot.Types;

namespace SimpleLion.Bot.Services.ApiService
{
    public class ApiService : IApiService
    {
        private readonly RestClient _restService;

        public ApiService()
        {
            _restService = new RestClient("http://95.217.1.188:5000");
        }
        public IEnumerable<EventDto> GetEvents(Location location, string rubric = null, int distance = 500)
        {
            var request = new RestRequest("api/Events/", Method.GET);
            request.AddUrlSegment("lat", location.Latitude);
            request.AddUrlSegment("lng", location.Longitude);
            request.AddUrlSegment("distance", distance);
            request.AddUrlSegment("rubric", rubric);
            try
            {
                return _restService.Execute<List<EventDto>>(request).Data ?? new List<EventDto>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void Create(EventDto ev)
        {
            var request = new RestRequest("api/Events/", Method.POST);
            request.AddJsonBody(ev);

            _restService.Execute(request);
        }
    }
}
