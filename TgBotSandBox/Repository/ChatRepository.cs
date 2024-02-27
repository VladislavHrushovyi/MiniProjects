using TgBotSandBox.Repository.Models;

namespace TgBotSandBox.Repository;

public class ChatRepository : IChatRepository
{
    private readonly Dictionary<string, ICommandState> _chatStates = new();
    
    public Task InitCommandState(string chatId, ICommandState state)
    {
        if (_chatStates.ContainsKey(chatId))
        {
            _chatStates[chatId] = state;
            return Task.CompletedTask;
        }
        
        _chatStates.Add(chatId, state);
        
        return Task.CompletedTask;
    }

    public Task UpdateCommandState(string chatId, ICommandState state)
    {
        if (!_chatStates.ContainsKey(chatId)) 
            return Task.CompletedTask;
        
        _chatStates[chatId] = state;
        
        return Task.CompletedTask;

    }

    public Task<ICommandState> GetChatStateByChatId(string chatId)
    {
        if (_chatStates.TryGetValue(chatId, out var state))
        {
            return Task.FromResult(state);
        }

        throw new KeyNotFoundException($"ChatId {chatId} does not exist");
    }
}