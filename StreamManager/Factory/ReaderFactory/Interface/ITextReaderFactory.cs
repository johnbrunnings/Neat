using System.IO;
using Neat.StreamManager.Factory.ReaderFactory.Parameters.Abstract;

namespace Neat.StreamManager.Factory.ReaderFactory.Interface
{
    public interface ITextReaderFactory
    {
        TextReaderType TextReaderType { get; }
        TextReader Create(TextReaderParameters textReaderParameters);
    }
}