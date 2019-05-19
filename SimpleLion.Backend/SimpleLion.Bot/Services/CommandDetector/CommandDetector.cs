using Autofac;
using SimpleLion.Bot.Commands;
using SimpleLion.Bot.Repositories.EventsStateRepository;
using SimpleLion.Bot.Repositories.StateRepository;
using Telegram.Bot.Types;

namespace SimpleLion.Bot.Services.CommandDetector
{
    public class CommandDetector : ICommandDetector
    {
        private readonly IStateRepository _states;
        private readonly IEventsStateRepository _events;

        public CommandDetector(IStateRepository states, IEventsStateRepository events)
        {
            _states = states;
            _events = events;
        }
        public ICommand Detect(IContainer container, Message message)
        {
            ICommand command = null;

            if(message?.Text != null && message.Text.StartsWith("/") && container.IsRegisteredWithName<ICommand>(message.Text))
                return container.ResolveNamed<ICommand>(message.Text);

            var state = _states.GetState(message.Chat.Id);
            if (state?.NextCommand != null && !state.IsFinished)
            {
                command = container.ResolveNamed<ICommand>(state.NextCommand);
            }

            var evState = _events.GetState(message.Chat.Id);
            if (command==null && evState?.NextCommand != null)
            {
                command = container.ResolveNamed<ICommand>(evState.NextCommand);
            }

            return command;
        }
    }
}
