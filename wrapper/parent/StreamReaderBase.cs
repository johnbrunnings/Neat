using System;
using System.IO;
using System.Runtime.Remoting;
using System.Text;

namespace neat.wrapper.parent
{
    public abstract class StreamReaderBase
    {
        public abstract object GetLifetimeService();
        public abstract object InitializeLifetimeService();
        public abstract ObjRef CreateObjRef(Type requestedType);
        public abstract void Dispose();
        public abstract int ReadBlock(char[] buffer, int index, int count);
        public abstract void Close();
        public abstract void DiscardBufferedData();
        public abstract int Peek();
        public abstract int Read();
        public abstract int Read(char[] buffer, int index, int count);
        public abstract string ReadToEnd();
        public abstract string ReadLine();
        public abstract Encoding CurrentEncoding { get; }
        public abstract Stream BaseStream { get; }
        public abstract bool EndOfStream { get; }
    }
}