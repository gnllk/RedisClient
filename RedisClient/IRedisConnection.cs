using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RClient;

namespace Gnllk.RedisClient
{
    public interface IRedisConnection : IRedisExecution, IDisposable
    {
        EndPoint EndPoint { get; set; }

        string Password { get; set; }

        string Description { get; set; }

        bool Select(int dbIndex);
    }
}
