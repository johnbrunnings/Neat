using System;

namespace Neat.Service.Rest.Client
{
    public class HttpWebRequestParameters
    {
        public Uri RequestUri { get; set; }
        public HttpRequestMethod Method { get; set; }
        public byte[] RequestBytes { get; set; }
        public long ContentLength { get; set; }
        public string ContentType { get; set; }
        public int ReadWriteTimeout { get; set; }
        public int Timeout { get; set; }
        public string TransferEncoding { get; set; }
        public ResponseCallback ResponseCallback { get; set; }
    }
}