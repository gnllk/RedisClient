using System;
using System.Collections.Generic;

namespace RClient
{
    public class Converter
    {
        public static DateTime ParseDt(string dateStr, DateTime _default)
        {
            DateTime tmpDate = DateTime.Now;
            if (DateTime.TryParse(dateStr, out tmpDate))
                return tmpDate;
            else
                return _default;
        }

        public static DateTime? ParseDtOrNull(string dateStr)
        {
            DateTime tmpDate = DateTime.Now;
            if (DateTime.TryParse(dateStr, out tmpDate))
                return tmpDate;
            else
                return null;
        }

        public static int ParseInt(string intStr, int _default)
        {
            int tmpInt = 0;
            if (Int32.TryParse(intStr, out tmpInt))
                return tmpInt;
            else
                return _default;
        }

        public static long ParseLong(string longStr, long _default)
        {
            long tmpInt = 0;
            if (Int64.TryParse(longStr, out tmpInt))
                return tmpInt;
            else
                return _default;
        }

        public static float ParseFloat(string floatStr, float _default)
        {
            float tmpInt = 0;
            if (Single.TryParse(floatStr, out tmpInt))
                return tmpInt;
            else
                return _default;
        }

        public static double ParseDouble(string doubleStr, double _default)
        {
            double tmpInt = 0;
            if (Double.TryParse(doubleStr, out tmpInt))
                return tmpInt;
            else
                return _default;
        }

        public static decimal ParseDecimal(string decimalStr, decimal _default)
        {
            decimal tmpInt = 0;
            if (decimal.TryParse(decimalStr, out tmpInt))
                return tmpInt;
            else
                return _default;
        }

        public static bool ParseBoolean(string str, bool _default)
        {
            bool tmp = false;
            if (Boolean.TryParse(str, out tmp))
                return tmp;
            else
                return _default;
        }

        public static Guid ParseGuid(string str, Guid _default)
        {
            Guid tmp = Guid.Empty;
            if (Guid.TryParse(str, out tmp))
                return tmp;
            else
                return _default;
        }

        public static List<KeyValuePair<string, string>> ParseKVPair(string str, char[] itemSplit, char[] kvSplit)
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            if (!string.IsNullOrWhiteSpace(str) && itemSplit != null && kvSplit != null)
            {
                string[] items = str.Split(itemSplit, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in items)
                {
                    string[] kv = item.Split(kvSplit);
                    list.Add(new KeyValuePair<string, string>(kv[0], kv.Length > 1 ? kv[1] : string.Empty));
                }
            }
            return list;
        }

        public static List<KeyValuePair<string, string>> ParseKVPair(string str, char itemSplit = ',', char kvSplit = '=')
        {
            return ParseKVPair(str, new char[] { itemSplit }, new char[] { kvSplit });
        }
    }
}
