using System;
using System.Net;
using System.Threading;
using MihaZupan;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace SimpleLion.Bot
{
    internal class Program
    {
        private static ITelegramBotClient _botClient;

        private static void Main(string[] args)
        {
            _botClient = new TelegramBotClient(Environment.GetEnvironmentVariable("BotToken"));

            _botClient.OnMessage += OnMessage;
            _botClient.StartReceiving();
            var me = _botClient.GetMeAsync().Result;

            Console.WriteLine("Bot is started");
            Thread.Sleep(int.MaxValue);
        }

        private static async void OnMessage(object sender, MessageEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Message.Text))
                await _botClient.SendTextMessageAsync(
                    e.Message.Chat,
                    "Hello");
        }
    }
}
