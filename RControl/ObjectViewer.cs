using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;

namespace Gnllk.RControl
{
    public partial class ObjectViewer : UserControl
    {
        private const int DefaultNameMinLength = 10;

        private const int DefaultValueMaxLength = 100;

        private const int RightSpaceLength = 8;

        public ObjectViewer()
        {
            InitializeComponent();
        }

        public TreeView Tree { get { return objectTreeView; } }

        public void SetObject(string key, object obj)
        {
            if (string.IsNullOrWhiteSpace(key)) return;
            if (obj == null) return;
            string value = obj.GetType().FullName;
            string disply = string.Format("{0} : {1}", key.PadRight(key.Length + RightSpaceLength), value);
            NodeItem tag = new NodeItem(key, value, disply, obj, false, true);
            if (objectTreeView.Nodes.ContainsKey(key))//update
            {
                TreeNode node = objectTreeView.Nodes[key];
                node.Nodes.Clear();
                node.Text = disply;
                node.Tag = tag;
            }
            else//add
            {
                TreeNode node = new TreeNode(disply) { Name = key, Tag = tag };
                node.ContextMenu = GetTopNodeMenu(node);
                objectTreeView.Nodes.Add(node);
            }
        }

        public object GetObject(string key)
        {
            if (objectTreeView.Nodes.ContainsKey(key))
                return objectTreeView.Nodes[key].Tag;
            else return null;
        }

        public void RemoveObject(string key)
        {
            if (objectTreeView.Nodes.ContainsKey(key))
                objectTreeView.Nodes[key].Remove();
        }

        public void Clear()
        {
            objectTreeView.Nodes.Clear();
        }

        private List<Member> GetObjectMember(object obj, out int maxNameLength)
        {
            maxNameLength = DefaultNameMinLength;
            List<Member> list = new List<Member>();
            if (obj == null) return list;
            else if (obj is IEnumerable)
            {
                int i = 0;
                IEnumerable objList = obj as IEnumerable;
                foreach (var item in objList)
                {
                    string name = string.Format("[{0}]", i++.ToString());
                    if (name.Length > maxNameLength) maxNameLength = name.Length;
                    list.Add(new Member()
                    {
                        MName = name,
                        MObject = item,
                        MType = IsFinalValue(item.GetType()) ? MemberType.Final : MemberType.Object
                    });
                }
            }
            else
            {
                Type objType = obj.GetType();
                PropertyInfo[] ps = objType.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
                foreach (var item in ps)
                {
                    if (item.Name.Length > maxNameLength) maxNameLength = item.Name.Length;
                    var temp = item.GetValue(obj, null);
                    AddMemberTo(list, item, temp);
                }
                FieldInfo[] fs = objType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
                foreach (var item in fs)
                {
                    if (item.Name.Length > maxNameLength) maxNameLength = item.Name.Length;
                    var temp = item.GetValue(obj);
                    AddMemberTo(list, item, temp);
                }
            }
            return list;
        }

        private void AddMemberTo(List<Member> list, MemberInfo info, object mObject)
        {
            list.Add(new Member()
            {
                MName = info.Name,
                MObject = mObject,
                MType = GetMemberType(mObject)
            });
        }

        private bool IsFinalValue(Type type)
        {
            return (type.IsValueType
                || type.IsPointer
                || type.IsPrimitive
                || type.IsEnum
                || type == typeof(string));
        }

        private MemberType GetMemberType(object obj)
        {
            if (obj == null) return MemberType.Unkown;
            if (IsFinalValue(obj.GetType())) return MemberType.Final;
            if (obj is IEnumerable) return MemberType.List;
            return MemberType.Object;
        }

        private void AddChildsTo(TreeNode parent, NodeItem nodeItem, bool expand = true)
        {
            if (parent == null || nodeItem == null || nodeItem.IsFinal || nodeItem.Tag == null) return;
            int maxNameLenght;
            List<Member> list = GetObjectMember(nodeItem.Tag, out maxNameLenght);
            foreach (var item in list)
            {
                if (item.MType == MemberType.Final || item.MObject == null)
                {
                    string value = GetDisplayValue(item.MObject);
                    string display = string.Format("{0} : {1}", item.MName.PadRight(maxNameLenght), GetShortString(value, DefaultValueMaxLength));
                    NodeItem tag = new NodeItem(item.MName, value, display, item.MObject, true);
                    TreeNode tmpNode = new TreeNode(display);
                    tmpNode.Tag = tag;
                    tmpNode.ToolTipText = value;
                    tmpNode.ContextMenu = GetNodeMenu(tmpNode);
                    AddChildTo(parent, tmpNode);
                }
                else if (item.MType == MemberType.List)
                {
                    string value = item.MObject is ICollection ? ((ICollection)item.MObject).Count.ToString() : "*";
                    string display = string.Format("{0} : {1}", item.MName.PadRight(maxNameLenght), string.Format("[{0}]", value));
                    NodeItem tag = new NodeItem(item.MName, value, display, item.MObject);
                    TreeNode tmpNode = new TreeNode(display);
                    tmpNode.Tag = tag;
                    tmpNode.ContextMenu = GetNodeMenu(tmpNode);
                    AddChildTo(parent, tmpNode);
                }
                else//object
                {
                    string value = item.MObject.GetType().FullName;
                    string display = string.Format("{0} : {1}", item.MName.PadRight(maxNameLenght), item.MObject.GetType().FullName);
                    NodeItem tag = new NodeItem(item.MName, value, display, item.MObject);
                    TreeNode tmpNode = new TreeNode(display);
                    tmpNode.Tag = tag;
                    tmpNode.ContextMenu = GetNodeMenu(tmpNode);
                    AddChildTo(parent, tmpNode);
                }
            }
            ExpandOrNot(parent);
        }

        private string GetShortString(string src, int shortLength)
        {
            if (string.IsNullOrEmpty(src)) return string.Empty;
            if (src.Length <= shortLength) return src;
            return string.Format("{0}...", src.Substring(0, shortLength));
        }

        private void ExpandOrNot(TreeNode node, bool expand = true)
        {
            if (node == null) return;
            Action set = () => { if (expand) node.Expand(); else node.Collapse(); };
            if (InvokeRequired) Invoke(set);
            else set();
        }

        public void CollapseAll()
        {
            objectTreeView.CollapseAll();
        }

        public void ExpandAll()
        {
            objectTreeView.ExpandAll();
        }

        private void AddChildTo(TreeNode parent, TreeNode child)
        {
            if (parent == null || child == null) return;
            Action<TreeNode> add = _ => { parent.Nodes.Add(_); };
            if (InvokeRequired) Invoke(add, child);
            else add(child);
        }

        private string GetDisplayValue(object obj)
        {
            return obj == null ? "null" : obj.ToString();
        }

        private void RemoveNode(TreeNode node)
        {
            if (node == null) return;
            Action remove = () => { node.Remove(); };
            if (InvokeRequired) Invoke(remove);
            else remove();
        }

        private void ShowNode(TreeNode node, bool showXmlIfItIs = true)
        {
            if (node == null || node.Tag == null) return;
            NodeItem tag = node.Tag as NodeItem;
            string content = tag.Value;
            if (showXmlIfItIs && IsXml(content))
            {
                ShowXml(tag.Name, content);
            }
            else
            {
                ShowText(tag.Name, content);
            }
        }

        private void ShowXml(string name, string content)
        {
            var form = new XmlForm();
            form.Text = name;
            if (content.StartsWith("<?xml", StringComparison.CurrentCultureIgnoreCase))
                form.Data = content;
            else
                form.Data = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n{0}", content);
            form.Show(this);
        }

        private void ShowText(string name, string content)
        {
            var form = new TextForm();
            form.Text = name;
            form.Data = string.Format("{0}", content);
            form.Show(this);
        }

        private bool IsXml(string content)
        {
            if (!string.IsNullOrWhiteSpace(content))
            {
                content = content.Trim();
                if (content.StartsWith("<") && content.EndsWith(">"))
                {
                    return true;
                }
            }
            return false;
        }

        private void CopyNodeValueToClipboard(TreeNode node)
        {
            if (node == null && node.Tag != null) return;
            NodeItem tag = node.Tag as NodeItem;
            if (tag == null) return;
            Clipboard.SetText(tag.Value);
        }

        private void CopyNodeNameToClipboard(TreeNode node)
        {
            if (node == null || node.Tag == null) return;
            NodeItem tag = node.Tag as NodeItem;
            if (tag == null) return;
            Clipboard.SetText(tag.Name);
        }

        private ContextMenu GetNodeMenu(TreeNode node)
        {
            ContextMenu menu = new ContextMenu();
            menu.MenuItems.Add(new MenuItem("Copy value", new EventHandler(OnNodeClickCopyMenu), Shortcut.CtrlC) { Tag = node });
            menu.MenuItems.Add(new MenuItem("Copy name", new EventHandler(OnNodeClickCopyNameMenu), Shortcut.CtrlN) { Tag = node });
            menu.MenuItems.Add(new MenuItem("Show as text", new EventHandler(OnNodeClickShowMenu), Shortcut.CtrlK) { Tag = node });
            menu.MenuItems.Add(new MenuItem("Show as XML", new EventHandler(OnNodeClickShowMenu), Shortcut.CtrlL) { Tag = node });
            menu.MenuItems.Add(new MenuItem("Update value", new EventHandler(OnNodeClickUpdateValueMenu), Shortcut.F9) { Tag = node });
            return menu;
        }

        private ContextMenu GetTopNodeMenu(TreeNode node)
        {
            ContextMenu menu = new ContextMenu();
            menu.MenuItems.Add(new MenuItem("Delete", new EventHandler(OnNodeClickRemoveMenu), Shortcut.Del) { Tag = node });
            //menu.MenuItems.Add(new MenuItem("Export as XML", new EventHandler(OnNodeClicExportMenu), Shortcut.CtrlE) { Tag = node });
            return menu;
        }

        private void OnNodeClickCopyMenu(object sender, EventArgs e)
        {
            MenuItem item = sender as MenuItem;
            TreeNode node = item.Tag as TreeNode;
            CopyNodeValueToClipboard(node);
        }

        private void OnNodeClickShowMenu(object sender, EventArgs e)
        {
            MenuItem item = sender as MenuItem;
            TreeNode node = item.Tag as TreeNode;
            if (item.Shortcut == Shortcut.CtrlK)
            {
                ShowNode(node, false);
            }
            else
            {
                ShowNode(node);
            }
        }

        private void OnNodeClickCopyNameMenu(object sender, EventArgs e)
        {
            MenuItem item = sender as MenuItem;
            TreeNode node = item.Tag as TreeNode;
            CopyNodeNameToClipboard(node);
        }

        private void OnNodeClickUpdateValueMenu(object sender, EventArgs e)
        {
            MenuItem menu = sender as MenuItem;
            TreeNode node = menu.Tag as TreeNode;
            if (node == null || node.Tag == null && node.Parent == null) return;
            NodeItem item = node.Tag as NodeItem;

            using (var form = new EditForm() { EditName = item.Name, EditValue = item.Value })
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    item.Value = form.EditValue.Trim();
                }
            }
        }

        private void OnNodeClickRemoveMenu(object sender, EventArgs e)
        {
            MenuItem item = sender as MenuItem;
            TreeNode node = item.Tag as TreeNode;
            RemoveNode(node);
        }

        private void OnNodeClicExportMenu(object sender, EventArgs e)
        {
            try
            {
                MenuItem item = sender as MenuItem;
                TreeNode node = item.Tag as TreeNode;
                NodeItem nodeItem = node.Tag as NodeItem;
                object obj = nodeItem.Tag;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnPressDeleteKey(object sender, KeyEventArgs e)
        {
            TreeNode node = objectTreeView.SelectedNode;
            RemoveNode(node);
        }

        private void OnPressCtrlAndCKey(object sender, KeyEventArgs e)
        {
            TreeNode node = objectTreeView.SelectedNode;
            CopyNodeValueToClipboard(node);
        }

        private void OnPressCtrlAndKKey(object sender, KeyEventArgs e)
        {
            TreeNode node = objectTreeView.SelectedNode;
            ShowNode(node, false);
        }

        private void OnPressCtrlAndLKey(object sender, KeyEventArgs e)
        {
            TreeNode node = objectTreeView.SelectedNode;
            ShowNode(node);
        }

        private void OnPressCtrlAndNKey(object sender, KeyEventArgs e)
        {
            TreeNode node = objectTreeView.SelectedNode;
            CopyNodeNameToClipboard(node);
        }

        private void OnPressEnterKey(object sender, KeyEventArgs e)
        {
            TreeNode node = objectTreeView.SelectedNode;
            if (node.Tag == null) return;
            NodeItem tag = node.Tag as NodeItem;
            if (node.Nodes.Count > 0)
            {
                ExpandOrNot(node, !node.IsExpanded);
            }
            else if (tag.IsFinal)
            {
                ShowNode(node);
            }
            else
            {
                AddChildsTo(node, tag);
            }
        }

        private void objectTreeView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    OnPressEnterKey(sender, e);
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    OnPressDeleteKey(sender, e);
                }
                else if (e.Control && e.KeyCode == Keys.C)
                {
                    OnPressCtrlAndCKey(sender, e);
                }
                else if (e.Control && e.KeyCode == Keys.N)
                {
                    OnPressCtrlAndNKey(sender, e);
                }
                else if (e.Control && e.KeyCode == Keys.K)
                {
                    OnPressCtrlAndKKey(sender, e);
                }
                else if (e.Control && e.KeyCode == Keys.L)
                {
                    OnPressCtrlAndLKey(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void objectTreeView_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void objectTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                TreeNode node = e.Node;
                if (node.Nodes.Count > 0 || node.Tag == null) return;
                AddChildsTo(node, node.Tag as NodeItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void objectTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                TreeNode node = e.Node;
                NodeItem tag = node.Tag as NodeItem;
                if (tag != null && tag.IsFinal)
                {
                    ShowNode(node);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    internal enum MemberType
    {
        Unkown, Final, Object, List
    }

    internal class Member
    {
        public MemberType MType { get; set; }
        public string MName { get; set; }
        public object MObject { get; set; }
    }

    public class NodeItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string DisplayText { get; set; }
        public object Tag { get; set; }
        public bool IsFinal { get; set; }
        public bool IsTop { get; set; }
        public NodeItem() { }
        public NodeItem(string name, string value, string display)
        {
            Name = name;
            Value = value;
            DisplayText = display;
        }
        public NodeItem(string name, string value, string display, object tag, bool isFinal = false, bool isTop = false)
            : this(name, value, display)
        {
            IsFinal = isFinal;
            IsTop = isTop;
            Tag = tag;
        }
    }
}
