using System.Security.Cryptography;

namespace KeyKeep.Data.Services;

public static class EditData
{
    public static  byte[] Encrypt(string stringInput, byte[] key)
    {
        byte[] encrypted;
        using (var aes = new AesCryptoServiceProvider())
        {
            aes.Key = key;

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
                    swEncrypt.Write(stringInput);
                }
                encrypted = msEncrypt.ToArray();
            }
        }
        return encrypted;
    }

    public static string Decrypt(byte[] byteInput, byte[] key)
    {
        string decrypted;
        using (var aes = new AesCryptoServiceProvider())
        {
            aes.Key = key;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (var msDecryptor = new MemoryStream(byteInput))
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
        return decrypted;
    }
}