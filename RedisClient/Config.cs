using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Gnllk.RedisClient
{
    [DataContract]
    public class Config
    {
        private Dictionary<string, RedisConnection> mConnections = null;

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

        private string mEncoding = null;

        [DataMember]
        public string Encoding
        {
            get
            {
                if (mEncoding == null) mEncoding = "utf-8";
                return mEncoding;
            }
            set
            {
                mEncoding = value;
            }
        }
    }
}
