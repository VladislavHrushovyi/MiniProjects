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
        var httpClients = new List<HttpClient>();
        var proxies = File.ReadAllLines("./Proxy.txt");
        foreach (var proxy in proxies)
        {
            var configuredHttpClientState = BuildHttpClientState(proxy);
            httpClients.Add(configuredHttpClientState);
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
        //httpClient.DefaultRequestHeaders.Add("Accept-Encoding", new []{"gzip", "deflate", "br", "zstd", "utf-8"});
        //httpClient.DefaultRequestHeaders.Add("Accept-language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7,uk;q=0.6");
        // httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
        // httpClient.DefaultRequestHeaders.Add("Cookie", "wagmi.recentConnectorId=\"io.metamask\"; wagmi.store={\"state\":{\"connections\":{\"__type\":\"Map\",\"value\":[[\"c95c27a42ab\",{\"accounts\":[\"0x0610F5E903520b3A1013445a2C12eF5CB64b34A2\"],\"chainId\":161221135,\"connector\":{\"id\":\"io.metamask\",\"name\":\"MetaMask\",\"type\":\"injected\",\"uid\":\"c95c27a42ab\"}}]]},\"chainId\":185,\"current\":\"c95c27a42ab\"},\"version\":2}");
        // httpClient.DefaultRequestHeaders.Add("Host", "www.mintchain.io");
        // httpClient.DefaultRequestHeaders.Add("Sec-Ch-Ua", "\"Not)A;Brand\";v=\"99\", \"Google Chrome\";v=\"127\", \"Chromium\";v=\"127\"");
        // httpClient.DefaultRequestHeaders.Add("Sec-Ch-Ua-Mobile", "?0");
        // httpClient.DefaultRequestHeaders.Add("Sec-Ch-Ua-Platform", "\"Windows\"");
        // httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
        // httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
        // httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
        // httpClient.DefaultRequestHeaders.Add("Sec-Gpc", "1");
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