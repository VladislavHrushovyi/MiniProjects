using MintTreeSearcher;

Console.WriteLine("Enter auth token");
var authToken = Console.ReadLine();

var idsFromFile = File.ReadAllLines("./Ids.txt");

MintRequestSender mintClient = new MintRequestSender(authToken);

async Task DoClaim(string id)
{
    var idNumber = Int32.Parse(id);
    var userInfo = await mintClient.GetUserInfo(idNumber);
    var steelInfo = await mintClient.GetNotClaimedMintTree(userInfo.Result.Id);
    var validTree = steelInfo.Result.FirstOrDefault(x => x is { Stealable: true, Amount: >= 1000 });
    if (validTree != default)
    {
        var result = await mintClient.SteelTree(userInfo.Result.Id);
        Console.WriteLine($"Steel  id {id}: {result.SreelInfo.Amount}ME");
    }
}

async void Body(string id) => await DoClaim(id);

Parallel.ForEach(idsFromFile, new ParallelOptions() { MaxDegreeOfParallelism = 10 }, Body);
