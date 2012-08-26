using System.IO;
using Neat.WindowsPhone7.StreamManager.Factory.ReaderFactory.Parameters.Abstract;

namespace Neat.WindowsPhone7.StreamManager.Factory.ReaderFactory.Interface
{
    public interface ITextReaderFactory
    {
        TextReaderType TextReaderType { get; }
        TextReader Create(TextReaderParameters textReaderParameters);
    }
}