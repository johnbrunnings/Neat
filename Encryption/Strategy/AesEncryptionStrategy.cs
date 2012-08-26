using System;
using System.Security.Cryptography;
using Neat.Encryption.Parameters;
using Neat.Encryption.Parameters.Abstract;
using Neat.Encryption.ProviderFactory.Interface;
using Neat.Encryption.Strategy.Interface;
using Neat.Wrapper.Stream.Factory.Interface;

namespace Neat.Encryption.Strategy
{
    public class AesEncryptionStrategy : IEncryptionStrategy
    {
        private readonly IAesCryptoServiceProviderFactory _aesCryptoServiceProviderFactory;
        private readonly IMemoryStreamFactory _memoryStreamFactory;
        private readonly ICryptoStreamFactory _cryptoStreamFactory;

        public AesEncryptionStrategy(IAesCryptoServiceProviderFactory aesCryptoServiceProviderFactory, IMemoryStreamFactory memoryStreamFactory, ICryptoStreamFactory cryptoStreamFactory)
        {
            _aesCryptoServiceProviderFactory = aesCryptoServiceProviderFactory;
            _memoryStreamFactory = memoryStreamFactory;
            _cryptoStreamFactory = cryptoStreamFactory;
        }

        public EncryptionMethod EncryptionMethod
        {
            get { return EncryptionMethod.Aes; }
        }

        public byte[] Encrypt(EncryptionParameters encryptionParameters)
        {
            if (encryptionParameters == null)
            {
                throw new ArgumentNullException();
            }
            var aesEncryptionParameters = encryptionParameters as AesEncryptionParameters;
            if (aesEncryptionParameters == null)
            {
                throw new ArgumentException("EncryptionParameters must be of type AesEncryptionParameters!");
            }

            byte[] returnValue;
            var aes = _aesCryptoServiceProviderFactory.Create();
            var cryptoTransform = aes.CreateEncryptor(aesEncryptionParameters.Key, aesEncryptionParameters.Vector);

            using (var memoryStream = _memoryStreamFactory.Create())
            {
                using (var cryptoStream = _cryptoStreamFactory.Create(memoryStream, cryptoTransform, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(aesEncryptionParameters.Data, 0, aesEncryptionParameters.Data.Length);
                }

                returnValue = memoryStream.ToArray();
            }

            aes.Clear();

            return returnValue;
        }

        public byte[] Decrypt(EncryptionParameters encryptionParameters)
        {
            if (encryptionParameters == null)
            {
                throw new ArgumentNullException();
            }
            var aesEncryptionParameters = encryptionParameters as AesEncryptionParameters;
            if (aesEncryptionParameters == null)
            {
                throw new ArgumentException("EncryptionParameters must be of type AesEncryptionParameters!");
            }

            byte[] returnValue;
            var aes = _aesCryptoServiceProviderFactory.Create();
            var cryptoTransform = aes.CreateDecryptor(aesEncryptionParameters.Key, aesEncryptionParameters.Vector);

            using (var memoryStream = _memoryStreamFactory.Create())
            {
                using (var cryptoStream = _cryptoStreamFactory.Create(memoryStream, cryptoTransform, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(aesEncryptionParameters.Data, 0, aesEncryptionParameters.Data.Length);
                }

                returnValue = memoryStream.ToArray();
            }

            return returnValue;
        }
    }
}