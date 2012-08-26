using Neat.Wrapper.Stream.Abstract;

namespace Neat.Wrapper.Stream.Factory.Interface
{
    public interface IStreamWriterFactory
    {
        StreamWriterBase Create(System.IO.Stream stream);
    }
}