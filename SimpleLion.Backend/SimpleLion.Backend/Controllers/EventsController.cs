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
        public async Task<ActionResult<IEnumerable<Event>>> Get(double lat, double lng, double distance, string rubric = "any")
        {
            return await eventsStore.GetEventsNearby(lat, lng, distance, rubric);
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

            await eventsStore.AddEvent(@event);
            return Created(string.Empty, @event);
        }
    }
}
