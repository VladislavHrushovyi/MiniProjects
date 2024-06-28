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
            Console.WriteLine($"https://www.mintchain.io/mint-forest?id={id} ---> {validObj.Amount}ME");
            if (validObj.Amount >= moreThan)
            {
                string line = $"https://www.mintchain.io/mint-forest?id={id} ---> {validObj.Amount}ME \n";
                File.AppendAllText("Trees.txt",line);
            }
        }   
    }
}
Parallel.For(from, to,new ParallelOptions(){MaxDegreeOfParallelism = 8}, _ => DoSearch(Random.Shared.Next(from,to)));
// async Task DoSearchAsync(int id)
// {
//     var claimableInfo = await mintClient.GetNotClaimedMintTree(id);
//     if (claimableInfo.Result != null)
//     {
//         var validObj = claimableInfo.Result.FirstOrDefault(x => x is { Stealable: true, Amount: >= 100 });
//         if (validObj is not null)
//         {
//             Console.WriteLine($"https://www.mintchain.io/mint-forest?id={id} ---> {validObj.Amount}ME");
//             if (validObj.Amount >= moreThan)
//             {
//                 string line = $"https://www.mintchain.io/mint-forest?id={id} ---> {validObj.Amount}ME \n";
//                 file.Write(Encoding.ASCII.GetBytes(line));   
//             }
//         }   
//     }
// }
//
// var reqTasks = Enumerable.Range(from, to).Select(DoSearchAsync);
// await Task.WhenAll(reqTasks);
