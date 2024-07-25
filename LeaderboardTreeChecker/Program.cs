using MintForestBase;
using MintForestBase.Models;

Console.WriteLine("Enter auth token");
var authToken = Console.ReadLine();

var httpClients = new HttpClientFactory(authToken);

async Task DoCheckLeaderboard(int page)
{
    
    var mintClient = new MintRequestSender(httpClients.HttpClients[^page]);
    var treesByPage = await mintClient.GetTreesByLeaderboardPage(page);
    await Task.Delay(Random.Shared.Next(100, 200));
    var clientsChunked = httpClients.HttpClients.Skip((page - 1) * 50)
        .Take(50)
        .ToArray();
    
    var indexClient = 0;
    if (treesByPage.Result.Any())
    {
        IEnumerable<Task> tasks = treesByPage.Result.Select(x => DoClaim(clientsChunked,x));
        await Task.WhenAll(tasks);
    }
}

async Task DoClaim(HttpClient[] clients, UserLeaderboard user)
{
    int clientIndex = 0;
    var mintRequestSender = new MintRequestSender(clients[clientIndex]);
    var steelInfo = await mintRequestSender.GetNotClaimedMintTree(user.Id);
    //clientIndex += 1;
    
    await Task.Delay(Random.Shared.Next(100, 200));
    if (steelInfo.Result.Any())
    {
        var validTree = steelInfo.Result.FirstOrDefault(x => x is { Stealable: true, Amount: >= 1000 });
        if (validTree != default)
        {
            string output = $"https://www.mintchain.io/mint-forest?id={user.TreeId} ---> {validTree.Amount}ME";
            Console.WriteLine(validTree.Amount >= 1000 ? output +  " \t <<--- BINGO" : output);
            if (validTree.Amount >= 3000)
            {
                //mintRequestSender.ChangeHttpClient(clients[clientIndex]);
                var steelingResult = await mintRequestSender.SteelTree(user.Id);
                //await Task.Delay(Random.Shared.Next(200, 300));
                Console.WriteLine($"STEELING treeId {user.TreeId} Amount {steelingResult.SteelInfo.Amount}");
            }
        }
    }
}

try
{
    List<Task> tasks = new List<Task>();
    
    for (int i = 1; i <= 20; i++)
    {
        Console.WriteLine($"PAGE {i}");
        tasks.Add(DoCheckLeaderboard(i));
        await Task.Delay(1000);
    }

    await Task.WhenAll(tasks);
}
catch(Exception e)
{
    Console.WriteLine(e);
}

Console.WriteLine("Finish. Press F to close");
Console.ReadKey();