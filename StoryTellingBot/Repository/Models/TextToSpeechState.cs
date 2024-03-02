using StoryTellingBot.GptInteraction;
using Telegram.Bot;
using Telegram.Bot.Types;
using File = System.IO.File;

namespace StoryTellingBot.Repository.Models;

public class TextToSpeechState(ITelegramBotClient botClient) : ICommandState
{
    private readonly GptClient _gptClient = new();
    public async Task Handle(Message message, CancellationToken cts)
    {
        if (string.IsNullOrEmpty(message.Text))
        {
            return;
        }
        
        var chatId = message.Chat.Id;
        var text = message.Text;
        
        if (text.Length > 1500)
        {
            text = message.Text[..1500];
            await botClient.SendTextMessageAsync(
                    chatId:chatId,
                    text: "Максимальна кількість символів не більше 1500. Ваший текст було обрізано.",
                    cancellationToken: cts
                );
        }
        
        var mp3FileName = await _gptClient.GptTextToSpeech(chatId.ToString(), text);
        
        await using var streamMp3 = File.OpenRead($"./{mp3FileName}");
        await botClient.SendAudioAsync(
            chatId,
            audio: InputFile.FromStream(streamMp3),
            cancellationToken:cts);
    }
}