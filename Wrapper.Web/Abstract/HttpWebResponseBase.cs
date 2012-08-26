using System;
using System.Net;
using System.Runtime.Serialization;

namespace Neat.Wrapper.Web.Abstract
{
    public abstract class HttpWebResponseBase : WebResponse, ISerializable
    {
        public abstract void Dispose();
        public abstract CookieCollection Cookies { get; set; }
        public abstract string ContentEncoding { get; }
        public abstract string CharacterSet { get; }
        public abstract string Server { get; }
        public abstract DateTime LastModified { get; }
        public abstract HttpStatusCode StatusCode { get; }
        public abstract string StatusDescription { get; }
        public abstract Version ProtocolVersion { get; }
        public abstract string Method { get; }
        public abstract string GetResponseHeader(string headerName);
    }
}