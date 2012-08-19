using System;
using System.IO;
using System.Net;
using Neat.Service.Rest.Interface;
using Neat.Service.Rest.Proxy.Interface;
using Neat.Wrapper.Factory.Interface;

namespace Neat.Service.Rest.Proxy
{
    public class HttpWebProxy : IHttpWebProxy
    {
        private readonly IHttpWebResponseFactory _httpWebResponseFactory;
        private readonly IHttpWebResponseProcessor _httpWebResponseProcessor;

        public HttpWebProxy(IHttpWebResponseFactory httpWebResponseFactory, IHttpWebResponseProcessor httpWebResponseProcessor)
        {
            _httpWebResponseFactory = httpWebResponseFactory;
            _httpWebResponseProcessor = httpWebResponseProcessor;
        }

        /// <summary>
        /// Send the httpWebProxyRequest to the remote server and return the response
        /// </summary>
        /// <param name="httpWebProxyRequest">Request to send to the server </param>
        /// <returns>Response data received from the remote server </returns>
        public string Request(HttpWebProxyRequest httpWebProxyRequest)
        {
            var httpWebRequest = httpWebProxyRequest.HttpWebRequestBase;

            if (httpWebProxyRequest.Method == HttpRequestMethod.Post || httpWebProxyRequest.Method == HttpRequestMethod.Put)
            {
                Stream requestStream = null;

                try
                {
                    var requestBytes = httpWebProxyRequest.RequestBytes;
                    requestStream = httpWebRequest.GetRequestStream();
                    requestStream.Write(requestBytes, 0, requestBytes.Length);
                }
                finally
                {
                    if (requestStream != null)
                    {
                        requestStream.Close();
                    }
                }
            }

            var httpWebResponse = _httpWebResponseFactory.Create(httpWebRequest.GetResponse() as HttpWebResponse);

            return _httpWebResponseProcessor.ExtractBodyAsString(httpWebResponse);
        }

        /// <summary>
        /// Send the httpWebProxyRequest to the remote server asynchronously and specify a Response callback
        /// </summary>
        /// <param name="httpWebProxyRequest">Request for async httpWebProxyRequest to the server </param>
        public void BeginRequest(HttpWebProxyRequest httpWebProxyRequest)
        {
            var httpWebRequest = httpWebProxyRequest.HttpWebRequestBase;
            if (httpWebProxyRequest.Method == HttpRequestMethod.Post || httpWebProxyRequest.Method == HttpRequestMethod.Put)
            {
                httpWebRequest.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), httpWebProxyRequest);
            }
            else
            {
                httpWebRequest.BeginGetResponse(new AsyncCallback(GetResponseCallback), httpWebProxyRequest);
            }
        }

        private void GetRequestStreamCallback(IAsyncResult asyncResult)
        {
            Stream requestStream = null;

            try
            {
                var httpAsyncWebRequestParameters = asyncResult.AsyncState as HttpWebProxyRequest;
                var httpWebRequest = httpAsyncWebRequestParameters.HttpWebRequestBase;
                requestStream = httpWebRequest.EndGetRequestStream(asyncResult);
                requestStream.Write(httpAsyncWebRequestParameters.RequestBytes, 0, httpAsyncWebRequestParameters.RequestBytes.Length);
                httpWebRequest.BeginGetResponse(new AsyncCallback(GetResponseCallback), httpAsyncWebRequestParameters);
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
            var httpAsyncWebRequestParameters = asyncResult.AsyncState as HttpWebProxyRequest;
            var httpWebRequest = httpAsyncWebRequestParameters.HttpWebRequestBase;
            var httpWebResponse = _httpWebResponseFactory.Create(httpWebRequest.EndGetResponse(asyncResult) as HttpWebResponse);
            httpAsyncWebRequestParameters.ResponseCallback(_httpWebResponseProcessor.ExtractBodyAsString(httpWebResponse));
        }
    }
}