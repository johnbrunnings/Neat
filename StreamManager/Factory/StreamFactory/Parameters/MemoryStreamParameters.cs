using Neat.StreamManager.Factory.StreamFactory.Parameters.Abstract;

namespace Neat.StreamManager.Factory.StreamFactory.Parameters
{
    public class MemoryStreamParameters : StreamParameters
    {
        public byte[] Buffer { get; set; }
        public bool? Writable { get; set; }
        public int? Index { get; set; }
        public int? Count { get; set; }
        public bool? PubliclyVisible { get; set; }
        public int? Capacity { get; set; }
    }
}