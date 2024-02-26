using StoryTellingBot.BotCommands;
using Telegram.Bot;

namespace TgBotSandBox.Commands;

public class CommandFactory
{
    public static ICommand HandleCommand(string text, TelegramBotClient botClient )
    {
        return text switch
        {
            "/long_time_command" => new LongTermCommand(botClient),
            _ => new UnknownCommand(botClient)
        };
    }
}