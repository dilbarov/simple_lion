using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace SimpleLion.Bot.Commands
{
    public interface ICommand
    {
        Task ExecuteAsync(Message message);
    }
}
