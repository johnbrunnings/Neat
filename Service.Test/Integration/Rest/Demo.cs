using System;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neat.Service.Rest.Client;
using Neat.Service.Rest.Client.Factory;
using Neat.Service.Rest.Client.Factory.Interface;
using Neat.Service.Rest.Client.Interface;
using Neat.Service.Rest.Client.Proxy;
using Neat.Service.Rest.Client.Proxy.Interface;
using Neat.Wrapper.Factory;
using Neat.Wrapper.Factory.Interface;

namespace Neat.Service.Test.Integration.Rest
{
    [TestClass]
    public class Demo
    {
        private const string POST_DATA_TEST = "{\"PostData\":\"Test\"}";
        private const string POST_DATA_TEST_CONTENTS = "\"PostData\": \"Test\"";
        private const string HTTP_DOMAIN = "httpbin.org";
        private IHttpWebRequestFactory _httpWebRequestFactory;
        private IHttpWebProxyRequestFactory _httpWebProxyRequestFactory;
        private IHttpWebResponseFactory _httpWebResponseFactory;
        private IHttpWebProxy _httpWebProxy;
        private IStreamReaderFactory _streamReaderFactory;
        private IHttpWebProcessor _httpWebProcessor;
        private HttpWebRequestParameters _httpWebRequestParametersGet;
        private HttpWebRequestParameters _httpWebRequestParametersPost;
        private ManualResetEvent _manualResetEventGet;
        private ManualResetEvent _manualResetEventPost;

        [TestInitialize]
        public void Initialize()
        {
            _httpWebRequestFactory = new HttpWebRequestFactory();
            _streamReaderFactory = new StreamReaderFactory();
            _httpWebProcessor = new HttpWebProcessor(_streamReaderFactory);
            _httpWebProxyRequestFactory = new HttpWebProxyRequestFactory(_httpWebRequestFactory, _httpWebProcessor);
            _httpWebResponseFactory = new HttpWebResponseFactory();
            _httpWebProxy = new HttpWebProxy(_httpWebProxyRequestFactory, _httpWebResponseFactory, _httpWebProcessor);
            
            _httpWebRequestParametersGet = new HttpWebRequestParameters()
            {
                RequestUri = new Uri("http://httpbin.org/get"),
                ContentType = "*/*",
                Method = HttpRequestMethod.Get,
                ReadWriteTimeout = 30000,
                Timeout = 30000,
                ResponseCallback = ResponseCallbackGet
            };

            var encoding = new ASCIIEncoding();
            _httpWebRequestParametersPost = new HttpWebRequestParameters()
            {
                RequestUri = new Uri("http://httpbin.org/post"),
                RequestData = POST_DATA_TEST,
                Encoding = encoding,
                ContentLength = POST_DATA_TEST.Length,
                ContentType = "*/*",
                Method = HttpRequestMethod.Post,
                ReadWriteTimeout = 30000,
                Timeout = 30000,
                ResponseCallback = ResponseCallbackPost
            };

            _manualResetEventGet = new ManualResetEvent(false);
            _manualResetEventPost = new ManualResetEvent(false);
        }

        [TestMethod]
        public void RunGetDemo()
        {
            var responseData = _httpWebProxy.Request(_httpWebRequestParametersGet);

            Console.WriteLine(responseData);
            Assert.IsNotNull(responseData);
            Assert.IsTrue(responseData.Contains(HTTP_DOMAIN));
        }

        [TestMethod]
        public void RunPostDemo()
        {
            var responseData = _httpWebProxy.Request(_httpWebRequestParametersPost);

            Console.WriteLine(responseData);
            Assert.IsNotNull(responseData);
            Assert.IsTrue(responseData.Contains(POST_DATA_TEST_CONTENTS));
        }

        [TestMethod]
        public void RunAsyncGetDemo()
        {
            _httpWebProxy.BeginRequest(_httpWebRequestParametersGet);

            if(_manualResetEventGet.WaitOne(30000))
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail();
            }
        }

        private void ResponseCallbackGet(string responseData)
        {
            Console.WriteLine(responseData);
            Assert.IsNotNull(responseData);
            Assert.IsTrue(responseData.Contains(HTTP_DOMAIN));

            while (!_manualResetEventGet.Set())
            {
                Thread.SpinWait(1000);
            }
        }

        [TestMethod]
        public void RunAsyncPostDemo()
        {
            _httpWebProxy.BeginRequest(_httpWebRequestParametersPost);

            if (_manualResetEventPost.WaitOne(30000))
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail();
            }
        }

        private void ResponseCallbackPost(string responseData)
        {
            Console.WriteLine(responseData);
            Assert.IsNotNull(responseData);
            Assert.IsTrue(responseData.Contains(POST_DATA_TEST_CONTENTS));

            while (!_manualResetEventPost.Set())
            {
                Thread.SpinWait(1000);
            }
        }
    }
}