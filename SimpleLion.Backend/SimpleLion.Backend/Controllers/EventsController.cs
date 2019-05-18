using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleLion.EventsLib;

namespace SimpleLion.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly EventsStore eventsStore;

        public EventsController(EventsStore eventsStore)
        {
            this.eventsStore = eventsStore;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> Get(double lat, double lng, double distance)
        {
            return await eventsStore.GetEventsNearby(lat, lng, distance);
        }

        // GET api/values/5
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Event @event)
        {
            await eventsStore.AddEvent(@event);
            return Ok();
        }
    }
}
