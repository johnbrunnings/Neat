using System;
using System.IO;
using System.Security.Cryptography;
using Neat.StreamManager.Factory.StreamFactory.Interface;
using Neat.StreamManager.Factory.StreamFactory.Parameters;
using Neat.StreamManager.Factory.StreamFactory.Parameters.Abstract;
using Neat.Wrapper.Stream.Abstract;
using Neat.Wrapper.Stream.Factory.Interface;

namespace Neat.StreamManager.Factory.StreamFactory
{
    public class CryptoStreamFactory : IStreamFactory, ICryptoStreamFactory
    {
        private readonly ICryptoStreamFactory _cryptoStreamFactory;

        public CryptoStreamFactory(ICryptoStreamFactory cryptoStreamFactory)
        {
            _cryptoStreamFactory = cryptoStreamFactory;
        }

        public StreamType StreamType { get { return StreamType.CryptoStream; } }

        public Stream Create(StreamParameters streamParameters)
        {
            if(streamParameters != null)
            {
                var cryptoStreamParameters = streamParameters as CryptoStreamParameters;
                
                if(cryptoStreamParameters != null && cryptoStreamParameters.Stream != null && cryptoStreamParameters.CryptoTransform != null)
                {
                    return _cryptoStreamFactory.Create(cryptoStreamParameters.Stream, cryptoStreamParameters.CryptoTransform, cryptoStreamParameters.CryptoStreamMode);
                }

                throw new ArgumentException("StreamParameters must be of type CryptoStreamParameters with Stream and CryptoTransform, no default constructor exists!");
            }

            throw new ArgumentNullException("streamParameters");
        }

        public CryptoStreamBase Create(Stream stream, ICryptoTransform cryptoTransform, CryptoStreamMode cryptoStreamMode)
        {
            return _cryptoStreamFactory.Create(stream, cryptoTransform, cryptoStreamMode);
        }
    }
}