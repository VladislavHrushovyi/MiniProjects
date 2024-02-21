using System.Net.Http.Headers;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;

namespace StoryTellingBot.GptInterraction;

public class GptClient
{
    private readonly HttpClient _httpClient;

    public GptClient()
    {
        _httpClient = new();
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", ProjectSettings.SettingsVars["OPEN_AI_TOKEN"]);
    }

    public async Task<string> GetQuestionFromGpt()
    {
        string url = "https://api.openai.com/v1/chat/completions";
        
        var response = await _httpClient.PostAsJsonAsync(url, new
        {
            Model="gpt-4-turbo-preview",
            Messages = Enumerable.Range(0,1).Select(_ => new
            {
                Role="user", 
                Content="Напиши 10 простих запитань до розповіді, щоб за відповідями можна було створити розповідь"
            }).ToArray()
        });

        var responseString = await response.Content.ReadAsStringAsync();
        var choices = JObject.Parse(responseString)["choices"];
        var message = choices.ToArray()[0]["message"]["content"].Value<string>();
        return message;
    }
}