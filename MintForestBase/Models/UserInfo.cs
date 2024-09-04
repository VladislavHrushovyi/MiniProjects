using System.Text.Json.Serialization;

namespace MintForestBase.Models;

public class UserInfo
{
    [JsonPropertyName("result")]
    public Result Result { get; set; }
}

public class Result
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }
}