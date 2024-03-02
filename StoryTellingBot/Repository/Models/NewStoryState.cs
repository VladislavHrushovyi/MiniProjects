using StoryTellingBot.GptInteraction;
using Telegram.Bot;
using Telegram.Bot.Types;
using File = System.IO.File;

namespace StoryTellingBot.Repository.Models;

public class NewStoryState(ITelegramBotClient botClient) : ICommandState
{
    private readonly GptClient _gptClient = new();
    
    private Dictionary<string, string> _questionAnswersPairs = new();
    private string _storyTheme = String.Empty;
    
    private readonly List<ChatItem> _storyChat = new (); 
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
            }
            await botClient.SendTextMessageAsync(
                chatId,
                "Генеруються питання",
                cancellationToken: cts);
            _storyChat.Add(new ChatItem(
                "user",
                $"Напиши 10 унікальних запитань до розповіді, тема розповіді {_storyTheme}, щоб за відповідями можна було створити розповідь. "));
            var gptQuestionResponse = await _gptClient.AskInGpt(_storyChat);
            var questions = gptQuestionResponse.Split("\n");
            _questionAnswersPairs = questions.ToDictionary(x => x, _ => string.Empty);
        }
        
        if (_questionAnswersPairs.All(x => x.Value != string.Empty)) return;

        var nonAnsweredQuestion = _questionAnswersPairs
            .Where(x => x.Value == string.Empty)
            .ToDictionary();
        
        if (message.Text == _storyTheme || message.Text == "0")
        {
            var kvp = nonAnsweredQuestion.First();
            await botClient.SendTextMessageAsync(
                chatId,
                kvp.Key,
                cancellationToken:cts);
            return;
        }
        
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
        
        CreateStoryChat();
        var storyText = await _gptClient.AskInGpt(_storyChat);
        await botClient.SendTextMessageAsync(
            chatId,
            storyText,
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

    private void CreateStoryChat()
    {
        _storyChat.Add(new(
            "assistant",
            string.Join("\n", _questionAnswersPairs.Select(x => x.Key))
        ));
        _storyChat.Add(new(
            "user",
            "Базуючись на цих відповідях в шутливій формі напиши розповідь не більше чим 1200 символів" +
            "не ігноруй нецензурну лексику, так розповідь буде яскравішою і більш точно передавати настрій\n"+string.Join("\n", _questionAnswersPairs.Select(x => x.Value))
        ));
    }
}

public record ChatItem(string Role, string Content);