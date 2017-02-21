using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using System.IO;
using RClient;
using Gnllk.RedisClient.Manager;
using Gnllk.RControl;
using Gnllk.RedisClient.Common;

namespace Gnllk.RedisClient
{
    public partial class FormMain : Form
    {
        #region Properties

        public readonly string[] EncodingName = new string[] { "gb2312", "utf-16", "unicodeFFFE", "Windows-1252", "x-mac-korean", "x-mac-chinesesimp", "utf-32", "utf-32BE", "us-ascii", "x-cp20936", "x-cp20949", "iso-8859-1", "iso-8859-8", "iso-8859-8-i", "iso-2022-jp", "csISO2022JP", "iso-2022-kr", "x-cp50227", "euc-jp", "EUC-CN", "euc-kr", "hz-gb-2312", "GB18030", "x-iscii-de", "x-iscii-be", "x-iscii-ta", "x-iscii-te", "x-iscii-as", "x-iscii-or", "x-iscii-ka", "x-iscii-ma", "x-iscii-gu", "x-iscii-pa", "utf-7", "utf-8" };

        public const string DefaultEncodingName = "utf-8";

        public const string DeleteAlertMsg = "Are you sure to delete?";

        public const string DeleteAlertMsgFmt = "Are you sure to delete {0}?";

        public const string KeyspaceSetion = "# Keyspace";

        public const int MAX_STRING_LENGTH = 100;

        private static object classLock = new object();

        private object GetValueLock = new object();

        #endregion Properties

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

        private void InitEncoding()
        {
            cbbEncoding.DataSource = EncodingName;
            cbbEncoding.SelectedItem = string.IsNullOrWhiteSpace(ConfigManager.Instance.Config.Encoding)
                || !EncodingName.Contains(ConfigManager.Instance.Config.Encoding)
                ? DefaultEncodingName : ConfigManager.Instance.Config.Encoding;
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
            if (node is KeyNode)
            {
                Task.Factory.StartNew(new Action<object>(_ => { ShowValue((KeyNode)_); }), node);
            }
            else if (node is DbNode)
            {
                Task.Factory.StartNew(new Action<object>(_ => { ShowKeys((DbNode)_); }), node);
            }
            else if (node is ConnectionNode)
            {
                Task.Factory.StartNew(new Action<object>(_ => { ShowDbs((ConnectionNode)_); }), node);
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

        private void AddConnectionToRoot(string name, IRedisConnection cont)
        {
            if (!string.IsNullOrWhiteSpace(name) && cont != null)
            {
                tvDataTree.Nodes.Add(CreateConnectionNode(name, cont));
            }
        }

        private void ShowConnections(IDictionary<string, RedisConnection> connections)
        {
            foreach (var item in connections)
            {
                AddConnectionToRoot(item.Key, item.Value);
            }
        }

        private void ShowDbs(ConnectionNode node, bool expand = true)
        {
            if (node == null || node.ConnectionItem == null || node.ConnectionItem.Loading) return;
            var item = node.ConnectionItem;
            if (item.Loading) return;
            try
            {
                item.Loading = true;
                SetPrograss(10);
                ShowStatusKey(item.ConnectionName);
                var cont = item.Connection;
                ShowStatusInfo("read database infomation from {0}", cont.EndPoint);
                AuthIfHasPwd(cont);

                var info = cont.Execute(new RedisCommand(Command.INFO)).Read<Info>(InfoReader.ReadAsInfo);
                var keys = info[KeyspaceSetion];
                node.ChildData = keys;
                SetPrograss(50);
                if (keys != null && keys.Any())
                {
                    double stepValue = 50;
                    double step = (double)stepValue / (double)keys.Count;
                    foreach (var db in keys)
                    {
                        AddNode(node, CreateDbNode(db.Key, db.Value, item));
                        SetPrograss((int)(stepValue += step));
                    }
                    NodeExpandOrNot(node, expand);
                }
                ShowStatusInfo("read {0} database", keys == null ? 0 : keys.Count);

                SetPrograss(100);
            }
            catch (Exception ex)
            {
                ShowStatusInfo(ex.Message);
                Show(ex.Message);
            }
            finally { item.Loading = false; }
        }

        private void ShowKeys(DbNode node, bool expand = true)
        {
            if (node == null || node.DatabaseItem == null || node.DatabaseItem.Loading) return;
            var item = node.DatabaseItem;
            try
            {
                item.Loading = true;
                SetPrograss(10);
                ShowStatusInfo("read database keys from {0}", item.Connection.EndPoint);
                if (item.Connection.Select(item.DbIndex))
                {
                    SetPrograss(25);
                    var keys = item.Connection.Execute(new RedisCommand(Command.KEYS, "*")).Read<List<string>>(Readers.ReadAsList);
                    ShowStatusKey(item.DbName);
                    node.ChildData = keys;
                    node.ViewData = keys;
                    SetPrograss(50);
                    AddKeysToTree(node, keys, 50);
                    NodeExpandOrNot(node, expand);
                }
                else
                {
                    Show(string.Format("Cannot select db:{0}", item.DbIndex));
                }
            }
            catch (Exception ex) { Show(ex.Message); }
            finally { item.Loading = false; }
        }

        private void AddKeysToTree(DbNode node, IEnumerable<string> keys, int prograssStart = 0)
        {
            int count = keys == null ? 0 : keys.Count();
            if (count > 0)
            {
                double stepValue = prograssStart;
                double step = stepValue / count;
                try
                {
                    if (InvokeRequired) Invoke(new Action(tvDataTree.BeginUpdate));
                    else tvDataTree.BeginUpdate();
                    foreach (var key in keys)
                    {
                        AddNode(node, CreateKeyNode(key, node.DatabaseItem));
                        var prograss = (int)(stepValue += step);

                        if (prograss % 10 != 0) continue;
                        ShowStatusInfo("read {0} keys", node.Nodes.Count);
                        SetPrograss(prograss);
                    }
                }
                finally
                {
                    if (InvokeRequired) Invoke(new Action(tvDataTree.EndUpdate));
                    else tvDataTree.EndUpdate();
                }
            }
            ShowStatusInfo("read {0} keys", count);
            SetPrograss(100);
        }

        private void ShowValue(KeyNode node, bool expand = true)
        {
            if (node == null || node.KeyItem == null) return;
            var item = node.KeyItem;
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

        public ConnectionNode CreateConnectionNode(string connectionName, IRedisConnection redisConnection)
        {
            string desc = StringHelper.GetString(redisConnection.Description, MAX_STRING_LENGTH, true);
            string text = string.IsNullOrWhiteSpace(desc) ?
                connectionName : string.Format("{0} ({1})", connectionName, desc);

            var result = new ConnectionNode(text);
            result.ImageIndex = ImageIndex.Connection;
            result.SelectedImageIndex = ImageIndex.Connection;
            result.ConnectionItem = new ConnectionItem(redisConnection, connectionName);
            result.ContextMenu = GetConnectionNodeMenu(result);
            return result;
        }

        private DbNode CreateDbNode(string dbName, string dbInfo, IConnectionItem connection)
        {
            var result = new DbNode(string.Format("{0}({1})", dbName, dbInfo));
            result.ImageIndex = ImageIndex.Db;
            result.SelectedImageIndex = ImageIndex.Db;
            result.DatabaseItem = new DatabaseItem(connection, dbName, dbInfo);
            result.ContextMenu = GetDbNodeMenu(result);
            return result;
        }

        private KeyNode CreateKeyNode(string keyName, IDatabaseItem db)
        {
            var result = new KeyNode(keyName);
            result.ImageIndex = ImageIndex.Key;
            result.SelectedImageIndex = ImageIndex.Key;
            result.ContextMenu = GetKeyNodeMenu(result);
            result.KeyItem = new KeyItem(db, keyName);
            return result;
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

        private void SetPrograss(int value)
        {
            Action<int> set = _ => { progressBar.Value = _; };
            if (InvokeRequired) Invoke(set, value);
            else set(value);
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
            try
            {
                if (node == null) return;
                Action set = () => { node.Nodes.Clear(); };
                if (InvokeRequired) Invoke(set);
                else set();
            }
            catch (Exception ex)
            {
                Show(ex.Message);
            }
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

        private ContextMenu GetDbNodeMenu(TreeNode node)
        {
            ContextMenu menu = new ContextMenu();
            menu.MenuItems.Add(new MenuItem("Refresh", new EventHandler(OnDbNodeMenuClick), Shortcut.F5) { Tag = node, ShowShortcut = true });
            menu.MenuItems.Add(new MenuItem("Search", new EventHandler(OnDbNodeMenuClick), Shortcut.CtrlF) { Tag = node, ShowShortcut = true });
            menu.MenuItems.Add(new MenuItem("Sort", new EventHandler(OnDbNodeMenuClick), Shortcut.CtrlS) { Tag = node, ShowShortcut = true });
            menu.MenuItems.Add(new MenuItem("Add key value", new EventHandler(OnDbNodeMenuClick), Shortcut.CtrlA) { Tag = node, ShowShortcut = true });
            menu.MenuItems.Add(new MenuItem("Delete", new EventHandler(OnDbNodeMenuClick), Shortcut.Del) { Tag = node, ShowShortcut = true });
            return menu;
        }

        private ContextMenu GetConnectionNodeMenu(TreeNode node)
        {
            ContextMenu menu = new ContextMenu();
            menu.MenuItems.Add(new MenuItem("Refresh", new EventHandler(OnConnectionNodeMenuClick), Shortcut.F5) { Tag = node, ShowShortcut = true });
            menu.MenuItems.Add(new MenuItem("Edit password", new EventHandler(OnConnectionNodeMenuClick), Shortcut.CtrlP) { Tag = node, ShowShortcut = true });
            menu.MenuItems.Add(new MenuItem("Edit description", new EventHandler(OnConnectionNodeMenuClick), Shortcut.CtrlD) { Tag = node, ShowShortcut = true });
            menu.MenuItems.Add(new MenuItem("Delete", new EventHandler(OnConnectionNodeMenuClick), Shortcut.Del) { Tag = node, ShowShortcut = true });
            return menu;
        }

        private ContextMenu GetKeyNodeMenu(TreeNode node)
        {
            ContextMenu menu = new ContextMenu();
            menu.MenuItems.Add(new MenuItem("Edit", new EventHandler(OnKeyNodeMenuClick), Shortcut.CtrlE) { Tag = node, ShowShortcut = true });
            menu.MenuItems.Add(new MenuItem("Rename", new EventHandler(OnKeyNodeMenuClick), Shortcut.CtrlR) { Tag = node, ShowShortcut = true });
            menu.MenuItems.Add(new MenuItem("Delete", new EventHandler(OnKeyNodeMenuClick), Shortcut.Del) { Tag = node, ShowShortcut = true });
            return menu;
        }

        private void AddKeyValueToDb(DbNode node)
        {
            try
            {
                if (node == null || node.DatabaseItem == null || node.DatabaseItem.Loading) return;
                var item = node.DatabaseItem;
                using (AddForm form = new AddForm(item.DbIndex))
                {
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        if (item.Connection.Select(form.DbIndex))
                        {
                            string name = form.AddName;
                            object val = form.ByteData == null ? (object)form.AddValue : (object)form.ByteData;
                            if (item.Connection.Execute(new RedisCommand(Command.SET, name, val)).Read<bool>(Readers.IsOK))
                            {
                                UpdateDbInfo(node);
                                node.ChildData.Add(name);
                                AddNode(node, CreateKeyNode(name, item), true);
                            }
                            else
                            {
                                Show("Set {0} fail", name);
                            }
                        }
                        else
                        {
                            Show("Cannot select db {0} fail", form.DbIndex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Show(ex.Message);
            }
        }

        private void UpdateValue(KeyNode node)
        {
            if (node == null || node.KeyItem == null) return;
            var item = node.KeyItem;
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

        private void SortKeys(DbNode node)
        {
            try
            {
                if (node == null || node.DatabaseItem == null || node.DatabaseItem.Loading) return;
                var keys = node.ViewData;
                if (keys != null && keys.Any())
                {
                    keys.Sort();
                    ClearChildNode(node);
                    node.ViewData.Sort();
                    AddKeysToTree(node, keys, 50);
                    NodeExpandOrNot(node, true);
                }
            }
            catch (Exception ex) { Show(ex.Message); }
        }

        private void SortKeysAsync(DbNode node)
        {
            Task.Factory.StartNew(new Action<object>(_ => { SortKeys((DbNode)_); }), node);
        }

        private void SearchKeysAsync(DbNode node)
        {
            if (node == null || node.DatabaseItem == null || node.DatabaseItem.Loading) return;
            using (var form = new OneValueForm() { Text = "Search" })
            {
                form.StartPosition = FormStartPosition.CenterScreen;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Task.Factory.StartNew(new Action<object>(_ => { SearchKeys((DbNode)_, form.Value.Trim()); }), node);
                }
            }
        }

        private void SearchKeys(DbNode node, string keyWord)
        {
            try
            {
                if (node == null || node.DatabaseItem == null || node.DatabaseItem.Loading) return;
                var originalKeys = node.ChildData;
                if (originalKeys == null || !originalKeys.Any() || string.IsNullOrWhiteSpace(keyWord)) return;

                ClearChildNode(node);
                var lowerCase = keyWord.ToLower();
                node.ViewData = originalKeys.FindAll(item => item.ToLower().Contains(lowerCase));
                if (node.ViewData == null) return;
                AddKeysToTree(node, node.ViewData, 0);
                NodeExpandOrNot(node, true);
            }
            catch (Exception ex) { Show(ex.Message); }
        }

        private void Rename(KeyNode node)
        {
            if (node == null || node.KeyItem == null) return;
            var item = node.KeyItem;
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

        private void RefreshDbAsync(DbNode node)
        {
            if (node == null || node.DatabaseItem == null || node.DatabaseItem.Loading) return;
            Task.Factory.StartNew(new Action<object>(_ => { UpdateDbInfo((DbNode)_); }), node);
            ClearChildNode(node);
            Task.Factory.StartNew(new Action<object>(_ => { ShowKeys((DbNode)_); }), node);
        }

        private void UpdateDbInfo(DbNode node)
        {
            try
            {
                if (node == null || node.DatabaseItem == null || node.DatabaseItem.Loading) return;
                var item = node.DatabaseItem;
                if (item == null) return;
                item.UpdateDbInfo();
                string text = string.Format("{0}({1})", item.DbName, item.DbInfo);
                ShowStatusKey(item.DbName);
                SetNodeText(node, text);
            }
            catch (Exception ex)
            {
                Show(ex.Message);
            }
        }

        private void RemoveDb(DbNode node)
        {
            if (node == null || node.DatabaseItem == null || node.DatabaseItem.Loading) return;
            DeleteAlert(yes =>
            {
                if (!yes) return;
                var item = node.DatabaseItem;
                if (item.Connection.Execute(new RedisCommand(Command.FLUSHDB)).Read<bool>(Readers.IsOK))
                {
                    RemoveNode(node);
                }
                else
                {
                    Show("Clear database fail. (FLUSHDB)");
                }
            }, string.Format(DeleteAlertMsgFmt, node.DatabaseItem.DbName));
        }

        private void RefreshConnectionAsync(ConnectionNode node)
        {
            if (node == null || node.ConnectionItem == null || node.ConnectionItem.Loading) return;
            ClearChildNode(node);
            Task.Factory.StartNew(new Action<object>(_ => { ShowDbs((ConnectionNode)_); }), node);
        }

        private void RemoveConnection(ConnectionNode node)
        {
            if (node == null || node.ConnectionItem == null || node.ConnectionItem.Loading) return;
            DeleteAlert(yes =>
            {
                if (!yes) return;
                var item = node.ConnectionItem;
                ConfigManager.Instance.Config.Connections.Remove(item.ConnectionName);
                RemoveNode(node);
            }, string.Format(DeleteAlertMsgFmt, node.ConnectionItem.ConnectionName));
        }

        private void RefreshKeyAsync(KeyNode node)
        {
            Task.Factory.StartNew(new Action<object>(_ => { ShowValue((KeyNode)_); }), node);
        }

        private void RemoveKey(KeyNode node)
        {
            if (node == null || node.KeyItem == null || node.KeyItem.Loading) return;
            DeleteAlert(yes =>
            {
                if (!yes) return;
                var item = node.KeyItem;
                if (item.Connection.Select(item.DbIndex))
                {
                    if (item.Connection.Execute(new RedisCommand(Command.DEL, item.Key)).Read<int>(Readers.ReadAsInt) > 0)
                    {
                        UpdateDbInfo(node.Parent as DbNode);
                        (node.Parent as DbNode).ChildData.Remove(item.Key);
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
            }, string.Format(DeleteAlertMsgFmt, node.KeyItem.Key));
        }

        public void RefreshNode(TreeNode node)
        {
            if (node is KeyNode)
            {
                RefreshKeyAsync(node as KeyNode);
            }
            else if (node is DbNode)
            {
                RefreshDbAsync(node as DbNode);
            }
            else if (node is ConnectionNode)
            {
                RefreshConnectionAsync(node as ConnectionNode);
            }
        }

        public void RemoveNodeIncludeData(TreeNode node)
        {
            if (node is KeyNode)
            {
                RemoveKey(node as KeyNode);
            }
            else if (node is DbNode)
            {
                RemoveDb(node as DbNode);
            }
            else if (node is ConnectionNode)
            {
                RemoveConnection(node as ConnectionNode);
            }
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

        private void UpdatePassword(ConnectionNode node)
        {
            if (node == null || node.ConnectionItem == null) return;
            var item = node.ConnectionItem;
            using (var form = new OneValueForm() { Text = "Setup a new password", EnablePassword = true })
            {
                if (!string.IsNullOrWhiteSpace(item.Connection.Password))
                {
                    form.Value = item.Connection.Password.Trim();
                }
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    item.Connection.Password = form.Value.Trim();
                }
            }
        }

        private void UpdateDescription(ConnectionNode node)
        {
            if (node == null || node.ConnectionItem == null) return;
            var item = node.ConnectionItem;
            using (var form = new OneValueForm() { Text = "Write some comments", Value = item.Connection.Description })
            {
                if (form.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    item.Connection.Description = form.Value.Trim();
                    if (!string.IsNullOrEmpty(item.Connection.Description))
                    {
                        string desc = StringHelper.GetString(item.Connection.Description, MAX_STRING_LENGTH, true);
                        node.Text = string.Format("{0} ({1})", item.ConnectionName, desc);
                        node.ToolTipText = item.Connection.Description;
                    }
                    else
                    {
                        node.Text = item.ConnectionName;
                    }
                }
            }
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

        #region UI Event

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
                ShowConnections(ConfigManager.Instance.Config.Connections);
            }
            catch (Exception ex)
            {
                Show("Initialize control fail: {0}", ex.Message);
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
                    AddConnectionToRoot(endPoint, cont);
                }
                else
                {
                    Show("Existed {0}", endPoint);
                }
            }
            catch (Exception ex)
            {
                Show(ex.Message);
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
                if (tree.SelectedNode is KeyNode)
                {
                    Task.Factory.StartNew(_ => ShowValue((KeyNode)_), tree.SelectedNode);
                }
            }
        }

        private void tvDataTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (e.Button != MouseButtons.Left) return;
                LoadNodeChilds(e.Node);
            }
            catch (Exception ex)
            {
                Show(ex.Message);
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

        private void OnDbNodeMenuClick(object sender, EventArgs e)
        {
            try
            {
                this.UseWaitCursor = true;
                MenuItem menu = sender as MenuItem;
                if (menu != null)
                {
                    DbNode node = menu.Tag as DbNode;
                    if (node != null)
                    {
                        if (menu.Shortcut == Shortcut.F5)
                        {
                            RefreshDbAsync(node);
                        }
                        else if (menu.Shortcut == Shortcut.Del)
                        {
                            RemoveDb(node);
                        }
                        else if (menu.Shortcut == Shortcut.CtrlA)
                        {
                            AddKeyValueToDb(node);
                        }
                        else if (menu.Shortcut == Shortcut.CtrlS)
                        {
                            SortKeysAsync(node);
                        }
                        else if (menu.Shortcut == Shortcut.CtrlF)
                        {
                            SearchKeysAsync(node);
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
                MenuItem menu = sender as MenuItem;
                if (menu != null)
                {
                    var node = menu.Tag as ConnectionNode;
                    if (node != null)
                    {
                        if (menu.Shortcut == Shortcut.F5)
                        {
                            RefreshConnectionAsync(node);
                        }
                        else if (menu.Shortcut == Shortcut.Del)
                        {
                            RemoveConnection(node);
                        }
                        else if (menu.Shortcut == Shortcut.CtrlP)
                        {
                            UpdatePassword(node);
                        }
                        else if (menu.Shortcut == Shortcut.CtrlD)
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
                    KeyNode node = item.Tag as KeyNode;
                    if (node != null)
                    {
                        if (item.Shortcut == Shortcut.Del)
                        {
                            RemoveKey(node);
                        }
                        else if (item.Shortcut == Shortcut.CtrlE)
                        {
                            UpdateValue(node);
                        }
                        else if (item.Shortcut == Shortcut.CtrlR)
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
                else if (e.Control && e.KeyCode == Keys.A)
                {
                    if (tvDataTree.SelectedNode is DbNode)
                        AddKeyValueToDb(tvDataTree.SelectedNode as DbNode);
                }
                else if (e.Control && e.KeyCode == Keys.E)
                {
                    if (tvDataTree.SelectedNode is KeyNode)
                        UpdateValue(tvDataTree.SelectedNode as KeyNode);
                }
                else if (e.Control && e.KeyCode == Keys.F)
                {
                    if (tvDataTree.SelectedNode is DbNode)
                        SearchKeysAsync(tvDataTree.SelectedNode as DbNode);
                }
                else if (e.Control && e.KeyCode == Keys.R)
                {
                    if (tvDataTree.SelectedNode is KeyNode)
                        Rename(tvDataTree.SelectedNode as KeyNode);
                }
                else if (e.Control && e.KeyCode == Keys.S)
                {
                    if (tvDataTree.SelectedNode is DbNode)
                        SortKeysAsync(tvDataTree.SelectedNode as DbNode);
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    RemoveNodeIncludeData(tvDataTree.SelectedNode);
                }
            }
            catch (Exception ex)
            {
                Show(ex.Message);
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

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] data = AppCache.Instance.CurrentGetData;
                if (data != null && data.Length > 0)
                {
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.Filter = "Text file (*.txt)|*.txt|Raw file (*.*)|*.*";
                    dialog.FileName = AppCache.Instance.CurrentKey ?? "Default1";
                    string ext = Path.GetExtension(dialog.FileName).ToLower();
                    if (string.IsNullOrWhiteSpace(ext))
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
                                using (FileStream file = new FileStream(dialog.FileName, FileMode.Create))
                                {
                                    var writer = new StreamWriter(file, AppCache.Instance.CurrentEncoding);
                                    writer.Write(txtValue.Text);
                                    writer.Flush();
                                }
                            }
                            else
                            {
                                using (FileStream file = new FileStream(dialog.FileName, FileMode.Create))
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

        #endregion UI Event
    }
}
