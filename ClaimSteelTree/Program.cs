using MintForestBase;

var configs = new SettingsLoader();
var contractInteraction = new SmartContractInteraction(configs.GetValue("PrivateKey"));

var idsFromFile = File.ReadAllLines("./Ids.txt");

HttpClientFactory httpClientFactory = new HttpClientFactory(configs.GetValue("AuthToken"));

async Task DoClaim(HttpClient client, string id)
{
    MintRequestSender mintClient = new MintRequestSender(client);

    var idNumber = Int32.Parse(id);
    
    var proofModel = await mintClient.GetProofSteal(idNumber);
    if (proofModel != null)
    {
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
        var task = DoClaim(httpClientFactory.HttpClients[skip], id);
        tasks.Add(task);
        skip++;
    }

    await Task.WhenAll(tasks);
}
catch (Exception e)
{
    Console.WriteLine(e);
}

Console.WriteLine("Finish press F to close");
Console.ReadKey();