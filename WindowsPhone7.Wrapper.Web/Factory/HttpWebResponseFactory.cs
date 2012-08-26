using System.Net;
using Neat.WindowsPhone7.Wrapper.Web.Factory.Interface;

namespace Neat.WindowsPhone7.Wrapper.Web.Factory
{
    public class HttpWebResponseFactory : IHttpWebResponseFactory
    {
         public HttpWebResponseWrapper Create(HttpWebResponse response)
         {
             return new HttpWebResponseWrapper(response);
         }
    }
}