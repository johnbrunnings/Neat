using System;
using System.IO;
using System.Net;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using neat.wrapper.parent;

namespace neat.wrapper
{
    public class HttpWebResponseWrapper : HttpWebResponseBase
    {
        private readonly HttpWebResponse _httpWebResponse;
        public override object GetLifetimeService()
        {
            return _httpWebResponse.GetLifetimeService();
        }

        public override object InitializeLifetimeService()
        {
            return _httpWebResponse.InitializeLifetimeService();
        }

        public override ObjRef CreateObjRef(Type requestedType)
        {
            return _httpWebResponse.CreateObjRef(requestedType);
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable) _httpWebResponse).GetObjectData(info, context);
        }

        public override void Dispose()
        {
            ((IDisposable) _httpWebResponse).Dispose();
        }

        public override bool IsFromCache
        {
            get { return _httpWebResponse.IsFromCache; }
        }

        public override Stream GetResponseStream()
        {
            return _httpWebResponse.GetResponseStream();
        }

        public override void Close()
        {
            _httpWebResponse.Close();
        }

        public override string GetResponseHeader(string headerName)
        {
            return _httpWebResponse.GetResponseHeader(headerName);
        }

        public override bool IsMutuallyAuthenticated
        {
            get { return _httpWebResponse.IsMutuallyAuthenticated; }
        }

        public override CookieCollection Cookies
        {
            get { return _httpWebResponse.Cookies; }
            set { _httpWebResponse.Cookies = value; }
        }

        public override WebHeaderCollection Headers
        {
            get { return _httpWebResponse.Headers; }
        }

        public override long ContentLength
        {
            get { return _httpWebResponse.ContentLength; }
            set { _httpWebResponse.ContentLength = value; }
        }

        public override string ContentEncoding
        {
            get { return _httpWebResponse.ContentEncoding; }
        }

        public override string ContentType
        {
            get { return _httpWebResponse.ContentType; }
            set { _httpWebResponse.ContentType = value; }
        }

        public override string CharacterSet
        {
            get { return _httpWebResponse.CharacterSet; }
        }

        public override string Server
        {
            get { return _httpWebResponse.Server; }
        }

        public override DateTime LastModified
        {
            get { return _httpWebResponse.LastModified; }
        }

        public override HttpStatusCode StatusCode
        {
            get { return _httpWebResponse.StatusCode; }
        }

        public override string StatusDescription
        {
            get { return _httpWebResponse.StatusDescription; }
        }

        public override Version ProtocolVersion
        {
            get { return _httpWebResponse.ProtocolVersion; }
        }

        public override Uri ResponseUri
        {
            get { return _httpWebResponse.ResponseUri; }
        }

        public override string Method
        {
            get { return _httpWebResponse.Method; }
        }

        public HttpWebResponseWrapper(HttpWebResponse httpWebResponse)
        {
            _httpWebResponse = httpWebResponse;
        }
    }
}