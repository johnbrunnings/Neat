using System.IO;
using Neat.StreamManager.Factory.StreamFactory.Interface;
using Neat.StreamManager.Factory.StreamFactory.Parameters;
using Neat.StreamManager.Factory.StreamFactory.Parameters.Abstract;

namespace Neat.StreamManager.Factory.StreamFactory
{
    public class MemoryStreamFactory : IStreamFactory
    {
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
                                    return new MemoryStream(memoryStreamParameters.Buffer, memoryStreamParameters.Index.Value, memoryStreamParameters.Count.Value, memoryStreamParameters.Writable.Value, memoryStreamParameters.PubliclyVisible.Value);
                                }

                                return new MemoryStream(memoryStreamParameters.Buffer, memoryStreamParameters.Index.Value, memoryStreamParameters.Count.Value, memoryStreamParameters.Writable.Value);
                            }

                            return new MemoryStream(memoryStreamParameters.Buffer, memoryStreamParameters.Writable.Value);
                        }

                        if (memoryStreamParameters.Index.HasValue && memoryStreamParameters.Count.HasValue)
                        {
                            return new MemoryStream(memoryStreamParameters.Buffer, memoryStreamParameters.Index.Value, memoryStreamParameters.Count.Value);
                        }

                        return new MemoryStream(memoryStreamParameters.Buffer);
                    }

                    if (memoryStreamParameters.Capacity.HasValue)
                    {
                        return new MemoryStream(memoryStreamParameters.Capacity.Value);
                    }
                }
            }

            return new MemoryStream();
        }
    }
}