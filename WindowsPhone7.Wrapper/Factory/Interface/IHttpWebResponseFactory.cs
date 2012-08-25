using System.Net;

namespace Neat.WindowsPhone7.Wrapper.Factory.Interface
{
    public interface IHttpWebResponseFactory
    {
        HttpWebResponseWrapper Create(HttpWebResponse response);
    }
}