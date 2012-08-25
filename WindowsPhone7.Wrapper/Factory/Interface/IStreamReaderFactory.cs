using System.IO;
using Neat.WindowsPhone7.Wrapper.Abstract;

namespace Neat.WindowsPhone7.Wrapper.Factory.Interface
{
    public interface IStreamReaderFactory
    {
        StreamReaderBase Create(Stream stream);
    }
}