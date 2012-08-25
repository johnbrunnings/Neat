using Neat.WindowsPhone7.Wrapper.Abstract;

namespace Neat.WindowsPhone7.Service.Rest.Client
{
    public class HttpWebProxyRequest
    {
        public HttpWebRequestBase HttpWebRequestBase { get; set; }
        public ResponseCallback ResponseCallback { get; set; }
        public HttpRequestMethod Method { get; set; }
        public byte[] RequestBytes { get; set; }
    }
}