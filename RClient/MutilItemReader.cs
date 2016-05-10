using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RClient
{
    /// <summary>
    /// handle data that start with *
    /// </summary>
    public class MutilItemReader : IDataReader<string>
    {
        private readonly string mData = string.Empty;

        private int mStartIndex = 0;

        private int mEndIndex = 0;

        private readonly int mItemCount = 0;

        private int mCurrentItem = 0;

        public int ItemCount { get { return mItemCount; } }

        public MutilItemReader(string data)
        {
            if (data != null && data.StartsWith("*"))
            {
                mData = data;
                mItemCount = GetItemCount();
            }
        }

        public bool Next()
        {
            bool hasNext = mCurrentItem++ < mItemCount;
            if (hasNext)
            {
                mStartIndex = mEndIndex;
                string line = ReadLine(mData, mStartIndex, out mEndIndex);
                int length = line.GetNumber().ToInt32(0);
                mStartIndex = mEndIndex;
                mEndIndex += length + 2;
            }
            return hasNext;
        }

        public string GetValue()
        {
            return mData.Substring(mStartIndex, mEndIndex - mStartIndex - 2);
        }

        public void Reset()
        {
            mCurrentItem = 0;
            mStartIndex = 0;
            GetItemCount();
        }

        private int GetItemCount()
        {
            string line = ReadLine(mData, mStartIndex, out mEndIndex);
            return line.GetNumber().ToInt32(0);
        }

        private string ReadLine(string data, int start, out int end)
        {
            int index = data.IndexOf('\n', start);
            if (index != -1)
            {
                end = index + 1;
                return data.Substring(start, end - start);
            }
            else
            {
                end = data.Length + 1;
                return data.Substring(start, end - start);
            }
        }
    }
}
