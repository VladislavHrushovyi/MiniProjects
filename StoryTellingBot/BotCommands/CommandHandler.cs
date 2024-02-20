using Telegram.Bot;

namespace StoryTellingBot.BotCommands;

public class CommandHandler
{
    public ICommand HandleCommand(string text, TelegramBotClient botClient )
    {
        switch (text)
        {
            case "/new_story":
                return new NewStoryCommand(botClient);
            default:
                return new UnknownCommand(botClient);
        }
    }
}