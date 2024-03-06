namespace TgBotSandBox.SenderRequest;

public class FetchingSomeData
{
    public async Task FetchSomeData()
    {
        await Task.Delay(50);
        if (Random.Shared.Next(3) == 2)
        {
            throw new Exception("Деякий чутливий контент");
        }
        
    }
}