namespace Neat.WindowsPhone7.Service.Rest.Client.Factory.Interface
{
    public interface IHttpWebProxyRequestFactory
    {
        HttpWebProxyRequest Create(HttpWebRequestParameters httpWebRequestParameters);
    }
}