using System.Text;
using System.Text.Json;
using MintTreeSearcher;

MintRequestSender mintClient = new MintRequestSender();
if (File.Exists("./Trees.txt"))
{
    File.Delete("./Trees.txt");
}
var file = File.Create("./Trees.txt");
Console.WriteLine("From");
int from = Int32.Parse(Console.ReadLine());
Console.WriteLine("To");
int to = Int32.Parse(Console.ReadLine());

for (int i = from; i <= to; i++)
{
    var claimableInfo = await mintClient.GetNotClaimedMintTree(i);
    if (claimableInfo.Result != null)
    {
        var validObj = claimableInfo.Result.FirstOrDefault(x => x is { Stealable: true, Amount: >= 100 });
        if (validObj is not null)
        {
            Console.WriteLine($"https://www.mintchain.io/mint-forest?id={i} ---> {validObj.Amount}ME");
            if (validObj.Amount >= 800)
            {
                string line = $"https://www.mintchain.io/mint-forest?id={i} ---> {validObj.Amount}ME \n";
                file.Write(Encoding.ASCII.GetBytes(line));   
            }
        }   
    }
}