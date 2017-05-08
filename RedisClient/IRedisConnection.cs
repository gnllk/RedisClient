using System;
using RClient;

namespace Gnllk.RedisClient
{
    public interface IRedisConnection : IRedisExecution, IDisposable
    {
        EndPoint EndPoint { get; set; }

        string Password { get; set; }

        string Description { get; set; }

        int CurrentIndex { get; }

        bool Select(int dbIndex);

        IRedisConnection Copy();

        bool Login();
    }
}
