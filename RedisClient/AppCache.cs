namespace Gnllk.RedisClient
{
    public class AppCache : Singleton<AppCache>
    {
        public IKeyItem Item { get; set; }
    }
}
