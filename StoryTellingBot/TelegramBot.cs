using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace StoryTellingBot;

public class TelegramBot
{
    public readonly TelegramBotClient _botClient = new(ProjectSettings.SettingsVars["BOT_TOKEN"]);

    public async Task StartBot()
    {
        using var cts = new CancellationTokenSource();

        ReceiverOptions opt = new ReceiverOptions()
        {
            AllowedUpdates = Array.Empty<UpdateType>(),
        };
        
        _botClient.StartReceiving(
            HandleUpdateAsync,
            HandlePollingErrorAsync,
            opt,
            cts.Token
            );
    }

    private static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cts)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(errorMessage);
        return Task.CompletedTask;
    }

    private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cts)
    {
        if (update.Message is not { } message)
            return;
        if (message.Text is not { } messageText)
            return;

        var chatId = message.Chat.Id;

        Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

        // Echo received message text
        Message sentMessage = await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "You said:\n" + messageText,
            cancellationToken: cts);
    }
}