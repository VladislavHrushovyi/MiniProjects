using MintForestBase.Models;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Util;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace MintForestBase;

public class SmartContractInteraction
{
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
        var nonce = await _web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(_account.Address);

        var gasPrice = Web3.Convert.ToWei(0.0001, UnitConversion.EthUnit.Gwei);
        var gasLimit = 50000;

        var transactionInput = new TransactionInput()
        {
            From = _account.Address,
            To = ContractAddress,
            Data = proofModel.Result.Tx,
            Gas = new HexBigInteger(gasLimit),
            GasPrice = new HexBigInteger(gasPrice),
            Nonce = new HexBigInteger(nonce)
        };

        try
        {
            var transactionSigner = new AccountOfflineTransactionSigner();
            var chainId = 185;
            
            var rawTransaction = transactionSigner.SignTransaction(_account, transactionInput, chainId);

            var txHash = await _web3.Eth.Transactions.SendRawTransaction.SendRequestAsync("0x" + rawTransaction);
            Console.WriteLine($"Transaction Hash: {txHash}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return true;
    }
}