using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace RClient
{
    public class RedisCommand : IRedisCommand
    {
        //end mark \r\n
        protected const string CRLF_STR = "\r\n";
        //end mark \r\n
        protected readonly byte[] CRLF_BYTE = new byte[2] { (byte)'\r', (byte)'\n' };
        //format *<number of arguments>\r\n
        protected const string FMT_HEAD = "*{0}\r\n";
        //format $<number of bytes of argument N>\r\n<argument data>\r\n
        protected const string FMT_PARA = "${0}\r\n{1}\r\n";
        //format $<number of bytes of argument N>\r\n<argument data>\r\n
        protected const string FMT_PARA_BYTES = "${0}\r\n";
        //it's just binary parameter
        protected const string BINARY_SIGN = "[BinaryParameter]";

        public Encoding mEncoding = Encoding.UTF8;

        public Encoding Encoding
        {
            get { return mEncoding; }
            set
            {
                if (value == null) throw new ArgumentNullException("Encoding");
                mEncoding = value;
            }
        }

        private List<CommandParameter> mParameters = null;

        private List<CommandParameter> Parameters
        {
            get
            {
                if (mParameters == null) mParameters = new List<CommandParameter>();
                return mParameters;
            }
        }

        public Command Command { get; set; }

        public int Count
        {
            get { return Parameters.Count; }
        }

        public RedisCommand() { }

        public RedisCommand(Command cmd)
        {
            Command = cmd;
        }

        public RedisCommand(Command cmd, params object[] args)
        {
            Command = cmd;
            AddParameter(args);
        }

        public void ClearParameter()
        {
            Parameters.Clear();
        }

        protected void Write(Stream writer, Encoding encoding, string format, params object[] args)
        {
            byte[] data = encoding.GetBytes(string.Format(format, args));
            writer.Write(data, 0, data.Length);
        }

        public byte[] ToBytes()
        {
            using (MemoryStream ms = new MemoryStream(1024))
            {
                string cmd = Command.ToString();
                Write(ms, this.Encoding, FMT_HEAD, Parameters.Count + 1);
                Write(ms, this.Encoding, FMT_PARA, cmd.Length, cmd);
                foreach (var item in Parameters)
                {
                    if (item.Type == CommandParameterType.General)
                    {
                        string name = item.Data.ToString();
                        Write(ms, this.Encoding, FMT_PARA, name.Length, name);
                    }
                    else if ((item.Type == CommandParameterType.Bytes))
                    {
                        byte[] buf = item.Data as byte[];
                        Write(ms, this.Encoding, FMT_PARA_BYTES, buf.Length);
                        ms.Write(buf, 0, buf.Length);
                        ms.Write(CRLF_BYTE, 0, CRLF_BYTE.Length);
                    }
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            string cmd = Command.ToString();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(FMT_HEAD, Parameters.Count + 1);
            sb.AppendFormat(FMT_PARA, cmd.Length, cmd);
            foreach (var item in Parameters)
            {
                if (item.Type == CommandParameterType.General)
                {
                    string name = item.Data.ToString();
                    sb.AppendFormat(FMT_PARA, name.Length, name);
                }
                else if ((item.Type == CommandParameterType.Bytes))
                {
                    byte[] buf = item.Data as byte[];
                    sb.AppendFormat(FMT_PARA, buf.Length, BINARY_SIGN);
                }
            }
            return sb.ToString();
        }

        public void AddParameter(byte[] data)
        {
            if (data != null && data.Length > 0)
            {
                Parameters.Add(new CommandParameter(data, CommandParameterType.Bytes));
            }
        }

        public void AddParameter(object arg)
        {
            if (arg != null)
            {
                if (arg is byte[])
                {
                    Parameters.Add(new CommandParameter(arg, CommandParameterType.Bytes));
                }
                else if (arg is IEnumerable<byte>)
                {
                    Parameters.Add(new CommandParameter((arg as IEnumerable<byte>).ToArray(), CommandParameterType.Bytes));
                }
                else
                {
                    Parameters.Add(new CommandParameter(arg, CommandParameterType.General));
                }
            }
        }

        public void AddParameter(params object[] args)
        {
            if (args != null)
            {
                foreach (object item in args)
                {
                    AddParameter(item);
                }
            }
        }

        public void RemoveParameterAt(int index)
        {
            Parameters.RemoveAt(index);
        }

        public void RemoveLastParameter()
        {
            if (Parameters.Count > 0)
                Parameters.RemoveAt(Parameters.Count - 1);
        }
    }

    class CommandParameter
    {
        public object Data { get; set; }

        public CommandParameterType Type { get; set; }

        public CommandParameter() { }

        public CommandParameter(object data, CommandParameterType type = CommandParameterType.General)
        {
            Data = data;
            Type = type;
        }
    }

    enum CommandParameterType
    {
        General, Bytes
    }
}
