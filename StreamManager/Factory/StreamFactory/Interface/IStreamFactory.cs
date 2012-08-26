using System.IO;
using Neat.StreamManager.Factory.StreamFactory.Parameters.Abstract;

namespace Neat.StreamManager.Factory.StreamFactory.Interface
{
    public interface IStreamFactory
    {
        StreamType StreamType { get; }
        Stream Create(StreamParameters streamParameters);
    }
}