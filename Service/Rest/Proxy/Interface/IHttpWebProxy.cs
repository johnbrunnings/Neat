using Neat.Wrapper.Abstract;

namespace Neat.Service.Rest.Proxy.Interface
{
    public interface IHttpWebProxy
    {
        /// <summary>
        /// Send the httpWebProxyRequest to the remote server and return the response
        /// </summary>
        /// <param name="httpWebProxyRequest">Request to send to the server </param>
        /// <returns>Response data received from the remote server </returns>
        string Request(HttpWebProxyRequest httpWebProxyRequest);

        /// <summary>
        /// Send the httpWebProxyRequest to the remote server asynchronously and specify a Response callback
        /// </summary>
        /// <param name="httpWebProxyRequest">Request for async httpWebProxyRequest to the server </param>
        void BeginRequest(HttpWebProxyRequest httpWebProxyRequest);
    }
}