using MintTreeSearcher;

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
void DoSearch(int treeId)
{
    var userInfo = mintClient.GetUserInfo(treeId).GetAwaiter().GetResult();
    var claimableInfo = mintClient.GetNotClaimedMintTree(userInfo.Result.Id).GetAwaiter().GetResult();
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

Parallel.For(from, to, new ParallelOptions() { MaxDegreeOfParallelism = 25 }, i => DoSearch(i));
Console.WriteLine("FINISH. Press any key to exit...");
Console.ReadKey();
