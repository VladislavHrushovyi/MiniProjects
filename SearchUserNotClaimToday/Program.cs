using MintForestBase;
using MintForestBase.Models;

var configs = new SettingsLoader();

var fileWriter = new FindTreeFileManager("IdsNotClaimedToday.txt");
var httpClients = new HttpClientFactory(configs.GetValue("AuthToken"));

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

    fileWriter.WriteUsers(allUsers);
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