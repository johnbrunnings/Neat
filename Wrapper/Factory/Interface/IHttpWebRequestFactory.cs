using System;
using System.Net;

namespace Neat.Wrapper.Factory.Interface
{
    public interface IHttpWebRequestFactory
    {
        HttpWebRequestWrapper Create(Uri requestUri);
        HttpWebRequestWrapper Create(string requestUri);
        HttpWebRequestWrapper Create(HttpWebRequest request);
    }
}