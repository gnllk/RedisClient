using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnllk.RedisClient
{
    public class ConnectionItem : ItemBase
    {
        private string mName = null;
        public string CntName
        {
            get { return mName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Name");
                mName = value;
            }
        }

        public ConnectionItem(IRedisConnection cont, string cntName)
            : base(cont)
        {
            CntName = cntName;
        }

        public ConnectionItem(ItemBase item, string cntName)
            : base(item.Connection)
        {
            CntName = cntName;
        }
    }
}
