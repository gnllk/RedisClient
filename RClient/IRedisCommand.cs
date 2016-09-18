namespace RClient
{
    public interface IRedisCommand
    {
        Command Command { get; set; }

        int Count { get; }

        byte[] ToBytes();

        void AddParameter(byte[] data);

        void AddParameter(object arg);

        void AddParameter(params object[] args);

        void RemoveParameterAt(int index);

        void RemoveLastParameter();

        void ClearParameter();
    }
}
