using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MintTreeSearcher;

public class MintRequestSender
{
    private readonly HttpClient _httpClient;

    public MintRequestSender(string authToken)
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        _httpClient.DefaultRequestHeaders.Add("Accept", new []{"application/json", "text/plain", "*/*"});
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36");
    }
    public async Task<Response> GetNotClaimedMintTree(int id)
    {
        var userInfo = await GetUserInfo(id);
        if (userInfo.Result.Id == 0)
        {
            return new Response() { Result = new ItemsTree[] { new ItemsTree() { Amount = 1, Stealable = false } } };
        }
        var uri = new Uri($"https://www.mintchain.io/api/tree/steal/energy-list?id={userInfo.Result.Id}");
        try
        {
            var response = await _httpClient.GetAsync(uri);
            var jsonString = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var claimableInfo = JsonSerializer.Deserialize<Response>(jsonString);
                return claimableInfo;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"ERROR UserId {id}");
        }

        return new Response() { Result = new ItemsTree[] { new ItemsTree() { Amount = 1, Stealable = false } } };
    }

    private async Task<UserInfo> GetUserInfo(int treeId)
    {
        var uri = new Uri($"https://www.mintchain.io/api/tree/user-info?treeid={treeId}");
        try
        {
            var response = await _httpClient.GetAsync(uri);
            var jsonString = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var objResult = JsonSerializer.Deserialize<UserInfo>(jsonString);
                return objResult;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"ERROR TreeId {treeId}");
        }

        return new UserInfo() { Result = new Result() { Id = 0 } };
    }
}