using System.Text.Json.Serialization;

namespace MintTreeSearcher;

public class Response
{
    [JsonPropertyName("result")]
    public IEnumerable<ItemsTree> Result { get; set; }
}

public class ItemsTree
{
    [JsonPropertyName("stealable")]
    public bool Stealable { get; set; }
    
    [JsonPropertyName("amount")]
    public int Amount { get; set; }
}