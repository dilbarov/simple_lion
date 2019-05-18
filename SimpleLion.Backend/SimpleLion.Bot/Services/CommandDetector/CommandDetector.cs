using Autofac;
using SimpleLion.Bot.Commands;
using SimpleLion.Bot.StateRepository;
using Telegram.Bot.Types;

namespace SimpleLion.Bot.Services.CommandDetector
{
    public class CommandDetector : ICommandDetector
    {
        private readonly IStateRepository _states;

        public CommandDetector(IStateRepository states)
        {
            _states = states;
        }
        public ICommand Detect(IContainer container, Message message)
        {
            ICommand command = null;

            if(message?.Text != null &&message.Text.StartsWith("/") && container.IsRegisteredWithName<ICommand>(message.Text))
                return container.ResolveNamed<ICommand>(message.Text);

            var state = _states.GetState(message.Chat.Id);
            if (state?.NextCommand != null && !state.IsFinished)
            {
                command = container.ResolveNamed<ICommand>(state.NextCommand);
            }

            return command;
        }
    }
}
