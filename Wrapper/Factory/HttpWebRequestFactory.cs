using System;
using System.Net;
using Neat.Wrapper.Factory.Interface;

namespace Neat.Wrapper.Factory
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