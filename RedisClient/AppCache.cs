using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnllk.RedisClient
{
    public class AppCache : Singleton<AppCache>
    {
        public byte[] CurrentGetData { get; set; }

        public Encoding CurrentEncoding { get; set; }
    }
}
