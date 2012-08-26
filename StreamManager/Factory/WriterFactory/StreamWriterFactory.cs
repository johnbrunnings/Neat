using System;
using System.IO;
using Neat.StreamManager.Factory.WriterFactory.Interface;
using Neat.StreamManager.Factory.WriterFactory.Parameters;
using Neat.StreamManager.Factory.WriterFactory.Parameters.Abstract;
using Neat.Wrapper.Stream.Abstract;
using Neat.Wrapper.Stream.Factory.Interface;

namespace Neat.StreamManager.Factory.WriterFactory
{
    public class StreamWriterFactory : ITextWriterFactory, IStreamWriterFactory
    {
        private readonly IStreamWriterFactory _streamWriterFactory;

        public StreamWriterFactory(IStreamWriterFactory streamWriterFactory)
        {
            _streamWriterFactory = streamWriterFactory;
        }

        public TextWriterType TextWriterType { get { return TextWriterType.StreamWriter; } }

        public TextWriter Create(TextWriterParameters textWriterParameters)
        {
            if (textWriterParameters != null)
            {
                var streamWriterParameters = textWriterParameters as StreamWriterParameters;

                if (streamWriterParameters != null)
                {
                    return new StreamWriter(streamWriterParameters.Stream);
                }

                throw new ArgumentException("TextWriterParameters must be of type StreamWriterParameters, no default constructor defined!");
            }

            throw new ArgumentNullException("textWriterParameters");
        }

        public StreamWriterBase Create(Stream stream)
        {
            return _streamWriterFactory.Create(stream);
        }
    }
}