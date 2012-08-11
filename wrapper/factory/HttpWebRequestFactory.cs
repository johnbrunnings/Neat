using System;
using System.Net;
using neat.wrapper.factory.@interface;

namespace neat.wrapper.factory
{
    public class HttpWebRequestFactory : IHttpWebRequestFactory
    {
        public HttpWebRequestWrapper Create(Uri requestUri)
        {
            return new HttpWebRequestWrapper(WebRequest.Create(requestUri) as System.Net.HttpWebRequest);
        }

        public HttpWebRequestWrapper Create(string requestUri)
        {
            return new HttpWebRequestWrapper(WebRequest.Create(requestUri) as System.Net.HttpWebRequest);
        }

        public HttpWebRequestWrapper Create(System.Net.HttpWebRequest request)
        {
            return new HttpWebRequestWrapper(request);
        }
    }
}