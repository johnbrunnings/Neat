using System;
using System.Security.Cryptography;
using System.Text;

namespace Neat.Infrastructure.Encryption
{
    public class Sha512HashProvider : IHashProvider
    {
        private readonly IEncryptionContext _encryptionContext;

        public Sha512HashProvider(IEncryptionContext encryptionContext)
        {
            _encryptionContext = encryptionContext;
        }

        public string Hash(string value)
        {
            string hashedValue;
            var saltedValue = string.Format("{0}{1}", _encryptionContext.Salt, value);

            using (var sha512 = SHA512.Create())
            {
                var result = sha512.ComputeHash(Encoding.UTF8.GetBytes(saltedValue));
                hashedValue = Convert.ToBase64String(result);
            }

            return hashedValue;
        }
    }
}