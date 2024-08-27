using System.Net;
using System.Text.Json;
using MintForestBase.Models;

namespace MintForestBase;

public class MintRequestSender(HttpClient httpClient)
{
    public void ChangeHttpClient(HttpClient newClient)
    {
        httpClient = newClient;
    }

    public async Task<Response> GetNotClaimedMintTree(int userId)
    {
        if (userId == 0)
        {
            return new Response() { Result = new ItemsTree[] { new ItemsTree() { Amount = 1, Stealable = false } } };
        }

        var uri = new Uri($"https://www.mintchain.io/api/tree/steal/energy-list?id={userId}");
        try
        {
            var response = await httpClient.GetAsync(uri);
            var jsonString = await response.Content.ReadAsStringAsync();
            if (jsonString.Contains("else") || jsonString.Contains("owner"))
            {
                return new Response()
                    { Result = new ItemsTree[] { new ItemsTree() { Amount = 0, Stealable = false } } };
            }
            //Console.WriteLine(jsonString.Length > 1000 ? "MANY SYMBOLS RESPONSE" : jsonString);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var claimableInfo = JsonSerializer.Deserialize<Response>(jsonString);
                return claimableInfo;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"ERROR UserId {userId}");
        }

        return new Response() { Result = new ItemsTree[] { new ItemsTree() { Amount = 0, Stealable = false } } };
    }

    public async Task<UserInfo> GetUserInfo(int treeId)
    {
        var uri = new Uri($"https://www.mintchain.io/api/tree/user-info?treeid={treeId}");
        try
        {
            var response = await httpClient.GetAsync(uri);
            var jsonString = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(jsonString);
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

    public async Task<SteelResponse> SteelTree(int userId)
    {
        while (true)
        {
            var uri = new Uri($"https://www.mintchain.io/api/tree/steal/claim?id={userId}");
            try
            {
                var response = await httpClient.GetAsync(uri);
                var jsonString = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonString.Length > 1000 ? "MANY SYMBOLS RESPONSE" : jsonString);
                
                if (jsonString.Contains("late") || jsonString.Contains("can"))
                {
                    return new SteelResponse() { SteelInfo = new SteelInfo() { Amount = 0 } };
                }
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var steelResponse = JsonSerializer.Deserialize<SteelResponse>(jsonString);
                    if (steelResponse.SteelInfo.Amount > 0)
                    {
                        return steelResponse;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        return new SteelResponse() { SteelInfo = new SteelInfo() { Amount = 0 } };
    }

    public async Task<LeaderboardTrees> GetTreesByLeaderboardPage(int page)
    {
        var uri = new Uri($"https://www.mintchain.io/api/tree/leaderboard?page={page}");
        try
        {
            var response = await httpClient.GetAsync(uri);
            var jsonString = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = JsonSerializer.Deserialize<LeaderboardTrees>(jsonString);

                return result;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return new LeaderboardTrees() { Result = ArraySegment<UserLeaderboard>.Empty };
    }

    public async Task<UserActivity> GetUserActivity(int treeId)
    {
        var uri = new Uri($"https://www.mintchain.io/api/tree/activity?page=1&treeid={treeId}");
        try
        {
            var response = await httpClient.GetAsync(uri);
            var jsonString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<UserActivity>(jsonString);
                return result;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return new UserActivity()
        {
            Result = new[]
            {
                new ActivityItem()
                {
                    Amount = 0, Type = "", ClaimAt = DateTime.MinValue
                }
            }
        };
    }
}