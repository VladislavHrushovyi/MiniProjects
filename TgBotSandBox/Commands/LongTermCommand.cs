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
        await chatRepository.InitCommandState(chatId.ToString(), new ImageCreationState(botClient));
        
        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "очікую опис для картинки",
            cancellationToken: cts);
    }
}