using System.Security.Cryptography;
using Neat.WindowsPhone7.Wrapper.Stream.Abstract;
using Neat.WindowsPhone7.Wrapper.Stream.Factory.Interface;

namespace Neat.WindowsPhone7.Wrapper.Stream.Factory
{
    public class CryptoStreamFactory : ICryptoStreamFactory
    {
        public CryptoStreamBase Create(System.IO.Stream stream, ICryptoTransform cryptoTransform, CryptoStreamMode cryptoStreamMode)
        {
            return new CryptoStreamWrapper(new CryptoStream(stream, cryptoTransform, cryptoStreamMode));
        }
    }
}