using System.Net.Http.Headers;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;
using StoryTellingBot.Repository.Models;
using File = System.IO.File;

namespace StoryTellingBot.GptInteraction;

public class GptClient
{
    private readonly HttpClient _httpClient;

    public GptClient()
    {
        _httpClient = new();
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", ProjectSettings.SettingsVars["OPEN_AI_TOKEN"]);
    }

    public async Task<string> AskInGpt(List<ChatItem> messages)
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

    public async Task<string> GptTextToSpeech(string fileName, string text)
    {
        var voices = new string[] { "alloy", "echo", "fable", "onyx", "nova", "shimmer" };
        string textToSpeechEndpoint = "https://api.openai.com/v1/audio/speech";
        HttpResponseMessage result = await _httpClient.PostAsJsonAsync(textToSpeechEndpoint, new
        {
            Model = "tts-1",
            Input = text,
            Voice = voices[Random.Shared.Next(5)]
            
        });

        var mp3Bytes = await result.Content.ReadAsByteArrayAsync();
        await File.WriteAllBytesAsync($"./{fileName}.mp3", mp3Bytes);
        return $"{fileName}.mp3";
    }

    public async Task<string> CreateImage(string imageDescription)
    {
        string imageCreationEndpoint = "https://api.openai.com/v1/images/generations";
        
        HttpResponseMessage result = await _httpClient.PostAsJsonAsync(imageCreationEndpoint, new
        {
            Model = "dall-e-3",
            Prompt = imageDescription,
            N = 1,
            Size = "1024x1024"
        });
        
        var responseString = await result.Content.ReadAsStringAsync();
        var choices = JObject.Parse(responseString)["data"];
        var imageUrl = choices.ToArray()[0]["url"].Value<string>();
        
        return imageUrl;
    }
}