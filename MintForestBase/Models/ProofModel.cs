using System.Text.Json.Serialization;

namespace MintForestBase.Models;

public class ProofModel
{
    [JsonPropertyName("result")]
    public ProofResult Result { get; set; }
}

public class ProofResult
{
    [JsonPropertyName("tx")]
    public string Tx { get; set; }
    
    [JsonPropertyName("amount")]
    public int Amount { get; set; }
}