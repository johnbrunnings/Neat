using System;
using System.IO;
using System.Net;
using Neat.WindowsPhone7.Wrapper.Abstract;

namespace Neat.WindowsPhone7.Wrapper
{
    public class HttpWebResponseWrapper : HttpWebResponseBase
    {
        private readonly HttpWebResponse _httpWebResponse;

        public HttpWebResponseWrapper(HttpWebResponse httpWebResponse)
        {
            _httpWebResponse = httpWebResponse;
        }

        public override void Dispose()
        {
            ((IDisposable) _httpWebResponse).Dispose();
        }

        public override bool SupportsHeaders
        {
            get { return _httpWebResponse.SupportsHeaders; }
        }

        public override Stream GetResponseStream()
        {
            return _httpWebResponse.GetResponseStream();
        }

        public override void Close()
        {
            _httpWebResponse.Close();
        }

        public override CookieCollection Cookies
        {
            get { return _httpWebResponse.Cookies; }
        }

        public override WebHeaderCollection Headers
        {
            get { return _httpWebResponse.Headers; }
        }

        public override long ContentLength
        {
            get { return _httpWebResponse.ContentLength; }
        }

        public override string ContentType
        {
            get { return _httpWebResponse.ContentType; }
        }

        public override HttpStatusCode StatusCode
        {
            get { return _httpWebResponse.StatusCode; }
        }

        public override string StatusDescription
        {
            get { return _httpWebResponse.StatusDescription; }
        }

        public override Uri ResponseUri
        {
            get { return _httpWebResponse.ResponseUri; }
        }

        public override string Method
        {
            get { return _httpWebResponse.Method; }
        }
    }
}