using neat.wrapper.parent;

namespace neat.service.rest.@interface
{
    public interface IHttpWebResponseProcessor
    {
        string ExtractBodyAsString(HttpWebResponseBase httpWebResponse);
    }
}