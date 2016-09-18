namespace RClient
{
    public interface IDataReader<T>
    {
        bool Next();

        T GetValue();

        void Reset();
    }
}
