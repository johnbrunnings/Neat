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

        private void Setup(HttpWebRequestParameters httpWebRequestParameters, HttpWebRequestBase httpWebRequestBase)
        {
            httpWebRequestBase.Method = httpWebRequestParameters.Method.ToString().ToUpperInvariant();
            httpWebRequestBase.ContentType = httpWebRequestParameters.ContentType;
            if (httpWebRequestParameters.Method == HttpRequestMethod.Post || httpWebRequestParameters.Method == HttpRequestMethod.Put)
            {
                httpWebRequestBase.ContentLength = httpWebRequestParameters.ContentLength;
            }
            httpWebRequestBase.ReadWriteTimeout = httpWebRequestParameters.ReadWriteTimeout;
            httpWebRequestBase.Timeout = httpWebRequestParameters.Timeout;
            httpWebRequestBase.TransferEncoding = httpWebRequestParameters.TransferEncoding;
            httpWebRequestBase.AllowAutoRedirect = false;
            httpWebRequestBase.Proxy = null;
        }
    }
}