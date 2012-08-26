using System.IO;
using Neat.WindowsPhone7.StreamManager.Factory.ReaderFactory.Parameters.Abstract;
using Neat.WindowsPhone7.StreamManager.Factory.StreamFactory.Parameters.Abstract;
using Neat.WindowsPhone7.StreamManager.Factory.WriterFactory.Parameters.Abstract;

namespace Neat.WindowsPhone7.StreamManager.Interface
{
    public interface IStreamManager
    {
        Stream GetStream(StreamType streamType, StreamParameters streamParameters);
        TextReader GetTextReader(TextReaderType textReaderType, TextReaderParameters textReaderParameters);
        TextWriter GetTextWriter(TextWriterType textWriterType, TextWriterParameters textReaderParameters);
    }
}