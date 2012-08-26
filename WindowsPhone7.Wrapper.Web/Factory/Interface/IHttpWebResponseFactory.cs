using System.Net;

namespace Neat.WindowsPhone7.Wrapper.Web.Factory.Interface
{
    public interface IHttpWebResponseFactory
    {
        HttpWebResponseWrapper Create(HttpWebResponse response);
    }
}