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
        var chatId = message.Chat.Id;
        await chatRepository.InitCommandState(chatId.ToString(), new NewStoryState(botClient));
        
        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Введіть тему розповіді. Для випадкової просто відправте 0",
            cancellationToken: cts);
    }
}