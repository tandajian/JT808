using System;

namespace JT808.Protocol.Formatters
{
    public interface IJT808Formatter<T>: IJT808Formatter
    {
        T Deserialize(ReadOnlySpan<byte> bytes, out int readSize);

        int Serialize(ref byte[] bytes, int offset, T value);
    }

    public interface IJT808Formatter
    {

    }
}
