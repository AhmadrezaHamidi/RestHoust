using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Lib
{
    public static class AesCbcEncryptionService
    {
        private const int AesKeySize = 256,
            AesBlockSize = 128,
            AesSaltSize = 8,
            Pbkdf2IterationCount = 10000;
        private const CipherMode AesCipherMode = CipherMode.CBC;
        private const PaddingMode AesPaddingMode = PaddingMode.PKCS7;

        public static (byte[] Ciphertext, byte[] InitializationVector, byte[] Salt) Encrypt(string plaintext, string secret)
        {
            using (var aes = Aes.Create())
            {
                aes.Mode = AesCipherMode;
                aes.KeySize = AesKeySize;
                aes.BlockSize = AesBlockSize;
                aes.Padding = AesPaddingMode;

                aes.GenerateIV();
                var salt = RandomNumberGenerator.GetBytes(AesSaltSize);
                aes.Key = KeyDerivation.Pbkdf2(secret, salt, KeyDerivationPrf.HMACSHA512, Pbkdf2IterationCount, aes.Key.Length);

                var plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
                using (var encryptor = aes.CreateEncryptor())
                using (var input = new MemoryStream(plaintextBytes))
                using (var cryptoStream = new CryptoStream(input, encryptor, CryptoStreamMode.Read))
                using (var output = new MemoryStream())
                {
                    cryptoStream.CopyTo(output);
                    var ciphertext = output.ToArray();
                    return (ciphertext, aes.IV, salt);
                }
            }
        }

        public static string Decrypt(byte[] ciphertext, byte[] initializationVector, byte[] salt, string secret)
        {
            using (var aes = Aes.Create())
            {
                aes.Mode = AesCipherMode;
                aes.KeySize = AesKeySize;
                aes.BlockSize = AesBlockSize;
                aes.Padding = AesPaddingMode;

                aes.IV = initializationVector;
                aes.Key = KeyDerivation.Pbkdf2(secret, salt, KeyDerivationPrf.HMACSHA512, Pbkdf2IterationCount, aes.Key.Length);

                using (var decryptor = aes.CreateDecryptor())
                using (var input = new MemoryStream(ciphertext))
                using (var cryptoStream = new CryptoStream(input, decryptor, CryptoStreamMode.Read))
                using (var output = new MemoryStream())
                {
                    cryptoStream.CopyTo(output);
                    var plaintextBytes = output.ToArray();
                    var plaintext = Encoding.UTF8.GetString(plaintextBytes);
                    return plaintext;
                }
            }
        }

    }
}