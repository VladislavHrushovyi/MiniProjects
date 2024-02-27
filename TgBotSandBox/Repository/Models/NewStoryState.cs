using Telegram.Bot;

namespace TgBotSandBox.Repository.Models;

public class NewStoryState(ITelegramBotClient botClient, IEnumerable<string> questions) : ICommandState
{
    private readonly Dictionary<string, string> _questionAnswersPairs = questions.ToDictionary(x => x, _ => string.Empty);
    public Task Handle(int charId, string message)
    {
        //update state of creation story, save progress and collect answers
        throw new NotImplementedException();
    }
}

public record ChatItem(string Role, string Content);