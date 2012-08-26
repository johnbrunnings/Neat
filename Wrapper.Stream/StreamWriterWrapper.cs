using System;
using System.IO;
using System.Runtime.Remoting;
using System.Text;
using Neat.Wrapper.Stream.Abstract;

namespace Neat.Wrapper.Stream
{
    public class StreamWriterWrapper : StreamWriterBase
    {
        private readonly StreamWriter _streamWriter;

        public StreamWriterWrapper(StreamWriter streamWriter)
        {
            _streamWriter = streamWriter;
        }

        public new object GetLifetimeService()
        {
            return _streamWriter.GetLifetimeService();
        }

        public override object InitializeLifetimeService()
        {
            return _streamWriter.InitializeLifetimeService();
        }

        public override ObjRef CreateObjRef(Type requestedType)
        {
            return _streamWriter.CreateObjRef(requestedType);
        }

        public new void Dispose()
        {
            _streamWriter.Dispose();
        }

        public override void Write(bool value)
        {
            _streamWriter.Write(value);
        }

        public override void Write(int value)
        {
            _streamWriter.Write(value);
        }

        public override void Write(uint value)
        {
            _streamWriter.Write(value);
        }

        public override void Write(long value)
        {
            _streamWriter.Write(value);
        }

        public override void Write(ulong value)
        {
            _streamWriter.Write(value);
        }

        public override void Write(float value)
        {
            _streamWriter.Write(value);
        }

        public override void Write(double value)
        {
            _streamWriter.Write(value);
        }

        public override void Write(decimal value)
        {
            _streamWriter.Write(value);
        }

        public override void Write(object value)
        {
            _streamWriter.Write(value);
        }

        public override void Write(string format, object arg0)
        {
            _streamWriter.Write(format, arg0);
        }

        public override void Write(string format, object arg0, object arg1)
        {
            _streamWriter.Write(format, arg0, arg1);
        }

        public override void Write(string format, object arg0, object arg1, object arg2)
        {
            _streamWriter.Write(format, arg0, arg1, arg2);
        }

        public override void Write(string format, params object[] arg)
        {
            _streamWriter.Write(format, arg);
        }

        public override void WriteLine()
        {
            _streamWriter.WriteLine();
        }

        public override void WriteLine(char value)
        {
            _streamWriter.WriteLine(value);
        }

        public override void WriteLine(char[] buffer)
        {
            _streamWriter.WriteLine(buffer);
        }

        public override void WriteLine(char[] buffer, int index, int count)
        {
            _streamWriter.WriteLine(buffer, index, count);
        }

        public override void WriteLine(bool value)
        {
            _streamWriter.WriteLine(value);
        }

        public override void WriteLine(int value)
        {
            _streamWriter.WriteLine(value);
        }

        public override void WriteLine(uint value)
        {
            _streamWriter.WriteLine(value);
        }

        public override void WriteLine(long value)
        {
            _streamWriter.WriteLine(value);
        }

        public override void WriteLine(ulong value)
        {
            _streamWriter.WriteLine(value);
        }

        public override void WriteLine(float value)
        {
            _streamWriter.WriteLine(value);
        }

        public override void WriteLine(double value)
        {
            _streamWriter.WriteLine(value);
        }

        public override void WriteLine(decimal value)
        {
            _streamWriter.WriteLine(value);
        }

        public override void WriteLine(string value)
        {
            _streamWriter.WriteLine(value);
        }

        public override void WriteLine(object value)
        {
            _streamWriter.WriteLine(value);
        }

        public override void WriteLine(string format, object arg0)
        {
            _streamWriter.WriteLine(format, arg0);
        }

        public override void WriteLine(string format, object arg0, object arg1)
        {
            _streamWriter.WriteLine(format, arg0, arg1);
        }

        public override void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            _streamWriter.WriteLine(format, arg0, arg1, arg2);
        }

        public override void WriteLine(string format, params object[] arg)
        {
            _streamWriter.WriteLine(format, arg);
        }

        public override IFormatProvider FormatProvider
        {
            get { return _streamWriter.FormatProvider; }
        }

        public override string NewLine
        {
            get { return _streamWriter.NewLine; }
            set { _streamWriter.NewLine = value; }
        }

        public override void Close()
        {
            _streamWriter.Close();
        }

        public override void Flush()
        {
            _streamWriter.Flush();
        }

        public override void Write(char value)
        {
            _streamWriter.Write(value);
        }

        public override void Write(char[] buffer)
        {
            _streamWriter.Write(buffer);
        }

        public override void Write(char[] buffer, int index, int count)
        {
            _streamWriter.Write(buffer, index, count);
        }

        public override void Write(string value)
        {
            _streamWriter.Write(value);
        }

        public override bool AutoFlush
        {
            get { return _streamWriter.AutoFlush; }
            set { _streamWriter.AutoFlush = value; }
        }

        public override System.IO.Stream BaseStream
        {
            get { return _streamWriter.BaseStream; }
        }

        public override Encoding Encoding
        {
            get { return _streamWriter.Encoding; }
        }
    }
}