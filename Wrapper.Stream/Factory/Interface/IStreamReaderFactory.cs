using Neat.Wrapper.Stream.Abstract;

namespace Neat.Wrapper.Stream.Factory.Interface
{
    public interface IStreamReaderFactory
    {
        StreamReaderBase Create(System.IO.Stream stream);
    }
}