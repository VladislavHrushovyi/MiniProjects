using System.Text;
using MintTreeSearcher;

if (File.Exists("./Trees.txt"))
{
    File.Delete("./Trees.txt");
}

//using var file = File.AppendText("./Trees.txt");

Console.WriteLine("Enter auth token:");
var authToken = Console.ReadLine();

Console.WriteLine("From:");
int from = Int32.Parse(Console.ReadLine());

Console.WriteLine("To:");
int to = Int32.Parse(Console.ReadLine());

Console.WriteLine("Enter more than:");
int moreThan = Int32.Parse(Console.ReadLine());

MintRequestSender mintClient = new MintRequestSender(authToken);

void DoSearch(int id)
{
    var claimableInfo = mintClient.GetNotClaimedMintTree(id).GetAwaiter().GetResult();
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
                File.AppendAllText("Trees.txt",line);
            }
        }   
    }
}
Parallel.For(from, to,new ParallelOptions(){MaxDegreeOfParallelism = 30}, i => DoSearch(i));

Console.WriteLine("FINISH. Press any key to exit...");
Console.ReadKey();
