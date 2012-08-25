using System;
using System.IO;
using System.Net;
using Neat.WindowsPhone7.Service.Rest.Client.Factory.Interface;
using Neat.WindowsPhone7.Service.Rest.Client.Interface;
using Neat.WindowsPhone7.Service.Rest.Client.Proxy.Interface;
using Neat.WindowsPhone7.Wrapper.Factory.Interface;

namespace Neat.WindowsPhone7.Service.Rest.Client.Proxy
{
    public class HttpWebProxy : IHttpWebProxy
    {
        private readonly IHttpWebProxyRequestFactory _httpWebProxyRequestFactory;
        private readonly IHttpWebResponseFactory _httpWebResponseFactory;
        private readonly IHttpWebResponseProcessor _httpWebResponseProcessor;

        public HttpWebProxy(IHttpWebProxyRequestFactory httpWebProxyRequestFactory, IHttpWebResponseFactory httpWebResponseFactory, IHttpWebResponseProcessor httpWebResponseProcessor)
        {
            _httpWebProxyRequestFactory = httpWebProxyRequestFactory;
            _httpWebResponseFactory = httpWebResponseFactory;
            _httpWebResponseProcessor = httpWebResponseProcessor;
        }

        /// <summary>
        /// Send the httpWebProxyRequest to the remote server asynchronously and specify a Response callback
        /// </summary>
        /// <param name="httpWebRequestParameters"> </param>
        public void BeginRequest(HttpWebRequestParameters httpWebRequestParameters)
        {
            var httpWebProxyRequest = _httpWebProxyRequestFactory.Create(httpWebRequestParameters);

            if (httpWebRequestParameters.Method == HttpRequestMethod.Post || httpWebRequestParameters.Method == HttpRequestMethod.Put)
            {
                httpWebProxyRequest.HttpWebRequestBase.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), httpWebProxyRequest);
            }
            else
            {
                httpWebProxyRequest.HttpWebRequestBase.BeginGetResponse(new AsyncCallback(GetResponseCallback), httpWebProxyRequest);
            }
        }

        private void GetRequestStreamCallback(IAsyncResult asyncResult)
        {
            Stream requestStream = null;

            try
            {
                var httpWebProxyRequest = asyncResult.AsyncState as HttpWebProxyRequest;
                var httpWebRequest = httpWebProxyRequest.HttpWebRequestBase;
                requestStream = httpWebRequest.EndGetRequestStream(asyncResult);
                requestStream.Write(httpWebProxyRequest.RequestBytes, 0, httpWebProxyRequest.RequestBytes.Length);
                httpWebRequest.BeginGetResponse(new AsyncCallback(GetResponseCallback), httpWebProxyRequest);
            }
            finally
            {
                if (requestStream != null)
                {
                    requestStream.Close();
                }
            }
        }

        private void GetResponseCallback(IAsyncResult asyncResult)
        {
            var httpWebProxyRequest = asyncResult.AsyncState as HttpWebProxyRequest;
            var httpWebRequest = httpWebProxyRequest.HttpWebRequestBase;
            var httpWebResponse = _httpWebResponseFactory.Create(httpWebRequest.EndGetResponse(asyncResult) as HttpWebResponse);
            httpWebProxyRequest.ResponseCallback(_httpWebResponseProcessor.ExtractBodyAsString(httpWebResponse));
        }
    }
}