using StoryTellingBot.GptInteraction;
using Telegram.Bot;
using Telegram.Bot.Types;
using File = System.IO.File;

namespace StoryTellingBot.Repository.Models;

public class NewStoryState(ITelegramBotClient botClient, IEnumerable<string> questions) : ICommandState
{
    private readonly GptClient _gptClient = new();
    private readonly Dictionary<string, string> _questionAnswersPairs = questions.ToDictionary(x => x, _ => string.Empty);
    
    public async Task Handle(Message message, CancellationToken cts)
    {
        if (_questionAnswersPairs.All(x => x.Value != string.Empty)) return;
        var chatId = message.Chat.Id;
        var nonAnsweredQuestion = _questionAnswersPairs
            .Where(x => x.Value == String.Empty)
            .ToDictionary();
        if (nonAnsweredQuestion.Count != 0)
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
        var answers = _questionAnswersPairs.Select(x => x.Value);
        await botClient.SendTextMessageAsync(
            chatId,
            string.Join(", ", answers),
            cancellationToken:cts);
        await botClient.SendTextMessageAsync(
            chatId,
            "Генерується розповідь",
            cancellationToken:cts);

        var storyChat = CreateStoryChat();
        var storyText = await _gptClient.AskInGpt(storyChat);
        
        await botClient.SendTextMessageAsync(
            chatId,
            storyText[..50] + "...",
            cancellationToken:cts);
        await botClient.SendTextMessageAsync(
            chatId,
            "Генерується аудіо файл",
            cancellationToken:cts);

        var mp3Name = await _gptClient.GptTextToSpeech(message.Chat.Id.ToString(),storyText);
        await using var streamMp3 = File.OpenRead($"./{mp3Name}");
        await botClient.SendAudioAsync(
            chatId,
            audio: InputFile.FromStream(streamMp3),
            cancellationToken:cts);
    }

    private List<ChatItem> CreateStoryChat()
    {
        var result = new List<ChatItem>
        {
            new(
                "user",
                "Напиши 10 простих запитань до розповіді, щоб за відповідями можна було створити розповідь. Питання можуть містити шутливу форму"
            ),
            new(
                "assistant",
                string.Join("\n", _questionAnswersPairs.Select(x => x.Key))
            ),
            new(
                "user",
                "Базуючись на цих відповідях в шутливій формі напиши розповідь\n"+string.Join("\n", _questionAnswersPairs.Select(x => x.Value))
            )
        };
        return result;
    }
}

public record ChatItem(string Role, string Content);