using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace Web3Interacting;

public class ContractInteracting
{
    private readonly Web3 web3;
    private readonly string proxyContractAddress;

    public ContractInteracting(string nodeUrl, string privateKey, string proxyContractAddress)
    {
        var account = new Account(privateKey);
        this.web3 = new Web3(account, nodeUrl);
        this.proxyContractAddress = proxyContractAddress;
    }

    [Function("steal")]
    public class StealFunction : FunctionMessage
    {
        [Parameter("address", "target", 1)] public string Target { get; set; }

        [Parameter("uint64", "time", 2)] public long Time { get; set; }

        [Parameter("uint256", "point", 3)] public BigInteger Point { get; set; }

        [Parameter("bytes", "signature", 4)] public byte[] Signature { get; set; }
    }

    public async Task<string> StealAsync(string target, long time, BigInteger point, byte[] signature)
    {
        var stealFunction = new StealFunction
        {
            Target = target,
            Time = time,
            Point = point,
            Signature = signature,
            Gas = new HexBigInteger(300000), // Збільшено ліміт газу
            GasPrice = new HexBigInteger(Web3.Convert.ToWei(0.01, Nethereum.Util.UnitConversion.EthUnit.Gwei)) // Встановлення ціни газу
        };

        var handler = web3.Eth.GetContractTransactionHandler<StealFunction>();
        var transactionReceipt =
            await handler.SendRequestAsync(proxyContractAddress, stealFunction);
        return transactionReceipt;
    }
}