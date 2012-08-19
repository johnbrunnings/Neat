using Neat.Wrapper.Abstract;

namespace Neat.Service.Rest
{
    public class HttpWebProxyRequest
    {
        public HttpWebRequestBase HttpWebRequestBase { get; set; }
        public ResponseCallback ResponseCallback { get; set; }
        public HttpRequestMethod Method { get; set; }
        public byte[] RequestBytes { get; set; }
    }
}