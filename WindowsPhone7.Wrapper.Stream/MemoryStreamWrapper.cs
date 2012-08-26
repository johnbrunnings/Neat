using System;
using System.IO;
using Neat.WindowsPhone7.Wrapper.Stream.Abstract;

namespace Neat.WindowsPhone7.Wrapper.Stream
{
    public class MemoryStreamWrapper : MemoryStreamBase
    {
        private readonly MemoryStream _memoryStream;

        public MemoryStreamWrapper(MemoryStream memoryStream)
        {
            _memoryStream = memoryStream;
        }

        public new void CopyTo(System.IO.Stream destination)
        {
            _memoryStream.CopyTo(destination);
        }

        public new void CopyTo(System.IO.Stream destination, int bufferSize)
        {
            _memoryStream.CopyTo(destination, bufferSize);
        }

        public override void Close()
        {
            _memoryStream.Close();
        }

        public new void Dispose()
        {
            _memoryStream.Dispose();
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return _memoryStream.BeginRead(buffer, offset, count, callback, state);
        }

        public override int EndRead(IAsyncResult asyncResult)
        {
            return _memoryStream.EndRead(asyncResult);
        }

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return _memoryStream.BeginWrite(buffer, offset, count, callback, state);
        }

        public override void EndWrite(IAsyncResult asyncResult)
        {
            _memoryStream.EndWrite(asyncResult);
        }

        public override bool CanTimeout
        {
            get { return _memoryStream.CanTimeout; }
        }

        public override int ReadTimeout
        {
            get { return _memoryStream.ReadTimeout; }
            set { _memoryStream.ReadTimeout = value; }
        }

        public override int WriteTimeout
        {
            get { return _memoryStream.WriteTimeout; }
            set { _memoryStream.WriteTimeout = value; }
        }

        public override void Flush()
        {
            _memoryStream.Flush();
        }

        public override byte[] GetBuffer()
        {
            return _memoryStream.GetBuffer();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _memoryStream.Read(buffer, offset, count);
        }

        public override int ReadByte()
        {
            return _memoryStream.ReadByte();
        }

        public override long Seek(long offset, SeekOrigin loc)
        {
            return _memoryStream.Seek(offset, loc);
        }

        public override void SetLength(long value)
        {
            _memoryStream.SetLength(value);
        }

        public override byte[] ToArray()
        {
            return _memoryStream.ToArray();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _memoryStream.Write(buffer, offset, count);
        }

        public override void WriteByte(byte value)
        {
            _memoryStream.WriteByte(value);
        }

        public override void WriteTo(System.IO.Stream stream)
        {
            _memoryStream.WriteTo(stream);
        }

        public override bool CanRead
        {
            get { return _memoryStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return _memoryStream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return _memoryStream.CanWrite; }
        }

        public override int Capacity
        {
            get { return _memoryStream.Capacity; }
            set { _memoryStream.Capacity = value; }
        }

        public override long Length
        {
            get { return _memoryStream.Length; }
        }

        public override long Position
        {
            get { return _memoryStream.Position; }
            set { _memoryStream.Position = value; }
        }
    }
}