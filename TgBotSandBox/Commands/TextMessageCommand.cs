using StoryTellingBot.BotCommands;
using Telegram.Bot.Types;
using TgBotSandBox.Repository;

namespace TgBotSandBox.Commands;

public class TextMessageCommand(IChatRepository chatRepository) : ICommand
{
    public async Task Handle(Message message, CancellationToken cts)
    {
        var chatState = await chatRepository.GetChatStateByChatId(message.Chat.Id.ToString())!;
        if (chatState is null)
        {
            return;
        }
        await chatState.Handle(message, cts);
    }
}