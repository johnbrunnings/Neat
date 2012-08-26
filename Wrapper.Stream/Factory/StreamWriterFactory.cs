using System.IO;
using Neat.Wrapper.Stream.Abstract;
using Neat.Wrapper.Stream.Factory.Interface;

namespace Neat.Wrapper.Stream.Factory
{
    public class StreamWriterFactory : IStreamWriterFactory
    {
        public StreamWriterBase Create(System.IO.Stream stream)
        {
            return new StreamWriterWrapper(new StreamWriter(stream));
        }
    }
}