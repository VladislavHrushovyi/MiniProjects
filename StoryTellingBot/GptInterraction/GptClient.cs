using System.Net.Http.Headers;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;
using File = System.IO.File;

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

    public async Task<string> GetQuestionFromGpt(List<object> messages)
    {
        string url = "https://api.openai.com/v1/chat/completions";
        
        var response = await _httpClient.PostAsJsonAsync(url, new
        {
            Model="gpt-4-turbo-preview",
            Messages = messages.ToArray()
        });

        var responseString = await response.Content.ReadAsStringAsync();
        var choices = JObject.Parse(responseString)["choices"];
        var message = choices.ToArray()[0]["message"]["content"].Value<string>();
        return message;
    }

    public async Task<string> GptTextToSpeech(string text)
    {
        string textToSpeechEndpoint = "https://api.openai.com/v1/audio/speech";
        HttpResponseMessage result = await _httpClient.PostAsJsonAsync(textToSpeechEndpoint, new
        {
            Model = "tts-1",
            Input = text,
            Voice = "onyx"
            
        });

        var mp3Bytes = await result.Content.ReadAsByteArrayAsync();
        await File.WriteAllBytesAsync("./audio.mp3", mp3Bytes);
        return "audio.mp3";
    }
}