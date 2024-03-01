using StoryTellingBot.BotCommands;
using Telegram.Bot;
using TgBotSandBox.Repository;

namespace TgBotSandBox.Commands;

public abstract class CommandFactory
{
    private static readonly IChatRepository _chatRepository = new ChatRepository();
    public static ICommand HandleCommand(string text, TelegramBotClient botClient)
    {
        return text switch
        {
            "/long_time_command" => new LongTermCommand(botClient, _chatRepository),
            _ => new TextMessageCommand(_chatRepository)
        };
    }
}