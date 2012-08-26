using System;
using System.IO;
using Neat.StreamManager.Factory.ReaderFactory.Interface;
using Neat.StreamManager.Factory.ReaderFactory.Parameters;
using Neat.StreamManager.Factory.ReaderFactory.Parameters.Abstract;
using Neat.Wrapper.Stream.Abstract;
using Neat.Wrapper.Stream.Factory.Interface;

namespace Neat.StreamManager.Factory.ReaderFactory
{
    public class StreamReaderFactory : ITextReaderFactory, IStreamReaderFactory
    {
        private readonly IStreamReaderFactory _streamReaderFactory;

        public StreamReaderFactory(IStreamReaderFactory streamReaderFactory)
        {
            _streamReaderFactory = streamReaderFactory;
        }

        public TextReaderType TextReaderType { get { return TextReaderType.StreamReader; } }

        public TextReader Create(TextReaderParameters textReaderParameters)
        {
            if (textReaderParameters != null)
            {
                var streamReaderParameters = textReaderParameters as StreamReaderParameters;

                if (streamReaderParameters != null)
                {
                    return _streamReaderFactory.Create(streamReaderParameters.Stream);
                }

                throw new ArgumentException("TextReaderParameters must be of type StreamReaderParameters, no default constructor defined!");
            }

            throw new ArgumentNullException("textReaderParameters");
        }

        public StreamReaderBase Create(Stream stream)
        {
            return _streamReaderFactory.Create(stream);
        }
    }
}