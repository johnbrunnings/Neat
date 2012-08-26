using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Neat.WindowsPhone7.StreamManager.Factory.ReaderFactory.Interface;
using Neat.WindowsPhone7.StreamManager.Factory.ReaderFactory.Parameters.Abstract;
using Neat.WindowsPhone7.StreamManager.Factory.StreamFactory.Interface;
using Neat.WindowsPhone7.StreamManager.Factory.StreamFactory.Parameters.Abstract;
using Neat.WindowsPhone7.StreamManager.Factory.WriterFactory.Interface;
using Neat.WindowsPhone7.StreamManager.Factory.WriterFactory.Parameters.Abstract;
using Neat.WindowsPhone7.StreamManager.Interface;

namespace Neat.WindowsPhone7.StreamManager
{
    public class StreamManager : IStreamManager
    {
        private readonly IEnumerable<IStreamFactory> _streamFactories;
        private readonly IEnumerable<ITextReaderFactory> _textReaderFactories;
        private readonly IEnumerable<ITextWriterFactory> _textWriterFactories;
        private Dictionary<StreamType, IStreamFactory> _streamFactoriesHash;
        private Dictionary<TextReaderType, ITextReaderFactory> _textReaderFactoriesHash;
        private Dictionary<TextWriterType, ITextWriterFactory> _textWriterFactoriesHash;
        private bool _streamFactoriesHashLoaded;
        private bool _textReaderFactoriesHashLoaded;
        private bool _textWriterFactoriesHashLoaded;
        private readonly object _streamFactoriesHashLock;
        private readonly object _textReaderFactoriesHashLock;
        private readonly object _textWriterFactoriesHashLock;

        public StreamManager(IEnumerable<IStreamFactory> streamFactories, IEnumerable<ITextReaderFactory> textReaderFactories, IEnumerable<ITextWriterFactory> textWriterFactories)
        {
            _streamFactories = streamFactories;
            _textReaderFactories = textReaderFactories;
            _textWriterFactories = textWriterFactories;
            _streamFactoriesHashLoaded = false;
            _textReaderFactoriesHashLoaded = false;
            _textWriterFactoriesHashLoaded = false;
            _streamFactoriesHashLock = new object();
            _textReaderFactoriesHashLock = new object();
            _textWriterFactoriesHashLock = new object();
        }

        public Stream GetStream(StreamType streamType, StreamParameters streamParameters)
        {
            LoadStreamFactories();

            if (_streamFactoriesHash.ContainsKey(streamType))
            {
                return _streamFactoriesHash[streamType].Create(streamParameters);
            }

            throw new ArgumentException(string.Format("No Factory defined for type {0}", streamType));
        }

        public TextReader GetTextReader(TextReaderType textReaderType, TextReaderParameters textReaderParameters)
        {
            LoadTextReaderFactories();

            if (_textReaderFactoriesHash.ContainsKey(textReaderType))
            {
                return _textReaderFactoriesHash[textReaderType].Create(textReaderParameters);
            }

            throw new ArgumentException(string.Format("No Factory defined for type {0}", textReaderType));
        }

        public TextWriter GetTextWriter(TextWriterType textWriterType, TextWriterParameters textReaderParameters)
        {
            LoadTextWriterFactories();

            if (_textWriterFactoriesHash.ContainsKey(textWriterType))
            {
                return _textWriterFactoriesHash[textWriterType].Create(textReaderParameters);
            }

            throw new ArgumentException(string.Format("No Factory defined for type {0}", textWriterType));
        }

        private void LoadStreamFactories()
        {
            if(!_streamFactoriesHashLoaded)
            {
                lock(_streamFactoriesHashLock)
                {
                    if(!_streamFactoriesHashLoaded)
                    {
                        var streamFactoriesHash = new Dictionary<StreamType, IStreamFactory>();
                        foreach (var streamFactory in _streamFactories)
                        {
                            if(!streamFactoriesHash.ContainsKey(streamFactory.StreamType))
                            {
                                streamFactoriesHash.Add(streamFactory.StreamType, streamFactory);
                            }
                        }
                        _streamFactoriesHash = streamFactoriesHash;
                        Thread.MemoryBarrier();
                        _streamFactoriesHashLoaded = true;
                    }
                }
            }
        }

        private void LoadTextReaderFactories()
        {
            if (!_textReaderFactoriesHashLoaded)
            {
                lock (_textReaderFactoriesHashLock)
                {
                    if (!_textReaderFactoriesHashLoaded)
                    {
                        var textReaderFactoriesHash = new Dictionary<TextReaderType, ITextReaderFactory>();
                        foreach (var textReaderFactory in _textReaderFactories)
                        {
                            if (!textReaderFactoriesHash.ContainsKey(textReaderFactory.TextReaderType))
                            {
                                textReaderFactoriesHash.Add(textReaderFactory.TextReaderType, textReaderFactory);
                            }
                        }
                        _textReaderFactoriesHash = textReaderFactoriesHash;
                        Thread.MemoryBarrier();
                        _textReaderFactoriesHashLoaded = true;
                    }
                }
            }
        }

        private void LoadTextWriterFactories()
        {
            if (!_textWriterFactoriesHashLoaded)
            {
                lock (_textWriterFactoriesHashLock)
                {
                    if (!_textWriterFactoriesHashLoaded)
                    {
                        var textWriterFactoriesHash = new Dictionary<TextWriterType, ITextWriterFactory>();
                        foreach (var textWriterFactory in _textWriterFactories)
                        {
                            if (!textWriterFactoriesHash.ContainsKey(textWriterFactory.TextWriterType))
                            {
                                textWriterFactoriesHash.Add(textWriterFactory.TextWriterType, textWriterFactory);
                            }
                        }
                        _textWriterFactoriesHash = textWriterFactoriesHash;
                        Thread.MemoryBarrier();
                        _textWriterFactoriesHashLoaded = true;
                    }
                }
            }
        }
    }
}