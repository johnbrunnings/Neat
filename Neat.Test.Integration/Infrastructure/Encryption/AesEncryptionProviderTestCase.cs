using System;
using Neat.Infrastructure.Config;
using Neat.Infrastructure.Encryption;
using NUnit.Framework;

namespace Neat.Test.Integration.Infrastructure.Encryption
{
    [TestFixture]
    public class AesEncryptionProviderTestCase
    {
        private AesEncryptionProvider aesEncryptionProvider;

        [SetUp]
        public void Setup()
        {
            aesEncryptionProvider = new AesEncryptionProvider(new EncryptionContext(new MsXmlConfig()));
        }

        [Test]
        public void TestEncryptDecrypt()
        {
            var result1 = aesEncryptionProvider.Encrypt("Test");
            Console.WriteLine(result1);
            var result2 = aesEncryptionProvider.Decrypt(result1);
            Console.WriteLine(result2);

            Assert.AreEqual("Test", result2);
        }
    }
}