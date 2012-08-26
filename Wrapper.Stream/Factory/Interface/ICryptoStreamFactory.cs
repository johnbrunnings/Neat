using System.Security.Cryptography;
using Neat.Wrapper.Stream.Abstract;

namespace Neat.Wrapper.Stream.Factory.Interface
{
    public interface ICryptoStreamFactory
    {
        CryptoStreamBase Create(System.IO.Stream stream, ICryptoTransform cryptoTransform, CryptoStreamMode cryptoStreamMode);
    }
}