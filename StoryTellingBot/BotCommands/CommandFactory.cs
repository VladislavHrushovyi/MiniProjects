using StoryTellingBot.Repository;
using Telegram.Bot;

namespace StoryTellingBot.BotCommands;

public static class CommandFactory
{
    private static readonly IChatRepository _chatRepository = new ChatRepository();
    public static ICommand GetCommandHandler(string text, TelegramBotClient botClient )
    {
        return text switch
        {
            "/new_story" => new NewStoryCommand(botClient, _chatRepository),
            "/text_to_speech" => new TextToSpeechCommand(botClient, _chatRepository),
            "/create_image" => new CreateImageCommand(botClient, _chatRepository),
            _ => new TextMessageCommand(_chatRepository)
        };
    }
}