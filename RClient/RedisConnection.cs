using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace RClient
{
    public class RedisConnection : IRedisConnection
    {
        private Socket mSocket = null;

        protected IPEndPoint mEndPoint = null;

        public RedisConnection(string ipString, int port)
        {
            if (string.IsNullOrWhiteSpace(ipString)) throw new ArgumentNullException("ipString");
            if (port < IPEndPoint.MinPort || port > IPEndPoint.MaxPort) throw new ArgumentOutOfRangeException("port");
            mEndPoint = (new IPEndPoint(IPAddress.Parse(ipString), port));
            mSocket = new Socket(mEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Close()
        {
            if (mSocket != null)
            {
                try
                {
                    if (mSocket.Connected)
                    {
                        Execute(new RedisCommand(Command.QUIT));
                        mSocket.Shutdown(SocketShutdown.Both);
                    }
                }
                finally
                {
                    mSocket.Close();
                    mSocket = null;
                }
            }
        }

        /// <summary>
        /// Call the Close()
        /// </summary>
        public void Dispose()
        {
            Close();
        }

        public IRedisReader Execute(IRedisCommand cmd)
        {
            if (cmd == null) throw new ArgumentNullException("cmd");
            if (cmd.Command == Command.UNKNOWN) throw new ArgumentException("'UNKNOWN' command cannot execute", "cmd");
            if (!mSocket.Connected) mSocket.Connect(mEndPoint);
            byte[] data = cmd.ToBytes();
            int sended = mSocket.Send(data);
            if (sended != data.Length) throw new Exception("send fail");
            return new RedisReader(SocketHelper.ReadAsBytes(mSocket));
        }

        public bool Connected
        {
            get { return mSocket.Connected; }
        }
    }
}
