using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MintTreeSearcher;

public class MintRequestSender
{

    public async Task<Response> GetNotClaimedMintTree(int id)
    {
        var userInfo = await GetUserInfo(id);
        var uri = new Uri($"https://www.mintchain.io/api/tree/steal/energy-list?id={userInfo.Result.Id}");
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
            "eyJhbGciOiJIUzI1NiJ9.eyJhZGRyZXNzIjoiMHgwNjEwZjVlOTAzNTIwYjNhMTAxMzQ0NWEyYzEyZWY1Y2I2NGIzNGEyIiwidWlkIjo2NjgyNTUsImV4cCI6MTcyMDAyMjc4M30.uHX4mQaVNIrahwW1fA6jx708-1VvTL1DqDQjH6Bk2Ts");
        var response = await httpClient.GetAsync(uri);
        var jsonString = await response.Content.ReadAsStringAsync();
        var claimableInfo = JsonSerializer.Deserialize<Response>(jsonString);

        return claimableInfo;
    }

    private async Task<UserInfo> GetUserInfo(int treeId)
    {
        var uri = new Uri($"https://www.mintchain.io/api/tree/user-info?treeid={treeId}");
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
            "eyJhbGciOiJIUzI1NiJ9.eyJhZGRyZXNzIjoiMHgwNjEwZjVlOTAzNTIwYjNhMTAxMzQ0NWEyYzEyZWY1Y2I2NGIzNGEyIiwidWlkIjo2NjgyNTUsImV4cCI6MTcyMDAyMjc4M30.uHX4mQaVNIrahwW1fA6jx708-1VvTL1DqDQjH6Bk2Ts");
        var response = await httpClient.GetAsync(uri);
        var jsonString = await response.Content.ReadAsStringAsync();
        try
        {
            var objResult = JsonSerializer.Deserialize<UserInfo>(jsonString);
            return objResult;
        }
        catch (Exception e)
        {
            Console.WriteLine(jsonString);
            throw;
        }
    }
}