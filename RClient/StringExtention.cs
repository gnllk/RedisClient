using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RClient
{
    public static class StringExtention
    {
        public static bool ToBoolean(this string str, bool _default = false)
        {
            return Converter.ParseBoolean(str, _default);
        }

        public static string ToNotNullString(this string str, string _default = "")
        {
            if (string.IsNullOrEmpty(str))
                return _default;
            return str;
        }

        public static DateTime ToDateTime(this string str, DateTime _default)
        {
            if (str != null)
            {
                return Converter.ParseDt(str, _default);
            }
            return _default;
        }

        public static int ToInt32(this string str, int _default = 0)
        {
            if (str != null)
            {
                return Converter.ParseInt(str, _default);
            }
            return _default;
        }

        public static long ToInt64(this string str, long _default = 0)
        {
            if (str != null)
            {
                return Converter.ParseLong(str, _default);
            }
            return _default;
        }

        public static float ToSingle(this string str, float _default = 0)
        {
            if (str != null)
            {
                return Converter.ParseFloat(str, _default);
            }
            return _default;
        }

        public static double ToDouble(this string str, double _default = 0)
        {
            if (str != null)
            {
                return Converter.ParseDouble(str, _default);
            }
            return _default;
        }

        public static Guid ToGuid(this string str, Guid _default)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return Converter.ParseGuid(str, _default);
            }
            return _default;
        }

        public static string GetNumber(this string str, string _default = "")
        {
            if (string.IsNullOrWhiteSpace(str)) return _default;
            StringBuilder sb = new StringBuilder(32);
            foreach (char c in str)
            {
                if (c >= '0' && c <= '9') sb.Append(c);
            }
            if (sb.Length == 0) return _default;
            return sb.ToString();
        }
    }
}
