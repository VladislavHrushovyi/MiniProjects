namespace MintForestBase;

public class SettingsLoader
{
    private const string ConfigFilePath = "./Config.txt";
    private readonly Dictionary<string, string> _configVars;

    public SettingsLoader()
    {
        _configVars = ReadConfigFile();
    }

    private Dictionary<string, string> ReadConfigFile()
    {
        var lines = File.ReadAllLines(ConfigFilePath);

        return lines.Select(line => line.Split("="))
            .ToDictionary(splitLine => splitLine[0], splitLine => splitLine[1]);
    }

    public string GetValue(string key)
    {
        if (_configVars.TryGetValue(key, out var value))
        {
            return value;
        }

        throw new Exception($"Not provided setting {key}");
    }
}