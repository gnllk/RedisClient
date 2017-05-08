using System.Collections.Generic;

namespace Gnllk.RedisClient
{
    public class AppCache : Singleton<AppCache>
    {
        public IKeyItem Item { get; set; }

        public List<IRedisConnection> DbConnections { get; } = new List<IRedisConnection>();
    }
}
