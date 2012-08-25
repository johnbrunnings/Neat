using Neat.Wrapper.Abstract;

namespace Neat.Service.Rest.Client
{
    public class HttpWebProxyRequest
    {
        public HttpWebRequestBase HttpWebRequestBase { get; set; }
        public ProcessRequestStream ProcessRequestStream { get; set; }
        public ProcessResponseStream ProcessResponseStream { get; set; }
        public ResponseCallback ResponseCallback { get; set; }
        public HttpRequestMethod Method { get; set; }
        public byte[] RequestBytes { get; set; }
    }
}