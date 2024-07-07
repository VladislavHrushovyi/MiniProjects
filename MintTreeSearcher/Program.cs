﻿using MintTreeSearcher;

Console.WriteLine("Enter auth token:");
var authToken = Console.ReadLine();

Console.WriteLine("From:");
int from = Int32.Parse(Console.ReadLine());

Console.WriteLine("To:");
int to = Int32.Parse(Console.ReadLine());

Console.WriteLine("Enter more than:");
int moreThan = Int32.Parse(Console.ReadLine());

FindTreeFileManager fileManager = new FindTreeFileManager();
var httpClientsFactory = new HttpClientFactory(authToken);

fileManager.AppendLine($"\t RANGE {from}-{to} \n");
async Task DoSearch(HttpClient client, int treeId)
{
    MintRequestSender mintClient = new MintRequestSender(client);
    var userInfo = await mintClient.GetUserInfo(treeId);
    await Task.Delay(400);
    var claimableInfo = await mintClient.GetNotClaimedMintTree(userInfo.Result.Id);
    await Task.Delay(400);
    if (claimableInfo.Result != null)
    {
        var validObj = claimableInfo.Result.FirstOrDefault(x => x is { Stealable: true, Amount: >= 100 });
        if (validObj is not null)
        {
            string output = $"https://www.mintchain.io/mint-forest?id={treeId} ---> {validObj.Amount}ME";
            Console.WriteLine(validObj.Amount >= moreThan ? output +  " \t <<--- BINGO" : output);
            if (validObj.Amount >= moreThan)
            {
                string line = $"https://www.mintchain.io/mint-forest?id={treeId} ---> {validObj.Amount}ME \n";
                fileManager.AppendLine(line);
            }
        }   
    }
}

async Task DoSearchChunk(HttpClient client, IEnumerable<int> treeIds)
{
    foreach (var treeId in treeIds)
    {
        await DoSearch(client, treeId);
    }
}

int index = 0;
var tasks = Enumerable.Range(from, to - from + 1)
                        .Chunk(httpClientsFactory.HttpClients.Count)
                        .Select(x => DoSearchChunk(httpClientsFactory.HttpClients[index++], x));

await Task.WhenAll(tasks);
Console.WriteLine("FINISH. Press any key to exit...");
Console.ReadKey();
