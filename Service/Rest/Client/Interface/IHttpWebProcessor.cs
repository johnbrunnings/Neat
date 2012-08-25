using System.IO;
using System.Text;

namespace Neat.Service.Rest.Client.Interface
{
    public interface IHttpWebProcessor
    {
        byte[] GetRequestBytesFromRequestData(string requestData, Encoding encoding);
        string GetResponseDataAsString(Stream responseStream);
    }
}