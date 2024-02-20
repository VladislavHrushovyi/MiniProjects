using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace StoryTellingBot.BotCommands;

public class NewStoryCommand(TelegramBotClient botClient) : ICommand
{
    private List<string> _answers = new();
    public async Task Handle(Message message, CancellationToken cts)
    {
        var chatId = message.Chat.Id;
        var questions = Enumerable.Range(0, 10).ToArray();
        foreach (var question in questions)
        {
            Message questionMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"{question}: Question{question}",
                cancellationToken: cts);
        }
        
        Message finalMessage = await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: $"Answers: {string.Join(", ", _answers)}",
            cancellationToken: cts);
    }
}