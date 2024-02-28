using StoryTellingBot.Repository;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace StoryTellingBot.BotCommands;

public class TextMessageCommand(IChatRepository chatRepository) : ICommand
{
    public async Task Handle(Message message, CancellationToken cts)
    {
        var chatState = await chatRepository.GetChatStateByChatId(message.Chat.Id.ToString())!;
        await chatState.Handle(message, cts);
    }
}