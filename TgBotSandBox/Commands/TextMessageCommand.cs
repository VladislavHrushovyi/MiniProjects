using StoryTellingBot.BotCommands;
using Telegram.Bot.Types;
using TgBotSandBox.Repository;

namespace TgBotSandBox.Commands;

public class TextMessageCommand(IChatRepository chatRepository) : ICommand
{
    public Task Handle(Message message, CancellationToken cts)
    {
        //get chat state by chatId
        // call state handle
        
        return Task.CompletedTask;
    }
}