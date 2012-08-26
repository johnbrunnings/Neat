using System;
using Neat.Encryption.Strategy.Interface;

namespace Neat.Encryption.Strategy
{
    public class AesEncryptionStrategy : IEncryptionStrategy
    {
        public EncryptionMethod EncryptionMethod
        {
            get { return EncryptionMethod.Aes; }
        }

        public byte[] Encrypt(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException();
            }
            if(data.Length <= 0)
            {
                throw new ArgumentException();
            }

            return data;
        }

        public byte[] Decrypt(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException();
            }
            if (data.Length <= 0)
            {
                throw new ArgumentException();
            }

            return data;
        }
    }
}