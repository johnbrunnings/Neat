using System.IO;
using Neat.StreamManager.Factory.StreamFactory.Interface;
using Neat.StreamManager.Factory.StreamFactory.Parameters;
using Neat.StreamManager.Factory.StreamFactory.Parameters.Abstract;
using Neat.Wrapper.Stream.Abstract;
using Neat.Wrapper.Stream.Factory.Interface;

namespace Neat.StreamManager.Factory.StreamFactory
{
    public class MemoryStreamFactory : IStreamFactory, IMemoryStreamFactory
    {
        private readonly IMemoryStreamFactory _memoryStreamFactory;

        public MemoryStreamFactory(IMemoryStreamFactory memoryStreamFactory)
        {
            _memoryStreamFactory = memoryStreamFactory;
        }

        public StreamType StreamType { get { return StreamType.MemoryStream; } }

        public Stream Create(StreamParameters streamParameters)
        {
            if (streamParameters != null)
            {
                var memoryStreamParameters = streamParameters as MemoryStreamParameters;

                if (memoryStreamParameters != null)
                {
                    if (memoryStreamParameters.Buffer != null)
                    {
                        if (memoryStreamParameters.Writable.HasValue)
                        {
                            if (memoryStreamParameters.Index.HasValue && memoryStreamParameters.Count.HasValue)
                            {
                                if (memoryStreamParameters.PubliclyVisible.HasValue)
                                {
                                    return _memoryStreamFactory.Create(memoryStreamParameters.Buffer, memoryStreamParameters.Index.Value, memoryStreamParameters.Count.Value, memoryStreamParameters.Writable.Value, memoryStreamParameters.PubliclyVisible.Value);
                                }

                                return _memoryStreamFactory.Create(memoryStreamParameters.Buffer, memoryStreamParameters.Index.Value, memoryStreamParameters.Count.Value, memoryStreamParameters.Writable.Value);
                            }

                            return _memoryStreamFactory.Create(memoryStreamParameters.Buffer, memoryStreamParameters.Writable.Value);
                        }

                        if (memoryStreamParameters.Index.HasValue && memoryStreamParameters.Count.HasValue)
                        {
                            return _memoryStreamFactory.Create(memoryStreamParameters.Buffer, memoryStreamParameters.Index.Value, memoryStreamParameters.Count.Value);
                        }

                        return _memoryStreamFactory.Create(memoryStreamParameters.Buffer);
                    }

                    if (memoryStreamParameters.Capacity.HasValue)
                    {
                        return _memoryStreamFactory.Create(memoryStreamParameters.Capacity.Value);
                    }
                }
            }

            return _memoryStreamFactory.Create();
        }

        public MemoryStreamBase Create()
        {
            return _memoryStreamFactory.Create();
        }

        public MemoryStreamBase Create(byte[] buffer)
        {
            return _memoryStreamFactory.Create(buffer);
        }

        public MemoryStreamBase Create(byte[] buffer, bool writable)
        {
            return _memoryStreamFactory.Create(buffer, writable);
        }

        public MemoryStreamBase Create(byte[] buffer, int index, int count)
        {
            return _memoryStreamFactory.Create(buffer, index, count);
        }

        public MemoryStreamBase Create(byte[] buffer, int index, int count, bool writable)
        {
            return _memoryStreamFactory.Create(buffer, index, count, writable);
        }

        public MemoryStreamBase Create(byte[] buffer, int index, int count, bool writable, bool publiclyVisible)
        {
            return _memoryStreamFactory.Create(buffer, index, count, writable, publiclyVisible);
        }

        public MemoryStreamBase Create(int capacity)
        {
            return _memoryStreamFactory.Create(capacity);
        }
    }
}