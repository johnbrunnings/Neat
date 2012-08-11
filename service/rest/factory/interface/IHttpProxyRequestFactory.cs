using neat.wrapper.parent;

namespace neat.service.rest.factory.@interface
{
    public interface IHttpProxyRequestFactory
    {
        HttpWebRequestBase Create(HttpWebRequestParameters httpWebRequestParameters);
    }
}