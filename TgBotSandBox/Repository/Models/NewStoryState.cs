namespace TgBotSandBox.Repository.Models;

public class NewStoryState : ICommandState
{
    public Task Handle(int charId, string message)
    {
        //update state of creation story, save progress and collect answers
        throw new NotImplementedException();
    }
} 