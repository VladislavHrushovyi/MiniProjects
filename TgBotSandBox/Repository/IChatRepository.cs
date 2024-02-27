using TgBotSandBox.Repository.Models;

namespace TgBotSandBox.Repository;

public interface IChatRepository
{
    Task InitCommandState(string chatId, ICommandState state);
    Task UpdateCommandState(string chatId, ICommandState state);
    Task<ICommandState>? GetChatStateByChatId(string chatId);
}