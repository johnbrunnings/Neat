using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;

namespace Neat.Wrapper.Web.Abstract
{
    public abstract class HttpWebRequestBase
    {
        public abstract object GetLifetimeService();
        public abstract object InitializeLifetimeService();
        public abstract ObjRef CreateObjRef(Type requestedType);
        public abstract void GetObjectData(SerializationInfo info, StreamingContext context);
        public abstract RequestCachePolicy CachePolicy { get; set; }
        public abstract AuthenticationLevel AuthenticationLevel { get; set; }
        public abstract TokenImpersonationLevel ImpersonationLevel { get; set; }
        public abstract bool AllowAutoRedirect { get; set; }
        public abstract bool AllowWriteStreamBuffering { get; set; }
        public abstract bool HaveResponse { get; }
        public abstract bool KeepAlive { get; set; }
        public abstract bool Pipelined { get; set; }
        public abstract bool PreAuthenticate { get; set; }
        public abstract bool UnsafeAuthenticatedConnectionSharing { get; set; }
        public abstract bool SendChunked { get; set; }
        public abstract DecompressionMethods AutomaticDecompression { get; set; }
        public abstract int MaximumResponseHeadersLength { get; set; }
        public abstract X509CertificateCollection ClientCertificates { get; set; }
        public abstract CookieContainer CookieContainer { get; set; }
        public abstract Uri RequestUri { get; }
        public abstract long ContentLength { get; set; }
        public abstract int Timeout { get; set; }
        public abstract int ReadWriteTimeout { get; set; }
        public abstract Uri Address { get; }
        public abstract HttpContinueDelegate ContinueDelegate { get; set; }
        public abstract ServicePoint ServicePoint { get; }
        public abstract string Host { get; set; }
        public abstract int MaximumAutomaticRedirections { get; set; }
        public abstract string Method { get; set; }
        public abstract ICredentials Credentials { get; set; }
        public abstract bool UseDefaultCredentials { get; set; }
        public abstract string ConnectionGroupName { get; set; }
        public abstract WebHeaderCollection Headers { get; set; }
        public abstract IWebProxy Proxy { get; set; }
        public abstract Version ProtocolVersion { get; set; }
        public abstract string ContentType { get; set; }
        public abstract string MediaType { get; set; }
        public abstract string TransferEncoding { get; set; }
        public abstract string Connection { get; set; }
        public abstract string Accept { get; set; }
        public abstract string Referer { get; set; }
        public abstract string UserAgent { get; set; }
        public abstract string Expect { get; set; }
        public abstract DateTime IfModifiedSince { get; set; }
        public abstract DateTime Date { get; set; }
        public abstract IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state);
        public abstract Stream EndGetRequestStream(IAsyncResult asyncResult);
        public abstract Stream EndGetRequestStream(IAsyncResult asyncResult, out TransportContext context);
        public abstract Stream GetRequestStream();
        public abstract Stream GetRequestStream(out TransportContext context);
        public abstract IAsyncResult BeginGetResponse(AsyncCallback callback, object state);
        public abstract WebResponse EndGetResponse(IAsyncResult asyncResult);
        public abstract WebResponse GetResponse();
        public abstract void Abort();
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