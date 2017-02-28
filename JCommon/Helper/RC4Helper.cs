namespace Gnllk.JCommon.Helper
{
    public static class RC4Helper
    {
        /// <summary>
        /// 加解密
        /// </summary>
        /// <param name="bytes">引用字节数组数据</param>
        /// <param name="key">字节数组密匙</param>
        public static void RC4(ref byte[] bytes, byte[] key)
        {
            byte[] s = new byte[256];
            byte[] k = new byte[256];
            byte temp;
            int i, j, keyLen = key.GetLength(0), dataLen = bytes.GetLength(0);
            for (i = 0; i < 256; i++)
            {
                s[i] = (byte)i;
                k[i] = key[i % keyLen];
            }
            j = 0;
            for (i = 0; i < 256; i++)
            {
                j = (j + s[i] + k[i]) % 256;
                temp = s[i];
                s[i] = s[j];
                s[j] = temp;
            }
            i = j = 0;
            for (int x = 0; x < dataLen; x++)
            {
                i = (i + 1) % 256;
                j = (j + s[i]) % 256;
                temp = s[i];
                s[i] = s[j];
                s[j] = temp;
                int t = (s[i] + s[j]) % 256;
                bytes[x] ^= s[t];
            }
        }

        /// <summary>
        /// 加解密（源数组不变）
        /// </summary>
        /// <param name="bytes">字节数组数据</param>
        /// <param name="key">字节数组密匙</param>
        /// <returns>加解密后的字节数组数据</returns>
        public static byte[] RC4(byte[] bytes, byte[] key)
        {
            byte[] s = new byte[256];
            byte[] k = new byte[256];
            byte temp;
            int i, j, keyLen = key.GetLength(0), dataLen = bytes.GetLength(0);
            byte[] o = new byte[dataLen];
            for (i = 0; i < 256; i++)
            {
                s[i] = (byte)i;
                k[i] = key[i % keyLen];
            }
            j = 0;
            for (i = 0; i < 256; i++)
            {
                j = (j + s[i] + k[i]) % 256;
                temp = s[i];
                s[i] = s[j];
                s[j] = temp;
            }
            i = j = 0;
            for (int x = 0; x < dataLen; x++)
            {
                i = (i + 1) % 256;
                j = (j + s[i]) % 256;
                temp = s[i];
                s[i] = s[j];
                s[j] = temp;
                int t = (s[i] + s[j]) % 256;
                o[x] = (byte)(bytes[x] ^ s[t]);
            }
            return o;
        }
    }
}
