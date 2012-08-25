using System;
using System.Net;
using Neat.WindowsPhone7.Wrapper.Factory.Interface;

namespace Neat.WindowsPhone7.Wrapper.Factory
{
    public class HttpWebRequestFactory : IHttpWebRequestFactory
    {
        public HttpWebRequestWrapper Create(Uri requestUri)
        {
            return new HttpWebRequestWrapper(WebRequest.Create(requestUri) as HttpWebRequest);
        }

        public HttpWebRequestWrapper Create(string requestUri)
        {
            return new HttpWebRequestWrapper(WebRequest.Create(requestUri) as HttpWebRequest);
        }

        public HttpWebRequestWrapper Create(HttpWebRequest request)
        {
            return new HttpWebRequestWrapper(request);
        }
    }
}