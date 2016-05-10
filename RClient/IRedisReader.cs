using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace RClient
{
    public interface IRedisReader
    {
        T Read<T>(Func<byte[], T> reader);

        T Read<T>(Func<string, T> reader, Encoding encoding);

        T Read<T>(Func<byte[], Encoding, T> reader, Encoding encoding);
    }
}
