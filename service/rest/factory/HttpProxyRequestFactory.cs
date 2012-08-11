using neat.service.rest.factory.@interface;
using neat.wrapper.factory.@interface;
using neat.wrapper.parent;

namespace neat.service.rest.factory
{
    public class HttpProxyRequestFactory : IHttpProxyRequestFactory
    {
        private readonly IHttpWebRequestFactory _httpWebRequestFactory;

        public HttpProxyRequestFactory(IHttpWebRequestFactory httpWebRequestFactory)
        {
            _httpWebRequestFactory = httpWebRequestFactory;
        }

        public HttpWebRequestBase Create(HttpWebRequestParameters httpWebRequestParameters)
        {
            var httpWebRequestBase = _httpWebRequestFactory.Create(httpWebRequestParameters.RequestUri);
            Setup(httpWebRequestParameters, httpWebRequestBase);

            return httpWebRequestBase;
        }

        private void Setup(HttpWebRequestParameters httpWebRequestParameters, HttpWebRequestBase httpWebRequest)
        {
            httpWebRequest.Method = httpWebRequestParameters.Method.ToString().ToUpperInvariant();
            httpWebRequest.ContentType = httpWebRequestParameters.ContentType;
            if (httpWebRequestParameters.Method == HttpRequestMethod.Post || httpWebRequestParameters.Method == HttpRequestMethod.Put)
            {
                httpWebRequest.ContentLength = httpWebRequestParameters.ContentLength;
            }
            httpWebRequest.ReadWriteTimeout = httpWebRequestParameters.ReadWriteTimeout;
            httpWebRequest.Timeout = httpWebRequestParameters.Timeout;
            httpWebRequest.TransferEncoding = httpWebRequestParameters.TransferEncoding;
            httpWebRequest.AllowAutoRedirect = false;
            httpWebRequest.KeepAlive = true;
            httpWebRequest.Proxy = null;
        }
    }
}