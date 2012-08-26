using System.IO;

namespace Neat.Wrapper.Stream.Abstract
{
    public abstract class StreamWriterBase : TextWriter
    {
        public abstract bool AutoFlush { get; set; }
        public abstract System.IO.Stream BaseStream { get; }
    }
}