using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Neat.Infrastructure.Encryption
{
    public class AesEncryptionProvider : IEncryptionProvider
    {
        private readonly IEncryptionContext _encryptionContext;

        public AesEncryptionProvider(IEncryptionContext encryptionContext)
        {
            _encryptionContext = encryptionContext;
        }

        public string Encrypt(string value)
        {
            string encryptedValue;

            using (var aes = AesManaged.Create())
            {
                aes.KeySize = 256;
                aes.BlockSize = 128;
                var saltBytes = Encoding.UTF8.GetBytes(_encryptionContext.Salt);
                var password = new Rfc2898DeriveBytes(_encryptionContext.Key, saltBytes);
                var vectorBytes = password.GetBytes(aes.BlockSize / 8);
                using (var encryptor = aes.CreateEncryptor(password.GetBytes(aes.KeySize / 8), vectorBytes))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        var lockObject = new Object();

                        lock (lockObject)
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                var valueBytes = Encoding.UTF8.GetBytes(value);
                                cryptoStream.Write(valueBytes, 0, valueBytes.Length);
                                cryptoStream.FlushFinalBlock();

                                var encryptedBytes = memoryStream.ToArray();
                                encryptedValue = Convert.ToBase64String(encryptedBytes);
                            }
                        }
                    }
                }
            }

            return encryptedValue;
        }

        public string Decrypt(string encryptedValue)
        {
            var value = string.Empty;

            using (var aes = AesManaged.Create())
            {
                aes.KeySize = 256;
                aes.BlockSize = 128;
                var saltBytes = Encoding.UTF8.GetBytes(_encryptionContext.Salt);
                var password = new Rfc2898DeriveBytes(_encryptionContext.Key, saltBytes);
                var vectorBytes = password.GetBytes(aes.BlockSize/8);
                using (var decryptor = aes.CreateDecryptor(password.GetBytes(aes.KeySize / 8), vectorBytes))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        var lockObject = new Object();

                        lock (lockObject)
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
                            {
                                var encryptedValueBytes = Convert.FromBase64String(encryptedValue);
                                cryptoStream.Write(encryptedValueBytes, 0, encryptedValueBytes.Length);
                                cryptoStream.FlushFinalBlock();

                                var valueBytes = memoryStream.ToArray();
                                value = Encoding.UTF8.GetString(valueBytes);
                            }
                        }
                    }
                }
            }

            return value;
        }
    }
}