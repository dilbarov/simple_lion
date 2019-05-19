using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Telegram.Bot.Types.ReplyMarkups;

namespace SimpleLion.Bot.Services.MessageConstants
{
    public class MessageConstants
    {
        public MessageConstantsModel Messages { get; set; }

        public MessageConstants()
        {
            var json = File.ReadAllText($"strings.json");
            Messages = JsonConvert.DeserializeObject<MessageConstantsModel>(json);
        }

        public ReplyKeyboardMarkup GetReplyKeyboardMarkupByCategories()
        {
            var rkm = new ReplyKeyboardMarkup();
            //rkm.ResizeKeyboard = true;
            rkm.Keyboard =
                new[]
                {
                    Messages.Categories.Select(c=> new KeyboardButton(c))
                };
            return rkm;
        }
    }
}
