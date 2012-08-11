using System.Net;
using neat.wrapper.factory.@interface;

namespace neat.wrapper.factory
{
    public class HttpWebResponseFactory : IHttpWebResponseFactory
    {
         public HttpWebResponseWrapper Create(HttpWebResponse response)
         {
             return new HttpWebResponseWrapper(response);
         }
    }
}