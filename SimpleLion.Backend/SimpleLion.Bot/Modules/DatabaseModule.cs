using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using SimpleLion.Bot.StateRepository;

namespace SimpleLion.Bot.Modules
{
    public class DatabaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<BotContext>().InstancePerLifetimeScope();

            builder.RegisterType<StateRepository.StateRepository>().As<IStateRepository>().InstancePerLifetimeScope();
        }
    }
}
