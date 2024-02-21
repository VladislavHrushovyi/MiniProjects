using StoryTellingBot.GptInterraction;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace StoryTellingBot.BotCommands;

public class NewStoryCommand(TelegramBotClient botClient) : ICommand
{
    private readonly GptClient _gptClient = new();
    private List<string> _answers = new();
    public async Task Handle(Message message, CancellationToken cts)
    {
        var chatId = message.Chat.Id;
        var answersString = await _gptClient.GetQuestionFromGpt();
        var questions = answersString.Split("\n");
        var prevMessage = message.Text;
        foreach (var question in questions)
        {
            Message questionMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: question,
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