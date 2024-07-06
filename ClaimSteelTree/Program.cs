using MintTreeSearcher;

Console.WriteLine("Enter auth token");
var authToken = Console.ReadLine();

var idsFromFile = File.ReadAllLines("./Ids.txt");

MintRequestSender mintClient = new MintRequestSender(authToken);

void DoClaim(string id)
{
    var idNumber = Int32.Parse(id);
    var userInfo = mintClient.GetUserInfo(idNumber).GetAwaiter().GetResult();
    var steelInfo = mintClient.GetNotClaimedMintTree(userInfo.Result.Id).GetAwaiter().GetResult();
    var validTree = steelInfo.Result.FirstOrDefault(x => x is { Stealable: true, Amount: >= 1000 });
    if (validTree != default)
    {
        var result = mintClient.SteelTree(userInfo.Result.Id).GetAwaiter().GetResult();
        Console.WriteLine($"Steel  id {id}: {result.SreelInfo.Amount}ME");
    }
    else
    {
        Console.WriteLine($"Not stolen {id}");
    }
}


Parallel.ForEach(idsFromFile, new ParallelOptions() { MaxDegreeOfParallelism = 5 }, DoClaim);
Console.WriteLine("Finish press F to close");
Console.ReadKey();