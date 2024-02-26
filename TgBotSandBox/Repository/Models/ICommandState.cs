namespace TgBotSandBox.Repository.Models;

public interface ICommandState
{
    Task Handle(int charId, string message);
}