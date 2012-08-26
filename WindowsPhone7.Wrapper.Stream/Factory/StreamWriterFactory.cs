using System.IO;
using Neat.WindowsPhone7.Wrapper.Stream.Abstract;
using Neat.WindowsPhone7.Wrapper.Stream.Factory.Interface;

namespace Neat.WindowsPhone7.Wrapper.Stream.Factory
{
    public class StreamWriterFactory : IStreamWriterFactory
    {
        public StreamWriterBase Create(System.IO.Stream stream)
        {
            return new StreamWriterWrapper(new StreamWriter(stream));
        }
    }
}