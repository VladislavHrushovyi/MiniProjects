using Telegram.Bot.Types;

namespace TgBotSandBox.Repository.Models;

public interface ICommandState
{
    Task Handle(Message message, CancellationToken cts);
}