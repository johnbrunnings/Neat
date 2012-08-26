using System.IO;
using Neat.WindowsPhone7.StreamManager.Factory.WriterFactory.Parameters.Abstract;

namespace Neat.WindowsPhone7.StreamManager.Factory.WriterFactory.Parameters
{
    public class StreamWriterParameters : TextWriterParameters
    {
        public Stream Stream { get; set; }
    }
}