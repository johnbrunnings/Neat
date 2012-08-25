using System;
using System.IO;
using System.Net;

namespace Neat.WindowsPhone7.Wrapper.Abstract
{
    public abstract class HttpWebRequestBase
    {
        public abstract bool UseDefaultCredentials { get; set; }
        public abstract IWebRequestCreate CreatorInstance { get; }
        public abstract bool AllowReadStreamBuffering { get; set; }
        public abstract CookieContainer CookieContainer { get; set; }
        public abstract Uri RequestUri { get; }
        public abstract bool AllowAutoRedirect { get; set; }
        public abstract bool HaveResponse { get; }
        public abstract string Method { get; set; }
        public abstract ICredentials Credentials { get; set; }
        public abstract WebHeaderCollection Headers { get; set; }
        public abstract string ContentType { get; set; }
        public abstract string Accept { get; set; }
        public abstract string UserAgent { get; set; }
        public abstract bool SupportsCookieContainer { get; }
        public abstract void Abort();
        public abstract IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state);
        public abstract Stream EndGetRequestStream(IAsyncResult asyncResult);
        public abstract IAsyncResult BeginGetResponse(AsyncCallback callback, object state);
        public abstract WebResponse EndGetResponse(IAsyncResult asyncResult);
    }
}