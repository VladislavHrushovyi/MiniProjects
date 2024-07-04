﻿using MintTreeSearcher;

Console.WriteLine("Enter auth token:");
var authToken = Console.ReadLine();

Console.WriteLine("From:");
int from = Int32.Parse(Console.ReadLine());

Console.WriteLine("To:");
int to = Int32.Parse(Console.ReadLine());

Console.WriteLine("Enter more than:");
int moreThan = Int32.Parse(Console.ReadLine());

MintRequestSender mintClient = new MintRequestSender(authToken);
FindTreeFileManager fileManager = new FindTreeFileManager();

fileManager.AppendLine($"\t RANGE {from}-{to} \n");
//var userIds = Enumerable.Range(from, to - from).Chunk(25);
//Console.WriteLine(userIds.Last().Last());
void DoSearch(int id)
{
    var claimableInfo = mintClient.GetNotClaimedMintTree(id).GetAwaiter().GetResult();
    //var claimableInfo = new Response() { Result = Enumerable.Range(1, 2).Select(x => new ItemsTree(){Stealable = true, Amount = Random.Shared.Next(100,2000)}) };
    if (claimableInfo.Result != null)
    {
        var validObj = claimableInfo.Result.FirstOrDefault(x => x is { Stealable: true, Amount: >= 100 });
        if (validObj is not null)
        {
            string output = $"https://www.mintchain.io/mint-forest?id={id} ---> {validObj.Amount}ME";
            Console.WriteLine(validObj.Amount >= moreThan ? output +  " \t <<--- BINGO" : output);
            if (validObj.Amount >= moreThan)
            {
                string line = $"https://www.mintchain.io/mint-forest?id={id} ---> {validObj.Amount}ME \n";
                fileManager.AppendLine(line);
            }
        }   
    }
}

Parallel.For(from, to, new ParallelOptions() { MaxDegreeOfParallelism = 25 }, i => DoSearch(i));
Console.WriteLine("FINISH. Press any key to exit...");
Console.ReadKey();
