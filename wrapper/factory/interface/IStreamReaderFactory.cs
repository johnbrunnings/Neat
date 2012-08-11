using System.IO;
using neat.wrapper.parent;

namespace neat.wrapper.factory.@interface
{
    public interface IStreamReaderFactory
    {
        StreamReaderBase Create(Stream stream);
    }
}