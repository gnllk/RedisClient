using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RClient
{
    public interface IRedisDatabase : IRedisConnection
    {
        int DbIndex { get; }

        bool Select(int dbIndex);

        bool Flush();
    }
}
