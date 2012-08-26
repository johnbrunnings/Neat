using System.Security.Cryptography;
using Neat.WindowsPhone7.Wrapper.Stream.Abstract;

namespace Neat.WindowsPhone7.Wrapper.Stream.Factory.Interface
{
    public interface ICryptoStreamFactory
    {
        CryptoStreamBase Create(System.IO.Stream stream, ICryptoTransform cryptoTransform, CryptoStreamMode cryptoStreamMode);
    }
}