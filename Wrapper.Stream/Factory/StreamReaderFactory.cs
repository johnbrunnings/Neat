using System.IO;
using Neat.Wrapper.Stream.Abstract;
using Neat.Wrapper.Stream.Factory.Interface;

namespace Neat.Wrapper.Stream.Factory
{
    public class StreamReaderFactory : IStreamReaderFactory
    {
        public StreamReaderBase Create(System.IO.Stream stream)
        {
            return new StreamReaderWrapper(new StreamReader(stream));
        }
    }
}