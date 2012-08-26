using Neat.Wrapper.Stream.Abstract;

namespace Neat.Wrapper.Stream.Factory.Interface
{
    public interface IMemoryStreamFactory
    {
        MemoryStreamBase Create();
        MemoryStreamBase Create(byte[] buffer);
        MemoryStreamBase Create(byte[] buffer, bool writable);
        MemoryStreamBase Create(byte[] buffer, int index, int count);
        MemoryStreamBase Create(byte[] buffer, int index, int count, bool writable);
        MemoryStreamBase Create(byte[] buffer, int index, int count, bool writable, bool publiclyVisible);
        MemoryStreamBase Create(int capacity);
    }
}