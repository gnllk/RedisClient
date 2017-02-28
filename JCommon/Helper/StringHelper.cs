namespace Gnllk.JCommon.Helper
{
    public static class StringHelper
    {
        public static string GetString(string src, int getMaxLength, bool addMoreSymbolToEnd = false)
        {
            if (src == null || getMaxLength <= 0 || getMaxLength > src.Length) return src;
            else return string.Format("{0}{1}",
                src.Substring(0, getMaxLength),
                addMoreSymbolToEnd ? "..." : string.Empty);
        }
    }
}
