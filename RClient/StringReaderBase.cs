using System;
using System.Text;

namespace RClient
{
    public abstract class StringReaderBase : IDataReader<string>
    {
        private Encoding mEncoding = Encoding.UTF8;

        public Encoding Encoding
        {
            get { return mEncoding; }
            protected set
            {
                if (value == null) throw new ArgumentNullException();
                mEncoding = value;
            }
        }

        protected StringReaderBase(Encoding encoding)
        {
            this.Encoding = encoding;
        }

        public abstract bool Next();

        public abstract void Reset();

        public abstract string GetValue();
    }
}
