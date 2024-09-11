using MintForestBase;
using MintForestBase.Models;

var configs = new SettingsLoader();

var fileWriter = new FindTreeFileManager("IdsNotClaimedToday.txt");
var httpClients = new HttpClientFactory(configs.GetValue("AuthToken"));

async Task DoFetchPage(int page)
{
    var mintClient = new MintRequestSender(httpClients.GetDefaultHttpClient());
    var treesByPage = await mintClient.GetTreesByLeaderboardPage(page);
    
    if (treesByPage.Result.Any())
    {
        IEnumerable<Task> tasks = treesByPage.Result.Select(x => DoCheckUser(httpClients.GetDefaultHttpClient(),x));
        await Task.WhenAll(tasks);
    }
}

async Task DoCheckUser(HttpClient client, UserLeaderboard user)
{
    var mintRequestSender = new MintRequestSender(client);

    var activitiesList = await mintRequestSender.GetUserActivity(user.TreeId);
    
    if (activitiesList.Result.Any())
    {
        var firstDaily = activitiesList.Result.FirstOrDefault(x => x.Type == "daily");
        if (firstDaily != default && firstDaily.ClaimAt.Date != DateTime.Now.Date && firstDaily.Amount > 8000)
        {
            Console.WriteLine($"{user.TreeId} -- {firstDaily.Amount}ME");
            fileWriter.AppendLine($"UserId = {user.Id} -- TreeId = {user.TreeId} -- {firstDaily.Amount}ME \n");
        }
    }
}

try
{
    List<Task> tasks = new List<Task>();
    
    for (int i = 1; i <= 20; i++)
    {
        Console.WriteLine($"PAGE {i}");
        tasks.Add(DoFetchPage(i));
        await Task.Delay(500);
    }

    await Task.WhenAll(tasks);
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