using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RClient
{
    public interface IRedisClient : IRedisExecution, IDisposable
    {
        bool Set(string name, string value);

        bool Set(string name, string value, out string statusText);

        string Get(string name);

        string Get(string name, out string statusText);

        bool Select(int dbIndex);

        bool Connected { get; }
    }
}
