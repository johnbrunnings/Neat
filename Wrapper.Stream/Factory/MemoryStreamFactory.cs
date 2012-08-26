using System.IO;
using Neat.Wrapper.Stream.Abstract;
using Neat.Wrapper.Stream.Factory.Interface;

namespace Neat.Wrapper.Stream.Factory
{
    public class MemoryStreamFactory : IMemoryStreamFactory
    {
        public MemoryStreamBase Create()
        {
            return new MemoryStreamWrapper(new MemoryStream());
        }

        public MemoryStreamBase Create(byte[] buffer)
        {
            return new MemoryStreamWrapper(new MemoryStream(buffer));
        }

        public MemoryStreamBase Create(byte[] buffer, bool writable)
        {
            return new MemoryStreamWrapper(new MemoryStream(buffer, writable));
        }

        public MemoryStreamBase Create(byte[] buffer, int index, int count)
        {
            return new MemoryStreamWrapper(new MemoryStream(buffer, index, count));
        }

        public MemoryStreamBase Create(byte[] buffer, int index, int count, bool writable)
        {
            return new MemoryStreamWrapper(new MemoryStream(buffer, index, count, writable));
        }

        public MemoryStreamBase Create(byte[] buffer, int index, int count, bool writable, bool publiclyVisible)
        {
            return new MemoryStreamWrapper(new MemoryStream(buffer, index, count, writable, publiclyVisible));
        }

        public MemoryStreamBase Create(int capacity)
        {
            return new MemoryStreamWrapper(new MemoryStream(capacity));
        }
    }
}