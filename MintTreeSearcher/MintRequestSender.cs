using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MintTreeSearcher;

public class MintRequestSender(string authToken)
{
    public async Task<Response> GetNotClaimedMintTree(int id)
    {
        var userInfo = await GetUserInfo(id);
        var uri = new Uri($"https://www.mintchain.io/api/tree/steal/energy-list?id={userInfo.Result.Id}");
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        try
        {
            var response = await httpClient.GetAsync(uri);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
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
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        try
        {
            var response = await httpClient.GetAsync(uri);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
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