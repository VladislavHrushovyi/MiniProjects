using StoryTellingBot;
using Telegram.Bot;

var bot = new TelegramBot();
await bot.StartBot();

var me = await bot._botClient.GetMeAsync();
Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();
