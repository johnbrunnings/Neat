using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using neat.wrapper.parent;

namespace neat.wrapper
{
    public class HttpWebRequestWrapper : HttpWebRequestBase
    {
        private readonly System.Net.HttpWebRequest _httpWebRequest;
        public override object GetLifetimeService()
        {
            return _httpWebRequest.GetLifetimeService();
        }

        public override object InitializeLifetimeService()
        {
            return _httpWebRequest.InitializeLifetimeService();
        }

        public override ObjRef CreateObjRef(Type requestedType)
        {
            return _httpWebRequest.CreateObjRef(requestedType);
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable) _httpWebRequest).GetObjectData(info, context);
        }

        public override RequestCachePolicy CachePolicy
        {
            get { return _httpWebRequest.CachePolicy; }
            set { _httpWebRequest.CachePolicy = value; }
        }

        public override AuthenticationLevel AuthenticationLevel
        {
            get { return _httpWebRequest.AuthenticationLevel; }
            set { _httpWebRequest.AuthenticationLevel = value; }
        }

        public override TokenImpersonationLevel ImpersonationLevel
        {
            get { return _httpWebRequest.ImpersonationLevel; }
            set { _httpWebRequest.ImpersonationLevel = value; }
        }

        public override IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
        {
            return _httpWebRequest.BeginGetRequestStream(callback, state);
        }

        public override Stream EndGetRequestStream(IAsyncResult asyncResult)
        {
            return _httpWebRequest.EndGetRequestStream(asyncResult);
        }

        public override Stream EndGetRequestStream(IAsyncResult asyncResult, out TransportContext context)
        {
            return _httpWebRequest.EndGetRequestStream(asyncResult, out context);
        }

        public override Stream GetRequestStream()
        {
            return _httpWebRequest.GetRequestStream();
        }

        public override Stream GetRequestStream(out TransportContext context)
        {
            return _httpWebRequest.GetRequestStream(out context);
        }

        public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
        {
            return _httpWebRequest.BeginGetResponse(callback, state);
        }

        public override WebResponse EndGetResponse(IAsyncResult asyncResult)
        {
            return _httpWebRequest.EndGetResponse(asyncResult);
        }

        public override WebResponse GetResponse()
        {
            return _httpWebRequest.GetResponse();
        }

        public override void Abort()
        {
            _httpWebRequest.Abort();
        }

        public override void AddRange(int @from, int to)
        {
            _httpWebRequest.AddRange(@from, to);
        }

        public override void AddRange(long @from, long to)
        {
            _httpWebRequest.AddRange(@from, to);
        }

        public override void AddRange(int range)
        {
            _httpWebRequest.AddRange(range);
        }

        public override void AddRange(long range)
        {
            _httpWebRequest.AddRange(range);
        }

        public override void AddRange(string rangeSpecifier, int @from, int to)
        {
            _httpWebRequest.AddRange(rangeSpecifier, @from, to);
        }

        public override void AddRange(string rangeSpecifier, long @from, long to)
        {
            _httpWebRequest.AddRange(rangeSpecifier, @from, to);
        }

        public override void AddRange(string rangeSpecifier, int range)
        {
            _httpWebRequest.AddRange(rangeSpecifier, range);
        }

        public override void AddRange(string rangeSpecifier, long range)
        {
            _httpWebRequest.AddRange(rangeSpecifier, range);
        }

        public override bool AllowAutoRedirect
        {
            get { return _httpWebRequest.AllowAutoRedirect; }
            set { _httpWebRequest.AllowAutoRedirect = value; }
        }

        public override bool AllowWriteStreamBuffering
        {
            get { return _httpWebRequest.AllowWriteStreamBuffering; }
            set { _httpWebRequest.AllowWriteStreamBuffering = value; }
        }

        public override bool HaveResponse
        {
            get { return _httpWebRequest.HaveResponse; }
        }

        public override bool KeepAlive
        {
            get { return _httpWebRequest.KeepAlive; }
            set { _httpWebRequest.KeepAlive = value; }
        }

        public override bool Pipelined
        {
            get { return _httpWebRequest.Pipelined; }
            set { _httpWebRequest.Pipelined = value; }
        }

        public override bool PreAuthenticate
        {
            get { return _httpWebRequest.PreAuthenticate; }
            set { _httpWebRequest.PreAuthenticate = value; }
        }

        public override bool UnsafeAuthenticatedConnectionSharing
        {
            get { return _httpWebRequest.UnsafeAuthenticatedConnectionSharing; }
            set { _httpWebRequest.UnsafeAuthenticatedConnectionSharing = value; }
        }

        public override bool SendChunked
        {
            get { return _httpWebRequest.SendChunked; }
            set { _httpWebRequest.SendChunked = value; }
        }

        public override DecompressionMethods AutomaticDecompression
        {
            get { return _httpWebRequest.AutomaticDecompression; }
            set { _httpWebRequest.AutomaticDecompression = value; }
        }

        public override int MaximumResponseHeadersLength
        {
            get { return _httpWebRequest.MaximumResponseHeadersLength; }
            set { _httpWebRequest.MaximumResponseHeadersLength = value; }
        }

        public override X509CertificateCollection ClientCertificates
        {
            get { return _httpWebRequest.ClientCertificates; }
            set { _httpWebRequest.ClientCertificates = value; }
        }

        public override CookieContainer CookieContainer
        {
            get { return _httpWebRequest.CookieContainer; }
            set { _httpWebRequest.CookieContainer = value; }
        }

        public override Uri RequestUri
        {
            get { return _httpWebRequest.RequestUri; }
        }

        public override long ContentLength
        {
            get { return _httpWebRequest.ContentLength; }
            set { _httpWebRequest.ContentLength = value; }
        }

        public override int Timeout
        {
            get { return _httpWebRequest.Timeout; }
            set { _httpWebRequest.Timeout = value; }
        }

        public override int ReadWriteTimeout
        {
            get { return _httpWebRequest.ReadWriteTimeout; }
            set { _httpWebRequest.ReadWriteTimeout = value; }
        }

        public override Uri Address
        {
            get { return _httpWebRequest.Address; }
        }

        public override HttpContinueDelegate ContinueDelegate
        {
            get { return _httpWebRequest.ContinueDelegate; }
            set { _httpWebRequest.ContinueDelegate = value; }
        }

        public override ServicePoint ServicePoint
        {
            get { return _httpWebRequest.ServicePoint; }
        }

        public override string Host
        {
            get { return _httpWebRequest.Host; }
            set { _httpWebRequest.Host = value; }
        }

        public override int MaximumAutomaticRedirections
        {
            get { return _httpWebRequest.MaximumAutomaticRedirections; }
            set { _httpWebRequest.MaximumAutomaticRedirections = value; }
        }

        public override string Method
        {
            get { return _httpWebRequest.Method; }
            set { _httpWebRequest.Method = value; }
        }

        public override ICredentials Credentials
        {
            get { return _httpWebRequest.Credentials; }
            set { _httpWebRequest.Credentials = value; }
        }

        public override bool UseDefaultCredentials
        {
            get { return _httpWebRequest.UseDefaultCredentials; }
            set { _httpWebRequest.UseDefaultCredentials = value; }
        }

        public override string ConnectionGroupName
        {
            get { return _httpWebRequest.ConnectionGroupName; }
            set { _httpWebRequest.ConnectionGroupName = value; }
        }

        public override WebHeaderCollection Headers
        {
            get { return _httpWebRequest.Headers; }
            set { _httpWebRequest.Headers = value; }
        }

        public override IWebProxy Proxy
        {
            get { return _httpWebRequest.Proxy; }
            set { _httpWebRequest.Proxy = value; }
        }

        public override Version ProtocolVersion
        {
            get { return _httpWebRequest.ProtocolVersion; }
            set { _httpWebRequest.ProtocolVersion = value; }
        }

        public override string ContentType
        {
            get { return _httpWebRequest.ContentType; }
            set { _httpWebRequest.ContentType = value; }
        }

        public override string MediaType
        {
            get { return _httpWebRequest.MediaType; }
            set { _httpWebRequest.MediaType = value; }
        }

        public override string TransferEncoding
        {
            get { return _httpWebRequest.TransferEncoding; }
            set { _httpWebRequest.TransferEncoding = value; }
        }

        public override string Connection
        {
            get { return _httpWebRequest.Connection; }
            set { _httpWebRequest.Connection = value; }
        }

        public override string Accept
        {
            get { return _httpWebRequest.Accept; }
            set { _httpWebRequest.Accept = value; }
        }

        public override string Referer
        {
            get { return _httpWebRequest.Referer; }
            set { _httpWebRequest.Referer = value; }
        }

        public override string UserAgent
        {
            get { return _httpWebRequest.UserAgent; }
            set { _httpWebRequest.UserAgent = value; }
        }

        public override string Expect
        {
            get { return _httpWebRequest.Expect; }
            set { _httpWebRequest.Expect = value; }
        }

        public override DateTime IfModifiedSince
        {
            get { return _httpWebRequest.IfModifiedSince; }
            set { _httpWebRequest.IfModifiedSince = value; }
        }

        public override DateTime Date
        {
            get { return _httpWebRequest.Date; }
            set { _httpWebRequest.Date = value; }
        }

        public HttpWebRequestWrapper(System.Net.HttpWebRequest httpWebRequest)
        {
            _httpWebRequest = httpWebRequest;
        }
    }
}