using System.IO;
using Neat.WindowsPhone7.Wrapper.Abstract;
using Neat.WindowsPhone7.Wrapper.Factory.Interface;

namespace Neat.WindowsPhone7.Wrapper.Factory
{
    public class StreamReaderFactory : IStreamReaderFactory
    {
         public StreamReaderBase Create(Stream stream)
         {
             return new StreamReaderWrapper(new StreamReader(stream));
         }
    }
}