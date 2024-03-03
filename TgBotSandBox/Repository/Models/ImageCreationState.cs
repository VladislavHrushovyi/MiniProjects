using StoryTellingBot.GptInteraction;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBotSandBox.Repository.Models;

public class ImageCreationState(ITelegramBotClient botClient) : ICommandState
{
    private readonly GptClient _gptClient = new();
    public async Task Handle(Message message, CancellationToken cts)
    {
        var chatId = message.Chat.Id;
        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Очікуйте, роблю картинку",
            cancellationToken: cts
        );
        var imageUrl = await _gptClient.CreateImage(message.Text);

        await botClient.SendPhotoAsync(
            chatId: chatId,
            InputFile.FromUri(imageUrl),
            cancellationToken: cts
        );
    }
}