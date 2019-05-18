using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SimpleLion.Bot.Models;

namespace SimpleLion.Bot
{
    public class BotContext : DbContext
    {
        public DbSet<CommandState> CommandStates { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./dbbot.db");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
