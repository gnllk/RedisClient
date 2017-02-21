using System;
using System.Linq;
using RClient;

namespace Gnllk.RedisClient
{
    public interface IDatabaseItem : IConnectionItem
    {
        int DbIndex { get; }
        string DbInfo { get; set; }
        string DbName { get; set; }
        void UpdateDbInfo();
        void CheckDbIndex();
    }

    public class DatabaseItem : ConnectionItem, IDatabaseItem
    {
        public const string EnptyDb = "key=0";
        public const string Keyspace = "Keyspace";

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

        public DatabaseItem(IConnectionItem item, string dbName, string dbInfo = "")
            : base(item, item.ConnectionName)
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

        public void CheckDbIndex()
        {
            if (DbIndex != Connection.CurrentIndex)
                Connection.Select(DbIndex);
        }
    }
}
