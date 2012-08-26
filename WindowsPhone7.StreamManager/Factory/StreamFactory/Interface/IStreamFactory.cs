using System.IO;
using Neat.WindowsPhone7.StreamManager.Factory.StreamFactory.Parameters.Abstract;

namespace Neat.WindowsPhone7.StreamManager.Factory.StreamFactory.Interface
{
    public interface IStreamFactory
    {
        StreamType StreamType { get; }
        Stream Create(StreamParameters streamParameters);
    }
}