namespace Gnllk.RedisClient
{
    public class ClipboardKeyValue
    {
        public ClipboardKeyValue() { }

        public ClipboardKeyValue(string key, byte[] value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }

        public byte[] Value { get; set; }
    }
}
