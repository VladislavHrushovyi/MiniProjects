using System.Net;

namespace MintForestBase.Extension;

public static class HttpClientExtension
{
    public static void SetProxy(this HttpClient client, string proxyString)
    {
        var proxySplit = proxyString.Split(":");
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
    }
}