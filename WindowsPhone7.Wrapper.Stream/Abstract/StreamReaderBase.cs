using System.IO;
using System.Text;

namespace Neat.WindowsPhone7.Wrapper.Stream.Abstract
{
    public abstract class StreamReaderBase : TextReader
    {
        public abstract Encoding CurrentEncoding { get; }
        public abstract System.IO.Stream BaseStream { get; }
        public abstract bool EndOfStream { get; }
        public abstract void DiscardBufferedData();
    }
}