using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleLion.Bot.Models
{
    public class EventsState
    {
        [Key]
        public int Id { get; set; }

        public long ChatId { get; set; }

        public string CurrentCommand { get; set; }

        public string NextCommand { get; set; }
    }
}
