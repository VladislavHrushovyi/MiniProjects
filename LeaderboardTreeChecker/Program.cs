using MintForestBase;
using MintForestBase.Models;

var configs = new SettingsLoader();
var contractInteraction = new SmartContractInteraction(configs.GetValue("PrivateKey"));
var httpClients = new HttpClientFactory(configs.GetValue("AuthToken"));

async Task DoCheckLeaderboard(int page)
{
    
    var mintClient = new MintRequestSender(httpClients.GetDefaultHttpClient());
    var treesByPage = await mintClient.GetTreesByLeaderboardPage(page);
    
    if (treesByPage.Result.Any())
    {
        IEnumerable<Task> tasks = treesByPage.Result.Select(x => DoClaim(httpClients.GetDefaultHttpClient(),x));
        await Task.WhenAll(tasks);
    }
}

async Task DoClaim(HttpClient client, UserLeaderboard user)
{
    await Task.Delay(Random.Shared.Next(100, 200));
    var mintRequestSender = new MintRequestSender(client);
    var steelInfo = await mintRequestSender.GetNotClaimedMintTree(user.Id);
    
    if (steelInfo.Result.Any())
    {
        var validTree = steelInfo.Result.FirstOrDefault(x => x is { Stealable: true, Amount: >= 40000 });
        if (validTree != default)
        {
            string output = $"https://www.mintchain.io/mint-forest?id={user.TreeId} ---> {validTree.Amount}ME";
            Console.WriteLine(validTree.Amount >= 1000 ? output +  " \t <<--- BINGO" : output);
            if (validTree.Amount >= 40000)
            {
                var proofModel = await mintRequestSender.GetProofSteal(user.Id);
                if (proofModel is {Result.Amount: > 80000})
                {
                    var isDone = await contractInteraction.StealActionInteraction(proofModel);
                    if (isDone)
                    {
                        Console.WriteLine($"STEELING treeId {user.TreeId} Amount {proofModel.Result.Amount}");   
                    }
                }
            }
        }
    }
}

try
{
    var dateTimeNow = DateTime.UtcNow;
    var deadLineTime = DateTime.Parse($"{dateTimeNow.Year}-{dateTimeNow.Month}-{dateTimeNow.Day} {14}:{00}:{00}")
        .AddMilliseconds(-200)
        .ToUniversalTime();
    while (DateTime.UtcNow < deadLineTime)
    {
        Console.Clear();
        Console.WriteLine(DateTime.UtcNow.ToString("HH:mm:ss") + $" Target {deadLineTime:HH:mm:ss}");
        await Task.Delay(10);
    }
    List<Task> tasks = new List<Task>();
    
    for (int i = 1; i <= 20; i++)
    {
        Console.WriteLine($"PAGE {i}");
        tasks.Add(DoCheckLeaderboard(i));
        await Task.Delay(100);
    }

    await Task.WhenAll(tasks);
}
catch(Exception e)
{
    Console.WriteLine(e);
}

Console.WriteLine("Finish. Press F to close");
Console.ReadKey();