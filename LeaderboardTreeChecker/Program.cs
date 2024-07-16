using MintForestBase;
using MintForestBase.Models;

Console.WriteLine("Enter auth token");
var authToken = Console.ReadLine();

var httpClients = new HttpClientFactory(authToken);

async Task DoCheckLeaderboard(int page)
{
    
    var mintClient = new MintRequestSender(httpClients.HttpClients[page - 1]);
    var treesByPage = await mintClient.GetTreesByLeaderboardPage(page);
    await Task.Delay(Random.Shared.Next(200, 300));
    var indexClient = page * treesByPage.Result.Count() - treesByPage.Result.Count();
    if (treesByPage.Result.Any())
    {
        IEnumerable<Task> tasks = treesByPage.Result.Select(x => DoClaim(httpClients.HttpClients[indexClient++],x));
        await Task.WhenAll(tasks);
    }
}

async Task DoClaim(HttpClient client, UserLeaderboard user)
{
    var mintRequestSender = new MintRequestSender(client);
    var steelInfo = await mintRequestSender.GetNotClaimedMintTree(user.Id);
    await Task.Delay(Random.Shared.Next(200, 300));
    if (steelInfo.Result.Any())
    {
        var validTree = steelInfo.Result.FirstOrDefault(x => x is { Stealable: true, Amount: >= 1000 });
        if (validTree != default)
        {
            string output = $"https://www.mintchain.io/mint-forest?id={user.TreeId} ---> {validTree.Amount}ME";
            Console.WriteLine(validTree.Amount >= 1000 ? output +  " \t <<--- BINGO" : output);
            if (validTree.Amount >= 3000)
            {
                var steelingResult = await mintRequestSender.SteelTree(user.Id);
                await Task.Delay(Random.Shared.Next(200, 300));
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
        DoCheckLeaderboard(i);
        await Task.Delay(5000);
    }

    await Task.WhenAll(tasks);
}
catch(Exception e)
{
    Console.WriteLine(e);
}

Console.WriteLine("Finish. Press F to close");
Console.ReadKey();