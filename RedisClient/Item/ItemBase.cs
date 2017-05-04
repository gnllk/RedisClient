using System;

namespace Gnllk.RedisClient
{
    public interface IItemBase
    {
        bool Loading { get; set; }
        IRedisConnection Connection { get; }
    }

    public class ItemBase : IItemBase
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
