using System;
using System.Net;
using System.Threading;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MihaZupan;
using SimpleLion.Bot.Commands;
using SimpleLion.Bot.Services.CommandDetector;
using SimpleLion.Bot.StateRepository;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace SimpleLion.Bot
{
    internal class Program
    {
        private static IContainer Container { get; set; }

        private static ITelegramBotClient _botClient;

        private static void Main(string[] args)
        {
            _botClient = InitTelegramBot();

            ConfigureContainer(_botClient);

            Console.WriteLine("Bot is started");
            Thread.Sleep(int.MaxValue);
        }

        private static ITelegramBotClient InitTelegramBot()
        {
            var botClient = new TelegramBotClient(Environment.GetEnvironmentVariable("BotToken"));

            botClient.OnMessage += OnMessage;
            botClient.StartReceiving();
            return botClient;
        }

        private static void ConfigureContainer(ITelegramBotClient botClient)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<BotContext>().InstancePerLifetimeScope();

            builder.Register(c => botClient).As<ITelegramBotClient>().SingleInstance();

            builder.RegisterType<StateRepository.StateRepository>().As<IStateRepository>().InstancePerLifetimeScope();

            builder.RegisterType<CommandDetector>().As<ICommandDetector>().InstancePerLifetimeScope();

            
            builder.RegisterType<HelloCommand>().Named<ICommand>("hello");
            builder.RegisterType<StartCreateCommand>().Named<ICommand>(StartCreateCommand.Name);
            builder.RegisterType<SetLocationCommand>().Named<ICommand>(SetLocationCommand.Name);
            builder.RegisterType<IsNewCommand>().Named<ICommand>(IsNewCommand.Name);
            builder.RegisterType<SetTitleCommand>().Named<ICommand>(SetTitleCommand.Name);
            builder.RegisterType<SetDateCommand>().Named<ICommand>(SetDateCommand.Name);
            builder.RegisterType<SetTimeCommand>().Named<ICommand>(SetTimeCommand.Name);
            builder.RegisterType<SetEndDateCommand>().Named<ICommand>(SetEndDateCommand.Name);
            builder.RegisterType<SetEndTimeCommand>().Named<ICommand>(SetEndTimeCommand.Name);


            Container = builder.Build();
        }

        private static void OnMessage(object sender, MessageEventArgs e)
        {
            var detector = Container.Resolve<ICommandDetector>();
            var command = detector.Detect(Container, e.Message);

            command?.ExecuteAsync(e.Message);
        }
    }
}
