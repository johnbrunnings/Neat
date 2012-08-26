using System.IO;
using Neat.StreamManager.Factory.WriterFactory.Parameters.Abstract;

namespace Neat.StreamManager.Factory.WriterFactory.Parameters
{
    public class StreamWriterParameters : TextWriterParameters
    {
        public Stream Stream { get; set; }
    }
}