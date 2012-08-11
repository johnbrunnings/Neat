using System;
using System.IO;
using System.Net;
using neat.service.rest.proxy.@interface;
using neat.wrapper.factory.@interface;
using neat.wrapper.parent;

namespace neat.service.rest.proxy
{
    public class HttpWebProxy : IHttpWebProxy
    {
        private readonly IHttpWebResponseFactory _httpWebResponseFactory;

        public HttpWebProxy(IHttpWebResponseFactory httpWebResponseFactory)
        {
            _httpWebResponseFactory = httpWebResponseFactory;
        }

        /// <summary>
        /// Send the request to the remote server and return the response
        /// </summary>
        /// <param name="httpWebRequestBase">Request to send to the server </param>
        /// <returns>Response received from the remote server </returns>
        public HttpWebResponseBase GetResponse(HttpWebRequestBase httpWebRequestBase)
        {
            return _httpWebResponseFactory.Create(httpWebRequestBase.GetResponse() as HttpWebResponse);
        }

        /// <summary>
        /// Send the request to the remote server as POST and return the response
        /// </summary>
        /// <param name="httpWebRequestBase">Request to send to the server </param>
        /// /// <param name="requestBytes">Request Bytes to send to the server </param>
        /// <returns>Response received from the remote server </returns>
        public HttpWebResponseBase PostForResponse(HttpWebRequestBase httpWebRequestBase, byte[] requestBytes)
        {
            if (httpWebRequestBase.Method != HttpRequestMethod.Post.ToString().ToUpperInvariant() && httpWebRequestBase.Method != HttpRequestMethod.Put.ToString().ToUpperInvariant())
            {
                throw new ArgumentException(string.Format("HttpWebProxy::PostForResponse - Invalid HTTP Method {0}.", httpWebRequestBase.Method));
            }

            Stream requestStream = null;

            try
            {
                requestStream = httpWebRequestBase.GetRequestStream();
                requestStream.Write(requestBytes, 0, requestBytes.Length);
            }
            finally
            {
                if (requestStream != null)
                {
                    requestStream.Close();
                }
            }

            return GetResponse(httpWebRequestBase);
        }
    }
}