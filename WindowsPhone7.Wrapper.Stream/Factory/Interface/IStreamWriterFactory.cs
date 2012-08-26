using Neat.WindowsPhone7.Wrapper.Stream.Abstract;

namespace Neat.WindowsPhone7.Wrapper.Stream.Factory.Interface
{
    public interface IStreamWriterFactory
    {
        StreamWriterBase Create(System.IO.Stream stream);
    }
}