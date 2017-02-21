using System.Windows.Forms;

namespace Gnllk.RedisClient
{
    public class KeyNode : TreeNode
    {
        public IKeyItem KeyItem { get; set; }

        public object Value { get; set; }

        public KeyNode() { }

        public KeyNode(string text) : base(text) { }
    }
}
