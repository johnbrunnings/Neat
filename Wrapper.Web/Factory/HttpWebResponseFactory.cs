using System.Net;
using Neat.Wrapper.Web.Factory.Interface;

namespace Neat.Wrapper.Web.Factory
{
    public class HttpWebResponseFactory : IHttpWebResponseFactory
    {
         public HttpWebResponseWrapper Create(HttpWebResponse response)
         {
             return new HttpWebResponseWrapper(response);
         }
    }
}