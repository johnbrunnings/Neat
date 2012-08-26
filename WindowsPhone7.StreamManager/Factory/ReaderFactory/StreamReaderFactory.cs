using System;
using System.IO;
using Neat.WindowsPhone7.StreamManager.Factory.ReaderFactory.Interface;
using Neat.WindowsPhone7.StreamManager.Factory.ReaderFactory.Parameters;
using Neat.WindowsPhone7.StreamManager.Factory.ReaderFactory.Parameters.Abstract;

namespace Neat.WindowsPhone7.StreamManager.Factory.ReaderFactory
{
    public class StreamReaderFactory : ITextReaderFactory
    {
        public TextReaderType TextReaderType { get { return TextReaderType.StreamReader; } }

        public TextReader Create(TextReaderParameters textReaderParameters)
        {
            if (textReaderParameters != null)
            {
                var streamReaderParameters = textReaderParameters as StreamReaderParameters;

                if (streamReaderParameters != null)
                {
                    return new StreamReader(streamReaderParameters.Stream);
                }

                throw new ArgumentException("TextReaderParameters must be of type StreamReaderParameters, no default constructor defined!");
            }

            throw new ArgumentNullException("textReaderParameters");
        }
    }
}