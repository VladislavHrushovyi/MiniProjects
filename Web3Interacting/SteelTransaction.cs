using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Web3Interacting;

[Function("_implementation")]
public class SteelTransaction
{
    [Parameter("address", "target")]
    public string Target { get; set; }
}