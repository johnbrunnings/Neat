using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using neat.service.rest;
using neat.service.rest.factory;
using neat.service.rest.factory.@interface;
using neat.service.rest.@interface;
using neat.service.rest.proxy;
using neat.service.rest.proxy.@interface;
using neat.wrapper.factory;
using neat.wrapper.factory.@interface;

namespace neat.service.test.rest
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