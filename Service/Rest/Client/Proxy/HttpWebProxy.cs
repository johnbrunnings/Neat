using System;
using System.IO;
using System.Net;
using Neat.Service.Rest.Client.Factory.Interface;
using Neat.Service.Rest.Client.Interface;
using Neat.Service.Rest.Client.Proxy.Interface;
using Neat.Wrapper.Abstract;
using Neat.Wrapper.Factory.Interface;

namespace Neat.Service.Rest.Client.Proxy
{
    public class HttpWebProxy : IHttpWebProxy
    {
        private readonly IHttpWebProxyRequestFactory _httpWebProxyRequestFactory;
        private readonly IHttpWebResponseFactory _httpWebResponseFactory;
        private readonly IHttpWebProcessor _httpWebProcessor;

        public HttpWebProxy(IHttpWebProxyRequestFactory httpWebProxyRequestFactory, IHttpWebResponseFactory httpWebResponseFactory, IHttpWebProcessor httpWebProcessor)
        {
            _httpWebProxyRequestFactory = httpWebProxyRequestFactory;
            _httpWebResponseFactory = httpWebResponseFactory;
            _httpWebProcessor = httpWebProcessor;
        }

        /// <summary>
        /// Send the httpWebProxyRequest to the remote server and return the response
        /// </summary>
        /// <param name="httpWebRequestParameters"> </param>
        /// <returns>Response data received from the remote server </returns>
        public string Request(HttpWebRequestParameters httpWebRequestParameters)
        {
            var httpWebProxyRequest = _httpWebProxyRequestFactory.Create(httpWebRequestParameters);
            SetupDelegates(httpWebProxyRequest);

            if (httpWebRequestParameters.Method == HttpRequestMethod.Post || httpWebRequestParameters.Method == HttpRequestMethod.Put)
            {
                ProcessRequest(httpWebProxyRequest);
            }
            
            var httpWebResponse = _httpWebResponseFactory.Create(httpWebProxyRequest.HttpWebRequestBase.GetResponse() as HttpWebResponse);

            return ProcessReponse(httpWebResponse, httpWebProxyRequest);
        }

        /// <summary>
        /// Send the httpWebProxyRequest to the remote server asynchronously and specify a Response callback
        /// </summary>
        /// <param name="httpWebRequestParameters"> </param>
        public void BeginRequest(HttpWebRequestParameters httpWebRequestParameters)
        {
            var httpWebProxyRequest = _httpWebProxyRequestFactory.Create(httpWebRequestParameters);
            SetupDelegates(httpWebProxyRequest);

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
            var httpWebProxyRequest = asyncResult.AsyncState as HttpWebProxyRequest;
            ProcessAsyncRequest(httpWebProxyRequest, asyncResult);
        }

        private void GetResponseCallback(IAsyncResult asyncResult)
        {
            var httpWebProxyRequest = asyncResult.AsyncState as HttpWebProxyRequest;
            var httpWebResponse = _httpWebResponseFactory.Create(httpWebProxyRequest.HttpWebRequestBase.EndGetResponse(asyncResult) as HttpWebResponse);
            ProcessReponse(httpWebResponse, httpWebProxyRequest);
        }

        private void ProcessRequest(HttpWebProxyRequest httpWebProxyRequest)
        {
            Stream requestStream = null;
            try
            {
                requestStream = httpWebProxyRequest.HttpWebRequestBase.GetRequestStream();
                httpWebProxyRequest.ProcessRequestStream(httpWebProxyRequest.RequestBytes, requestStream);
            }
            finally
            {
                if (requestStream != null)
                {
                    requestStream.Close();
                }
            }
        }

        private void ProcessAsyncRequest(HttpWebProxyRequest httpWebProxyRequest, IAsyncResult asyncResult)
        {
            Stream requestStream = null;
            try
            {
                requestStream = httpWebProxyRequest.HttpWebRequestBase.EndGetRequestStream(asyncResult);
                httpWebProxyRequest.ProcessRequestStream(httpWebProxyRequest.RequestBytes, requestStream);
                httpWebProxyRequest.HttpWebRequestBase.BeginGetResponse(new AsyncCallback(GetResponseCallback), httpWebProxyRequest);
            }
            finally
            {
                if (requestStream != null)
                {
                    requestStream.Close();
                }
            }
        }

        private string ProcessReponse(HttpWebResponseBase httpWebResponse, HttpWebProxyRequest httpWebProxyRequest)
        {
            Stream responseStream = null;
            try
            {
                responseStream = httpWebResponse.GetResponseStream();
                var responseData = httpWebProxyRequest.ProcessResponseStream(responseStream);
                if (httpWebProxyRequest.ResponseCallback != null)
                {
                    httpWebProxyRequest.ResponseCallback(responseData);
                }
                return responseData;
            }
            finally
            {
                if (responseStream != null)
                {
                    responseStream.Close();
                }
                if(httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }
            }
        }

        private void SetupDelegates(HttpWebProxyRequest httpWebProxyRequest)
        {
            if (httpWebProxyRequest.ProcessRequestStream == null)
            {
                httpWebProxyRequest.ProcessRequestStream = ProcessRequestStream;
            }
            if (httpWebProxyRequest.ProcessResponseStream == null)
            {
                httpWebProxyRequest.ProcessResponseStream = ProcessResponseStream;
            }
        }

        private void ProcessRequestStream(byte[] requestBytes, Stream requestStream)
        {
            requestStream.Write(requestBytes, 0, requestBytes.Length);
        }

        private string ProcessResponseStream(Stream responseStream)
        {
            return _httpWebProcessor.GetResponseDataAsString(responseStream);
        }
    }
}