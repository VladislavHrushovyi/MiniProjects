﻿using System.Text;
using MintForestBase;
using MintForestBase.Models;

Console.WriteLine("Enter auth token");
var authToken = Console.ReadLine();
FileStream writer;
if (File.Exists("IdsNotToday.txt"))
{
    File.Delete("IdsNotToday.txt");
    writer = File.OpenWrite("IdsNotToday.txt");
}
else
{
    writer = File.OpenWrite("IdsNotToday.txt");
}
var httpClients = new HttpClientFactory(authToken);

async Task DoFetchPage(int page)
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
        IEnumerable<Task> tasks = treesByPage.Result.Select(x => DoCheckUser(clientsChunked,x));
        await Task.WhenAll(tasks);
    }
}

async Task DoCheckUser(HttpClient[] clients, UserLeaderboard user)
{
    int clientIndex = 0;
    var mintRequestSender = new MintRequestSender(clients[clientIndex]);

    var activitiesList = await mintRequestSender.GetUserActivity(user.TreeId);
    
    if (activitiesList.Result.Any())
    {
        var firstDaily = activitiesList.Result.FirstOrDefault(x => x.Type == "daily");
        if (firstDaily != default && firstDaily.ClaimAt.Date != DateTime.Now.Date && firstDaily.Amount > 8000)
        {
            //File.AppendAllText("./Ids.txt", $"{user.TreeId} -- {firstDaily.Amount} \n");
            Console.WriteLine($"{user.TreeId} -- {firstDaily.Amount}");
            await writer.WriteAsync(Encoding.UTF8.GetBytes($"{user.TreeId} -- {firstDaily.Amount}ME \n"));
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
        await Task.Delay(1000);
    }

    await Task.WhenAll(tasks);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
writer.Close();
Console.WriteLine("Finish. Press any key for exit");
Console.ReadKey();

/*
 * 1 read auth token [x]
 * 2 read proxy and create http clients [x]
 * 3 fetch leaderboard users [x]
 * 4 filter by not claim today and after claim more than 10_000 or steeling more 5000ME [x]
 * write user id into file [x]
*/