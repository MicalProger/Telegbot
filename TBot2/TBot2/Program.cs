using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TBot2
{
    class Program
    {
        static int currentAns;
        static int limit;
        static bool isTest;
        private static TelegramBotClient client;
        static KeyboardButton[] buttons;
        public static void Main(string[] args)
        {
            client = new TelegramBotClient("2110113682:AAFPpv2X6JRWCESwktzQKKBB4Wl6EctW5uo");
            client.OnMessage += Client_OnMessage;
            client.StartReceiving();
            Console.ReadLine();
            client.StopReceiving();
        }

        private static void Client_OnMessage(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message?.Type == MessageType.Text)
            {
               
                if (isTest && limit != 0)
                {
                    int cur = int.Parse(message.Text);
                    if (cur == currentAns)
                    {
                        client.SendTextMessageAsync(message.Chat.Id, "правильно");
                    }
                    else
                    {
                        client.SendTextMessageAsync(message.Chat.Id, "Heправильно");
                    }
                    Random random = new Random();
                    int n1 = random.Next(20);
                    int n2 = random.Next(20);
                    currentAns = n1 + n2;
                    buttons = new KeyboardButton[5];
                    buttons[0] = new KeyboardButton(currentAns.ToString());
                    for (int i = 0; i < 4; i++)
                    {
                        buttons[i + 1] = new KeyboardButton(random.Next(40).ToString());
                    }
                    buttons = buttons.ToList().OrderBy(x => Guid.NewGuid().ToString()).ToArray();
                    var fin = new List<List<KeyboardButton>>() {buttons.Where(i => buttons.ToList().IndexOf(i) % 2 == 0).ToList(), buttons.Where(i => buttons.ToList().IndexOf(i) % 2 == 1).ToList() };
                    client.SendTextMessageAsync(message.Chat.Id, $"{n1} + {n2} =", replyMarkup: new ReplyKeyboardMarkup(fin, false, true));
                    limit--;
                }
                if (isTest && limit == 0)
                {
                    if (!int.TryParse(message.Text, out limit))
                    {
                        client.SendTextMessageAsync(e.Message.Chat.Id, "Введите количоство примеров");
                    }
                    Random random = new Random();
                    int n1 = random.Next(20);
                    int n2 = random.Next(20);
                    currentAns = n1 + n2;
                    buttons = new KeyboardButton[5];
                    buttons[0] = new KeyboardButton(currentAns.ToString());
                    for (int i = 0; i < 4; i++)
                    {
                        buttons[i + 1] = new KeyboardButton(random.Next(40).ToString());
                    }
                    buttons = buttons.ToList().OrderBy(x => Guid.NewGuid().ToString()).ToArray();
                    var fin = new List<List<KeyboardButton>>() { buttons.Where(i => buttons.ToList().IndexOf(i) % 2 == 0).ToList(), buttons.Where(i => buttons.ToList().IndexOf(i) % 2 == 1).ToList() };
                    client.SendTextMessageAsync(message.Chat.Id, $"{n1} + {n2} =", replyMarkup: new ReplyKeyboardMarkup(fin, false, true));
                    limit--;
                }
                if (limit == 0)
                {
                    isTest = false;
                }
                if (message.Text == @"/test")
                {
                    client.SendTextMessageAsync(e.Message.Chat.Id, "Введите количоство примеров");
                    isTest = true;
                }
            }
            Console.WriteLine(message.Text);
        }
    }
}
