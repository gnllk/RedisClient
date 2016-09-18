namespace RClient
{
    public interface IRedisExecution
    {
        IRedisReader Execute(IRedisCommand cmd);
    }
}
