using neat.service.rest.@interface;
using neat.wrapper.factory.@interface;
using neat.wrapper.parent;

namespace neat.service.rest
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
            var responseStream = _streamReaderFactory.Create(httpWebResponse.GetResponseStream());

             return responseStream.ReadToEnd().Trim();
         }
    }
}