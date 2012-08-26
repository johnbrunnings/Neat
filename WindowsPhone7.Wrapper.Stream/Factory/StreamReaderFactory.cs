using System.IO;
using Neat.WindowsPhone7.Wrapper.Stream.Abstract;
using Neat.WindowsPhone7.Wrapper.Stream.Factory.Interface;

namespace Neat.WindowsPhone7.Wrapper.Stream.Factory
{
    public class StreamReaderFactory : IStreamReaderFactory
    {
        public StreamReaderBase Create(System.IO.Stream stream)
        {
            return new StreamReaderWrapper(new StreamReader(stream));
        }
    }
}