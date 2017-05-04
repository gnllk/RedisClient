using System;
using System.Net;
using System.Runtime.Serialization;

namespace Gnllk.RedisClient
{
    [DataContract]
    public class EndPoint
    {
        private string _ip = "127.0.0.1";
        private int _port = 0;

        /// <summary>
        /// IP
        /// </summary>
        [DataMember]
        public string IP
        {
            get { return _ip; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("IP");
                _ip = value.Trim();
            }
        }

        /// <summary>
        /// 0 meaning ignore port
        /// </summary>
        [DataMember]
        public int Port
        {
            get { return _port; }
            set
            {
                if (value < 0 || value > IPEndPoint.MaxPort)
                    throw new ArgumentOutOfRangeException("Port");
                _port = value;
            }
        }

        public EndPoint()
        {
        }

        public EndPoint(string ip, int port)
        {
            IP = ip;
            Port = port;
        }

        public EndPoint(string endPoint)
        {
            if (string.IsNullOrWhiteSpace(endPoint))
                throw new ArgumentNullException("endPoint");
            string[] spl = endPoint.Split(':');
            IP = spl[0];
            if (spl.Length > 1)
                Port = Int32.Parse(spl[1].Trim());
        }

        public override string ToString()
        {
            return (Port == 0) ? IP : string.Format("{0}:{1}", IP, Port);
        }
    }
}
