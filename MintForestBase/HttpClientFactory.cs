using System.Net;
using System.Net.Http.Headers;

namespace MintForestBase;

public class HttpClientFactory
{
    public List<HttpClient> HttpClients { get; set; }
    private readonly string _authToken;

    public HttpClientFactory(string authToken)
    {
        _authToken = authToken;
        HttpClients = InitHttpClients();
    }

    private List<HttpClient> InitHttpClients()
    {
        int index = 0;
        var httpClients = new List<HttpClient>();
        var proxies = File.ReadAllLines("./Proxy.txt");
        foreach (var proxy in proxies)
        {
            var configuredHttpClientState = BuildHttpClientState(proxy);
            httpClients.Add(configuredHttpClientState);
            index++;
        }
        return httpClients;
    }

    private HttpClient BuildHttpClientState(string proxyLine)
    {
        var proxySplit = proxyLine.Split(":");
        var proxy = new WebProxy()
        {
            Address = new Uri($"http://{proxySplit[0]}:{proxySplit[1]}"),
            BypassProxyOnLocal = false,
            UseDefaultCredentials = false,
            
            Credentials = new NetworkCredential()
            {
                UserName = proxySplit[2],
                Password = proxySplit[3]
            }
            
        };

        var httpClientHandler = new HttpClientHandler()
        {
            Proxy = proxy
        };

        var httpClient = new HttpClient(httpClientHandler);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authToken);
        httpClient.DefaultRequestHeaders.Add("Accept", new []{"application/json", "text/plain", "*/*"});
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36");


        return httpClient;
    }

    public HttpClient GetDefaultHttpClient()
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authToken);
        httpClient.DefaultRequestHeaders.Add("Accept", new []{"application/json", "text/plain", "*/*"});
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36");
        return httpClient;
    }
}

public class HttpClientState
{
    public int Id { get; set; }
    public bool IsFree { get; set; }
    public HttpClient HttpClient { get; set; }
}