using System.Net;

namespace neat.wrapper.factory.@interface
{
    public interface IHttpWebResponseFactory
    {
        HttpWebResponseWrapper Create(HttpWebResponse response);
    }
}