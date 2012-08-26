using System.IO;
using Neat.StreamManager.Factory.ReaderFactory.Parameters.Abstract;
using Neat.StreamManager.Factory.StreamFactory.Parameters.Abstract;
using Neat.StreamManager.Factory.WriterFactory.Parameters.Abstract;

namespace Neat.StreamManager.Interface
{
    public interface IStreamManager
    {
        Stream GetStream(StreamType streamType, StreamParameters streamParameters);
        TextReader GetTextReader(TextReaderType textReaderType, TextReaderParameters textReaderParameters);
        TextWriter GetTextWriter(TextWriterType textWriterType, TextWriterParameters textReaderParameters);
    }
}