using System.Text;

namespace RClient
{
    public class LineReader : StringReaderBase
    {
        private long mStartIndex = 0;
        private long mEndIndex = -1;
        private byte[] mData = null;
        private const byte LF = (byte)'\n';

        public LineReader(byte[] data, Encoding encoding)
            : base(encoding)
        {
            mData = data ?? new byte[0];
        }

        public override bool Next()
        {
            mStartIndex = mEndIndex + 1;
            bool result = mStartIndex <= mData.Length;
            long lfFndex = IndexOf(mData, LF, mStartIndex);
            if (lfFndex != -1L) mEndIndex = lfFndex;
            else mEndIndex = mData.LongLength - 1;
            return result;
        }

        public override void Reset()
        {
            mStartIndex = 0;
            mEndIndex = -1;
        }

        public override string GetValue()
        {
            long size = mEndIndex - mStartIndex + 1;
            if (size > 0)
            {
                byte[] data = CopyFrom(mData, mStartIndex, size);
                return this.Encoding.GetString(data);
            }
            return string.Empty;
        }

        protected byte[] CopyFrom(byte[] data, long start, long size)
        {
            if (data != null && data.LongLength > start)
            {
                byte[] result = new byte[size];
                for (int i = 0; i < size && i + start < data.LongLength; i++)
                {
                    result[i] = data[i + start];
                }
                return result;
            }
            return new byte[0];
        }

        protected long IndexOf(byte[] data, byte asc2, long start = 0)
        {
            if (data == null || data.Length < 1) return -1L;
            if (start < 0) start = 0;
            if (start > data.Length) return -1L;
            for (long i = start; i < data.Length; i++)
            {
                if (data[i] == asc2) return i;
            }
            return -1L;
        }
    }
}
