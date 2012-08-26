using Neat.WindowsPhone7.Wrapper.Stream.Abstract;

namespace Neat.WindowsPhone7.Wrapper.Stream.Factory.Interface
{
    public interface IStreamReaderFactory
    {
        StreamReaderBase Create(System.IO.Stream stream);
    }
}