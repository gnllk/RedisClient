using RClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Gnllk.RControl
{
    public interface IShowInPlugin : IComponent
    {
        Guid GetId();

        Icon GetIcon();

        string GetAuthor();

        string GetName();

        string GetDescription();

        string GetConfig();

        bool ShouldShowConnection(ConnectionData data);

        bool ShouldShowDatabase(DatabaseData data);

        bool ShouldShowAs(ShowData data);

        void OnShowConnection(ConnectionData data);

        void OnShowDatabase(DatabaseData data);

        void OnShowAs(ShowData data);

        void OnBlur();

        void OnAppClosing();

        void OnSetConfig(string config);
    }

    public class ConnectionData
    {
        public virtual IRedisClient Client { get; set; }

        public virtual string EndPoint { get; set; }

        public virtual ICollection<string> DbNames { get; set; }
    }

    public class DatabaseData
    {
        public virtual IRedisClient Client { get; set; }

        public virtual string DbName { get; set; }

        public virtual int DbIndex { get; set; }

        public virtual ICollection<string> Keys { get; set; }
    }

    public class ShowData
    {
        public virtual IRedisClient Client { get; set; }

        public virtual string EndPoint { get; set; }

        public virtual string DbName { get; set; }

        public virtual string Key { get; set; }

        public virtual byte[] Value { get; set; }

        public override int GetHashCode()
        {
            return (EndPoint + DbName + Key).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj == null ? false : GetHashCode() == obj.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0},{1}", DbName, Key);
        }
    }
}
