using System.Runtime.Serialization;

namespace ShowAsTextPlugin
{
    [DataContract]
    public class ShowAsTextConfig
    {
        private string mEncodingName = "utf-8";

        [DataMember]
        public string EncodingName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(mEncodingName)) mEncodingName = "utf-8";
                return mEncodingName;
            }
            set
            {
                mEncodingName = value;
            }
        }
    }
}
