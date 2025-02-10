using MintForestBase;
using MintForestBase.Models;

var configs = new SettingsLoader();

var fileWriter = new FindTreeFileManager("IdsNotClaimedToday.txt");
var httpClients = new HttpClientFactory(configs.GetValue("AuthToken"));
var settings = new SettingsLoader();
var contract = new SmartContractInteraction(settings.GetValue("PrivateKey"));
async Task<List<UserActivityDTO?>> DoFetchPage(int page)
{
    var mintClient = new MintRequestSender(httpClients.GetDefaultHttpClient());
    var treesByPage = await mintClient.GetTreesByLeaderboardPage(page);
    
    if (treesByPage.Result.Any())
    {
        var tasks = treesByPage.Result.Select(x => DoCheckUser(httpClients.GetDefaultHttpClient(),x));
        var results = await Task.WhenAll(tasks);

        return results.Where(x => x != null).ToList();
    }

    return new List<UserActivityDTO>()!;
}

async Task<UserActivityDTO?> DoCheckUser(HttpClient client, UserLeaderboard user)
{
    var mintRequestSender = new MintRequestSender(client);

    var activitiesList = await mintRequestSender.GetUserActivity(user.TreeId);
    
    if (activitiesList.Result.Any())
    {
        var firstDaily = activitiesList.Result.FirstOrDefault(x => x.Type == "daily");
        if (firstDaily != null && firstDaily.ClaimAt.Date != DateTime.Now.Date && firstDaily.Amount > 8000)
        {
            Console.WriteLine($"{user.TreeId} -- {firstDaily.Amount}ME");
            return new UserActivityDTO()
            {
                Id = user.Id.ToString(),
                TreeId = user.TreeId.ToString(),
                Amount = firstDaily.Amount.ToString(),
            };
            //fileWriter.AppendLine($"UserId = {user.Id} -- TreeId = {user.TreeId} -- {firstDaily.Amount}ME \n");
        }
    }

    return null;
}

async Task DoClaim(HttpClient client, string id)
{
    MintRequestSender mintClient = new MintRequestSender(client);

    var idNumber = Int32.Parse(id);
    
    var proofModel = await mintClient.GetProofSteal(idNumber);
    if (proofModel is { Result.Amount: > 55000 })
    {
        Console.WriteLine($"Proof {proofModel.Result.Tx.Substring(0, 30)} {proofModel.Result.Amount}ME");
        var isDone = await contract.StealActionInteraction(proofModel);
        if (isDone)
        {
            Console.WriteLine(proofModel.Result.Amount != 0 ? $"Steel  id {id}: {proofModel.Result.Amount}ME" : "Null");   
        }
    }
}

try
{
    List<Task<List<UserActivityDTO>>> tasks = new List<Task<List<UserActivityDTO>>>();
    
    for (int i = 1; i <= 20; i++)
    {
        Console.WriteLine($"PAGE {i}");
        tasks.Add(DoFetchPage(i)!);
        await Task.Delay(500);
    }

    var users = await Task.WhenAll(tasks);
    var allUsers = users.SelectMany(x => x.Select(user => user))
        .OrderByDescending(x => Int32.Parse(x.Amount))
        .ToList();
    
    await Task.WhenAll(tasks);
    fileWriter.WriteUsers(allUsers);
    
    
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
    
    var steelTasks = new List<Task>();
    foreach (var user in allUsers)
    {
        var task = DoClaim(httpClients.GetDefaultHttpClient(), user.Id);
        steelTasks.Add(task);
        await Task.Delay(1000);
    }
    await Task.WhenAll(steelTasks);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
Console.WriteLine("Finish. Press any key for exit");
Console.ReadKey();

/*
 * 1 read auth token [x]
 * 2 read proxy and create http clients [x]
 * 3 fetch leaderboard users [x]
 * 4 filter by not claim today and after claim more than 10_000 or steeling more 5000ME [x]
 * write user id into file [x]
*/