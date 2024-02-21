using Telegram.Bot;
using Telegram.Bot.Polling;
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
        var questions = Enumerable.Range(0, 10).ToArray(); // get this question from gpt
        var prevMessage = message.Text;
        foreach (var question in questions)
        {
            Message questionMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"{question}: Question{question}",
                cancellationToken: cts);
            while (true)
            {
                var newUpdates = await botClient.GetUpdatesAsync(offset:-1, cancellationToken: cts);
                var currMessage = newUpdates[^1].Message.Text;
                if (prevMessage != currMessage)
                {
                    _answers.Add(currMessage);
                    prevMessage = currMessage;
                    break;
                }
                await Task.Delay(500, cts).ConfigureAwait(false);
            }
        }
        Message finalMessage = await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: $"Answers: {string.Join(", ", _answers)}",
            cancellationToken: cts);
        
        //next step get text base on this answers
        //and convert text to audio
    }
}