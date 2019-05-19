using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleLion.Bot.Models
{
    public class CommandState
    {
        [Key]
        public int Id { get; set; }

        public long ChatId { get; set; }

        public string CurrentCommand { get; set; }

        public string NextCommand { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Title { get; set; }

        public DateTime DateTime { get; set; }

        public DateTime DateEnd { get; set; }

        public bool IsFinished { get; set; }

        public string Category { get; set; }

        public string Comment { get; set; }
    }
}
