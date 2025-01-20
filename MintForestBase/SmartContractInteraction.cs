using MintForestBase.FunctionMessagesModels;
using MintForestBase.Models;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Util;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace MintForestBase;

public class SmartContractInteraction
{
    private object _lock = new();
    private readonly Account _account;
    private readonly Web3 _web3;
    private const string ContractAddress = "0x12906892AaA384ad59F2c431867af6632c68100a";
    public SmartContractInteraction(string privateKey)
    {
        string rpcUrl = "https://rpc.mintchain.io";
        _account = new Account(privateKey);
        _web3 = new Web3(_account, rpcUrl);
    }

    public async Task<bool> StealActionInteraction(ProofModel proofModel)
    {
        lock (_lock)
        {
            var nonce = _web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(_account.Address);

            var gasPrice = Web3.Convert.ToWei(0.05, UnitConversion.EthUnit.Gwei);
            var gasLimit = 100000;

            var transactionInput = new TransactionInput()
            {
                From = _account.Address,
                To = ContractAddress,
                Data = proofModel.Result.Tx,
                Gas = new HexBigInteger(gasLimit),
                GasPrice = new HexBigInteger(gasPrice),
                Nonce = new HexBigInteger(nonce.Result)
            };

            try
            {
                var transactionSigner = new AccountOfflineTransactionSigner();
                var chainId = 185;
            
                var rawTransaction = transactionSigner.SignTransaction(_account, transactionInput, chainId);

                var txHash = _web3.Eth.Transactions.SendRawTransaction.SendRequestAsync("0x" + rawTransaction);
                Console.WriteLine($"Transaction Hash: {txHash.Result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return true;   
        }
    }

    public async Task<bool> StealActionInteraction2(ProofModel proofModel, string targetAddress)
    {
        var stealFunction = new StealFunction()
        {
            Params = new StealParams()
            {
                Point = proofModel.Result.Amount,
                Target = targetAddress,
                Time = (ulong)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds
            },
            Signature = proofModel.Result.Tx.HexToByteArray(),
            Gas = new HexBigInteger(100000),
        };

        var handler = _web3.Eth.GetContractTransactionHandler<StealFunction>();

        var receipt = await handler.SendRequestAndWaitForReceiptAsync(ContractAddress, stealFunction);
        Console.WriteLine($"receipt: {receipt.TransactionHash} tx, {receipt.Status} status");
        return true;
    }
}