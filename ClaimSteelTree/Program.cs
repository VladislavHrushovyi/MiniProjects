using MintForestBase;
//var dateTimeNow = DateTime.UtcNow;
// var deadLineTime = DateTime.Parse($"{dateTimeNow.Year}-{dateTimeNow.Month}-{dateTimeNow.Day} {14}:{00}:{00}").AddMilliseconds(-200).ToUniversalTime();
// while (DateTime.UtcNow < deadLineTime)
// {
//     Console.Clear();
//     Console.WriteLine(DateTime.UtcNow.ToString("HH:mm:ss") + $" Target {deadLineTime:HH:mm:ss}");
//     await Task.Delay(10);
// }
var configs = new SettingsLoader();
var contractInteraction = new SmartContractInteraction(configs.GetValue("PrivateKey"));

var idsFromFile = File.ReadAllLines("./Ids.txt");

HttpClientFactory httpClientFactory = new HttpClientFactory(configs.GetValue("AuthToken"));

var dateTimeNow = DateTime.UtcNow;
var deadLineTime = DateTime.Parse($"{dateTimeNow.Year}-{dateTimeNow.Month}-{dateTimeNow.Day} {14}:{00}:{00}").AddMilliseconds(-200).ToUniversalTime();
while (DateTime.UtcNow < deadLineTime)
{
    Console.Clear();
    Console.WriteLine(DateTime.UtcNow.ToString("HH:mm:ss") + $" Target {deadLineTime:HH:mm:ss}");
    await Task.Delay(10);
}

async Task DoClaim(HttpClient client, string id)
{
    MintRequestSender mintClient = new MintRequestSender(client);

    var idNumber = Int32.Parse(id);
    
    var proofModel = await mintClient.GetProofSteal(idNumber);
    if (proofModel is { Result.Amount: > 55000 })
    {
        Console.WriteLine($"Proof {proofModel.Result.Tx.Substring(0, 30)} {proofModel.Result.Amount}ME");
        var isDone = await contractInteraction.StealActionInteraction(proofModel);
        if (isDone)
        {
            Console.WriteLine(proofModel.Result.Amount != 0 ? $"Steel  id {id}: {proofModel.Result.Amount}ME" : "Null");   
        }
    }
}

try
{
    List<Task> tasks = new List<Task>();
    int skip = 0;

    foreach (var id in idsFromFile)
    {
        var task = DoClaim(httpClientFactory.GetDefaultHttpClient(), id);
        tasks.Add(task);
        skip++;
        await Task.Delay(1000);
    }

    await Task.WhenAll(tasks);
}
catch (Exception e)
{
    Console.WriteLine(e);
}

Console.WriteLine("Finish press F to close");
Console.ReadKey();