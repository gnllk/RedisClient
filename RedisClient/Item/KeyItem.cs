using System;
using System.Text;
using RClient;

namespace Gnllk.RedisClient
{
    public interface IKeyItem : IDatabaseItem
    {
        string Key { get; set; }

        byte[] Value { get; set; }

        string GetValue(Encoding encoding = null);

        bool SetValue(byte[] value);

        bool SetValue(string value, Encoding encoding = null);

        bool Rename(string newName);
    }

    public class KeyItem : DatabaseItem, IKeyItem
    {
        public string Key { get; set; }

        public byte[] Value
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Key)) return null;
                return Connection.Execute(new RedisCommand(Command.GET, Key)).Read(Readers.ReadAsBytes);
            }
            set
            {
                if (!Connection.Execute(new RedisCommand(Command.SET, Key, value)).Read(Readers.IsOK))
                {
                    throw new Exception(string.Format("Cannot set {0} for value: {1} to redis", Key, value));
                }
            }
        }

        public string GetValue(Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            if (string.IsNullOrWhiteSpace(Key)) return string.Empty;
            return Connection.Execute(new RedisCommand(Command.GET, Key)).Read(Readers.ReadAsValue, encoding);
        }

        public bool SetValue(string value, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            if (string.IsNullOrWhiteSpace(Key)) return false;
            if (value == null) value = string.Empty;
            return Connection.Execute(new RedisCommand(Command.SET, Key, encoding.GetBytes(value))).Read(Readers.IsOK);
        }

        public bool SetValue(byte[] value)
        {
            if (string.IsNullOrWhiteSpace(Key)) return false;
            return Connection.Execute(new RedisCommand(Command.SET, Key, value)).Read(Readers.IsOK);
        }

        public bool Rename(string newName)
        {
            if (string.IsNullOrWhiteSpace(Key)) return false;
            if (Key == newName) return false;
            bool ret = Connection.Execute(new RedisCommand(Command.RENAME, Key, newName)).Read(Readers.IsOK);
            if (ret) Key = newName;
            return ret;
        }

        public KeyItem(IRedisConnection cont, string cntName, string dbName, string key, string dbInfo = "")
            : base(cont, cntName, dbName, dbInfo)
        {
            Key = key;
        }

        public KeyItem(IDatabaseItem item, string key)
            : base(item, item.DbName, item.DbInfo)
        {
            Key = key;
        }

        public KeyItem(IConnectionItem item, string dbName, string key, string dbInfo = "")
            : base(item, dbName, dbInfo)
        {
            Key = key;
        }
    }
}
