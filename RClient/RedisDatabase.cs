using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace RClient
{
    public class RedisDatabase : RedisConnection, IRedisDatabase
    {
        private int mdDBIndex = 0;

        public int DbIndex
        {
            get { return mdDBIndex; }
        }

        protected static string ResolveIPAddress(string hostOrIP)
        {
            if (string.IsNullOrWhiteSpace(hostOrIP)) throw new ArgumentException("参数不能为空", "hostOrIP");
            IPHostEntry entry = Dns.GetHostEntry(hostOrIP);
            if (entry.AddressList == null || !entry.AddressList.Any()) throw new Exception(string.Format("DNS无法解析主机名: {0}", hostOrIP));
            return entry.AddressList[0].ToString();
        }

        public RedisDatabase(string hostOrIP, int port, int dbIndex = 0)
            : base(ResolveIPAddress(hostOrIP), port)
        {
            if (dbIndex > 0 && !Select(dbIndex))
            {
                throw new Exception(string.Format("can not select db: {0}", dbIndex));
            }
        }

        public bool Select(int dbIndex)
        {
            if (Execute(new RedisCommand(Command.SELECT, dbIndex)).Read<bool>(Readers.IsOK))
            {
                mdDBIndex = dbIndex;
                return true;
            }
            return false;
        }

        public bool Flush()
        {
            return Execute(new RedisCommand(Command.SAVE)).Read<bool>(Readers.IsOK);
        }
    }
}
