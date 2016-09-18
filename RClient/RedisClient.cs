namespace RClient
{
    public class RedisClient : IRedisClient
    {
        private IRedisDatabase mDb = null;

        public RedisClient(string hostOrIP, int port, int dbIndex = 0)
        {
            mDb = new RedisDatabase(hostOrIP, port, dbIndex);
        }

        public bool Set(string name, string value)
        {
            return Execute(new RedisCommand(Command.SET, name, value)).Read<bool>(Readers.IsOK);
        }

        public string Get(string name)
        {
            return Execute(new RedisCommand(Command.GET, name)).Read<string>(Readers.ReadAsValue);
        }

        public bool Set(string name, string value, out string statusText)
        {
            statusText = Execute(new RedisCommand(Command.SET, name, value)).Read<string>(Readers.ReadAsString);
            return statusText != null && statusText.Contains("+OK");
        }

        public string Get(string name, out string statusText)
        {
            IRedisReader reader = Execute(new RedisCommand(Command.GET, name));
            statusText = reader.Read<string>(Readers.ReadAsString);
            return reader.Read<string>(Readers.ReadAsValue);
        }

        public bool Select(int dbIndex)
        {
            return mDb.Select(dbIndex);
        }

        public IRedisReader Execute(IRedisCommand cmd)
        {
            return mDb.Execute(cmd);
        }

        public void Dispose()
        {
            mDb.Dispose();
        }

        public bool Connected
        {
            get { return mDb.Connected; }
        }
    }
}
