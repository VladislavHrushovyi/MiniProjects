using StoryTellingBot.GptInterraction;
using Telegram.Bot;
using Telegram.Bot.Types;
using File = System.IO.File;

namespace StoryTellingBot.BotCommands;

public class NewStoryCommand(TelegramBotClient botClient) : ICommand
{
    private readonly GptClient _gptClient = new();
    private List<string> _answers = new();
    public async Task Handle(Message message, CancellationToken cts)
    {
        var chatHistory = new List<object>()
        {
            new
            {
                Role = "user",
                Content = "Напиши 10 простих запитань до розповіді, щоб за відповідями можна було створити розповідь"
            }
        };
        var chatId = message.Chat.Id;
        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Очікуйте, формуються питання",
            cancellationToken: cts);
        var questionString = await _gptClient.GetQuestionFromGpt(chatHistory);
        chatHistory.Add(new{Role="assistant", Content=questionString});
        
        var questions = questionString.Split("\n");
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

        var answersString = string.Join(", ", _answers);
        Message finalMessage = await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: $"Відповіді: {answersString} \n Генерується розповідь",
            cancellationToken: cts);
        chatHistory.Add(new
        {
            Role="user",
            Content=answersString+". Базуючись на цих відповідях, склади розповідь. не більше 1200 символів"
        });

        var story = await _gptClient.GetQuestionFromGpt(chatHistory);
        
        Message storyMessage = await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: $"Генерується аудиозапис. \n {story}",
            cancellationToken: cts);
        var mp3Name = await _gptClient.GptTextToSpeech(story);
        await using var streamMp3 = File.OpenRead($"./{mp3Name}");
            
            
        var audioMessage = await botClient.SendAudioAsync(
            chatId: chatId,
            audio: InputFile.FromStream(streamMp3), cancellationToken: cts);
    }
}