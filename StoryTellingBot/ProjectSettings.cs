namespace StoryTellingBot;

public static class ProjectSettings
{
    public static readonly Dictionary<string, string> SettingsVars = new();
    static ProjectSettings()
    {
        if (File.Exists(Environment.CurrentDirectory + "/.env"))
        {
            var lines = File.ReadAllLines(Environment.CurrentDirectory + "/.env");
            foreach (var line in lines)
            {
                var separateIndex = line.IndexOf('=');
                var key = line[..separateIndex];
                var value = line[(separateIndex + 1)..];
                
                SettingsVars.Add(key, value);
            }
        }
    }
}