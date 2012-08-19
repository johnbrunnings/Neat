using Neat.Wrapper.Abstract;
using Neat.Service.Rest.Factory.Interface;
using Neat.Wrapper.Factory.Interface;

namespace Neat.Service.Rest.Factory
{
    public class HttpWebProxyRequestFactory : IHttpWebProxyRequestFactory
    {
        private readonly IHttpWebRequestFactory _httpWebRequestFactory;

        public HttpWebProxyRequestFactory(IHttpWebRequestFactory httpWebRequestFactory)
        {
            _httpWebRequestFactory = httpWebRequestFactory;
        }

        public HttpWebProxyRequest Create(HttpWebRequestParameters httpWebRequestParameters)
        {
            var httpWebProxyRequest = new HttpWebProxyRequest()
            {
                Method = httpWebRequestParameters.Method,
                RequestBytes = httpWebRequestParameters.RequestBytes,
                ResponseCallback = httpWebRequestParameters.ResponseCallback
            };

            var httpWebRequest = _httpWebRequestFactory.Create(httpWebRequestParameters.RequestUri);
            Setup(httpWebRequestParameters, httpWebRequest);
            httpWebProxyRequest.HttpWebRequestBase = httpWebRequest;

            return httpWebProxyRequest;
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