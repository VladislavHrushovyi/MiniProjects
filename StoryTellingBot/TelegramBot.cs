using StoryTellingBot.BotCommands;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace StoryTellingBot;

public class TelegramBot
{
    private readonly TelegramBotClient _botClient = new(ProjectSettings.SettingsVars["BOT_TOKEN"]);
    private readonly CommandHandler _commandHandler = new ();

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

        var command = _commandHandler.HandleCommand(messageText, _botClient);
        
        await command.Handle(message, cts);
    }
}