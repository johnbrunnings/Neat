﻿using System;
using System.Text;
using System.Threading;
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
            _httpWebProxy = new HttpWebProxy(_httpWebResponseFactory, _httpWebResponseProcessor);
            
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
        public void RunGetDemo()
        {
            var httpWebProxyRequest = _httpWebProxyRequestFactory.Create(_httpWebRequestParametersGet);
            var responseData = _httpWebProxy.Request(httpWebProxyRequest);

            Console.WriteLine(responseData);
            Assert.IsNotNull(responseData);
            Assert.IsTrue(responseData.Contains(HTTP_DOMAIN));
        }

        [TestMethod]
        public void RunPostDemo()
        {
            var httpWebProxyRequest = _httpWebProxyRequestFactory.Create(_httpWebRequestParametersPost);
            var responseData = _httpWebProxy.Request(httpWebProxyRequest);

            Console.WriteLine(responseData);
            Assert.IsNotNull(responseData);
            Assert.IsTrue(responseData.Contains(POST_DATA_TEST_CONTENTS));
        }

        [TestMethod]
        public void RunAsyncGetDemo()
        {
            var httpWebProxyRequest = _httpWebProxyRequestFactory.Create(_httpWebRequestParametersGet);
            _httpWebProxy.BeginRequest(httpWebProxyRequest);

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
            var httpWebProxyRequest = _httpWebProxyRequestFactory.Create(_httpWebRequestParametersPost);
            _httpWebProxy.BeginRequest(httpWebProxyRequest);

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