using System.Text.Json.Serialization;

namespace MintTreeSearcher;

public class SteelResponse
{
    [JsonPropertyName("result")]
    public SteelInfo SteelInfo { get; set; }
}

public class SteelInfo
{
    [JsonPropertyName("amount")]
    public int Amount { get; set; }
}