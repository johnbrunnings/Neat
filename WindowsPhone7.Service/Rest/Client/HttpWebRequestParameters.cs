using System;
using System.Text;

namespace Neat.WindowsPhone7.Service.Rest.Client
{
    public class HttpWebRequestParameters
    {
        public Uri RequestUri { get; set; }
        public HttpRequestMethod Method { get; set; }
        public string RequestData { get; set; }
        public Encoding Encoding { get; set; }
        public byte[] RequestBytes { get; set; }
        public string ContentType { get; set; }
        public ProcessRequestStream ProcessRequestStream { get; set; }
        public ProcessResponseStream ProcessResponseStream { get; set; }
        public ResponseCallback ResponseCallback { get; set; }
    }
}