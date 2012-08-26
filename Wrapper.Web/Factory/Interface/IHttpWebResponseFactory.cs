using System.Net;

namespace Neat.Wrapper.Web.Factory.Interface
{
    public interface IHttpWebResponseFactory
    {
        HttpWebResponseWrapper Create(HttpWebResponse response);
    }
}