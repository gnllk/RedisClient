using System;
using System.Text;

namespace Gnllk.JCommon.Helper
{
    /// <summary>
    /// password cryptograph
    /// </summary>
    public class PasswordHelper
    {
        private static readonly byte[] KEY = Encoding.UTF8.GetBytes("7479c^93(*)ddsg@!");

        /// <summary>
        /// Encript plaintext password and return hex string
        /// </summary>
        /// <param name="pwd">plaintext password</param>
        /// <returns>hex string</returns>
        public static string Encript(string pwd)
        {
            byte[] data = Encoding.UTF8.GetBytes(pwd);
            RC4Helper.RC4(ref data, KEY);
            return ToHex(data);
        }

        /// <summary>
        /// Decript hex password and return plaintext password
        /// </summary>
        /// <param name="hex">hex string, could start with "0x"</param>
        /// <returns>plaintext password</returns>
        public static string Decript(string hex)
        {
            byte[] data = FromHex(hex);
            RC4Helper.RC4(ref data, KEY);
            return Encoding.UTF8.GetString(data);
        }

        /// <summary>
        /// byte array to hex string without "0x"
        /// </summary>
        /// <param name="data">byte array</param>
        /// <returns>hex string</returns>
        public static string ToHex(byte[] data)
        {
            if (data == null) return null;
            StringBuilder sb = new StringBuilder(data.Length * 2 + 9);
            foreach (byte item in data)
            {
                sb.Append(Convert.ToString(item, 16).PadLeft(2, '0'));
            }
            return sb.ToString();
        }

        /// <summary>
        /// hex string to byte array
        /// </summary>
        /// <param name="hex">hex string, could start with "0x"</param>
        /// <returns>byte array</returns>
        public static byte[] FromHex(string hex)
        {
            if (string.IsNullOrEmpty(hex)) return null;
            if (hex.StartsWith("0x", StringComparison.CurrentCultureIgnoreCase))
                hex = hex.Substring(2);
            byte[] result = new byte[hex.Length / 2];
            for (int i = 0; i < result.Length; i++)
            {
                char left = hex[i * 2];
                char right = hex[i * 2 + 1];
                result[i] = (byte)(Convert.ToInt32(left.ToString(), 16) * 16 + Convert.ToInt32(right.ToString(), 16));
            }
            return result;
        }
    }
}
