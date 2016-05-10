using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RClient
{
    public class RedisUtil
    {
        public const char ERROR = '-';
        public const char LARGE = '$';
        public const char MUTIL = '*';
        public const char STATUS = '+';
        public const char INTEGER = ':';

        public const string OK = "+OK";

        public static ResultType GetResultType(byte[] redisResult)
        {
            if (redisResult == null || redisResult.Length < 1) return ResultType.Unknow;
            char first = (char)redisResult[0];
            switch (first)
            {
                case STATUS: return ResultType.Status;
                case MUTIL: return ResultType.Mutil;
                case LARGE: return ResultType.Large;
                case INTEGER: return ResultType.Integer;
                case ERROR: return ResultType.Error;
                default: return ResultType.Unknow;
            }
        }

        public static bool IsString(byte[] redisResult)
        {
            ResultType type = GetResultType(redisResult);
            switch (type)
            {
                case ResultType.Large://maybe string
                case ResultType.Unknow: return false;
                default: return true;
            }
        }

        public static bool IsOK(string redisResult)
        {
            return redisResult != null && redisResult.StartsWith(OK);
        }

        public static byte[] GetLarge(byte[] redisResult)
        {
            var reader = new LineReader(redisResult, Encoding.UTF8);
            if (reader.Next())
            {
                string first = reader.GetValue();
                int largeLength = first.GetNumber().ToInt32(0);
                if (largeLength <= 0) return null;
                byte[] result = new byte[largeLength];
                for (int i = 0; i < largeLength && i + first.Length < redisResult.Length; i++)
                {
                    result[i] = redisResult[i + first.Length];
                }
                return result;
            }
            return null;
        }

        public static string GetError(string redisResult)
        {
            if (string.IsNullOrWhiteSpace(redisResult)) return string.Empty;
            int firstWhiteSpcase = redisResult.IndexOf(" ");
            if (firstWhiteSpcase == -1) return string.Empty;
            return redisResult.Substring(firstWhiteSpcase + 1, redisResult.Length - firstWhiteSpcase - 3);
        }

        public static List<string> GetMutil(string redisResult)
        {
            List<string> result = new List<string>();
            var reader = new MutilItemReader(redisResult);
            while (reader.Next())
            {
                result.Add(reader.GetValue());
            }
            return result;
        }

        public static string GetStatus(string redisResult)
        {
            if (string.IsNullOrWhiteSpace(redisResult)) return string.Empty;
            if (redisResult.Length <= 3) return redisResult;
            return redisResult.Substring(1, redisResult.Length - 3);
        }

        public static int GetInteger(string redisResult)
        {
            return redisResult.GetNumber().ToInt32(0);
        }
    }
}
