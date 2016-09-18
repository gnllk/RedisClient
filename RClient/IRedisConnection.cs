using System;

namespace RClient
{
    public interface IRedisConnection : IRedisExecution, IDisposable
    {
        bool Connected { get; }

        void Close();
    }
}
