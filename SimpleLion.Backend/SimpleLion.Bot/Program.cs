using System;
using System.Net;
using System.Threading;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MihaZupan;
using SimpleLion.Bot.Commands;
using SimpleLion.Bot.Modules;
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

            builder.RegisterModule(new DatabaseModule());

            builder.Register(c => botClient).As<ITelegramBotClient>().SingleInstance();

            builder.RegisterModule(new CommandsModule());

            Container = builder.Build();
        }

        private static async void OnMessage(object sender, MessageEventArgs e)
        {
            var detector = Container.Resolve<ICommandDetector>();
            var command = detector.Detect(Container, e.Message);

            command?.ExecuteAsync(e.Message);

            if (command == null)
                await _botClient.SendTextMessageAsync(e.Message.Chat,
                    "Чет не то ты вводишь");
        }
    }
}
