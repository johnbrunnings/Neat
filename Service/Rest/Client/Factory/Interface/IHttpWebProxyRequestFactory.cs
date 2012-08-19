namespace Neat.Service.Rest.Client.Factory.Interface
{
    public interface IHttpWebProxyRequestFactory
    {
        HttpWebProxyRequest Create(HttpWebRequestParameters httpWebRequestParameters);
    }
}