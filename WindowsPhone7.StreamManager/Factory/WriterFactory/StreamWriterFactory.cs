using System;
using System.IO;
using Neat.WindowsPhone7.StreamManager.Factory.WriterFactory.Interface;
using Neat.WindowsPhone7.StreamManager.Factory.WriterFactory.Parameters;
using Neat.WindowsPhone7.StreamManager.Factory.WriterFactory.Parameters.Abstract;

namespace Neat.WindowsPhone7.StreamManager.Factory.WriterFactory
{
    public class StreamWriterFactory : ITextWriterFactory
    {
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
    }
}