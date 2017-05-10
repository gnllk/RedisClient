using System;
using System.Net.Sockets;
using System.Net;
using RClient.Properties;

namespace RClient
{
    public class RedisConnection : IRedisConnection
    {
        private const int WSAECONNRESET = 10054;

        private Socket mSocket = null;

        protected IPEndPoint mEndPoint = null;

        public RedisConnection(string ipString, int port)
        {
            if (string.IsNullOrWhiteSpace(ipString))
                throw new ArgumentException(
                    string.Format(Resources.NullOrEmptyExceptionFmt, "Internet address"), "ipString");

            if (port <= IPEndPoint.MinPort || port > IPEndPoint.MaxPort)
                throw new ArgumentOutOfRangeException("port",
                    string.Format(Resources.PortOutOfRangeFmt, port));

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
                        Execute(new RedisCommand(Command.QUIT));
                }
                catch (SocketException ex)
                {
                    // connection was forcibly closed by the remote host.
                    if (WSAECONNRESET != ex.ErrorCode)
                        throw ex;
                }
                finally
                {
                    if (mSocket.Connected)
                        mSocket.Shutdown(SocketShutdown.Both);
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

        public IRedisReader Execute(IRedisCommand command)
        {
            if (command == null)
                throw new ArgumentNullException("command",
                    string.Format(Resources.NullExceptionFmt, "Redis command"));
            if (command.Command == Command.UNKNOWN)
                throw new Exception(Resources.ExecuteErrorUnknowCommand);

            if (!mSocket.Connected) mSocket.Connect(mEndPoint);
            byte[] data = command.ToBytes();
            int sended = mSocket.Send(data);
            if (sended != data.Length)
                throw new Exception(Resources.ExecuteErrorSendDataFail);

            return new RedisReader(SocketHelper.ReadAsBytes(mSocket));
        }

        public bool Connected
        {
            get { return mSocket.Connected; }
        }
    }
}
