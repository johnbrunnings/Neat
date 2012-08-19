using Neat.Wrapper.Abstract;

namespace Neat.Service.Rest.Interface
{
    public interface IHttpWebResponseProcessor
    {
        string ExtractBodyAsString(HttpWebResponseBase httpWebResponse);
    }
}