namespace Neat.Service.Rest.Client.Proxy.Interface
{
    public interface IHttpWebProxy
    {
        /// <summary>
        /// Send the httpWebProxyRequest to the remote server and return the response
        /// </summary>
        /// <param name="httpWebRequestParameters"> </param>
        /// <returns>Response data received from the remote server </returns>
        string Request(HttpWebRequestParameters httpWebRequestParameters);

        /// <summary>
        /// Send the httpWebProxyRequest to the remote server asynchronously and specify a Response callback
        /// </summary>
        /// <param name="httpWebRequestParameters"> </param>
        void BeginRequest(HttpWebRequestParameters httpWebRequestParameters);
    }
}