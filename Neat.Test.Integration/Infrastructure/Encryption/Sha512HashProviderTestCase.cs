using System;
using Neat.Infrastructure.Config;
using Neat.Infrastructure.Encryption;
using NUnit.Framework;

namespace Neat.Test.Integration.Infrastructure.Encryption
{
    [TestFixture]
    public class Sha512HashProviderTestCase
    {
        private Sha512HashProvider sha512HashProvider;

        [SetUp]
        public void Setup()
        {
            sha512HashProvider = new Sha512HashProvider(new EncryptionContext(new MsXmlConfig()));
        }

        [Test]
        public void TestEncryptDecrypt()
        {
            var result1 = sha512HashProvider.Hash("Test");
            Console.WriteLine(result1);

            Assert.IsTrue(result1.Length > 0);
        }
    }
}