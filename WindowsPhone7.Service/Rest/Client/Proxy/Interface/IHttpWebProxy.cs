namespace Neat.WindowsPhone7.Service.Rest.Client.Proxy.Interface
{
    public interface IHttpWebProxy
    {
        /// <summary>
        /// Send the httpWebProxyRequest to the remote server asynchronously and specify a Response callback
        /// </summary>
        /// <param name="httpWebRequestParameters"> </param>
        void BeginRequest(HttpWebRequestParameters httpWebRequestParameters);
    }
}