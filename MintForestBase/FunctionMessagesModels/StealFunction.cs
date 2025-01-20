using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;

namespace MintForestBase.FunctionMessagesModels;

[Function("steal")]
public class StealFunction : FunctionMessage
{
    [Parameter("tuple", "params", 1)] public StealParams Params { get; set; }

    [Parameter("bytes", "signature", 2)] public byte[] Signature { get; set; }
}

public class StealParams
{
    [Parameter("address", "target", 1)] public string Target { get; set; }

    [Parameter("uint64", "time", 2)] public ulong Time { get; set; }

    [Parameter("uint256", "point", 3)] public BigInteger Point { get; set; }
}