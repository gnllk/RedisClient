using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RClient
{
    public interface IRedisConnection : IRedisExecution, IDisposable
    {
        bool Connected { get; }

        void Close();
    }
}
