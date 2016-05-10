using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RClient;

namespace Gnllk.RedisClient
{
    public class KeyItem : DatabaseItem
    {
        public string Key { get; set; }

        public string Value
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Key)) return string.Empty;
                return this.Connection.Execute(new RedisCommand(Command.GET, Key)).Read<string>(Readers.ReadAsValue);
            }
            set
            {
                if (!this.Connection.Execute(new RedisCommand(Command.SET, Key, value)).Read<bool>(Readers.IsOK))
                {
                    throw new Exception(string.Format("Can not set {0} for value: {1} to redis", Key, value));
                }
            }
        }

        public string GetValue(Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            if (string.IsNullOrWhiteSpace(Key)) return string.Empty;
            return this.Connection.Execute(new RedisCommand(Command.GET, Key)).Read<string>(Readers.ReadAsValue, encoding);
        }

        public bool SetValue(string value, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            if (string.IsNullOrWhiteSpace(Key)) return false;
            if (value == null) value = string.Empty;
            return this.Connection.Execute(new RedisCommand(Command.SET, Key, encoding.GetBytes(value))).Read<bool>(Readers.IsOK);
        }

        public bool SetValue(byte[] value)
        {
            if (string.IsNullOrWhiteSpace(Key)) return false;
            return this.Connection.Execute(new RedisCommand(Command.SET, Key, value)).Read<bool>(Readers.IsOK);
        }

        public bool Rename(string newName)
        {
            if (string.IsNullOrWhiteSpace(Key)) return false;
            if (Key == newName) return false;
            bool ret = this.Connection.Execute(new RedisCommand(Command.RENAME, Key, newName)).Read<bool>(Readers.IsOK);
            if (ret) Key = newName;
            return ret;
        }

        public KeyItem(IRedisConnection cont, string cntName, string dbName, string key, string dbInfo = "")
            : base(cont, cntName, dbName, dbInfo)
        {
            Key = key;
        }

        public KeyItem(DatabaseItem item, string key)
            : base(item, item.DbName, item.DbInfo)
        {
            Key = key;
        }

        public KeyItem(ConnectionItem item, string dbName, string key, string dbInfo = "")
            : base(item, dbName, dbInfo)
        {
            Key = key;
        }
    }
}
