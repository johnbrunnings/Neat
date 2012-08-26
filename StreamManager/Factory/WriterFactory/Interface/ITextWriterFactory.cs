using System.IO;
using Neat.StreamManager.Factory.WriterFactory.Parameters.Abstract;

namespace Neat.StreamManager.Factory.WriterFactory.Interface
{
    public interface ITextWriterFactory
    {
        TextWriterType TextWriterType { get; }
        TextWriter Create(TextWriterParameters textWriterParameters);
    }
}