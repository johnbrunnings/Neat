using System.IO;
using System.Security.Cryptography;
using Neat.StreamManager.Factory.StreamFactory.Parameters.Abstract;

namespace Neat.StreamManager.Factory.StreamFactory.Parameters
{
    public class CryptoStreamParameters : StreamParameters
    {
        public Stream Stream { get; set; }
        public ICryptoTransform CryptoTransform { get; set; }
        public CryptoStreamMode CryptoStreamMode { get; set; }
    }
}