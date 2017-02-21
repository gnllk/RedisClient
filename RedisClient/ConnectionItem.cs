using System;

namespace Gnllk.RedisClient
{
    public interface IConnectionItem : IItemBase
    {
        string ConnectionName { get; set; }
    }

    public class ConnectionItem : ItemBase, IConnectionItem
    {
        private string mConnectionName = null;

        public string ConnectionName
        {
            get { return mConnectionName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("ConnectionName");
                mConnectionName = value;
            }
        }

        public ConnectionItem(IRedisConnection cont, string connectionName)
            : base(cont)
        {
            ConnectionName = connectionName;
        }

        public ConnectionItem(IItemBase item, string connectionName)
            : base(item.Connection)
        {
            ConnectionName = connectionName;
        }
    }
}
