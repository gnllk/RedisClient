using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnllk.RedisClient.Manager
{
    public class ManagerBase<T> : Singleton<T> where T : new()
    {
    }
}
