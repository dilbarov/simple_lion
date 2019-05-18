using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using SimpleLion.Bot.Commands;
using SimpleLion.Bot.Services.CommandDetector;

namespace SimpleLion.Bot.Modules
{
    public class CommandsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<CommandDetector>().As<ICommandDetector>().InstancePerLifetimeScope();

            builder.RegisterType<StartCommand>().Named<ICommand>(StartCommand.Name);
            builder.RegisterType<StartCreateCommand>().Named<ICommand>(StartCreateCommand.Name);
            builder.RegisterType<SetLocationCommand>().Named<ICommand>(SetLocationCommand.Name);
            builder.RegisterType<IsNewCommand>().Named<ICommand>(IsNewCommand.Name);
            builder.RegisterType<SetCategoryCommand>().Named<ICommand>(SetCategoryCommand.Name);
            builder.RegisterType<SetTitleCommand>().Named<ICommand>(SetTitleCommand.Name);
            builder.RegisterType<SetDateCommand>().Named<ICommand>(SetDateCommand.Name);
            builder.RegisterType<SetTimeCommand>().Named<ICommand>(SetTimeCommand.Name);
            builder.RegisterType<SetEndDateCommand>().Named<ICommand>(SetEndDateCommand.Name);
            builder.RegisterType<SetEndTimeCommand>().Named<ICommand>(SetEndTimeCommand.Name);
            builder.RegisterType<HelpCommand>().Named<ICommand>(HelpCommand.Name);
            builder.RegisterType<CancelCommand>().Named<ICommand>(CancelCommand.Name);
        }
    }
}
