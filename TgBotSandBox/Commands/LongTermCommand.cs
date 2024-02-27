using StoryTellingBot.BotCommands;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgBotSandBox.Repository;
using TgBotSandBox.Repository.Models;

namespace TgBotSandBox.Commands;

public class LongTermCommand(ITelegramBotClient botClient, IChatRepository chatRepository) : ICommand
{
    public async Task Handle(Message message, CancellationToken cts)
    {
        await botClient.SendTextMessageAsync(message.Chat.Id, "Очікуйте. Генеруються питання", cancellationToken:cts);
        await Task.Delay(5000, cts);
        var questions = Enumerable.Range(0, 10).Select(x => $"Question{x}");
        
        var longTermState = new NewStoryState(botClient, questions);
        await chatRepository.InitCommandState(message.Chat.Id.ToString(), longTermState);
        
        await botClient.SendTextMessageAsync(message.Chat.Id,questions.First() , cancellationToken:cts); 
    }
}