using Neat.Wrapper.Abstract;

namespace Neat.Service.Rest.Client.Interface
{
    public interface IHttpWebResponseProcessor
    {
        string ExtractBodyAsString(HttpWebResponseBase httpWebResponse);
    }
}