using Neat.WindowsPhone7.Service.Rest.Client.Factory.Interface;
using Neat.WindowsPhone7.Wrapper.Abstract;
using Neat.WindowsPhone7.Wrapper.Factory.Interface;

namespace Neat.WindowsPhone7.Service.Rest.Client.Factory
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
            httpWebRequest.AllowAutoRedirect = false;
        }
    }
}