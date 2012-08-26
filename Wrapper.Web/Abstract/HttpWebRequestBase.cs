using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;

namespace Neat.Wrapper.Web.Abstract
{
    public abstract class HttpWebRequestBase : WebRequest, ISerializable
    {
        public abstract bool AllowAutoRedirect { get; set; }
        public abstract bool AllowWriteStreamBuffering { get; set; }
        public abstract bool HaveResponse { get; }
        public abstract bool KeepAlive { get; set; }
        public abstract bool Pipelined { get; set; }
        public abstract bool UnsafeAuthenticatedConnectionSharing { get; set; }
        public abstract bool SendChunked { get; set; }
        public abstract DecompressionMethods AutomaticDecompression { get; set; }
        public abstract int MaximumResponseHeadersLength { get; set; }
        public abstract X509CertificateCollection ClientCertificates { get; set; }
        public abstract CookieContainer CookieContainer { get; set; }
        public abstract int ReadWriteTimeout { get; set; }
        public abstract Uri Address { get; }
        public abstract HttpContinueDelegate ContinueDelegate { get; set; }
        public abstract ServicePoint ServicePoint { get; }
        public abstract string Host { get; set; }
        public abstract int MaximumAutomaticRedirections { get; set; }
        public abstract Version ProtocolVersion { get; set; }
        public abstract string MediaType { get; set; }
        public abstract string TransferEncoding { get; set; }
        public abstract string Connection { get; set; }
        public abstract string Accept { get; set; }
        public abstract string Referer { get; set; }
        public abstract string UserAgent { get; set; }
        public abstract string Expect { get; set; }
        public abstract DateTime IfModifiedSince { get; set; }
        public abstract DateTime Date { get; set; }
        public abstract Stream EndGetRequestStream(IAsyncResult asyncResult, out TransportContext context);
        public abstract Stream GetRequestStream(out TransportContext context);
        public abstract void AddRange(int @from, int to);
        public abstract void AddRange(long @from, long to);
        public abstract void AddRange(int range);
        public abstract void AddRange(long range);
        public abstract void AddRange(string rangeSpecifier, int @from, int to);
        public abstract void AddRange(string rangeSpecifier, long @from, long to);
        public abstract void AddRange(string rangeSpecifier, int range);
        public abstract void AddRange(string rangeSpecifier, long range);
    }
}