using System.Net;
using System.Text.Json;

namespace MintTreeSearcher;

public class MintRequestSender(HttpClient httpClient)
{
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
            //Console.WriteLine(jsonString);
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

        return new Response() { Result = new ItemsTree[] { new ItemsTree() { Amount = 1, Stealable = false } } };
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

    public async Task<SteelResponse> SteelTree(int resultId)
    {
        var uri = new Uri($"https://www.mintchain.io/api/tree/steal/claim?id={resultId}");
        try
        {
            var response = await httpClient.GetAsync(uri);
            var jsonString = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(jsonString.Substring(0, 20) + "Steelling++++++++++++");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var steelResponse = JsonSerializer.Deserialize<SteelResponse>(jsonString);
                return steelResponse;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return new SteelResponse() { SteelInfo = new SteelInfo() { Amount = 0 } };
    }
}