using System.Numerics;
using Nethereum.Util;
using Web3Interacting;

string nodeUrl = "https://rpc.mintchain.io"; // Замість YOUR_INFURA_PROJECT_ID підставте ваш Infura project ID або URL вузла
string privateKey = ""; // Замість YOUR_PRIVATE_KEY підставте приватний ключ відправника
string proxyContractAddress = "0x12906892AaA384ad59F2c431867af6632c68100a"; // Замість YOUR_PROXY_CONTRACT_ADDRESS підставте адресу проксі контракту
string userAddress = "0xee89984c1ce5aa6ba8e4d8ae10d2547c67bc6695";
var client = new ContractInteracting(nodeUrl, privateKey, proxyContractAddress);

string targetAddress = "0x898cb4b876bf2fde5a64c4a346eea00ecb59195d"; // Замість TARGET_WALLET_ADDRESS підставте адресу цільового гаманця
long time = DateTime.Now.ToUnixTimestamp(); // Замість цього підставте правильне значення часу
BigInteger point = new BigInteger(177); // Замість цього підставте правильне значення балів
var signatureGenerator = new SignatureGenerator();
byte[] signature = signatureGenerator.GenerateSignature(userAddress, targetAddress, time, point, privateKey, proxyContractAddress);

string transactionHash = await client.StealAsync(targetAddress, time, point, signature);
Console.WriteLine($"Transaction hash: {transactionHash}");