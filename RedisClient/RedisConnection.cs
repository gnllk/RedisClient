using System;
using RClient;
using System.Net;
using System.Runtime.Serialization;
using Gnllk.RedisClient.Common;

namespace Gnllk.RedisClient
{
    [DataContract]
    public class RedisConnection : IRedisConnection
    {
        private object mLockThis = new object();

        private object LockThis
        {
            get
            {
                if (mLockThis == null)
                    mLockThis = new object();
                return mLockThis;
            }
        }

        public int CurrentIndex { get; protected set; }

        [DataMember]
        public EndPoint EndPoint { get; set; }

        public string Password { get; set; }

        [DataMember]
        public string PWD
        {
            get
            {
                return Encript(Password);
            }
            set
            {
                Password = Decript(value);
            }
        }

        [DataMember]
        public string Description { get; set; }

        [IgnoreDataMember]
        protected IRedisClient _Client = null;

        [IgnoreDataMember]
        protected IRedisClient Client
        {
            get
            {
                if (_Client == null || !_Client.Connected)
                {
                    lock (LockThis)
                    {
                        if (_Client == null || !_Client.Connected)
                        {
                            _Client = new RClient.RedisClient(EndPoint.IP, EndPoint.Port);
                        }
                    }
                }
                return _Client;
            }
            set
            {
                Dispose();
                _Client = value;
            }
        }

        #region constructor

        public RedisConnection()
            : this(new EndPoint("localhost", 6379), string.Empty)
        {
        }

        public RedisConnection(string endPoint, string password = "")
            : this(new EndPoint(endPoint), password)
        {
        }

        public RedisConnection(string hostOrIp, int port, string password = "")
            : this(new EndPoint(hostOrIp, port), password)
        {
        }

        public RedisConnection(EndPoint endPoint, string password = "")
        {
            EndPoint = endPoint;
            if (EndPoint.Port < IPEndPoint.MinPort || EndPoint.Port > IPEndPoint.MaxPort)
                throw new ArgumentOutOfRangeException("port");
            Password = password;
            CurrentIndex = 0;
        }

        #endregion constructor

        /// <summary>
        /// free connection
        /// </summary>
        public void Dispose()
        {
            if (_Client != null)
            {
                _Client.Dispose();
                _Client = null;
            }
        }

        /// <summary>
        /// select redis database
        /// </summary>
        /// <param name="dbIndex">database index</param>
        /// <returns>true if success</returns>
        public bool Select(int dbIndex)
        {
            if (dbIndex != CurrentIndex)
            {
                lock (LockThis)
                {
                    if (dbIndex != CurrentIndex)
                    {
                        bool ret = Client.Select(dbIndex);
                        if (ret) CurrentIndex = dbIndex;
                        return ret;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// execute redis command
        /// </summary>
        /// <param name="cmd">command</param>
        /// <returns>data reader</returns>
        public IRedisReader Execute(IRedisCommand cmd)
        {
            lock (LockThis)
            {
                return Client.Execute(cmd);
            }
        }

        protected virtual string Encript(string str)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            return PasswordHelper.Encript(str);
        }

        protected virtual string Decript(string str)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            return PasswordHelper.Decript(str);
        }
    }
}
