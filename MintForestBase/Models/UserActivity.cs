using System.Text.Json.Serialization;

namespace MintForestBase.Models;

public class UserActivity
{
    [JsonPropertyName("result")]
    public IEnumerable<ActivityItem> Result { get; set; }
}

public class ActivityItem
{
    [JsonPropertyName("type")]
    public string Type { get; set; }
    
    [JsonPropertyName("amount")]
    public int Amount { get; set; }
    
    [JsonPropertyName("claimAt")]
    public DateTime ClaimAt { get; set; }
}