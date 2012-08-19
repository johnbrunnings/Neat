namespace Neat.Service.Rest.Factory.Interface
{
    public interface IHttpWebProxyRequestFactory
    {
        HttpWebProxyRequest Create(HttpWebRequestParameters httpWebRequestParameters);
    }
}