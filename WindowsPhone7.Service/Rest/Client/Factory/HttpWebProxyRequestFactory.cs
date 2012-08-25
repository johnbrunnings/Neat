using Neat.WindowsPhone7.Service.Rest.Client.Factory.Interface;
using Neat.WindowsPhone7.Service.Rest.Client.Interface;
using Neat.WindowsPhone7.Wrapper.Abstract;
using Neat.WindowsPhone7.Wrapper.Factory.Interface;

namespace Neat.WindowsPhone7.Service.Rest.Client.Factory
{
    public class HttpWebProxyRequestFactory : IHttpWebProxyRequestFactory
    {
        private readonly IHttpWebRequestFactory _httpWebRequestFactory;
        private readonly IHttpWebProcessor _httpWebProcessor;

        public HttpWebProxyRequestFactory(IHttpWebRequestFactory httpWebRequestFactory, IHttpWebProcessor httpWebProcessor)
        {
            _httpWebRequestFactory = httpWebRequestFactory;
            _httpWebProcessor = httpWebProcessor;
        }

        public HttpWebProxyRequest Create(HttpWebRequestParameters httpWebRequestParameters)
        {
            byte[] requestBytes = httpWebRequestParameters.RequestBytes;
            if (requestBytes == null && !string.IsNullOrEmpty(httpWebRequestParameters.RequestData))
            {
                requestBytes = _httpWebProcessor.GetRequestBytesFromRequestData(httpWebRequestParameters.RequestData, httpWebRequestParameters.Encoding);
            }
            var httpWebProxyRequest = new HttpWebProxyRequest()
            {
                Method = httpWebRequestParameters.Method,
                RequestBytes = requestBytes,
                ProcessRequestStream = httpWebRequestParameters.ProcessRequestStream,
                ProcessResponseStream = httpWebRequestParameters.ProcessResponseStream,
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