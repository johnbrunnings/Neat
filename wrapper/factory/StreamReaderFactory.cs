using System.IO;
using neat.wrapper.factory.@interface;
using neat.wrapper.parent;

namespace neat.wrapper.factory
{
    public class StreamReaderFactory : IStreamReaderFactory
    {
         public StreamReaderBase Create(Stream stream)
         {
             return new StreamReaderWrapper(new StreamReader(stream));
         }
    }
}