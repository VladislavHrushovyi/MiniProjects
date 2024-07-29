using MintForestBase;

Console.WriteLine("Enter auth token");
var authToken = Console.ReadLine();

var idsFromFile = File.ReadAllLines("./Ids.txt");

HttpClientFactory httpClientFactory = new HttpClientFactory(authToken);

async Task DoClaim(HttpClient client, string id)
{
    MintRequestSender mintClient = new MintRequestSender(client);
    
    var idNumber = Int32.Parse(id);
    var userInfo = await mintClient.GetUserInfo(idNumber);
    await Task.Delay(150);
    
    var steelInfo = await mintClient.GetNotClaimedMintTree(userInfo.Result.Id);
    
    var validTree = steelInfo.Result.FirstOrDefault(x => x is { Stealable: true, Amount: >= 3000 });
    if (validTree != default)
    {
        var result = await mintClient.SteelTree(userInfo.Result.Id);
        Console.WriteLine(result.SteelInfo.Amount != 0 ? $"Steel  id {id}: {result.SteelInfo.Amount}ME" : "Null");
    }
    else
    {
        Console.WriteLine($"Not stolen {id}");
    }
    
    await Task.Delay(1500);
}

try
{
    List<Task> tasks = new List<Task>();
    int skip = 0;
    
    foreach (var id in idsFromFile)
    {
        var task = DoClaim(httpClientFactory.HttpClients[skip++], id);
        tasks.Add(task);
        skip++;
    }

    await Task.WhenAll(tasks);
}
catch (Exception e)
{
    Console.WriteLine(e);
}
Console.WriteLine("Finish press F to close");
Console.ReadKey();