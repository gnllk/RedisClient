using RClient;
using System.Windows.Forms;

namespace Gnllk.RedisClient
{
    public class ConnectionNode : TreeNode
    {
        public IConnectionItem ConnectionItem { get; set; }

        public Section DbNames { get; set; }

        public ConnectionNode() { }

        public ConnectionNode(string text) : base(text) { }
    }
}
