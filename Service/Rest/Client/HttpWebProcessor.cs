using System.IO;
using System.Text;
using Neat.Service.Rest.Client.Interface;
using Neat.StreamManager.Factory.ReaderFactory.Interface;
using Neat.StreamManager.Factory.ReaderFactory.Parameters;

namespace Neat.Service.Rest.Client
{
    public class HttpWebProcessor : IHttpWebProcessor
    {
        private readonly ITextReaderFactory _streamReaderFactory;

        public HttpWebProcessor(ITextReaderFactory streamReaderFactory)
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
            var streamReaderParameters = new StreamReaderParameters()
            {
                Stream = responseStream
            };
            TextReader responseStreamReader = null;

            try
            {
                responseStreamReader = _streamReaderFactory.Create(streamReaderParameters);
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