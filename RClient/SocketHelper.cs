using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace RClient
{
    public class SocketHelper
    {
        /// <summary>
        /// Receive data as string
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="encoding">Text Encoding</param>
        /// <returns>string</returns>
        public static string ReadAsString(Socket socket, Encoding encoding)
        {
            return encoding.GetString(ReadAsBytes(socket));
        }

        /// <summary>
        /// Receive data as bytes
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <returns>byte array</returns>
        public static byte[] ReadAsBytes(Socket socket)
        {
            byte[] result = null;
            using (MemoryStream ms = ReadToStream(socket))
            {
                result = ms.ToArray();
            }
            return result;
        }

        /// <summary>
        /// Receive data, remember to release the Stream
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <returns>Stream</returns>
        public static MemoryStream ReadToStream(Socket socket)
        {
            MemoryStream ms = new MemoryStream(2048);
            byte[] buffer = new byte[128];
            int receive = 0;
            int tryTime = 3;

            do
            {
                tryTime = 3;
                while (socket.Available < buffer.Length && tryTime-- > 0) Thread.Sleep(100);

                if (socket.Available == 0) break;

                receive = socket.Receive(buffer, buffer.Length, SocketFlags.None);
                if (receive > 0) ms.Write(buffer, 0, receive);
            }
            while (receive == buffer.Length);

            return ms;
        }

        /// <summary>
        /// Receive data, remember to release the Stream
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <returns>Stream</returns>
        public static MemoryStream ReadToStreamAsyn(Socket socket)
        {
            DataFlow flow = new DataFlow(socket);
            var buf = flow.Buf;
            IAsyncResult asyncResult = socket.BeginReceive(buf, 0, buf.Length,
                SocketFlags.None, new AsyncCallback(AsyncRead), flow);
            flow.AutoEvent.WaitOne();
            return flow.MStream;
        }

        /// <summary>
        /// Async read data(danger: recursive call)
        /// </summary>
        /// <param name="result">Async result</param>
        protected static void AsyncRead(IAsyncResult result)
        {
            DataFlow flow = result.AsyncState as DataFlow;
            Socket s = flow.SocketRef;
            int bytesRead = s.EndReceive(result);
            int tryCount = 3;
            while (s.Available == 0 && tryCount-- > 0)
            {
                Thread.Sleep(100);
            }
            if (bytesRead > 0 && s.Available > 0)
            {
                flow.MStream.Write(flow.Buf, 0, bytesRead);
                s.BeginReceive(flow.Buf, 0, flow.Buf.Length,
                    SocketFlags.None, new AsyncCallback(AsyncRead), flow);
            }
            else
            {
                flow.MStream.Write(flow.Buf, 0, bytesRead);
                flow.MStream.Position = 0;
                flow.AutoEvent.Set();
            }
        }

        /// <summary>
        /// Write the string to socket for sending
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="str">Be send string</param>
        /// <param name="encoding">Text Encoding</param>
        /// <returns>sended byte count</returns>
        public static int WriteTo(Socket socket, string str, Encoding encoding)
        {
            return WriteTo(socket, encoding.GetBytes(str));
        }

        /// <summary>
        /// Write the string to socket for sending
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="data">byte array</param>
        /// <returns>sended byte count</returns>
        public static int WriteTo(Socket socket, byte[] data)
        {
            if (data != null && data.Length > 0)
            {
                return socket.Send(data);
            }
            return 0;
        }
    }

    /// <summary>
    /// Sokeck async read data flow
    /// </summary>
    public class DataFlow
    {
        public Socket SocketRef { get; set; }
        public MemoryStream MStream { get; protected set; }
        public byte[] Buf { get; protected set; }
        public AutoResetEvent AutoEvent { get; protected set; }
        public DataFlow(Socket socket)
        {
            SocketRef = socket;
            MStream = new MemoryStream();
            Buf = new byte[128];
            AutoEvent = new AutoResetEvent(false);
        }
    }
}
