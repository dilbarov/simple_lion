using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleLion.EventsLib;

namespace SimpleLion.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly EventsStore eventsStore;
        private readonly HttpClient httpClient;

        public EventsController(EventsStore eventsStore, HttpClient httpClient)
        {
            this.eventsStore = eventsStore;
            this.httpClient = httpClient;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> Get(double lat, double lng, double distance, string rubric = "any")
        {
            return await eventsStore.GetEventsNearby(lat, lng, distance, rubric);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> Get(int id)
        {
            return await eventsStore.GetEventById(id);
        }

        // GET api/values/5
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Event @event)
        {
            if (@event.StartTime >= @event.EndTime)
                ModelState.AddModelError(string.Empty, "Дата начала должна быть раньше даты окончания");
            if (@event.StartTime < DateTime.Now)
                ModelState.AddModelError(string.Empty, "Событие должно начинаться в будущем");
            if (!ModelState.IsValid)
                return BadRequest(new { errors = ModelState.SelectMany(er => er.Value.Errors.Select(e => e.ErrorMessage)) });

            if (string.IsNullOrWhiteSpace(@event.LocationName))
            {
                try
                {
                    using (var response = await httpClient.GetAsync($"https://maps.googleapis.com/maps/api/geocode/json?latlng={@event.Latitude},{@event.Longitude}&key=AIzaSyC061NAm2MWNfRZsaD-5ShfZuUyJeWfBCw"))
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var jobject = JObject.Parse(content);
                        @event.LocationName = jobject["results"]["formatted_address"].Value<string>();
                    }
                } catch (Exception) {}
            }

            await eventsStore.AddEvent(@event);
            return Created(string.Empty, @event);
        }

    }
}
