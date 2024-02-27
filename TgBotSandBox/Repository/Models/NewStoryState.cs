using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBotSandBox.Repository.Models;

public class NewStoryState(ITelegramBotClient botClient, IEnumerable<string> questions) : ICommandState
{
    private readonly Dictionary<string, string> _questionAnswersPairs = questions.ToDictionary(x => x, _ => string.Empty);
    public async Task Handle(Message message, CancellationToken cts)
    {
        if (_questionAnswersPairs.All(x => x.Value != string.Empty)) return;
        var chatId = message.Chat.Id;
        var nonAnsweredQuestion = _questionAnswersPairs
                                                    .Where(x => x.Value == String.Empty)
                                                    .ToDictionary();
        if (nonAnsweredQuestion.Any())
        {
            var kvp = nonAnsweredQuestion.First();
            _questionAnswersPairs[kvp.Key] = message.Text;

            if (_questionAnswersPairs.Any(x => x.Value == string.Empty))
            {
                await botClient.SendTextMessageAsync(
                    chatId,
                    _questionAnswersPairs.First(x => x.Value == string.Empty).Key,
                    cancellationToken:cts);
            
                return;
            }
        }
        
        await botClient.SendTextMessageAsync(
            chatId,
            "Генерується розповідь",
            cancellationToken:cts);

        await Task.Delay(10000, cts);
        var answers = _questionAnswersPairs.Select(x => x.Value);
        await botClient.SendTextMessageAsync(
            chatId,
            string.Join(", ", answers),
            cancellationToken:cts);
        
        await botClient.SendTextMessageAsync(
            chatId,
            "Генерується аудіо файл",
            cancellationToken:cts);

        await Task.Delay(7000, cts);
        await botClient.SendTextMessageAsync(
            chatId,
            "Згенерований .mp3",
            cancellationToken:cts);
    }
}