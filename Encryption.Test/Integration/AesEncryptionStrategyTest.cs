using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neat.Encryption.Parameters;
using Neat.Encryption.ProviderFactory;
using Neat.Encryption.ProviderFactory.Interface;
using Neat.Encryption.Strategy;
using Neat.Wrapper.Stream.Factory;
using Neat.Wrapper.Stream.Factory.Interface;

namespace Neat.Encryption.Test.Integration
{
    [TestClass]
    public class AesEncryptionStrategyTest
    {
        private AesEncryptionStrategy _testAesEncryptionStrategy;
        private IAesCryptoServiceProviderFactory _aesCryptoServiceProviderFactory;
        private IMemoryStreamFactory _memoryStreamFactory;
        private ICryptoStreamFactory _cryptoStreamFactory;
        private UTF8Encoding _encoding;
        private byte[] _key;
        private byte[] _iv;

        [TestInitialize] 
        public void Initialize()
        {
            _aesCryptoServiceProviderFactory = new AesCryptoServiceProviderFactory();
            _memoryStreamFactory = new MemoryStreamFactory();
            _cryptoStreamFactory = new CryptoStreamFactory();
            _testAesEncryptionStrategy = new AesEncryptionStrategy(_aesCryptoServiceProviderFactory, _memoryStreamFactory, _cryptoStreamFactory);

            _encoding = new UTF8Encoding();
            byte[] bSalt;
            Rfc2898DeriveBytes oRFC2898_Key;
            Rfc2898DeriveBytes oRFC2898_IV;

            bSalt = Encoding.UTF8.GetBytes("SALTSALT");
            oRFC2898_Key = new Rfc2898DeriveBytes("PASSWORD", bSalt);
            oRFC2898_IV = new Rfc2898DeriveBytes("PASSWORDSALTSALT", bSalt);

            _key = oRFC2898_Key.GetBytes(32);
            _iv = oRFC2898_IV.GetBytes(16);
        }

        [TestMethod]
        public void RunTestEncrypt()
        {
            var aesEncryptionParameters = new AesEncryptionParameters()
            {
                Data = _encoding.GetBytes("TestData"),
                Key = _key,
                Vector = _iv
            };
            var result = _testAesEncryptionStrategy.Encrypt(aesEncryptionParameters);
            Console.WriteLine(Convert.ToBase64String(result));
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void RunTestDecrypt()
        {
            var aesEncryptionParameters = new AesEncryptionParameters()
            {
                Data = Convert.FromBase64String("I6w2xFTXkmShewSy7Pd1Qw=="),
                Key = _key,
                Vector = _iv
            };
            var result = _testAesEncryptionStrategy.Decrypt(aesEncryptionParameters);
            Console.WriteLine(_encoding.GetString(result));
            Assert.IsNotNull(result);
        }
    }
}