using MintTreeSearcher;

Console.WriteLine("Enter auth token");
var authToken = Console.ReadLine();

var idsFromFile = File.ReadAllLines("./Ids.txt");

MintRequestSender mintClient = new MintRequestSender(new HttpClient());

async Task DoClaim(string id)
{
    var idNumber = Int32.Parse(id);
    var userInfo = await mintClient.GetUserInfo(idNumber);
    await Task.Delay(400);
    var steelInfo = await mintClient.GetNotClaimedMintTree(userInfo.Result.Id);
    await Task.Delay(400);
    var validTree = steelInfo.Result.FirstOrDefault(x => x is { Stealable: true, Amount: >= 1500 });
    if (validTree != default)
    {
        var result = await mintClient.SteelTree(userInfo.Result.Id);
        Console.WriteLine(id);
        if (result.SteelInfo != default)
        {
            Console.WriteLine($"Steel  id {id}: {result.SteelInfo.Amount}ME");
        }
        else
        {
            Console.WriteLine("Null");
        }
    }
    else
    {
        Console.WriteLine($"Not stolen {id}");
    }
    
    await Task.Delay(3500);
}

try
{ 
    //Parallel.ForEach(idsFromFile, new ParallelOptions(){MaxDegreeOfParallelism = 1}, DoClaim);
    foreach (var id in idsFromFile)
    {
        await DoClaim(id);
    }
}
catch (Exception e)
{
    Console.WriteLine(e);
}
Console.WriteLine("Finish press F to close");
Console.ReadKey();