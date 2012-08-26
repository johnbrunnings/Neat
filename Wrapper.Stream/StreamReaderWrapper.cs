using System;
using System.IO;
using System.Runtime.Remoting;
using System.Text;
using Neat.Wrapper.Stream.Abstract;

namespace Neat.Wrapper.Stream
{
    public class StreamReaderWrapper : StreamReaderBase
    {
        private readonly StreamReader _streamReader;

        public StreamReaderWrapper(StreamReader streamReader)
        {
            _streamReader = streamReader;
        }

        public new object GetLifetimeService()
        {
            return _streamReader.GetLifetimeService();
        }

        public override object InitializeLifetimeService()
        {
            return _streamReader.InitializeLifetimeService();
        }

        public override ObjRef CreateObjRef(Type requestedType)
        {
            return _streamReader.CreateObjRef(requestedType);
        }

        public new void Dispose()
        {
            _streamReader.Dispose();
        }

        public override int ReadBlock(char[] buffer, int index, int count)
        {
            return _streamReader.ReadBlock(buffer, index, count);
        }

        public override void Close()
        {
            _streamReader.Close();
        }

        public override void DiscardBufferedData()
        {
            _streamReader.DiscardBufferedData();
        }

        public override int Peek()
        {
            return _streamReader.Peek();
        }

        public override int Read()
        {
            return _streamReader.Read();
        }

        public override int Read(char[] buffer, int index, int count)
        {
            return _streamReader.Read(buffer, index, count);
        }

        public override string ReadToEnd()
        {
            return _streamReader.ReadToEnd();
        }

        public override string ReadLine()
        {
            return _streamReader.ReadLine();
        }

        public override Encoding CurrentEncoding
        {
            get { return _streamReader.CurrentEncoding; }
        }

        public override System.IO.Stream BaseStream
        {
            get { return _streamReader.BaseStream; }
        }

        public override bool EndOfStream
        {
            get { return _streamReader.EndOfStream; }
        }
    }
}