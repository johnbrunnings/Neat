using System;
using System.IO;
using System.Net;

namespace Neat.WindowsPhone7.Wrapper.Web.Abstract
{
    public abstract class HttpWebResponseBase
    {
        public abstract void Dispose();
        public abstract bool SupportsHeaders { get; }
        public abstract CookieCollection Cookies { get; }
        public abstract WebHeaderCollection Headers { get; }
        public abstract long ContentLength { get; }
        public abstract string ContentType { get; }
        public abstract HttpStatusCode StatusCode { get; }
        public abstract string StatusDescription { get; }
        public abstract Uri ResponseUri { get; }
        public abstract string Method { get; }
        public abstract Stream GetResponseStream();
        public abstract void Close();
    }
}