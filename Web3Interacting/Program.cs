using MintForestBase;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Util;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

var httpClient =
    new HttpClientFactory(
            "eyJhbGciOiJIUzI1NiJ9.eyJhZGRyZXNzIjoiMHhlZTg5OTg0YzFjZTVhYTZiYThlNGQ4YWUxMGQyNTQ3YzY3YmM2Njk1IiwidWlkIjoxMjg3MTksImV4cCI6MTcyNTYxNDMzMX0.zxicfvotuIQ5hYVx77989MnoDxNPF49eTyN8ImPbe_A")
        .GetDefaultHttpClient();
var mintClient = new MintRequestSender(httpClient);
var treeId = 401402;

var userInfo = await mintClient.GetUserInfo(treeId);

var proofResult = await mintClient.GetProofSteal(userInfo.Result.Id);

// string privateKey = "c69ba1592fb8403ad843691e959099141b27a5601e9b82d498e0026e567e348e";
// string rpcUrl = "https://rpc.mintchain.io";
// var account = new Account(privateKey);
// var web3 = new Web3(account, rpcUrl);
//
// string proxyAddress = "0x12906892AaA384ad59F2c431867af6632c68100a";
// string abiProxy = @"[ { ""inputs"": [ { ""internalType"": ""address"", ""name"": ""_logic"", ""type"": ""address"" }, { ""internalType"": ""bytes"", ""name"": ""_data"", ""type"": ""bytes"" } ], ""stateMutability"": ""payable"", ""type"": ""constructor"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": false, ""internalType"": ""address"", ""name"": ""previousAdmin"", ""type"": ""address"" }, { ""indexed"": false, ""internalType"": ""address"", ""name"": ""newAdmin"", ""type"": ""address"" } ], ""name"": ""AdminChanged"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": true, ""internalType"": ""address"", ""name"": ""beacon"", ""type"": ""address"" } ], ""name"": ""BeaconUpgraded"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [ { ""indexed"": true, ""internalType"": ""address"", ""name"": ""implementation"", ""type"": ""address"" } ], ""name"": ""Upgraded"", ""type"": ""event"" }, { ""stateMutability"": ""payable"", ""type"": ""fallback"" }, { ""stateMutability"": ""payable"", ""type"": ""receive"" } ]";
// var contract = web3.Eth.GetContract(abiProxy, proxyAddress);
// var functions = contract.GetFunction("transferOwnership");
// var receipt = await functions.SendTransactionAndWaitForReceiptAsync(account.Address, 
//     new HexBigInteger(24000), // Gas limit
//     new HexBigInteger(Web3.Convert.ToWei(0.00001)), // Value
//     null,
//     proofResult.Result.Tx.HexToByteArray());
// Console.WriteLine(receipt.TransactionHash);

var url = "https://rpc.mintchain.io";

        var privateKey = "c69ba1592fb8403ad843691e959099141b27a5601e9b82d498e0026e567e348e";
        var account = new Account(privateKey);

        var web3 = new Web3(account, url);

        var contractAddress = "0x12906892AaA384ad59F2c431867af6632c68100a";

        var nonce = await web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(account.Address);

        var gasPrice = Web3.Convert.ToWei(0.0001, UnitConversion.EthUnit.Gwei);
        var gasLimit = 50000;

        var transactionInput = new TransactionInput()
        {
            From = account.Address,
            To = contractAddress,
            Data = proofResult.Result.Tx,
            Gas = new HexBigInteger(gasLimit),
            GasPrice = new HexBigInteger(gasPrice),
            Nonce = new HexBigInteger(nonce)
        };

        try
        {
            var transactionSigner = new AccountOfflineTransactionSigner();
            var chainId = 185; // ID мережі (1 для mainnet, 3 для ropsten, тощо)
            
            // Створення необробленої транзакції для підписання
            var rawTransaction = transactionSigner.SignTransaction(account, transactionInput, chainId);

            // Відправка підписаної транзакції
            var txHash = await web3.Eth.Transactions.SendRawTransaction.SendRequestAsync("0x" + rawTransaction);
            Console.WriteLine($"Transaction Hash: {txHash}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

