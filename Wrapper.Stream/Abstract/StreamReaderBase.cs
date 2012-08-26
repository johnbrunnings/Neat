using System.IO;
using System.Text;

namespace Neat.Wrapper.Stream.Abstract
{
    public abstract class StreamReaderBase : TextReader
    {
        public abstract void DiscardBufferedData();
        public abstract Encoding CurrentEncoding { get; }
        public abstract System.IO.Stream BaseStream { get; }
        public abstract bool EndOfStream { get; }
    }
}