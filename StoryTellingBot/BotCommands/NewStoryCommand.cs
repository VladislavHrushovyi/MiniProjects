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
        await botClient.SendTextMessageAsync(message.Chat.Id, "Очікуйте. Генеруються питання", cancellationToken:cts);
        var questionString = await _gptClient.AskInGpt(new List<ChatItem>()
        {
            new(
                "user",
                "Напиши 10 простих запитань до розповіді, щоб за відповідями можна було створити розповідь. Питання можуть містити шутливу форму")
        });
        var questions = questionString.Split("\n");
        
        var longTermState = new NewStoryState(botClient, questions);
        await chatRepository.InitCommandState(message.Chat.Id.ToString(), longTermState);
        
        await botClient.SendTextMessageAsync(message.Chat.Id,questions.First() , cancellationToken:cts); 
    }
}