using System.Security.Cryptography;
using System.Text;

namespace Fischless.Fetch.Lazy;

public sealed class LazyCrypto
{
    public static string Encrypt(string data)
    {
        return RSACrypto.Encrypt(RSACrypto.PublicKey, data);
    }

    public static string Decrypt(string data)
    {
        return RSACrypto.Decrypt(RSACrypto.PrivateKey, data);
    }
}

file class RSACrypto
{
    public const string PrivateKey = 
        """
        <RSAKeyValue>
            <Modulus>sPAuYecKadGFjndkqks/iIYL4/A6PrvHY6ygU5vzl+29H0mi2v5SPrdwKBRMxlDZGQXdiErNuDFfKwMxN7pN9ZI1uCxGFwbmlJ5v2+9lG01liJKLe0NPkaMMrXAdVBn8NOtOrbKmOGrUu3+2PDTkbXTuebpICA/FHitf1AcLOtk=</Modulus>
            <Exponent>AQAB</Exponent>
            <P>wu9MsBWpHBqrLFEst5NkBUDIRDj5YzTGDR6VYEwSYipTYuXPnzlmSbb0krF6IDlwiKD7MTUmUtoOihnCwggHhw==</P>
            <Q>6F2oCHB3Cbr+73gdUqqX2cuUxgmgNvb9emWOokEZKPVY98B+cwX8ZQpMBLRzYhAd6rknzt7YxxqZvHJSZOSCnw==</Q>
            <DP>B8vEOGKVYc9bOyl/7VOSs3cUy02wA5yvswSfGrvQHGbu8MyaEhcclSQhAeDhLnq9mmk9wokKoFOiMzm2hUYG7w==</DP>
            <DQ>w5oTWQdTLV28PNlf1bbFHRHCqvJva9V+iwnyh6NFCrV9rxDbYPgo/uUsGMnOVz458NzsJbhxwykhQQD7WvMBAQ==</DQ>
            <InverseQ>HUDf85yD8qJKMaXJexV4itjCIAUBiPxyTeYGiH21LvFYnvjUGrZwdAVtBFl9rbfh+C8bLiXaEw4Wahqz+Ut8DQ==</InverseQ>
            <D>eQkjwWcJsY9ZHDbCDtgAswR3V8p6HXXYlG9/ERKqVLCVYbS+ia3VVv+m4lPYnQSrLbncbCI3jZuAbWz2mxd/waugaCtjEp+tMa6EAWNPGuoiZ2wH5w4L5y7gfJoNNiFb7LN/6kJQdj782GPYo6z70jdDYPxpIgP9QNhynKy1cc0=</D>
        </RSAKeyValue>
        """;
    public const string PublicKey = 
        """
        <RSAKeyValue>
            <Modulus>sPAuYecKadGFjndkqks/iIYL4/A6PrvHY6ygU5vzl+29H0mi2v5SPrdwKBRMxlDZGQXdiErNuDFfKwMxN7pN9ZI1uCxGFwbmlJ5v2+9lG01liJKLe0NPkaMMrXAdVBn8NOtOrbKmOGrUu3+2PDTkbXTuebpICA/FHitf1AcLOtk=</Modulus>
            <Exponent>AQAB</Exponent>
        </RSAKeyValue>
        """;

    public static void CreateKeys(out string privateKey, out string publicKey)
    {
        RSACryptoServiceProvider rsa = new();

        privateKey = rsa.ToXmlString(true);
        publicKey = rsa.ToXmlString(false);
    }

    public static string Encrypt(string publicKey, string data)
    {
        RSACryptoServiceProvider rsa = new();

        rsa.FromXmlString(publicKey);
        return Convert.ToBase64String(rsa.Encrypt(Encoding.UTF8.GetBytes(data), false));
    }

    public static string Decrypt(string privateKey, string data)
    {
        RSACryptoServiceProvider rsa = new();

        rsa.FromXmlString(privateKey);
        return Encoding.UTF8.GetString(rsa.Decrypt(Convert.FromBase64String(data), false));
    }

    public static string Sign(string privateKey, string data)
    {
        RSACryptoServiceProvider rsa = new();

        rsa.FromXmlString(privateKey);
        return Convert.ToBase64String(rsa.SignData(Encoding.UTF8.GetBytes(data), "SHA1"));
    }

    public static bool Verify(string publicKey, string data, string signature)
    {
        RSACryptoServiceProvider rsa = new();

        rsa.FromXmlString(publicKey);
        return rsa.VerifyData(Encoding.UTF8.GetBytes(signature), "SHA1", Convert.FromBase64String(data));
    }
}
