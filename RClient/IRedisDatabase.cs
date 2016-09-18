namespace RClient
{
    public interface IRedisDatabase : IRedisConnection
    {
        int DbIndex { get; }

        bool Select(int dbIndex);

        bool Flush();
    }
}
