using Neat.WindowsPhone7.Service.Rest.Client.Interface;
using Neat.WindowsPhone7.Wrapper.Abstract;
using Neat.WindowsPhone7.Wrapper.Factory.Interface;

namespace Neat.WindowsPhone7.Service.Rest.Client
{
    public class HttpWebResponseProcessor : IHttpWebResponseProcessor
    {
        private readonly IStreamReaderFactory _streamReaderFactory;

        public HttpWebResponseProcessor(IStreamReaderFactory streamReaderFactory)
        {
            _streamReaderFactory = streamReaderFactory;
        }

        public string ExtractBodyAsString(HttpWebResponseBase httpWebResponse)
        {
            string responseData;
            StreamReaderBase responseStream = null;

            try
            {
                responseStream = _streamReaderFactory.Create(httpWebResponse.GetResponseStream());
                responseData = responseStream.ReadToEnd().Trim();
            }
            finally
            {
                if(responseStream != null)
                {
                    responseStream.Close();
                }
            }

            return responseData;
        }
    }
}