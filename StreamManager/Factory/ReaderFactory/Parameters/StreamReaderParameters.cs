using System.IO;
using Neat.StreamManager.Factory.ReaderFactory.Parameters.Abstract;

namespace Neat.StreamManager.Factory.ReaderFactory.Parameters
{
    public class StreamReaderParameters : TextReaderParameters
    {
        public Stream Stream { get; set; }
    }
}