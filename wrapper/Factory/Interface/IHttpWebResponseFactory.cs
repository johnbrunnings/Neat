using System.Net;

namespace Neat.Wrapper.Factory.Interface
{
    public interface IHttpWebResponseFactory
    {
        HttpWebResponseWrapper Create(HttpWebResponse response);
    }
}