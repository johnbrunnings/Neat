using System;
using System.IO;
using System.Runtime.Remoting;
using System.Security.Cryptography;
using Neat.Wrapper.Stream.Abstract;

namespace Neat.Wrapper.Stream
{
    public class CryptoStreamWrapper : CryptoStreamBase
    {
        private readonly CryptoStream _cryptoStream;

        public CryptoStreamWrapper(CryptoStream cryptoStream)
        {
            _cryptoStream = cryptoStream;
        }

        public new object GetLifetimeService()
        {
            return _cryptoStream.GetLifetimeService();
        }

        public override object InitializeLifetimeService()
        {
            return _cryptoStream.InitializeLifetimeService();
        }

        public override ObjRef CreateObjRef(Type requestedType)
        {
            return _cryptoStream.CreateObjRef(requestedType);
        }

        public new void CopyTo(System.IO.Stream destination)
        {
            _cryptoStream.CopyTo(destination);
        }

        public new void CopyTo(System.IO.Stream destination, int bufferSize)
        {
            _cryptoStream.CopyTo(destination, bufferSize);
        }

        public override void Close()
        {
            _cryptoStream.Close();
        }

        public new void Dispose()
        {
            _cryptoStream.Dispose();
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return _cryptoStream.BeginRead(buffer, offset, count, callback, state);
        }

        public override int EndRead(IAsyncResult asyncResult)
        {
            return _cryptoStream.EndRead(asyncResult);
        }

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return _cryptoStream.BeginWrite(buffer, offset, count, callback, state);
        }

        public override void EndWrite(IAsyncResult asyncResult)
        {
            _cryptoStream.EndWrite(asyncResult);
        }

        public override int ReadByte()
        {
            return _cryptoStream.ReadByte();
        }

        public override void WriteByte(byte value)
        {
            _cryptoStream.WriteByte(value);
        }

        public override bool CanTimeout
        {
            get { return _cryptoStream.CanTimeout; }
        }

        public override int ReadTimeout
        {
            get { return _cryptoStream.ReadTimeout; }
            set { _cryptoStream.ReadTimeout = value; }
        }

        public override int WriteTimeout
        {
            get { return _cryptoStream.WriteTimeout; }
            set { _cryptoStream.WriteTimeout = value; }
        }

        public override void FlushFinalBlock()
        {
            _cryptoStream.FlushFinalBlock();
        }

        public override void Flush()
        {
            _cryptoStream.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _cryptoStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _cryptoStream.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _cryptoStream.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _cryptoStream.Write(buffer, offset, count);
        }

        public override void Clear()
        {
            _cryptoStream.Clear();
        }

        public override bool CanRead
        {
            get { return _cryptoStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return _cryptoStream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return _cryptoStream.CanWrite; }
        }

        public override long Length
        {
            get { return _cryptoStream.Length; }
        }

        public override long Position
        {
            get { return _cryptoStream.Position; }
            set { _cryptoStream.Position = value; }
        }

        public override bool HasFlushedFinalBlock
        {
            get { return _cryptoStream.HasFlushedFinalBlock; }
        }
    }
}