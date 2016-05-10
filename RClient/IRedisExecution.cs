using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RClient
{
    public interface IRedisExecution
    {
        IRedisReader Execute(IRedisCommand cmd);
    }
}
