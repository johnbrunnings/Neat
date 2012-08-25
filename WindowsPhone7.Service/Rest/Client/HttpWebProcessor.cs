﻿using System.IO;
using System.Text;
using Neat.WindowsPhone7.Service.Rest.Client.Interface;
using Neat.WindowsPhone7.Wrapper.Abstract;
using Neat.WindowsPhone7.Wrapper.Factory.Interface;

namespace Neat.WindowsPhone7.Service.Rest.Client
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