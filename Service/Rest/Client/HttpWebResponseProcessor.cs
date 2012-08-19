using Neat.Service.Rest.Client.Interface;
using Neat.Wrapper.Abstract;
using Neat.Wrapper.Factory.Interface;

namespace Neat.Service.Rest.Client
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