using System.IO;
using Neat.WindowsPhone7.StreamManager.Factory.ReaderFactory.Parameters.Abstract;

namespace Neat.WindowsPhone7.StreamManager.Factory.ReaderFactory.Parameters
{
    public class StreamReaderParameters : TextReaderParameters
    {
        public Stream Stream { get; set; }
    }
}