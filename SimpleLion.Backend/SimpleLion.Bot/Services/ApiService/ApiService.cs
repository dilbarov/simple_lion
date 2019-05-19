using System;
using System.Collections.Generic;
using System.Net;
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
        public IEnumerable<EventDto> GetEvents(Location location, string rubric = "any", int distance = 500)
        {
            var request = new RestRequest($"api/Events?lat={location.Latitude.ToString().Replace(",",".")}&lng={location.Longitude.ToString().Replace(",", ".")}&distance={distance}&rubric={rubric??""}", Method.GET);

            try
            {
                return _restService.Execute<List<EventDto>>(request).Data ?? new List<EventDto>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Create(EventDto ev)
        {
            var request = new RestRequest("api/Events/", Method.POST);
            request.AddJsonBody(ev);

            var responce =_restService.Execute(request);
            return responce.StatusCode == HttpStatusCode.Created;
        }
    }
}
