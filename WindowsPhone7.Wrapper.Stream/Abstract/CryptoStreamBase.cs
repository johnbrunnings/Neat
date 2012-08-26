using System;

namespace Neat.WindowsPhone7.Wrapper.Stream.Abstract
{
    public abstract class CryptoStreamBase : System.IO.Stream, IDisposable
    {
        public abstract void FlushFinalBlock();
        public abstract void Clear();
    }
}