using StoryTellingBot.GptInteraction;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace StoryTellingBot.Repository.Models;

public class CreateImageState(ITelegramBotClient botClient) : ICommandState
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

        try
        {
            var imageUrl = await _gptClient.CreateImage(message.Text);
            await botClient.SendPhotoAsync(
                chatId: chatId,
                InputFile.FromUri(imageUrl),
                cancellationToken: cts
            );
        }
        catch (Exception e)
        {
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: e.Message,
                cancellationToken: cts
            );
        }
    }
}