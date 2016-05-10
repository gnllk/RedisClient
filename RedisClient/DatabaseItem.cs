using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RClient;

namespace Gnllk.RedisClient
{
    public class DatabaseItem : ConnectionItem
    {
        public const string EnptyDb = "key=0";
        public const string Keyspace = "Keyspace";
        public const string KeyspaceSetion = "# Keyspace";
        public int DbIndex { get; protected set; }
        public string DbInfo { get; set; }
        public string DbName { get; set; }

        public DatabaseItem(IRedisConnection cont, string cntName, string dbName, string dbInfo = "")
            : base(cont, cntName)
        {
            DbName = dbName;
            DbInfo = dbInfo;
            DbIndex = DbName.GetNumber().ToInt32(0);
        }

        public DatabaseItem(ConnectionItem item, string dbName, string dbInfo = "")
            : base(item, item.CntName)
        {
            DbName = dbName;
            DbInfo = dbInfo;
            DbIndex = DbName.GetNumber().ToInt32(0);
        }

        public void UpdateDbInfo()
        {
            var info = Connection.Execute(new RedisCommand(Command.INFO, Keyspace)).Read<Info>(InfoReader.ReadAsInfo);
            if (info != null && info.Any())
            {
                var section = info.First().Value;
                if (section != null && section.ContainsKey(DbName))
                    DbInfo = section[DbName];
                else
                    DbInfo = EnptyDb;
            }
            else
            {
                DbInfo = EnptyDb;
            }
        }
    }
}
