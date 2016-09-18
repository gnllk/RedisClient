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

        protected static string ResolveIPAddress(string hostOrIP, bool userIPv4 = true)
        {
            if (string.IsNullOrWhiteSpace(hostOrIP)) throw new ArgumentException("argument cannot be null or empty", "hostOrIP");
            IPHostEntry entry = userIPv4 ? Dns.Resolve(hostOrIP) : Dns.GetHostEntry(hostOrIP);
            if (entry.AddressList == null || !entry.AddressList.Any()) throw new Exception(string.Format("DNS cannot resolve: {0}", hostOrIP));
            return entry.AddressList[0].ToString();
        }

        public RedisDatabase(string hostOrIP, int port, int dbIndex = 0, bool userIPv4 = true)
            : base(ResolveIPAddress(hostOrIP, userIPv4), port)
        {
            if (dbIndex > 0 && !Select(dbIndex))
            {
                throw new Exception(string.Format("cannot select db: {0}", dbIndex));
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
