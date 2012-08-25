using System.IO;

namespace Neat.Service.Rest.Client
{
    public delegate void ProcessRequestStream(byte[] requestBytes, Stream requestStream);
    public delegate string ProcessResponseStream(Stream responseStream);
    public delegate void ResponseCallback(string responseData);
}