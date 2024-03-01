using Telegram.Bot;
using Telegram.Bot.Types;
using TgBotSandBox.SenderRequest;

namespace TgBotSandBox.Repository.Models;

public class NewStoryState(ITelegramBotClient botClient) : ICommandState
{
    private string _storyTheme = string.Empty;
    private readonly FetchingSomeData _fetchingSomeData = new();
    private Dictionary<string, string> _questionAnswersPairs = new();
    public async Task Handle(Message message, CancellationToken cts)
    {
        var chatId = message.Chat.Id;

        if (string.IsNullOrEmpty(_storyTheme))
        {
            if (message.Text == "0")
            {
                _storyTheme = "випадкова";
            }
            else
            {
                _storyTheme = message.Text;
                await botClient.SendTextMessageAsync(
                    chatId,
                    "Генеруються питання",
                    cancellationToken:cts);
                await _fetchingSomeData.FetchSomeData();
                var question = Enumerable.Range(0, 10).Select(i => $"Question{i}");
                _questionAnswersPairs = question.ToDictionary(x => x, _ => string.Empty);
            }
        }
        if (_questionAnswersPairs.All(x => x.Value != string.Empty)) return;
        
        var nonAnsweredQuestion = _questionAnswersPairs
                                                    .Where(x => x.Value == String.Empty)
                                                    .ToDictionary();
        if (message.Text == _storyTheme)
        {
            var kvp = nonAnsweredQuestion.First();
            await botClient.SendTextMessageAsync(
                chatId,
                kvp.Key,
                cancellationToken:cts);
            return;
        }
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

        await _fetchingSomeData.FetchSomeData();
        var answers = _questionAnswersPairs.Select(x => x.Value);
        await botClient.SendTextMessageAsync(
                chatId, 
            $"Тема {_storyTheme}\n" + string.Join(", ", answers),
            cancellationToken:cts);
        
        await botClient.SendTextMessageAsync(
            chatId,
            "Генерується аудіо файл",
            cancellationToken:cts);

        await _fetchingSomeData.FetchSomeData();
        await botClient.SendTextMessageAsync(
            chatId,
            "Згенерований .mp3",
            cancellationToken:cts);
    }
}