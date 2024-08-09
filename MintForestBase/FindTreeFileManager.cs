namespace MintForestBase;

public class FindTreeFileManager
{
    private readonly string _fileName;
    private readonly object _lock = new();
    public FindTreeFileManager(string fileName)
    {
        _fileName = fileName;
        if (File.Exists(_fileName) )
        {
            var lastAccessTime = File.GetLastAccessTime(_fileName);
            if (lastAccessTime.Date != DateTime.Now.Date)
            {
                Console.WriteLine($"Creation time {lastAccessTime.Date} -- Now date {DateTime.Now.Date}");
                Console.WriteLine("DELETE FILE");
                File.Delete(_fileName);
            }
        }
    }

    public void AppendLine(string data)
    {
        lock (_lock)
        { 
            File.AppendAllTextAsync(_fileName, data);
        }
    }
}