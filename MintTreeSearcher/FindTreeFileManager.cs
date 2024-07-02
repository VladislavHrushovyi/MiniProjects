using System.Text;

namespace MintTreeSearcher;

public class FindTreeFileManager
{
    private const string _fileName = "Trees.txt";
    public FindTreeFileManager()
    {
        if (File.Exists(_fileName) )
        {
            var dateCreationFile = File.GetCreationTime(_fileName);
            if (dateCreationFile.Date != DateTime.Now.Date)
            {
                Console.WriteLine($"Creation time {dateCreationFile.Date} -- Now date {DateTime.Now.Date}");
                Console.WriteLine("DELETE FILE");
                File.Delete(_fileName);
            }
        }
    }

    public async Task AppendLine(string data)
    {
        await File.AppendAllTextAsync(_fileName, data);
    }
}