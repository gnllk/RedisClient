using System.Collections.Generic;
using System.Windows.Forms;

namespace Gnllk.RedisClient
{
    public class DbNode : TreeNode
    {
        public IDatabaseItem DatabaseItem { get; set; }

        public List<string> ChildData { get; set; }

        public List<string> ViewData { get; set; }

        public DbNode() { }

        public DbNode(string text) : base(text) { }
    }
}
