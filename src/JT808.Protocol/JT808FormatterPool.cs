using JT808.Protocol.Formatters;
using Microsoft.Extensions.ObjectPool;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol
{
    public abstract class JT808FormatterPoolBase<JT808PackageFromatterImpl, TResult>
        where JT808PackageFromatterImpl : class, new()
    {
        private readonly ObjectPool<JT808PackageFromatterImpl> jT808FromatterPool;

        protected JT808FormatterPoolBase()
        {
            var jT808FromatterPolicy = new DefaultPooledObjectPolicy<JT808PackageFromatterImpl>();
            jT808FromatterPool = new DefaultObjectPool<JT808PackageFromatterImpl>(jT808FromatterPolicy);
        }

        public  TResult Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            var formatter = jT808FromatterPool.Get();
            try
            {
                return ((IJT808Formatter<TResult>)formatter).Deserialize(bytes, out readSize);
            }
            finally
            {
                jT808FromatterPool.Return(formatter);
            }
        }

        public  int Serialize(ref byte[] bytes, int offset, TResult value)
        {
            var formatter = jT808FromatterPool.Get();
            try
            {
                return ((IJT808Formatter<TResult>)formatter).Serialize(ref bytes, offset, value);
            }
            finally
            {
                jT808FromatterPool.Return(formatter);
            }
        }
    }

    public class JT808PackageFromatterPool: JT808FormatterPoolBase<JT808PackageFromatter, JT808Package>
    {

    }

    public class JT808HeaderFormatterPool : JT808FormatterPoolBase<JT808HeaderFormatter, JT808Header>
    {

    }

    public class JT808HeaderMessageBodyPropertyFormatterPool : JT808FormatterPoolBase<JT808HeaderMessageBodyPropertyFormatter, JT808HeaderMessageBodyProperty>
    {

    }
}
