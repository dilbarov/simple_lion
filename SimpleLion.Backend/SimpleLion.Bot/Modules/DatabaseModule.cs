using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using SimpleLion.Bot.Repositories.EventsStateRepository;
using SimpleLion.Bot.Repositories.StateRepository;

namespace SimpleLion.Bot.Modules
{
    public class DatabaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<BotContext>().InstancePerDependency();

            builder.RegisterType<StateRepository>().As<IStateRepository>().InstancePerDependency();

            builder.RegisterType<EventsStateRepository>().As<IEventsStateRepository>().InstancePerDependency();

        }
    }
}
