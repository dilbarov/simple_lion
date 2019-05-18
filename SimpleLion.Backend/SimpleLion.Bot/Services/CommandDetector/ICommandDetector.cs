using Autofac;
using SimpleLion.Bot.Commands;
using Telegram.Bot.Types;

namespace SimpleLion.Bot.Services.CommandDetector
{
    public interface ICommandDetector
    {
        ICommand Detect(IContainer container, Message message);
    }
}
