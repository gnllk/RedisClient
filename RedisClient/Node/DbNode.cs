using System.Collections.Generic;
using System.Windows.Forms;

namespace Gnllk.RedisClient
{
    public class DbNode : TreeNode
    {
        public IDatabaseItem DatabaseItem { get; set; }

        public List<string> Keys { get; set; }

        public List<string> ViewKeys { get; set; }

        public DbNode() { }

        public DbNode(string text) : base(text) { }
    }
}
