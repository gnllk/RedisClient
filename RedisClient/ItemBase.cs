using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnllk.RedisClient
{
    public class ItemBase
    {
        public bool Loading { get; set; }
        private IRedisConnection mConnection = null;
        public IRedisConnection Connection
        {
            get { return mConnection; }
            protected set
            {
                if (value == null) throw new ArgumentNullException("Connection");
                mConnection = value;
            }
        }
        public ItemBase(IRedisConnection cont)
        {
            Connection = cont;
        }
    }
}
