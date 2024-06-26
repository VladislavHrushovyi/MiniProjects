using System.Text.Json.Serialization;

namespace MintTreeSearcher;

public class UserInfo
{
    [JsonPropertyName("result")]
    public Result Result { get; set; }
}

public class Result
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
}