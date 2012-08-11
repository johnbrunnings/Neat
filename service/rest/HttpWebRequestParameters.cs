using System;

namespace neat.service.rest
{
    public class HttpWebRequestParameters
    {
        public Uri RequestUri { get; set; }
        public HttpRequestMethod Method { get; set; }
        public string ContentType { get; set; }
        public long ContentLength { get; set; }
        public int ReadWriteTimeout { get; set; }
        public int Timeout { get; set; }
        public string TransferEncoding { get; set; }
    }
}