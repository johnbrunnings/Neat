using System.IO;
using Neat.Wrapper.Abstract;

namespace Neat.Wrapper.Factory.Interface
{
    public interface IStreamReaderFactory
    {
        StreamReaderBase Create(Stream stream);
    }
}