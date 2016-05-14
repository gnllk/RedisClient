using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;
using RClient;
using Gnllk.RedisClient.Manager;
using System.Reflection;
using System.Diagnostics;
using Gnllk.RControl;
using Gnllk.RedisClient.Common;

namespace Gnllk.RedisClient
{
    public partial class FormMain : Form
    {
        public readonly string[] EncodingName = new string[] { "gb2312", "utf-16", "unicodeFFFE", "Windows-1252", "x-mac-korean", "x-mac-chinesesimp", "utf-32", "utf-32BE", "us-ascii", "x-cp20936", "x-cp20949", "iso-8859-1", "iso-8859-8", "iso-8859-8-i", "iso-2022-jp", "csISO2022JP", "iso-2022-kr", "x-cp50227", "euc-jp", "EUC-CN", "euc-kr", "hz-gb-2312", "GB18030", "x-iscii-de", "x-iscii-be", "x-iscii-ta", "x-iscii-te", "x-iscii-as", "x-iscii-or", "x-iscii-ka", "x-iscii-ma", "x-iscii-gu", "x-iscii-pa", "utf-7", "utf-8" };

        public const string DefaultEncodingName = "utf-8";

        public const string DeleteAlertMsg = "Are you sure delete?";

        public const int MAX_STRING_LENGTH = 100;

        private static object classLock = new object();

        private object GetValueLock = new object();

        public FormMain()
        {
            InitializeComponent();
            tvDataTree.KeyUp += new KeyEventHandler(tvDataTree_KeyUp);
            try
            {
                FileVersionInfo version = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);
                this.Text = string.Format("{0} (version: {1})", this.Text, version.ProductVersion);
                splitMain.SplitterDistance = (int)(this.Width / 2d);
                tvDataTree.Select();
            }
            catch (Exception ex)
            {
                Show("Initialize fail: {0}", ex.Message);
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigManager.Instance.LoadConnection();
            }
            catch (Exception ex)
            {
                Show("Load configuration fail: {0}", ex.Message);
            }
            try
            {
                InitEncoding();
                InitDataTree();
            }
            catch (Exception ex)
            {
                Show("Initialize control fail: {0}", ex.Message);
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                ShowStatusInfo("Saving configuration...");
                ConfigManager.Instance.Config.Encoding = cbbEncoding.SelectedItem.ToString();
                ConfigManager.Instance.SaveConnection();
            }
            catch (Exception ex)
            {
                Show("Save configuration error: {0}", ex.Message);
            }
            try
            {
                ShowStatusInfo("freeing connection...");
                ConfigManager.Instance.FreeConnection();
            }
            catch (Exception ex)
            {
                Show("free connection error: {0}", ex.Message);
            }
        }

        private void InitEncoding()
        {
            cbbEncoding.DataSource = EncodingName;
            cbbEncoding.SelectedItem = string.IsNullOrWhiteSpace(ConfigManager.Instance.Config.Encoding)
                || !EncodingName.Contains(ConfigManager.Instance.Config.Encoding)
                ? DefaultEncodingName : ConfigManager.Instance.Config.Encoding;
        }

        private void InitDataTree()
        {
            foreach (var item in ConfigManager.Instance.Config.Connections)
            {
                AddConnectionToDataTree(item.Key, item.Value);
            }
        }

        private string GetString(string src, int getMaxLength, bool addMoreSymbolToEnd = false)
        {
            if (src == null || getMaxLength <= 0 || getMaxLength > src.Length) return src;
            else return string.Format("{0}{1}", src.Substring(0, getMaxLength), addMoreSymbolToEnd ? "..." : string.Empty);
        }

        private void Show(string format, params object[] args)
        {
            try
            {
                MessageBox.Show(string.Format(format, args));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddConnectionToDataTree(string name, RedisConnection cont)
        {
            if (!string.IsNullOrWhiteSpace(name) && cont != null)
            {
                string desc = GetString(cont.Description, MAX_STRING_LENGTH, true);
                string text = string.IsNullOrWhiteSpace(desc) ?
                    name : string.Format("{0} ({1})", name, desc);
                var node = new TreeNode(text);
                node.ImageIndex = 1;
                node.SelectedImageIndex = 1;
                node.Tag = new ConnectionItem(cont, name);
                node.ContextMenu = GetConnectionNodeMenu(node);
                tvDataTree.Nodes.Add(node);
            }
        }

        private void btnAddConnect_Click(object sender, EventArgs e)
        {
            try
            {
                var manager = ConfigManager.Instance;
                var endPoint = txtEndPiont.Text.Trim();
                var password = txtPassword.Text.Trim();
                if (!manager.Config.Connections.ContainsKey(endPoint))
                {
                    var cont = new RedisConnection(endPoint, password);
                    manager.Config.Connections.Add(endPoint, cont);
                    AddConnectionToDataTree(endPoint, cont);
                }
                else
                {
                    Show("Existed");
                }
            }
            catch (Exception ex)
            {
                Show(ex.Message);
            }
        }

        private void tvDataTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (e.Button != System.Windows.Forms.MouseButtons.Left) return;
                LoadNodeChilds(e.Node);
            }
            catch (Exception ex)
            {
                Show(ex.Message);
            }
        }

        private void LoadNodeChilds(TreeNode node, bool expand = false)
        {
            if (node == null) return;
            if (node.Nodes.Count > 0)
            {
                if (expand)
                {
                    if (node.IsExpanded) node.Collapse();
                    else node.Expand();
                }
                return;
            }
            if (node.Tag is KeyItem)
            {
                Task.Factory.StartNew(new Action<object>(_ => { ShowValue((TreeNode)_); }), node);
            }
            else if (node.Tag is DatabaseItem)
            {
                Task.Factory.StartNew(new Action<object>(_ => { ShowKeys((TreeNode)_); }), node);
            }
            else if (node.Tag is ConnectionItem)
            {
                Task.Factory.StartNew(new Action<object>(_ => { ShowDbInfo((TreeNode)_); }), node);
            }
        }

        private void tvDataTree_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up
                || e.KeyCode == Keys.Down
                || e.KeyCode == Keys.PageUp
                || e.KeyCode == Keys.PageDown
                || e.KeyCode == Keys.Home
                || e.KeyCode == Keys.End)
            {
                TreeView tree = sender as TreeView;
                Task.Factory.StartNew(_ => ShowValue((TreeNode)_), tree.SelectedNode);
            }
        }

        private void cbbEncoding_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                AppCache.Instance.CurrentEncoding = Encoding.GetEncoding(cbbEncoding.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                AppCache.Instance.CurrentEncoding = Encoding.UTF8;
                cbbEncoding.SelectedItem = DefaultEncodingName;
                Show(ex.Message);
            }
            finally
            {
                ShowAppCacheValue();
            }
        }

        private void ShowAppCacheValue(int showLength = 320)
        {
            try
            {
                Encoding encoding = AppCache.Instance.CurrentEncoding;
                byte[] data = AppCache.Instance.CurrentGetData;
                string str = data == null ? string.Empty : encoding.GetString(data);
                if (str.Length > showLength) str = str.Substring(0, showLength) + "...";
                Action<string> set = _ =>
                {
                    Cursor.Current = Cursors.WaitCursor;
                    txtValue.Text = _;
                    Cursor.Current = Cursors.Default;
                };
                if (InvokeRequired) Invoke(set, str);
                else set(str);

            }
            catch (Exception ex)
            {
                Show(ex.Message);
            }
        }

        private void ShowStatusKey(string key)
        {
            try
            {
                ShowStatusInfo(string.Empty);
                Action<string> set = _ => { labCurrentKey.Text = _; };
                if (InvokeRequired) Invoke(set, key);
                else set(key);
            }
            catch (Exception ex)
            {
                Show(ex.Message);
            }
        }

        private void ShowStatusInfo(string format, params object[] args)
        {
            if (format == null) return;
            try
            {
                string info = string.Format(format, args);
                Action<string> set = _ => { labInfo.Text = _; };
                if (InvokeRequired) Invoke(set, info);
                else set(info);
            }
            catch (Exception ex)
            {
                Show(ex.Message);
            }
        }

        private void ShowDbInfo(TreeNode node, bool expand = true)
        {
            if (node == null || !(node.Tag is ConnectionItem)) return;
            var item = node.Tag as ConnectionItem;
            if (item.Loading) return;
            try
            {
                item.Loading = true;
                SetPrograss(10);
                ShowStatusKey(item.CntName);
                var cont = item.Connection;
                ShowStatusInfo("read database infomation from {0}", cont.EndPoint);
                AuthIfHasPwd(cont);
                var info = cont.Execute(new RedisCommand(Command.INFO)).Read<Info>(InfoReader.ReadAsInfo);
                var keys = info[DatabaseItem.KeyspaceSetion];
                var dbCount = 0;
                SetPrograss(50);
                if (keys != null && keys.Any())
                {
                    int stepValue = 50;
                    int step = stepValue / keys.Count;
                    foreach (var db in keys)
                    {
                        var tmpNode = new TreeNode(string.Format("{0}({1})", db.Key, db.Value));
                        tmpNode.ImageIndex = 2;
                        tmpNode.SelectedImageIndex = 2;
                        tmpNode.Tag = new DatabaseItem(item, db.Key, db.Value);
                        tmpNode.ContextMenu = GetDbNodeMenu(tmpNode);
                        AddNode(node, tmpNode);
                        SetPrograss(stepValue);
                    }
                    NodeExpandOrNot(node, expand);
                    dbCount = keys.Count;
                }
                ShowStatusInfo("read {0} database", dbCount);

                SetPrograss(100);
            }
            catch (Exception ex)
            {
                ShowStatusInfo(ex.Message);
                Show(ex.Message);
            }
            finally { item.Loading = false; }
        }

        private bool AuthIfHasPwd(IRedisConnection cont)
        {
            if (!string.IsNullOrWhiteSpace(cont.Password))
            {
                var authInfo = cont.Execute(new RedisCommand(Command.AUTH, cont.Password)).Read<string>(Readers.ReadAsString);
                if (!RedisUtil.IsOK(authInfo))
                {
                    Show(authInfo);
                    return false;
                }
            }
            return true;
        }

        private void ShowKeys(TreeNode node, bool expand = true)
        {
            if (node == null || !(node.Tag is DatabaseItem)) return;
            var item = node.Tag as DatabaseItem;
            if (item.Loading) return;
            try
            {
                item.Loading = true;
                SetPrograss(10);
                var cont = item.Connection;
                ShowStatusInfo("read database keys from {0}", cont.EndPoint);
                if (cont.Select(item.DbIndex))
                {
                    SetPrograss(25);
                    var keys = cont.Execute(new RedisCommand(Command.KEYS, "*")).Read<List<string>>(Readers.ReadAsList);
                    var keysCount = 0;
                    ShowStatusKey(item.DbName);
                    SetPrograss(50);
                    if (keys != null && keys.Any())
                    {
                        double stepValue = 50;
                        double step = (double)stepValue / (double)keys.Count;
                        foreach (var key in keys)
                        {
                            var tmpNode = CreateNode(item, key);
                            AddNode(node, tmpNode);
                            ShowStatusInfo("read {0} keys", node.Nodes.Count);
                            SetPrograss((int)(stepValue += step));
                        }
                        NodeExpandOrNot(node, expand);
                        keysCount = keys.Count;
                    }
                    ShowStatusInfo("read {0} keys", keysCount);

                    SetPrograss(100);
                }
                else
                {
                    Show(string.Format("Can not select db:{0}", item.DbIndex));
                }
            }
            catch (Exception ex) { Show(ex.Message); }
            finally { item.Loading = false; }
        }

        private TreeNode CreateNode(DatabaseItem item, string key)
        {
            var tmpNode = new TreeNode(key);
            tmpNode.ImageIndex = 3;
            tmpNode.SelectedImageIndex = 3;
            tmpNode.ContextMenu = GetKeyNodeMenu(tmpNode);
            tmpNode.Tag = new KeyItem(item, key);
            return tmpNode;
        }

        private void ShowValue(TreeNode node, bool expand = true)
        {
            if (node == null || !(node.Tag is KeyItem)) return;
            var item = node.Tag as KeyItem;
            if (item.Loading) return;
            try
            {
                item.Loading = true;
                ShowStatusKey(item.Key);
                SetPrograss(10);
                var cont = item.Connection;
                if (cont.Select(item.DbIndex))
                {
                    SetPrograss(50);
                    AppCache.Instance.CurrentKey = item.Key;
                    AppCache.Instance.CurrentGetData = cont.Execute(new RedisCommand(Command.GET, item.Key)).Read<byte[]>(Readers.ReadAsBytes);
                    ShowAppCacheValue();
                    SetPrograss(100);
                }
                else
                {
                    Show(string.Format("Can not select db:{0}", item.DbIndex));
                }
            }
            catch (Exception ex) { ShowStatusInfo(ex.Message); }
            finally { item.Loading = false; }
        }

        private void SetPrograss(int value)
        {
            lock (classLock)
            {
                Action<int> set = _ => { progressBar.Value = _; };
                if (InvokeRequired) Invoke(set, value);
                else set(value);
            }
        }

        private void SetNodeText(TreeNode node, string text)
        {
            if (node == null || text == null) return;
            Action<string> set = _ => { node.Text = _; };
            if (InvokeRequired) Invoke(set, text);
            else set(text);
        }

        private void ClearChildNode(TreeNode node)
        {
            if (node == null) return;
            Action set = () => { node.Nodes.Clear(); };
            if (InvokeRequired) Invoke(set);
            else set();
        }

        private void AddNode(TreeNode parent, TreeNode child, bool insertToHead = false)
        {
            if (parent == null && child == null) return;
            lock (classLock)
            {
                Action<TreeNode> add = _ =>
                {
                    if (insertToHead) parent.Nodes.Insert(0, _);
                    else parent.Nodes.Add(_);
                };
                if (InvokeRequired) Invoke(add, child);
                else add(child);
            }
        }

        public void NodeExpandOrNot(TreeNode node, bool expand = true)
        {
            if (node == null) return;
            Action<bool> set = _ =>
            {
                if (expand) node.Expand();
                else node.Collapse();
            };
            if (InvokeRequired) Invoke(set, expand);
            else set(expand);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] data = AppCache.Instance.CurrentGetData;
                if (data != null && data.Length > 0)
                {
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.Filter = "text file (*.txt)|*.txt|raw file (*.*)|*.*";
                    dialog.FileName = AppCache.Instance.CurrentKey ?? "Default1";
                    string ext = Path.GetExtension(dialog.FileName).ToLower();
                    if (ext == string.Empty
                        || ext == ".txt"
                        || ext == ".log")
                    {
                        dialog.FilterIndex = 1;
                    }
                    else
                    {
                        dialog.FilterIndex = 2;
                    }
                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (!string.IsNullOrWhiteSpace(dialog.FileName))
                        {
                            if (dialog.FilterIndex == 1)
                            {
                                using (FileStream file = new FileStream(dialog.FileName, FileMode.CreateNew))
                                {
                                    var writer = new StreamWriter(file, AppCache.Instance.CurrentEncoding);
                                    writer.Write(txtValue.Text);
                                    writer.Flush();
                                }
                            }
                            else
                            {
                                using (FileStream file = new FileStream(dialog.FileName, FileMode.CreateNew))
                                {
                                    file.Write(data, 0, data.Length);
                                    file.Flush();
                                }
                            }
                            ShowStatusInfo("File:{0}", dialog.FileName);
                        }
                    }
                }
                else
                {
                    ShowStatusInfo("No data");
                }
            }
            catch (Exception ex)
            {
                Show(ex.Message);
            }
        }

        private ContextMenu GetDbNodeMenu(TreeNode node)
        {
            ContextMenu menu = new ContextMenu();
            menu.MenuItems.Add(new MenuItem("Refresh", new EventHandler(OnDbNodeMenuClick), Shortcut.F5) { Tag = node, ShowShortcut = true });
            menu.MenuItems.Add(new MenuItem("Add key value", new EventHandler(OnDbNodeMenuClick), Shortcut.CtrlA) { Tag = node, ShowShortcut = false });
            menu.MenuItems.Add(new MenuItem("Delete", new EventHandler(OnDbNodeMenuClick), Shortcut.Del) { Tag = node, ShowShortcut = false });
            return menu;
        }

        private ContextMenu GetConnectionNodeMenu(TreeNode node)
        {
            ContextMenu menu = new ContextMenu();
            menu.MenuItems.Add(new MenuItem("Refresh", new EventHandler(OnConnectionNodeMenuClick), Shortcut.F5) { Tag = node, ShowShortcut = true });
            menu.MenuItems.Add(new MenuItem("Delete", new EventHandler(OnConnectionNodeMenuClick), Shortcut.Del) { Tag = node, ShowShortcut = false });
            menu.MenuItems.Add(new MenuItem("Update password", new EventHandler(OnConnectionNodeMenuClick), Shortcut.F6) { Tag = node, ShowShortcut = false });
            menu.MenuItems.Add(new MenuItem("Update description", new EventHandler(OnConnectionNodeMenuClick), Shortcut.F7) { Tag = node, ShowShortcut = false });
            return menu;
        }

        private ContextMenu GetKeyNodeMenu(TreeNode node)
        {
            ContextMenu menu = new ContextMenu();
            menu.MenuItems.Add(new MenuItem("Delete", new EventHandler(OnKeyNodeMenuClick), Shortcut.Del) { Tag = node, ShowShortcut = false });
            menu.MenuItems.Add(new MenuItem("Update value", new EventHandler(OnKeyNodeMenuClick), Shortcut.F5) { Tag = node, ShowShortcut = false });
            menu.MenuItems.Add(new MenuItem("Rename", new EventHandler(OnKeyNodeMenuClick), Shortcut.F6) { Tag = node, ShowShortcut = false });
            return menu;
        }

        private void OnDbNodeMenuClick(object sender, EventArgs e)
        {
            try
            {
                this.UseWaitCursor = true;
                MenuItem item = sender as MenuItem;
                if (item != null)
                {
                    TreeNode node = item.Tag as TreeNode;
                    if (node != null)
                    {
                        if (item.Shortcut == Shortcut.F5)
                        {
                            RefreshDb(node);
                        }
                        else if (item.Shortcut == Shortcut.Del)
                        {
                            RemoveDb(node);
                        }
                        else if (item.Shortcut == Shortcut.CtrlA)
                        {
                            AddKeyValue(node);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Show(ex.Message);
            }
            finally
            {
                this.UseWaitCursor = false;
            }
        }

        private void OnConnectionNodeMenuClick(object sender, EventArgs e)
        {
            try
            {
                MenuItem item = sender as MenuItem;
                if (item != null)
                {
                    TreeNode node = item.Tag as TreeNode;
                    if (node != null)
                    {
                        if (item.Shortcut == Shortcut.F5)
                        {
                            RefreshConnection(node);
                        }
                        else if (item.Shortcut == Shortcut.Del)
                        {
                            RemoveConnection(node);
                        }
                        else if (item.Shortcut == Shortcut.F6)
                        {
                            UpdatePassword(node);
                        }
                        else if (item.Shortcut == Shortcut.F7)
                        {
                            UpdateDescription(node);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Show(ex.Message);
            }
        }

        private void OnKeyNodeMenuClick(object sender, EventArgs e)
        {
            try
            {
                MenuItem item = sender as MenuItem;
                if (item != null)
                {
                    TreeNode node = item.Tag as TreeNode;
                    if (node != null)
                    {
                        if (item.Shortcut == Shortcut.Del)
                        {
                            RemoveKey(node);
                        }
                        else if (item.Shortcut == Shortcut.F5)
                        {
                            UpdateValue(node);
                        }
                        else if (item.Shortcut == Shortcut.F6)
                        {
                            Rename(node);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Show(ex.Message);
            }
        }

        private void UpdateValue(TreeNode node)
        {
            if (node == null) return;
            KeyItem item = node.Tag as KeyItem;
            if (item == null) return;
            if (item.Connection.Select(item.DbIndex))
            {
                using (var form = new EditForm()
                {
                    EditName = item.Key,
                    EditValue = item.GetValue(AppCache.Instance.CurrentEncoding)
                })
                {
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        if (form.ByteData != null)
                        {
                            item.SetValue(form.ByteData);
                        }
                        else
                        {
                            item.SetValue(form.EditValue.Trim(), AppCache.Instance.CurrentEncoding);
                        }
                        ShowValue(node);
                    }
                }
            }
            else
            {
                Show(string.Format("Can not select db:{0}", item.DbIndex));
            }
        }

        private void Rename(TreeNode node)
        {
            if (node == null) return;
            KeyItem item = node.Tag as KeyItem;
            if (item == null) return;
            if (item.Connection.Select(item.DbIndex))
            {
                using (var form = new OneValueForm()
                {
                    Text = "New name",
                    Value = item.Key
                })
                {
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        if (!string.IsNullOrWhiteSpace(form.Value))
                        {
                            if (item.Rename(form.Value))
                                node.Text = form.Value;
                        }
                    }
                }
            }
            else
            {
                Show(string.Format("Can not select db:{0}", item.DbIndex));
            }
        }

        private void RefreshDb(TreeNode node)
        {
            if (node == null) return;
            Task.Factory.StartNew(new Action<object>(_ => { UpdateDbInfo((TreeNode)_); }), node);
            ClearChildNode(node);
            Task.Factory.StartNew(new Action<object>(_ => { ShowKeys((TreeNode)_); }), node);
        }

        private void UpdateDbInfo(TreeNode node)
        {
            if (node == null) return;
            DatabaseItem item = node.Tag as DatabaseItem;
            if (item == null) return;
            item.UpdateDbInfo();
            string text = string.Format("{0}({1})", item.DbName, item.DbInfo);
            ShowStatusKey(item.DbName);
            SetNodeText(node, text);
        }

        private void RemoveDb(TreeNode node)
        {
            DeleteAlert(yes =>
            {
                if (!yes) return;
                if (node == null) return;
                ItemBase item = node.Tag as ItemBase;
                if (item.Connection.Execute(new RedisCommand(Command.FLUSHDB)).Read<bool>(Readers.IsOK))
                {
                    RemoveNode(node);
                }
                else
                {
                    Show("Clear database fail. (FLUSHDB)");
                }
            });
        }

        private void AddKeyValue(TreeNode node)
        {
            try
            {
                if (node == null) return;
                DatabaseItem item = node.Tag as DatabaseItem;
                if (item == null) return;
                using (AddForm form = new AddForm())
                {
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        string name = form.AddName;
                        object val = form.ByteData == null ? (object)form.AddValue : (object)form.ByteData;
                        if (item.Connection.Execute(new RedisCommand(Command.SET, name, val)).Read<bool>(Readers.IsOK))
                        {
                            UpdateDbInfo(node);
                            AddNode(node, CreateNode(item, name), true);
                        }
                        else
                        {
                            Show("Set {0} fail", name);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Show(ex.Message);
            }
        }

        private void RefreshConnection(TreeNode node)
        {
            ClearChildNode(node);
            Task.Factory.StartNew(new Action<object>(_ => { ShowDbInfo((TreeNode)_); }), node);
        }

        private void RemoveConnection(TreeNode node)
        {
            DeleteAlert(yes =>
            {
                if (!yes) return;
                if (node == null) return;
                ConnectionItem item = node.Tag as ConnectionItem;
                ConfigManager.Instance.Config.Connections.Remove(item.CntName);
                RemoveNode(node);
            });
        }

        private void UpdatePassword(TreeNode node)
        {
            if (node == null) return;
            ConnectionItem item = node.Tag as ConnectionItem;
            using (var form = new OneValueForm() { Text = "Setup a new password", EnablePassword = true })
            {
                if (!string.IsNullOrWhiteSpace(item.Connection.Password))
                {
                    form.Value = item.Connection.Password.Trim();
                }
                if (form.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    item.Connection.Password = form.Value.Trim();
                }
            }
        }

        private void UpdateDescription(TreeNode node)
        {
            if (node == null) return;
            ConnectionItem item = node.Tag as ConnectionItem;
            using (var form = new OneValueForm() { Text = "Write some comments", Value = item.Connection.Description })
            {
                if (form.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    item.Connection.Description = form.Value.Trim();
                    if (!string.IsNullOrEmpty(item.Connection.Description))
                    {
                        string desc = GetString(item.Connection.Description, MAX_STRING_LENGTH, true);
                        node.Text = string.Format("{0} ({1})", item.CntName, desc);
                        //node.ToolTipText = item.Connection.Description;
                    }
                }
            }
        }

        private void RefreshKey(TreeNode node)
        {
            Task.Factory.StartNew(new Action<object>(_ => { ShowValue((TreeNode)_); }), node);
        }

        private void RemoveKey(TreeNode node)
        {
            DeleteAlert(yes =>
            {
                if (!yes) return;
                if (node == null) return;
                KeyItem item = node.Tag as KeyItem;
                if (item.Connection.Select(item.DbIndex))
                {
                    if (item.Connection.Execute(new RedisCommand(Command.DEL, item.Key)).Read<int>(Readers.ReadAsInt) > 0)
                    {
                        UpdateDbInfo(node.Parent);
                        RemoveNode(node);
                    }
                    else
                    {
                        Show("Delete key fail.");
                    }
                }
                else
                {
                    Show(string.Format("Can not select db:{0}", item.DbIndex));
                }
            });
        }

        private void RemoveNode(TreeNode node)
        {
            if (node == null) return;
            Action set = () =>
            {
                if (node.Parent == null) tvDataTree.Nodes.Remove(node);
                else node.Parent.Nodes.Remove(node);
            };
            if (InvokeRequired) Invoke(set);
            else set();
        }

        private void DeleteAlert(Action<bool> deleteAction, string alert = DeleteAlertMsg)
        {
            if (MessageBox.Show(alert, "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                == System.Windows.Forms.DialogResult.Yes)
            {
                if (deleteAction != null) deleteAction(true);
            }
            else
            {
                if (deleteAction != null) deleteAction(false);
            }
        }

        private void tvDataTree_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    LoadNodeChilds(tvDataTree.SelectedNode, true);
                }
                else if (e.KeyCode == Keys.F5)
                {
                    RefreshNode(tvDataTree.SelectedNode);
                }
            }
            catch (Exception ex)
            {
                Show(ex.Message);
            }
        }

        public void RefreshNode(TreeNode node)
        {
            if (node.Tag is KeyItem)
            {
                RefreshKey(node);
            }
            else if (node.Tag is DatabaseItem)
            {
                RefreshDb(node);
            }
            else if (node.Tag is ConnectionItem)
            {
                RefreshConnection(node);
            }
        }

        private void tvDataTree_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnShowAllText_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => ShowAppCacheValue(Int32.MaxValue));
        }

        private void btnClearText_Click(object sender, EventArgs e)
        {
            txtValue.Text = string.Empty;
        }

        private void txtValue_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Control && e.KeyCode == Keys.A)
                {
                    txtValue.SelectAll();
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
