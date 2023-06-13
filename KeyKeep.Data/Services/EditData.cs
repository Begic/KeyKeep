using System.Security.Cryptography;

namespace KeyKeep.Data.Services;

public static class EditData
{
    public static byte[] Encrypt(string inputValue)
    {
        byte[] encrypted;
        using (var aes = new AesCryptoServiceProvider())
        {
            aes.Key = new byte[32];

            aes.GenerateIV();

            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (var msEncrypt = new MemoryStream())
            {
                msEncrypt.Write(aes.IV, 0, aes.IV.Length);
                var encoder = aes.CreateEncryptor();
                using (var csEncrypt = new CryptoStream(msEncrypt, encoder, CryptoStreamMode.Write))
                using (var swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(inputValue.Reverse());
                }
                encrypted = msEncrypt.ToArray();
            }
        }
        return encrypted;
    }

    public static string Decrypt(byte[] inputValue)
    {
        string decrypted;
        using (var aes = new AesCryptoServiceProvider())
        {
            aes.Key = new byte[32];
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (var msDecryptor = new MemoryStream(inputValue))
            {
                var readIV = new byte[16];
                msDecryptor.Read(readIV, 0, 16);
                aes.IV = readIV;
                var decoder = aes.CreateDecryptor();
                using (var csDecryptor = new CryptoStream(msDecryptor, decoder, CryptoStreamMode.Read))
                using (var srReader = new StreamReader(csDecryptor))
                {
                    decrypted = srReader.ReadToEnd();
                }
            }
        }
        return decrypted.Reverse().ToString();
    }
}