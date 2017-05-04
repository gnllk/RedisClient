using RClient.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace RClient
{
    public class RedisReader : IRedisReader
    {
        private byte[] mInData = null;

        protected byte[] InData
        {
            get { return mInData; }
            set
            {
                if (value == null) throw new Exception(string.Format(Resources.PropertySetNullErrorFmt, "InData"));
                mInData = value;
            }
        }

        public RedisReader(byte[] data)
        {
            InData = data;
        }

        public T Read<T>(Func<byte[], T> reader)
        {
            return reader(InData);
        }

        public T Read<T>(Func<byte[], Encoding, T> reader, Encoding encoding)
        {
            return reader(InData, encoding);
        }

        public T Read<T>(Func<string, T> reader, Encoding encoding)
        {
            string ret = Readers.ReadAsString(InData, encoding);
            return reader(ret);
        }

        public override string ToString()
        {
            return Readers.ReadAsString(InData, Encoding.UTF8);
        }
    }

    public class Readers
    {
        private static Encoding mGlobalEncoding = Encoding.UTF8;

        /// <summary>
        /// goubal text encoding, default is utf8
        /// </summary>
        public static Encoding GlobalEncoding
        {
            get { return mGlobalEncoding; }
            set
            {
                if (value == null) throw new Exception(string.Format(Resources.PropertySetNullErrorFmt, "GlobalEncoding"));
                mGlobalEncoding = value;
            }
        }

        /// <summary>
        /// read result as string
        /// </summary>
        /// <param name="data">bytes</param>
        /// <param name="encoding">text encoding</param>
        /// <returns>whole string</returns>
        public static string ReadAsString(byte[] data, Encoding encoding)
        {
            ThrowExceptionIfError(data);
            if (encoding == null) encoding = GlobalEncoding;
            if (data != null && data.Length > 0)
            {
                string result = encoding.GetString(data);
                return result;
            }
            return string.Empty;
        }

        /// <summary>
        /// read result as string
        /// </summary>
        /// <param name="data">bytes</param>
        /// <returns>whole string</returns>
        public static string ReadAsString(byte[] data)
        {
            ThrowExceptionIfError(data);
            return ReadAsString(data, GlobalEncoding);
        }

        /// <summary>
        /// read something start with $ as string
        /// </summary>
        /// <param name="data">bytes</param>
        /// <param name="encoding">text encoding</param>
        /// <returns>string without start $ and last CRLF</returns>
        public static string ReadAsValue(byte[] data, Encoding encoding)
        {
            ThrowExceptionIfError(data);
            string str = ReadAsString(data, encoding);
            if (!string.IsNullOrWhiteSpace(str))
            {
                if (str.StartsWith("$"))
                {
                    int index = 0;
                    foreach (char c in str)
                    {
                        if (index != 0)
                        {
                            if (c > '9' || c < '0') break;
                        }
                        index++;
                    }
                    return str.Substring(index + 2, str.Length - index - 4);
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// read something start with $ as string
        /// </summary>
        /// <param name="data">bytes</param>
        /// <returns>string without start $ and last CRLF</returns>
        public static string ReadAsValue(byte[] data)
        {
            ThrowExceptionIfError(data);
            return ReadAsValue(data, GlobalEncoding);
        }

        /// <summary>
        /// read something start with $ as bytes
        /// </summary>
        /// <param name="data">bytes</param>
        /// <returns>data</returns>
        public static byte[] ReadAsBytes(byte[] data)
        {
            ThrowExceptionIfError(data);
            ThrowExceptionIfTypeNotMatch(data, ResultType.Large);
            return RedisUtil.GetLarge(data);
        }

        /// <summary>
        /// read something start with * as string list
        /// </summary>
        /// <param name="data">bytes</param>
        /// <param name="encoding">text encoding</param>
        /// <returns>string list</returns>
        public static List<string> ReadAsList(byte[] data, Encoding encoding)
        {
            ThrowExceptionIfError(data);
            ThrowExceptionIfTypeNotMatch(data, ResultType.Mutil);
            string str = ReadAsString(data, encoding);
            return RedisUtil.GetMutil(str);
        }

        /// <summary>
        /// read something start with * as string list
        /// </summary>
        /// <param name="data">bytes</param>
        /// <returns>string list</returns>
        public static List<string> ReadAsList(byte[] data)
        {
            ThrowExceptionIfError(data);
            return ReadAsList(data, GlobalEncoding);
        }

        /// <summary>
        /// read result as integer
        /// </summary>
        /// <param name="data">bytes</param>
        /// <returns>integer</returns>
        public static int ReadAsInt(byte[] data)
        {
            ThrowExceptionIfError(data);
            string str = ReadAsString(data);
            return int.Parse(str.GetNumber());
        }

        /// <summary>
        /// read something start with + as bool
        /// </summary>
        /// <param name="data">bytes</param>
        /// <returns>ture if ok</returns>
        public static bool IsOK(byte[] data)
        {
            ThrowExceptionIfError(data);
            string str = ReadAsString(data);
            return RedisUtil.IsOK(str);
        }

        private static void ThrowExceptionIfError(byte[] redisResult)
        {
            ResultType type = RedisUtil.GetResultType(redisResult);
            if (type == ResultType.Error)
            {
                string info = GlobalEncoding.GetString(redisResult);
                throw new Exception(string.Format("Error: {0}", RedisUtil.GetError(info)));
            }
        }

        private static void ThrowExceptionIfTypeNotMatch(ResultType actual, ResultType expect)
        {
            if (actual != expect)
            {
                throw new Exception(string.Format("The result type is {0}, not {1}", actual, expect));
            }
        }

        private static void ThrowExceptionIfTypeNotMatch(byte[] data, ResultType expect)
        {
            ResultType actual = RedisUtil.GetResultType(data);
            ThrowExceptionIfTypeNotMatch(actual, expect);
        }
    }
}
