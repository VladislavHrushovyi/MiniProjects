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

    public Task StartBot()
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

    private Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cts)
    {
        // _botClient.SendTextMessageAsync(
        //     update.Message.Chat.Id,
        //     "Кінчились гроші, просіть розробника шоб він закинув грошей на іскуствєнні інтілєкти",
        //     cancellationToken:cts
        // );
        // return Task.CompletedTask;
        if (update.Message is not { } message)
            return Task.CompletedTask;
        if (message.Text is not { } messageText)
            return Task.CompletedTask;
        Console.WriteLine($"User: message={update.Message.Text} chatId={update.Message.Chat.Id} user={update.Message.Chat.Username}");
        var command = CommandFactory.GetCommandHandler(messageText, _botClient);
        
        try
        {
            command.Handle(message, cts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _botClient.SendTextMessageAsync(
                message.Chat.Id,
                e.Message,
                cancellationToken:cts
            );
        }
        return Task.CompletedTask;
    }
}