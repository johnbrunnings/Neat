using Neat.WindowsPhone7.Wrapper.Abstract;

namespace Neat.WindowsPhone7.Service.Rest.Client.Interface
{
    public interface IHttpWebResponseProcessor
    {
        string ExtractBodyAsString(HttpWebResponseBase httpWebResponse);
    }
}