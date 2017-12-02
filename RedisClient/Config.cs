using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Gnllk.RedisClient
{
    [DataContract]
    public class Config
    {
        private Dictionary<string, RedisConnection> mConnections = null;

        private Dictionary<string, string> mPluginsConfigString = null;

        [DataMember]
        public Dictionary<string, RedisConnection> Connections
        {
            get
            {
                if (mConnections == null)
                    mConnections = new Dictionary<string, RedisConnection>();
                return mConnections;
            }
            protected set
            {
                if (value == null) throw new ArgumentNullException("Connections");
                mConnections = value;
            }
        }

        [DataMember]
        public Dictionary<string, string> PluginsConfigString
        {
            get
            {
                if (mPluginsConfigString == null)
                    mPluginsConfigString = new Dictionary<string, string>();
                return mPluginsConfigString;
            }
            protected set
            {
                if (value == null) throw new ArgumentNullException("PluginsCondigString");
                mPluginsConfigString = value;
            }
        }

        [DataMember]
        public string GlobalTextEncoding { get; set; } = "utf-8";
    }
}
