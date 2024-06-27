using System.Text;
using MintTreeSearcher;

if (File.Exists("./Trees.txt"))
{
    File.Delete("./Trees.txt");
}

await using var file = File.Create("./Trees.txt");

Console.WriteLine("Enter auth token:");
var authToken = Console.ReadLine();

Console.WriteLine("From:");
int from = Int32.Parse(Console.ReadLine());

Console.WriteLine("To:");
int to = Int32.Parse(Console.ReadLine());

Console.WriteLine("Enter more than:");
int moreThan = Int32.Parse(Console.ReadLine());

MintRequestSender mintClient = new MintRequestSender(authToken);

Parallel.For(from, to,new ParallelOptions(){MaxDegreeOfParallelism = 8}, i =>
{
    var claimableInfo = mintClient.GetNotClaimedMintTree(i).GetAwaiter().GetResult();
     if (claimableInfo.Result != null)
     {
         var validObj = claimableInfo.Result.FirstOrDefault(x => x is { Stealable: true, Amount: >= 100 });
         if (validObj is not null)
         {
             Console.WriteLine($"https://www.mintchain.io/mint-forest?id={i} ---> {validObj.Amount}ME");
             if (validObj.Amount >= moreThan)
             {
                 string line = $"https://www.mintchain.io/mint-forest?id={i} ---> {validObj.Amount}ME \n";
                 file.Write(Encoding.ASCII.GetBytes(line));   
             }
         }   
     }
});