using Telegram.Bot.Types;

namespace StoryTellingBot.Repository.Models;

public interface ICommandState
{
    Task Handle(Message message, CancellationToken cts);
}