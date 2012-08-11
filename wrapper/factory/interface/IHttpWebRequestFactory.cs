using System;
using System.Net;

namespace neat.wrapper.factory.@interface
{
    public interface IHttpWebRequestFactory
    {
        HttpWebRequestWrapper Create(Uri requestUri);
        HttpWebRequestWrapper Create(string requestUri);
        HttpWebRequestWrapper Create(System.Net.HttpWebRequest request);
    }
}