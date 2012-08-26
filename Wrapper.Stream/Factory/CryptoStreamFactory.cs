using System.Security.Cryptography;
using Neat.Wrapper.Stream.Abstract;
using Neat.Wrapper.Stream.Factory.Interface;

namespace Neat.Wrapper.Stream.Factory
{
    public class CryptoStreamFactory : ICryptoStreamFactory
    {
        public CryptoStreamBase Create(System.IO.Stream stream, ICryptoTransform cryptoTransform, CryptoStreamMode cryptoStreamMode)
        {
            return new CryptoStreamWrapper(new CryptoStream(stream, cryptoTransform, cryptoStreamMode));
        }
    }
}