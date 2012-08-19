using System.IO;
using Neat.Wrapper.Abstract;
using Neat.Wrapper.Factory.Interface;

namespace Neat.Wrapper.Factory
{
    public class StreamReaderFactory : IStreamReaderFactory
    {
         public StreamReaderBase Create(Stream stream)
         {
             return new StreamReaderWrapper(new StreamReader(stream));
         }
    }
}