using System.IO;
using Neat.WindowsPhone7.StreamManager.Factory.WriterFactory.Parameters.Abstract;

namespace Neat.WindowsPhone7.StreamManager.Factory.WriterFactory.Interface
{
    public interface ITextWriterFactory
    {
        TextWriterType TextWriterType { get; }
        TextWriter Create(TextWriterParameters textWriterParameters);
    }
}