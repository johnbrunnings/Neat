using System.IO;
using System.Text;
using Neat.Service.Rest.Client.Interface;
using Neat.Wrapper.Stream.Abstract;
using Neat.Wrapper.Stream.Factory.Interface;

namespace Neat.Service.Rest.Client
{
    public class HttpWebProcessor : IHttpWebProcessor
    {
        private readonly IStreamReaderFactory _streamReaderFactory;

        public HttpWebProcessor(IStreamReaderFactory streamReaderFactory)
        {
            _streamReaderFactory = streamReaderFactory;
        }

        public byte[] GetRequestBytesFromRequestData(string requestData, Encoding encoding)
        {
            return encoding.GetBytes(requestData);
        }
        
        public string GetResponseDataAsString(Stream responseStream)
        {
            string responseData;
            StreamReaderBase responseStreamReader = null;

            try
            {
                responseStreamReader = _streamReaderFactory.Create(responseStream);
                responseData = responseStreamReader.ReadToEnd().Trim();
            }
            finally
            {
                if (responseStreamReader != null)
                {
                    responseStreamReader.Close();
                }
            }

            return responseData;
        }
    }
}