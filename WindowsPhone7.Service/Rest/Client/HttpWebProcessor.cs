using System.IO;
using System.Text;
using Neat.WindowsPhone7.Service.Rest.Client.Interface;
using Neat.WindowsPhone7.StreamManager.Factory.ReaderFactory.Interface;
using Neat.WindowsPhone7.StreamManager.Factory.ReaderFactory.Parameters;

namespace Neat.WindowsPhone7.Service.Rest.Client
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