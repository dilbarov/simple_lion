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
            var state = _states.GetState(message.Chat.Id);
            if (state?.NextCommand != null && !state.IsFinished)
            {
                command = container.ResolveNamed<ICommand>(state.NextCommand);
            }
            else if (message?.Text != null && container.IsRegisteredWithName<ICommand>(message.Text))
            {
                command = container.ResolveNamed<ICommand>(message.Text);
            }

            return command;
        }
    }
}
