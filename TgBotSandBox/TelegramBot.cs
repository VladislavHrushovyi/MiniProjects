using StoryTellingBot;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TgBotSandBox.Commands;

namespace TgBotSandBox;

public class TelegramBot
{
    private readonly TelegramBotClient _botClient = new(ProjectSettings.SettingsVars["BOT_TOKEN"]);

    public Task StartBot()
    {
        using var cts = new CancellationTokenSource();

        ReceiverOptions opt = new ReceiverOptions()
        {
            //AllowedUpdates = Array.Empty<UpdateType>(),
            AllowedUpdates = new [] { UpdateType.Message }
        };
        
        _botClient.StartReceiving(
            HandleUpdateAsync,
            HandlePollingErrorAsync,
            opt,
            cts.Token
        );

        return Task.CompletedTask;
    }
    private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cts)
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

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cts)
    {
        if (update.Message is not { } message)
            return;
        if (message.Text is not { } messageText)
            return;
        Console.WriteLine($"User: message={update.Message.Text} chatId={update.Message.Chat.Id} user={update.Message.Chat.Username}");
        var command = CommandFactory.HandleCommand(messageText, _botClient);
        
        await command.Handle(message, cts);
    }
}