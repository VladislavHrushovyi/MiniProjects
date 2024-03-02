using StoryTellingBot.GptInteraction;
using StoryTellingBot.Repository;
using StoryTellingBot.Repository.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace StoryTellingBot.BotCommands;

public class NewStoryCommand(ITelegramBotClient botClient, IChatRepository chatRepository) : ICommand
{
    private readonly GptClient _gptClient = new();
    public async Task Handle(Message message, CancellationToken cts)
    {
        var chatId = message.Chat.Id;
        await chatRepository.InitCommandState(chatId.ToString(), new NewStoryState(botClient));
        
        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Введіть тему розповіді. Для випадкової просто відправте 0",
            cancellationToken: cts); 
    }
}