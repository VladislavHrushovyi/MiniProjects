using Telegram.Bot.Types;

namespace StoryTellingBot.BotCommands;

public interface ICommand
{
    public Task Handle(Message message, CancellationToken cts);
}