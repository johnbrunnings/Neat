using System;
using System.IO;
using System.Net;
using Neat.WindowsPhone7.Wrapper.Web.Abstract;

namespace Neat.WindowsPhone7.Wrapper.Web
{
    public class HttpWebRequestWrapper : HttpWebRequestBase
    {
        private readonly HttpWebRequest _httpWebRequest;

        public HttpWebRequestWrapper(HttpWebRequest httpWebRequest)
        {
            _httpWebRequest = httpWebRequest;
        }

        public override bool UseDefaultCredentials
        {
            get { return _httpWebRequest.UseDefaultCredentials; }
            set { _httpWebRequest.UseDefaultCredentials = value; }
        }

        public override IWebRequestCreate CreatorInstance
        {
            get { return _httpWebRequest.CreatorInstance; }
        }

        public override void Abort()
        {
            _httpWebRequest.Abort();
        }

        public override IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
        {
            return _httpWebRequest.BeginGetRequestStream(callback, state);
        }

        public override Stream EndGetRequestStream(IAsyncResult asyncResult)
        {
            return _httpWebRequest.EndGetRequestStream(asyncResult);
        }

        public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
        {
            return _httpWebRequest.BeginGetResponse(callback, state);
        }

        public override WebResponse EndGetResponse(IAsyncResult asyncResult)
        {
            return _httpWebRequest.EndGetResponse(asyncResult);
        }

        public override bool AllowReadStreamBuffering
        {
            get { return _httpWebRequest.AllowReadStreamBuffering; }
            set { _httpWebRequest.AllowReadStreamBuffering = value; }
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

        public override bool AllowAutoRedirect
        {
            get { return _httpWebRequest.AllowAutoRedirect; }
            set { _httpWebRequest.AllowAutoRedirect = value; }
        }

        public override bool HaveResponse
        {
            get { return _httpWebRequest.HaveResponse; }
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

        public override WebHeaderCollection Headers
        {
            get { return _httpWebRequest.Headers; }
            set { _httpWebRequest.Headers = value; }
        }

        public override string ContentType
        {
            get { return _httpWebRequest.ContentType; }
            set { _httpWebRequest.ContentType = value; }
        }

        public override string Accept
        {
            get { return _httpWebRequest.Accept; }
            set { _httpWebRequest.Accept = value; }
        }

        public override string UserAgent
        {
            get { return _httpWebRequest.UserAgent; }
            set { _httpWebRequest.UserAgent = value; }
        }

        public override bool SupportsCookieContainer
        {
            get { return _httpWebRequest.SupportsCookieContainer; }
        }
    }
}