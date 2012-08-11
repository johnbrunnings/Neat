using neat.wrapper.parent;

namespace neat.service.rest.proxy.@interface
{
    public interface IHttpWebProxy
    {
        /// <summary>
        /// Send the request to the remote server and return the response
        /// </summary>
        /// <param name="httpWebRequestBase">Request to send to the server </param>
        /// <returns>Response received from the remote server </returns>
        HttpWebResponseBase GetResponse(HttpWebRequestBase httpWebRequestBase);

        /// <summary>
        /// Send the request to the remote server as POST and return the response
        /// </summary>
        /// <param name="httpWebRequestBase">Request to send to the server </param>
        /// /// <param name="requestBytes">Request Bytes to send to the server </param>
        /// <returns>Response received from the remote server </returns>
        HttpWebResponseBase PostForResponse(HttpWebRequestBase httpWebRequestBase, byte[] requestBytes);
    }
}