using System;

namespace Neat.WindowsPhone7.Service.Rest.Client
{
    public class HttpWebRequestParameters
    {
        public Uri RequestUri { get; set; }
        public HttpRequestMethod Method { get; set; }
        public byte[] RequestBytes { get; set; }
        public string ContentType { get; set; }
        public ResponseCallback ResponseCallback { get; set; }
    }
}