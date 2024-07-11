using MintTreeSearcher;

Console.WriteLine("Enter auth token");
var authToken = Console.ReadLine();

var idsFromFile = File.ReadAllLines("./Ids.txt");

HttpClientFactory httpClientFactory = new HttpClientFactory(authToken);

async Task DoClaim(HttpClient client, string id)
{
    MintRequestSender mintClient = new MintRequestSender(client);
    var idNumber = Int32.Parse(id);
    var userInfo = await mintClient.GetUserInfo(idNumber);
    await Task.Delay(400);
    var steelInfo = await mintClient.GetNotClaimedMintTree(userInfo.Result.Id);
    await Task.Delay(Random.Shared.Next(1000, 3000));
    var validTree = steelInfo.Result.FirstOrDefault(x => x is { Stealable: true, Amount: >= 1500 });
    if (validTree != default)
    {
        var result = await mintClient.SteelTree(userInfo.Result.Id);
        Console.WriteLine(id);
        if (result.SteelInfo.Amount != 0)
        {
            Console.WriteLine($"Steel  id {id}: {result.SteelInfo.Amount}ME");
        }
        else
        {
            Console.WriteLine("Null");
        }
    }
    else
    {
        Console.WriteLine($"Not stolen {id}");
    }
    
    await Task.Delay(3500);
}

try
{
    foreach (var idChunk in idsFromFile.Chunk(httpClientFactory.HttpClients.Count))
    {
        int index = 0;
        IEnumerable<Task> tasks = idChunk.Select(x =>
        {
            Console.WriteLine(index);
            return DoClaim(httpClientFactory.HttpClients[index++], x);
        });
        await Task.WhenAll(tasks);
        index = 0;
    }
}
catch (Exception e)
{
    Console.WriteLine(e);
}
Console.WriteLine("Finish press F to close");
Console.ReadKey();