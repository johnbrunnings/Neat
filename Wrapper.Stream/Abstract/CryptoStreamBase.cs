using System;

namespace Neat.Wrapper.Stream.Abstract
{
    public abstract class CryptoStreamBase : System.IO.Stream, IDisposable
    {
        public abstract bool HasFlushedFinalBlock { get; }
        public abstract void FlushFinalBlock();
        public abstract void Clear();
    }
}