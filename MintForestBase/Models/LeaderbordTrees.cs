using System.Text.Json.Serialization;

namespace MintForestBase.Models;

public class LeaderboardTrees
{
    [JsonPropertyName("result")]
    public IEnumerable<UserLeaderboard> Result { get; set; }
}

public class UserLeaderboard
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("treeId")]
    public int TreeId { get; set; }
}