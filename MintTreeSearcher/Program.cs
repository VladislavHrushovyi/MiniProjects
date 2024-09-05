
using MintForestBase;

var configs = new SettingsLoader();
var contractInteraction = new SmartContractInteraction(configs.GetValue("PrivateKey"));

Console.WriteLine("From:");
int from = Int32.Parse(Console.ReadLine());

Console.WriteLine("To:");
int to = Int32.Parse(Console.ReadLine());

Console.WriteLine("Enter more than:");
int moreThan = Int32.Parse(Console.ReadLine());

FindTreeFileManager fileManager = new FindTreeFileManager("Trees.txt");
var httpClientsFactory = new HttpClientFactory(configs.GetValue("AuthToken"));

fileManager.AppendLine($"\t RANGE {from}-{to} \n");
async Task DoSearch(HttpClient client, int treeId)
{
    MintRequestSender mintClient = new MintRequestSender(client);
    
    var userInfo = await mintClient.GetUserInfo(treeId);
    await Task.Delay(400);
    
    var claimableInfo = await mintClient.GetNotClaimedMintTree(userInfo.Result.Id);
    await Task.Delay(400);
    
    if (claimableInfo.Result.Any())
    {
        var validObj = claimableInfo.Result.FirstOrDefault(x => x is { Stealable: true, Amount: >= 100 });
        if (validObj != default)
        {
            string output = $"https://www.mintchain.io/mint-forest?id={treeId} ---> {validObj.Amount}ME";
            Console.WriteLine(validObj.Amount >= moreThan ? output +  " \t <<--- BINGO" : output);
            if (validObj.Amount >= moreThan)
            {
                var proofModel = await mintClient.GetProofSteal(userInfo.Result.Id);
                Console.WriteLine(proofModel.Result.Tx);
                if (!string.IsNullOrWhiteSpace(proofModel.Result.Tx))
                {
                    string line = $"https://www.mintchain.io/mint-forest?id={treeId} ---> {validObj.Amount}ME \n";
                    var isDone = await contractInteraction.StealActionInteraction(proofModel);
                    if (isDone)
                    {
                        Console.WriteLine($"Tree ${treeId} stolen. Amount {proofModel.Result.Amount}");
                    }
                    fileManager.AppendLine(line);
                }
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

try
{
    int amountIds = to - from;
    int amountProxy = httpClientsFactory.HttpClients.Count;
    var tasks = Enumerable.Range(from, amountIds)
        .Chunk(amountIds / 100)
        .Select(x => DoSearchChunk(httpClientsFactory.GetDefaultHttpClient(), x));
    await Task.WhenAll(tasks);
}
catch (Exception e)
{
    Console.WriteLine(e);
}
Console.WriteLine("FINISH. Press any key to exit...");
Console.ReadKey();
