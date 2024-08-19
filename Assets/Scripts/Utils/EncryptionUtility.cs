using System;
using System.Security.Cryptography;
using System.Text;

namespace Autoclicker.Scripts.Utils
{
    public class EncryptionUtility
    {
        private static readonly string key = "jUjozyPOULm1YJ5qAJxJK34ZqqNYwTnj";

        public static string Encrypt(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                Array.Resize(ref keyBytes, aesAlg.KeySize / 8);

                aesAlg.Key = keyBytes;
                aesAlg.GenerateIV();

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (var msEncrypt = new System.IO.MemoryStream())
                {
                    msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);

                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            byte[] fullCipher = Convert.FromBase64String(cipherText);

            using (Aes aesAlg = Aes.Create())
            {
                byte[] iv = new byte[aesAlg.BlockSize / 8];
                byte[] cipherBytes = new byte[fullCipher.Length - iv.Length];

                Array.Copy(fullCipher, iv, iv.Length);
                Array.Copy(fullCipher, iv.Length, cipherBytes, 0, cipherBytes.Length);

                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (var msDecrypt = new System.IO.MemoryStream(cipherBytes))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }

    }
}