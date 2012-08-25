using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neat.WindowsPhone7.Service.Rest.Client;
using Neat.WindowsPhone7.Service.Rest.Client.Factory;
using Neat.WindowsPhone7.Service.Rest.Client.Factory.Interface;
using Neat.WindowsPhone7.Service.Rest.Client.Interface;
using Neat.WindowsPhone7.Service.Rest.Client.Proxy;
using Neat.WindowsPhone7.Service.Rest.Client.Proxy.Interface;
using Neat.WindowsPhone7.Wrapper.Factory;
using Neat.WindowsPhone7.Wrapper.Factory.Interface;

namespace Neat.WindowsPhone7.Service.Test.Integration.Rest
{
    // Special Thanks to Jeff Wilcox - http://www.jeff.wilcox.name/2011/06/updated-ut-mango-bits/

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
        private IHttpWebResponseProcessor _httpWebResponseProcessor;
        private HttpWebRequestParameters _httpWebRequestParametersGet;
        private HttpWebRequestParameters _httpWebRequestParametersPost;
        private ManualResetEvent _manualResetEventGet;
        private ManualResetEvent _manualResetEventPost;
        private Byte[] _requestBytes;

        [TestInitialize]
        public void Initialize()
        {
            _httpWebRequestFactory = new HttpWebRequestFactory();
            _httpWebProxyRequestFactory = new HttpWebProxyRequestFactory(_httpWebRequestFactory);
            _httpWebResponseFactory = new HttpWebResponseFactory();
            _streamReaderFactory = new StreamReaderFactory();
            _httpWebResponseProcessor = new HttpWebResponseProcessor(_streamReaderFactory);
            _httpWebProxy = new HttpWebProxy(_httpWebProxyRequestFactory, _httpWebResponseFactory, _httpWebResponseProcessor);
            
            _httpWebRequestParametersGet = new HttpWebRequestParameters()
            {
                RequestUri = new Uri("http://httpbin.org/get"),
                ContentType = "*/*",
                Method = HttpRequestMethod.Get,
                ReadWriteTimeout = 30000,
                Timeout = 30000,
                ResponseCallback = ResponseCallbackGet
            };

            var encoding = new UTF8Encoding();
            _requestBytes = encoding.GetBytes(POST_DATA_TEST);
            _httpWebRequestParametersPost = new HttpWebRequestParameters()
            {
                RequestUri = new Uri("http://httpbin.org/post"),
                ContentLength = _requestBytes.Length,
                ContentType = "*/*",
                Method = HttpRequestMethod.Post,
                ReadWriteTimeout = 30000,
                Timeout = 30000,
                RequestBytes = _requestBytes,
                ResponseCallback = ResponseCallbackPost
            };

            _manualResetEventGet = new ManualResetEvent(false);
            _manualResetEventPost = new ManualResetEvent(false);
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
            Debug.WriteLine(responseData);
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
            Debug.WriteLine(responseData);
            Assert.IsNotNull(responseData);
            Assert.IsTrue(responseData.Contains(POST_DATA_TEST_CONTENTS));

            while (!_manualResetEventPost.Set())
            {
                Thread.SpinWait(1000);
            }
        }
    }
}