using System;
using System.IO;
using System.Security.Cryptography;
using Neat.WindowsPhone7.StreamManager.Factory.StreamFactory.Interface;
using Neat.WindowsPhone7.StreamManager.Factory.StreamFactory.Parameters;
using Neat.WindowsPhone7.StreamManager.Factory.StreamFactory.Parameters.Abstract;

namespace Neat.WindowsPhone7.StreamManager.Factory.StreamFactory
{
    public class CryptoStreamFactory : IStreamFactory
    {
        public StreamType StreamType { get { return StreamType.CryptoStream; } }

        public Stream Create(StreamParameters streamParameters)
        {
            if(streamParameters != null)
            {
                var cryptoStreamParameters = streamParameters as CryptoStreamParameters;
                
                if(cryptoStreamParameters != null && cryptoStreamParameters.Stream != null && cryptoStreamParameters.CryptoTransform != null)
                {
                    return new CryptoStream(cryptoStreamParameters.Stream, cryptoStreamParameters.CryptoTransform, cryptoStreamParameters.CryptoStreamMode);
                }

                throw new ArgumentException("StreamParameters must be of type CryptoStreamParameters with Stream and CryptoTransform, no default constructor exists!");
            }

            throw new ArgumentNullException("streamParameters");
        }
    }
}