using System.Net;
using Neat.Wrapper.Factory.Interface;

namespace Neat.Wrapper.Factory
{
    public class HttpWebResponseFactory : IHttpWebResponseFactory
    {
         public HttpWebResponseWrapper Create(HttpWebResponse response)
         {
             return new HttpWebResponseWrapper(response);
         }
    }
}