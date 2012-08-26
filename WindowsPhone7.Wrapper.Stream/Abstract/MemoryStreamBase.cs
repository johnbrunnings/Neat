namespace Neat.WindowsPhone7.Wrapper.Stream.Abstract
{
    public abstract class MemoryStreamBase : System.IO.Stream
    {
        public abstract int Capacity { get; set; }
        public abstract byte[] GetBuffer();
        public abstract byte[] ToArray();
        public abstract void WriteTo(System.IO.Stream stream);
    }
}