using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neat.Service.Rest;
using Neat.Service.Rest.Factory;
using Neat.Service.Rest.Factory.Interface;
using Neat.Service.Rest.Interface;
using Neat.Service.Rest.Proxy;
using Neat.Service.Rest.Proxy.Interface;
using Neat.Wrapper.Factory;
using Neat.Wrapper.Factory.Interface;

namespace Neat.Service.Test.Integration.Rest
{
    [TestClass]
    public class Demo
    {
        private IHttpWebRequestFactory _httpWebRequestFactory;
        private IHttpProxyRequestFactory _httpProxyRequestFactory;
        private IHttpWebResponseFactory _httpWebResponseFactory;
        private IHttpWebProxy _httpWebProxy;
        private IStreamReaderFactory _streamReaderFactory;
        private IHttpWebResponseProcessor _httpWebResponseProcessor;
        private HttpWebRequestParameters _httpWebRequestParameters;

        [TestInitialize]
        public void Initialize()
        {
            _httpWebRequestFactory = new HttpWebRequestFactory();
            _httpProxyRequestFactory = new HttpProxyRequestFactory(_httpWebRequestFactory);
            _httpWebResponseFactory = new HttpWebResponseFactory();
            _httpWebProxy = new HttpWebProxy(_httpWebResponseFactory);
            _streamReaderFactory = new StreamReaderFactory();
            _httpWebResponseProcessor = new HttpWebResponseProcessor(_streamReaderFactory);
            _httpWebRequestParameters = new HttpWebRequestParameters()
            {
                RequestUri = new Uri("http://www.google.com"),
                ContentType = "*/*",
                Method = HttpRequestMethod.Get,
                ReadWriteTimeout = 30000,
                Timeout = 30000
            };
        }

        [TestMethod]
        public void RunDemo()
        {
            var httpWebRequest = _httpProxyRequestFactory.Create(_httpWebRequestParameters);
            var httpWebResponse = _httpWebProxy.GetResponse(httpWebRequest);

            string response = _httpWebResponseProcessor.ExtractBodyAsString(httpWebResponse);
            Console.WriteLine(response);
            Assert.IsNotNull(response);
        }
    }
}