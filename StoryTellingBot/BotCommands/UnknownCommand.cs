using Telegram.Bot;
using Telegram.Bot.Types;

namespace StoryTellingBot.BotCommands;

public class UnknownCommand(TelegramBotClient botClient) : ICommand
{
    public async Task Handle(Message message, CancellationToken cts)
    {
        var chatId = message.Chat.Id;

        Console.WriteLine($"Received a '{message.Text}' message in chat {chatId}.");

        Message sentMessage = await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Unknown command:\n" + message.Text,
            cancellationToken: cts);
    }
}