using System;
using System.IO;
using System.Net;
using System.Runtime.Remoting;
using System.Runtime.Serialization;

namespace neat.wrapper.parent
{
    public abstract class HttpWebResponseWrapperBase
    {
        public abstract object GetLifetimeService();
        public abstract object InitializeLifetimeService();
        public abstract ObjRef CreateObjRef(Type requestedType);
        public abstract void GetObjectData(SerializationInfo info, StreamingContext context);
        public abstract void Dispose();
        public abstract bool IsFromCache { get; }
        public abstract bool IsMutuallyAuthenticated { get; }
        public abstract CookieCollection Cookies { get; set; }
        public abstract WebHeaderCollection Headers { get; }
        public abstract long ContentLength { get; set; }
        public abstract string ContentEncoding { get; }
        public abstract string ContentType { get; set; }
        public abstract string CharacterSet { get; }
        public abstract string Server { get; }
        public abstract DateTime LastModified { get; }
        public abstract HttpStatusCode StatusCode { get; }
        public abstract string StatusDescription { get; }
        public abstract Version ProtocolVersion { get; }
        public abstract Uri ResponseUri { get; }
        public abstract string Method { get; }
        public abstract Stream GetResponseStream();
        public abstract void Close();
        public abstract string GetResponseHeader(string headerName);
    }
}