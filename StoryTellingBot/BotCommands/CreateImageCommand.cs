using StoryTellingBot.Repository;
using StoryTellingBot.Repository.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace StoryTellingBot.BotCommands;

public class CreateImageCommand(ITelegramBotClient botClient, IChatRepository chatRepository) : ICommand
{
    public async Task Handle(Message message, CancellationToken cts)
    {
        var imageState = new CreateImageState(botClient);
        var chatId = message.Chat.Id;
        await chatRepository.InitCommandState(chatId.ToString(), imageState);

        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Очікую на опис картинки яку потрібно створити",
            cancellationToken: cts
            );
    }
}