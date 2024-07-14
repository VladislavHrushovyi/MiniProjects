using System.Text.Json.Serialization;

namespace MintForestBase.Models;

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