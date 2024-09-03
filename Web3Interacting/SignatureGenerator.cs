using Nethereum.ABI;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using System.Numerics;
using Nethereum.ABI.EIP712;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Util;

namespace Web3Interacting;

public class SignatureGenerator
{
     public class Eip712Domain : Domain
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public BigInteger ChainId { get; set; }
        public string VerifyingContract { get; set; }
    }

    [Struct("StealParams")]
    public class StealParams
    {
        [Parameter("address", "user", 1)]
        public string User { get; set; }

        [Parameter("address", "target", 2)]
        public string Target { get; set; }

        [Parameter("uint64", "time", 3)]
        public long Time { get; set; }

        [Parameter("uint256", "point", 4)]
        public BigInteger Point { get; set; }
    }

    private string GetDomainSeparator(Eip712Domain domain)
    {
        var abiEncode = new ABIEncode();
        var encoded = abiEncode.GetSha3ABIEncodedPacked(
            new ABIValue("bytes32", new Sha3Keccack().CalculateHash("EIP712Domain(string name,string version,uint256 chainId,address verifyingContract)")),
            new ABIValue("bytes32", new Sha3Keccack().CalculateHash(domain.Name)),
            new ABIValue("bytes32", new Sha3Keccack().CalculateHash(domain.Version)),
            new ABIValue("uint256", domain.ChainId),
            new ABIValue("address", domain.VerifyingContract)
        );
        return new Sha3Keccack().CalculateHash(encoded).ToHex();
    }

    private string GetTypeHash(StealParams parameters)
    {
        return new Sha3Keccack().CalculateHash("StealParams(address user,address target,uint64 time,uint256 point)");
    }

    private string GetStructHash(StealParams parameters)
    {
        var abiEncode = new ABIEncode();
        var encoded = abiEncode.GetSha3ABIEncodedPacked(
            new ABIValue("bytes32", GetTypeHash(parameters)),
            new ABIValue("address", parameters.User),
            new ABIValue("address", parameters.Target),
            new ABIValue("uint64", parameters.Time),
            new ABIValue("uint256", parameters.Point)
        );
        return new Sha3Keccack().CalculateHash(encoded).ToHex();
    }

    private byte[] GetMessageHashEip712(StealParams parameters, Eip712Domain domain)
    {
        var domainSeparator = GetDomainSeparator(domain);
        var structHash = GetStructHash(parameters);

        var abiEncode = new ABIEncode();
        var encoded = abiEncode.GetSha3ABIEncodedPacked(
            new ABIValue("bytes2", "0x1901"), // EIP-712 префікс
            new ABIValue("bytes32", domainSeparator),
            new ABIValue("bytes32", structHash)
        );

        return new Sha3Keccack().CalculateHash(encoded);
    }

    private byte[] SignMessage(byte[] messageHash, string privateKey)
    {
        var signer = new EthereumMessageSigner();
        var signature = signer.Sign(messageHash, privateKey);
        
        return signature.HexToByteArray();
    }

    public byte[] GenerateSignature(string user, string target, long time, BigInteger point, string privateKey, string contractAddress)
    {
        // Створюємо домен EIP-712
        var domain = new Eip712Domain
        {
            Name = "MintMainnet",  // Ім'я вашого домену
            Version = "1",
            ChainId = 185, // ChainId мережі, у якій ви працюєте (наприклад, 1 для Ethereum mainnet)
            VerifyingContract = contractAddress // Адреса контракту
        };

        // Створюємо параметри
        var parameters = new StealParams
        {
            User = user,
            Target = target,
            Time = time,
            Point = point
        };

        // Отримання хешу повідомлення за допомогою EIP-712
        var messageHash = GetMessageHashEip712(parameters, domain);

        // Підписання хешу приватним ключем
        var signature = SignMessage(messageHash, privateKey);

        return signature;
    }
}