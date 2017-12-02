using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using System.IO;
using RClient;
using Gnllk.RedisClient.Manager;
using Gnllk.RControl;
using Gnllk.JCommon.Helper;
using Gnllk.RedisClient.Plugin;

namespace Gnllk.RedisClient
{
    public partial class FormMain : Form
    {
        #region Properties

        public const string DeleteAlertMsg = "Are you sure to delete?";

        public const string DeleteAlertMsgFmt = "Are you sure to delete {0}?";

        public const string KeyspaceSetion = "# Keyspace";

        public const int MAX_STRING_LENGTH = 100;

        public const int AutoDetected = 0;

        public IPluginManager PluginsManager = InstalledPluginManager.Instance;

        #endregion Properties

        public FormMain()
        {
            InitializeComponent();
            cbbShowAs.SelectedIndex = 0;
            try
            {
                tvDataTree.KeyUp += new KeyEventHandler(tvDataTree_KeyUp);
                var version = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);
                Text = string.Format("{0} (version: {1})", Text, version.ProductVersion);
                splitMain.SplitterDistance = (int)(Width / 2d);
                tvDataTree.Select();
            }
            catch (Exception ex)
            {
                Show("Initialize fail: {0}", ex.Message);
            }
        }

        private bool Check(ConnectionNode node)
        {
            return !(node == null || node.ConnectionItem == null || node.ConnectionItem.Loading);
        }

        private bool Check(DbNode node)
        {
            return !(node == null || node.DatabaseItem == null || node.DatabaseItem.Loading);
        }

        private bool Check(KeyNode node)
        {
            return !(node == null || node.KeyItem == null || node.KeyItem.Loading);
        }

        private void AddPluginNameToShowAsCombobox(string pluginName)
        {
            Action<object> set = _ => { cbbShowAs.Items.Add(_); };
            if (InvokeRequired) Invoke(set, pluginName);
            else set(pluginName);
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

        private void ShowAs(IKeyItem item)
        {
            try
            {
                if (item == null || string.IsNullOrWhiteSpace(item.Key)) return;
                if (!PluginsManager.Plugins.Any()) return;

                var showData = ConvertToShowData(item);

                if (InvokeRequired) Invoke(new Action<ShowData>(ShowPluginData), showData);
                else ShowPluginData(showData);
            }
            catch (Exception ex)
            {
                Show(ex.Message);
            }
        }

        private ShowData ConvertToShowData(IKeyItem keyItem)
        {
            if (keyItem == null) return null;

            return new ShowData()
            {
                Key = keyItem.Key,
                Value = keyItem.Value,
                DbName = keyItem.DbName,
                EndPoint = keyItem.Connection.EndPoint.ToString()
            };
        }

        private void ShowPluginData(ShowData data)
        {
            var showAsName = cbbShowAs.SelectedItem.ToString();
            var pluginItem = PluginsManager.DefaultPlugin;
            foreach (var p in PluginsManager.Plugins)
            {
                if (cbbShowAs.SelectedIndex == AutoDetected)
                {
                    // Auto
                    if (p.Plugin.ShouldShowAs(data))
                    {
                        pluginItem = p;
                        break;
                    }
                }
                else
                {
                    // Manual
                    if (showAsName.Equals(p.Plugin.GetName()))
                    {
                        pluginItem = p;
                        break;
                    }
                }
            }
            if (pluginItem != null)
            {
                ShowPluginControl(pluginItem);
                SafetyCall(() => { pluginItem.Plugin.OnShowAs(data); });
            }
        }

        private void ShowPluginControl(IPluginItem pluginItem)
        {
            if (pluginItem == null) return;
            if (!(pluginItem.Plugin is Control)) return;

            var newControl = (Control)pluginItem.Plugin;
            if (pluginViewBox.Controls.Count == 0)
            {
                pluginViewBox.Controls.Add(newControl);
                return;
            }

            var oldControl = pluginViewBox.Controls[0];
            if (!newControl.Equals(oldControl))
            {
                pluginViewBox.Controls.Clear();
                pluginViewBox.Controls.Add(newControl);
                var oldPlugin = oldControl as IShowInPlugin;
                if (oldPlugin != null) SafetyCall(oldPlugin.OnBlur);
            }
        }

        private void SafetyCall(Action action)
        {
            if (action == null) return;
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Show(ex.Message);
            }
        }

        private void SafetyCall(Action<object> action, object arg)
        {
            if (action == null) return;
            try
            {
                action(arg);
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
            if (!Check(node)) return;
            var item = node.ConnectionItem;
            try
            {
                item.Loading = true;
                SetPrograss(10);
                ShowStatusKey(item.ConnectionName);
                var cont = item.Connection;
                ShowStatusInfo("read database infomation from {0}", cont.EndPoint);
                AuthIfHasPwd(cont);

                var info = cont.Execute(new RedisCommand(Command.INFO)).Read(InfoReader.ReadAsInfo);
                var keys = info[KeyspaceSetion];
                node.DbNames = keys;

                SetPrograss(50);
                var count = keys?.Count ?? 0;
                if (count > 0)
                {
                    double prograss = 50;
                    double step = prograss / count;
                    foreach (var db in keys)
                    {
                        var newConnectionItem = new ConnectionItem(item.Connection.Copy(), item.ConnectionName);
                        //TODO: remove to db click event
                        newConnectionItem.Connection.Login();
                        AppCache.Instance.DbConnections.Add(newConnectionItem.Connection);
                        AddNode(node, CreateDbNode(db.Key, db.Value, newConnectionItem));
                        SetPrograss((int)(prograss += step));
                    }
                    NodeExpandOrNot(node, expand);
                }
                ShowStatusInfo("read {0} database", count);
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
            if (!Check(node)) return;
            var item = node.DatabaseItem;
            try
            {
                item.Loading = true;
                SetPrograss(10);
                ShowStatusInfo("read database keys from {0}", item.Connection.EndPoint);
                if (item.Connection.Select(item.DbIndex))
                {
                    SetPrograss(25);
                    var keys = item.Connection.Execute(new RedisCommand(Command.KEYS, "*")).Read(Readers.ReadAsList);
                    ShowStatusKey(item.DbName);
                    node.Keys = keys;
                    node.ViewKeys = keys;
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

        private void ShowValue(KeyNode node, bool expand = true)
        {
            if (!Check(node)) return;
            var item = node.KeyItem;
            try
            {
                item.Loading = true;
                ShowStatusKey(item.Key);
                SetPrograss(10);
                var cont = item.Connection;
                if (cont.Select(item.DbIndex))
                {
                    SetPrograss(50);
                    AppCache.Instance.Item = item;
                    ShowAs(item);
                    SetPrograss(100);
                }
                else
                {
                    Show(string.Format("Cannot select db:{0}", item.DbIndex));
                }
            }
            catch (Exception ex) { ShowStatusInfo(ex.Message); }
            finally { item.Loading = false; }
        }

        private void AddKeysToTree(DbNode node, IEnumerable<string> keys, int prograssStart = 0)
        {
            int count = keys?.Count() ?? 0;
            if (count > 0)
            {
                try
                {
                    if (InvokeRequired) Invoke(new Action(tvDataTree.BeginUpdate));
                    else tvDataTree.BeginUpdate();

                    double stepValue = prograssStart;
                    double step = stepValue / count;
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

        private ConnectionNode CreateConnectionNode(string connectionName, IRedisConnection redisConnection)
        {
            var desc = StringHelper.GetString(redisConnection.Description, MAX_STRING_LENGTH, true);
            var text = string.IsNullOrWhiteSpace(desc) ?
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
                var authInfo = cont.Execute(new RedisCommand(Command.AUTH, cont.Password)).Read(Readers.ReadAsString);
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
            if (node == null || node.Nodes == null) return;
            Action set = () => { node.Nodes.Clear(); };
            if (InvokeRequired) Invoke(set);
            else set();
        }

        private void AddNode(TreeNode parent, TreeNode child, bool insertToHead = false)
        {
            if (parent == null || child == null) return;

            Action<TreeNode> add = _ =>
            {
                if (insertToHead) parent.Nodes.Insert(0, _);
                else parent.Nodes.Add(_);
            };
            if (InvokeRequired) Invoke(add, child);
            else add(child);
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
            menu.MenuItems.Add(new MenuItem("Paste", new EventHandler(OnDbNodeMenuClick), Shortcut.CtrlV) { Tag = node, ShowShortcut = true });
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
            menu.MenuItems.Add(new MenuItem("Copy", new EventHandler(OnKeyNodeMenuClick), Shortcut.CtrlC) { Tag = node, ShowShortcut = true });
            menu.MenuItems.Add(new MenuItem("Rename", new EventHandler(OnKeyNodeMenuClick), Shortcut.CtrlR) { Tag = node, ShowShortcut = true });
            menu.MenuItems.Add(new MenuItem("Delete", new EventHandler(OnKeyNodeMenuClick), Shortcut.Del) { Tag = node, ShowShortcut = true });
            return menu;
        }

        private void AddKeyValueToDb(DbNode node)
        {
            if (!Check(node)) return;
            var item = node.DatabaseItem;
            try
            {
                item.Loading = true;
                using (AddForm form = new AddForm(item.DbIndex))
                {
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        if (item.Connection.Select(form.DbIndex))
                        {
                            var name = form.AddName;
                            var val = form.FileData ?? form.AddValueBytes;
                            if (item.Connection.Execute(new RedisCommand(Command.SET, name, val)).Read(Readers.IsOK))
                            {
                                UpdateDbInfo(node);
                                node.Keys.Add(name);
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
            catch (Exception ex) { Show(ex.Message); }
            finally { item.Loading = false; }
        }

        private void PasteToDb(DbNode node)
        {
            if (!Check(node)) return;
            var item = node.DatabaseItem;
            try
            {
                item.Loading = true;

                var copyText = Clipboard.GetText();
                if (string.IsNullOrWhiteSpace(copyText)) return;
                if (!copyText.StartsWith("{") || !copyText.EndsWith("}")) return;

                var copyData = JsonHelper.FromJson<ClipboardKeyValue>(copyText);
                if (copyData == null) return;

                using (var form = new OneValueForm())
                {
                    form.Value = string.Format("{0}_Copy", copyData.Key);
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        if (item.Connection.Select(item.DbIndex))
                        {
                            var name = form.Value;
                            var val = copyData.Value;
                            if (item.Connection.Execute(new RedisCommand(Command.EXISTS, name)).Read(Readers.IsTrue))
                            {
                                if (MessageBox.Show(string.Format("Exists {0}, do you want to override it?",
                                    name), "Exists", MessageBoxButtons.YesNo) == DialogResult.No)
                                {
                                    return;
                                }
                            }
                            if (item.Connection.Execute(new RedisCommand(Command.SET, name, val)).Read(Readers.IsOK))
                            {
                                UpdateDbInfo(node);
                                node.Keys.Add(name);
                                AddNode(node, CreateKeyNode(name, item), true);
                            }
                            else
                            {
                                Show("Set {0} fail", name);
                            }
                        }
                        else
                        {
                            Show("Cannot select db {0} fail", item.DbIndex);
                        }
                    }
                }
            }
            catch (Exception ex) { Show(ex.Message); }
            finally { item.Loading = false; }
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
                    EditValue = item.Value
                })
                {
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        if (form.FileData != null)
                        {
                            item.SetValue(form.FileData);
                        }
                        else
                        {
                            item.SetValue(form.EditValue);
                        }
                        ShowValue(node);
                    }
                }
            }
            else
            {
                Show(string.Format("Cannot select db:{0}", item.DbIndex));
            }
        }

        private void SortKeys(DbNode node)
        {
            if (!Check(node)) return;
            var item = node.DatabaseItem;
            try
            {
                item.Loading = true;
                var keys = node.ViewKeys;
                if (keys != null && keys.Any())
                {
                    keys.Sort();
                    ClearChildNode(node);
                    node.ViewKeys.Sort();
                    AddKeysToTree(node, keys, 50);
                    NodeExpandOrNot(node, true);
                }
            }
            catch (Exception ex) { Show(ex.Message); }
            finally { item.Loading = false; }
        }

        private void SortKeysAsync(DbNode node)
        {
            if (!Check(node)) return;
            Task.Factory.StartNew(new Action(() => { SortKeys(node); }));
        }

        private void SearchKeysAsync(DbNode node)
        {
            if (!Check(node)) return;
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
            if (!Check(node) || string.IsNullOrWhiteSpace(keyWord)) return;
            var item = node.DatabaseItem;
            try
            {
                item.Loading = true;
                var originalKeys = node.Keys;
                if (originalKeys == null || !originalKeys.Any()) return;

                ClearChildNode(node);
                var lowerCase = keyWord.ToLower();
                node.ViewKeys = originalKeys.FindAll(_ => _.ToLower().Contains(lowerCase));
                if (node.ViewKeys == null) return;
                AddKeysToTree(node, node.ViewKeys, 0);
                NodeExpandOrNot(node, true);
            }
            catch (Exception ex) { Show(ex.Message); }
            finally { item.Loading = false; }
        }

        private void Rename(KeyNode node)
        {
            if (!Check(node)) return;
            var item = node.KeyItem;
            try
            {
                item.Loading = true;
                if (!item.Connection.Select(item.DbIndex))
                {
                    Show(string.Format("Cannot select db:{0}", item.DbIndex));
                    return;
                }
                using (var form = new OneValueForm())
                {
                    form.Text = "New name";
                    form.Value = item.Key;
                    if (form.ShowDialog(this) != DialogResult.OK) return;

                    if (!string.IsNullOrWhiteSpace(form.Value) && item.Rename(form.Value))
                        node.Text = form.Value;
                }
            }
            catch (Exception ex) { Show(ex.Message); }
            finally { item.Loading = false; }
        }

        private void RefreshDbAsync(DbNode node)
        {
            if (!Check(node)) return;
            Task.Factory.StartNew(new Action<object>(_ =>
            {
                UpdateDbInfo((DbNode)_);
                ClearChildNode((DbNode)_);
                ShowKeys((DbNode)_);
            }), node);
        }

        private void UpdateDbInfo(DbNode node)
        {
            if (!Check(node)) return;
            var item = node.DatabaseItem;
            try
            {
                item.Loading = true;
                item.UpdateDbInfo();
                ShowStatusKey(item.DbName);
                SetNodeText(node, string.Format("{0}({1})", item.DbName, item.DbInfo));
            }
            catch (Exception ex) { Show(ex.Message); }
            finally { item.Loading = false; }
        }

        private void RemoveDb(DbNode node)
        {
            if (!Check(node)) return;
            DeleteAlert(yes =>
            {
                if (!yes) return;
                var item = node.DatabaseItem;
                if (item.Connection.Execute(new RedisCommand(Command.FLUSHDB)).Read(Readers.IsOK))
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
            if (!Check(node)) return;
            ClearChildNode(node);
            Task.Factory.StartNew(new Action(() => { ShowDbs(node); }));
        }

        private void RemoveConnection(ConnectionNode node)
        {
            if (!Check(node)) return;
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
            Task.Factory.StartNew(new Action(() => { ShowValue(node); }));
        }

        private void RemoveKey(KeyNode node)
        {
            if (!Check(node)) return;
            DeleteAlert(yes =>
            {
                if (!yes) return;
                var item = node.KeyItem;
                if (!item.Connection.Select(item.DbIndex))
                {
                    Show(string.Format("Cannot select db:{0}", item.DbIndex));
                    return;
                }
                if (item.Connection.Execute(new RedisCommand(Command.DEL, item.Key)).Read(Readers.ReadAsInt) > 0)
                {
                    UpdateDbInfo(node.Parent as DbNode);
                    (node.Parent as DbNode).Keys.Remove(item.Key);
                    RemoveNode(node);
                }
                else
                {
                    Show("Delete key fail.");
                }
            }, string.Format(DeleteAlertMsgFmt, node.KeyItem.Key));
        }

        private void CopyToClipboard(KeyNode node)
        {
            if (!Check(node)) return;
            var item = node.KeyItem;
            try
            {
                item.Loading = true;
                var cont = item.Connection;
                if (cont.Select(item.DbIndex))
                {
                    var copyData = new ClipboardKeyValue(item.Key, item.Value);
                    var copyText = JsonHelper.ToJson(copyData);
                    Clipboard.SetText(copyText);
                    ShowStatusInfo("Copied");
                }
                else
                {
                    Show(string.Format("Cannot select db:{0}", item.DbIndex));
                }
            }
            catch (Exception ex) { ShowStatusInfo(ex.Message); }
            finally { item.Loading = false; }
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
                if (form.ShowDialog(this) == DialogResult.OK)
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
            if (MessageBox.Show(alert, "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                deleteAction?.Invoke(true);
            }
            else
            {
                deleteAction?.Invoke(false);
            }
        }

        #region UI Event

        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigManager.Instance.LoadConfig();
                PluginsManager.AppConfig = ConfigManager.Instance.Config;
                PluginsManager.OnPluginsInitialized += PluginsManager_OnPluginsInitialized;
            }
            catch (Exception ex)
            {
                Show("Load configuration fail: {0}", ex.Message);
            }
            try
            {
                ShowConnections(ConfigManager.Instance.Config.Connections);
            }
            catch (Exception ex)
            {
                Show("Initialize control fail: {0}", ex.Message);
            }
        }

        private void PluginsManager_OnPluginsInitialized(ICollection<IPluginItem> plugins)
        {
            if (plugins == null || plugins.Count == 0) return;
            foreach (var item in plugins)
            {
                AddPluginNameToShowAsCombobox(item.Plugin.GetName());
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

        private void OnDbNodeMenuClick(object sender, EventArgs e)
        {
            try
            {
                UseWaitCursor = true;
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
                        else if (menu.Shortcut == Shortcut.CtrlF)
                        {
                            SearchKeysAsync(node);
                        }
                        else if (menu.Shortcut == Shortcut.CtrlS)
                        {
                            SortKeysAsync(node);
                        }
                        else if (menu.Shortcut == Shortcut.CtrlV)
                        {
                            PasteToDb(node);
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
                UseWaitCursor = false;
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
                        else if (item.Shortcut == Shortcut.CtrlC)
                        {
                            CopyToClipboard(node);
                        }
                        else if (item.Shortcut == Shortcut.CtrlE)
                        {
                            UpdateValue(node);
                        }
                        else if (item.Shortcut == Shortcut.CtrlR)
                        {
                            Rename(node);
                        }
                        else if (item.Shortcut == Shortcut.CtrlV)
                        {
                            PasteToDb(node.Parent as DbNode);
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
                else if (e.Control && e.KeyCode == Keys.C)
                {
                    if (tvDataTree.SelectedNode is KeyNode)
                        CopyToClipboard(tvDataTree.SelectedNode as KeyNode);
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
                else if (e.Control && e.KeyCode == Keys.V)
                {
                    if (tvDataTree.SelectedNode is DbNode)
                        PasteToDb(tvDataTree.SelectedNode as DbNode);
                    else if (tvDataTree.SelectedNode is KeyNode)
                        PasteToDb(tvDataTree.SelectedNode.Parent as DbNode);
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

        private void btnExportFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (AppCache.Instance.Item == null
                    || AppCache.Instance.Item.Value == null
                    || AppCache.Instance.Item.Value.Length == 0)
                {
                    ShowStatusInfo("No data");
                    return;
                }
                var data = AppCache.Instance.Item.Value;
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "Text file (*.txt)|*.txt|Raw file (*.*)|*.*";
                dialog.FileName = AppCache.Instance.Item.Key ?? "Default1";
                string ext = Path.GetExtension(dialog.FileName).ToLower();
                if (string.IsNullOrWhiteSpace(ext))
                {
                    dialog.FilterIndex = 1;
                }
                else
                {
                    dialog.FilterIndex = 2;
                }
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrWhiteSpace(dialog.FileName))
                    {
                        using (FileStream file = new FileStream(dialog.FileName, FileMode.Create))
                        {
                            file.Write(data, 0, data.Length);
                            file.Flush();
                        }
                        ShowStatusInfo("File:{0}", dialog.FileName);
                    }
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
                PluginsManager.DisposePlugins();
                ConfigManager.Instance.SaveConfig();
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
            try
            {
                ShowStatusInfo("freeing db connection...");
                AppCache.Instance.DbConnections.ForEach(item => item.Dispose());
                AppCache.Instance.DbConnections.Clear();
            }
            catch (Exception ex)
            {
                Show("free db connection error: {0}", ex.Message);
            }
        }

        private void cbbShowAs_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Task.Factory.StartNew(() =>
                {
                    ShowAs(AppCache.Instance.Item);
                });
            }
            catch (Exception ex)
            {
                Show("Error: {0}", ex.Message);
            }
        }

        private void btnAboutMe_Click(object sender, EventArgs e)
        {
            SafetyCall(() =>
            {
                using (var form = new AboutMeForm())
                {
                    form.ShowDialog();
                }
            });
        }

        private void btnPluginsManage_Click(object sender, EventArgs e)
        {
            SafetyCall(() =>
            {
                using (var form = new PluginsManageForm())
                {
                    PluginsManager.Initialize(form);
                    form.ShowDialog();
                }
            });
        }

        #endregion UI Event
    }
}
