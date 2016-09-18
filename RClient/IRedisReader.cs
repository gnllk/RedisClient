using System;
using System.Text;

namespace RClient
{
    public interface IRedisReader
    {
        T Read<T>(Func<byte[], T> reader);

        T Read<T>(Func<byte[], Encoding, T> reader, Encoding encoding);

        T Read<T>(Func<string, T> reader, Encoding encoding);
    }
}
