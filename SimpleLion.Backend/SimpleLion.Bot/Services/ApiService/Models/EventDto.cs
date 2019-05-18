using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleLion.Bot.Services.ApiService.Models
{
    public class EventDto
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Comment { get; set; }

        public string Title { get; set; }

        public string Rubric { get; set; }

        public string LocationName { get; set; }
    }
}
