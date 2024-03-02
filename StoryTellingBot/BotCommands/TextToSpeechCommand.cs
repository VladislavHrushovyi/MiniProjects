using StoryTellingBot.Repository;
using StoryTellingBot.Repository.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace StoryTellingBot.BotCommands;

public class TextToSpeechCommand(ITelegramBotClient botClient, IChatRepository chatRepository) : ICommand
{
    public async Task Handle(Message message, CancellationToken cts)
    {
        var state = new TextToSpeechState(botClient);

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Очікую на ваший текст =)",
            cancellationToken:cts
        );
    }
}