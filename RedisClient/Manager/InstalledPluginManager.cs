using Gnllk.RControl;
using Gnllk.RedisClient.Plugin;
using Gnllk.RedisClient.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Gnllk.RedisClient.Manager
{
    public interface IPluginManager
    {
        Config AppConfig { get; set; }

        ICollection<IPluginItem> Plugins { get; }

        IPluginItem DefaultPlugin { get; }

        void ReloadPlugins();

        void DisposePlugins();

        void InstallPlugin(string pluginZipFileName);

        void RemovePlugin(string pluginDirectoryName);

        void Initialize(PluginsManageForm form);

        event Action<ICollection<IPluginItem>> OnPluginsInitialized;
    }

    public class InstalledPluginManager : ManagerBase<InstalledPluginManager>, IPluginManager
    {
        private DataTable _pluginsTable;

        private PluginsManageForm _managerForm;

        public const string DefaultPluginTypeName = "ShowAsText";

        private ICollection<IPluginItem> _plugins;

        public ICollection<IPluginItem> Plugins
        {
            get
            {
                if (_plugins == null)
                {
                    lock (this)
                    {
                        if (_plugins == null)
                        {
                            _plugins = PluginLoader.Instance.LoadPlugins().Select(
                                item => new PluginItem(item)).ToArray();

                            InitializePlugins(_plugins);
                            OnPluginsInitialized?.Invoke(_plugins);
                        }
                    }
                }
                return _plugins;
            }
        }

        public IPluginItem DefaultPlugin { get; protected set; }

        public Config AppConfig { get; set; }

        public event Action<ICollection<IPluginItem>> OnPluginsInitialized;

        public void ReloadPlugins()
        {
            lock (this)
            {
                _plugins = null;
            }
        }

        public void DisposePlugins()
        {
            if (_plugins == null) return;
            foreach (var p in _plugins)
            {
                try
                {
                    p.Plugin.OnAppClosing();
                }
                catch { }
                try
                {
                    AppConfig.PluginsConfigString[p.GetType().Name] = p.Plugin.GetConfig();
                }
                catch { }
                p.Plugin.Dispose();
            }
            _plugins = null;
        }

        public void InstallPlugin(string pluginZipFileName)
        {
            throw new NotImplementedException();
        }

        public void RemovePlugin(string pluginDirectoryName)
        {
            throw new NotImplementedException();
        }

        protected virtual void InitializePlugins(ICollection<IPluginItem> plugins)
        {
            foreach (var item in plugins)
            {
                if (item.Plugin is Control)
                {
                    (item.Plugin as Control).Dock = DockStyle.Fill;
                }
                var pluginTypeName = item.Plugin.GetType().Name;
                if (pluginTypeName == DefaultPluginTypeName)
                {
                    DefaultPlugin = item;
                }
                var pluginConfigString = string.Empty;
                if (AppConfig != null && AppConfig.PluginsConfigString.ContainsKey(pluginTypeName))
                {
                    pluginConfigString = AppConfig.PluginsConfigString[pluginTypeName];
                }
                try
                {
                    item.Plugin.OnSetConfig(pluginConfigString);
                }
                catch { }
            }
            if (DefaultPlugin == null && _plugins.Count > 0)
            {
                DefaultPlugin = _plugins.First();
            }
        }

        public void Initialize(PluginsManageForm form)
        {
            if (form == null)
                throw new ArgumentNullException("form", "Plugins Manager Form cannot be null.");

            _managerForm = form;
            form.OnRowEnter += Form_OnRowEnter;
            form.OnInstallClick += Form_OnInstallClick;
            BindData(form);
        }

        private void Form_OnInstallClick(object arg1, EventArgs arg2)
        {
            throw new NotImplementedException();
        }

        private void Form_OnRowEnter(object arg1, DataGridViewCellEventArgs arg2)
        {
            var dataGridView = arg1 as DataGridView;
            if (dataGridView == null || _managerForm == null) return;
            if (Plugins.Count <= arg2.RowIndex) return;

            var data = Plugins.ElementAt(arg2.RowIndex);
            _managerForm.LabName.Text = data.Name;
            _managerForm.LabAuthor.Text = data.Author;
            _managerForm.LabDescription.Text = data.Description;
            _managerForm.LabVersion.Text = data.Version.ToString();
            _managerForm.LabDate.Text = data.CreateDate.ToString();
            _managerForm.InstallButtonVisible = false;
        }

        private void BindData(PluginsManageForm form)
        {
            _pluginsTable = new DataTable();
            _pluginsTable.Columns.Add("Icon", typeof(Icon));
            _pluginsTable.Columns.Add("Name", typeof(String));
            _pluginsTable.Columns.Add("State", typeof(String));
            foreach (var item in Plugins)
            {
                _pluginsTable.Rows.Add(item.Icon, item.Name, Resources.Installed);
            }
            form.PluginsListView.DataSource = _pluginsTable;
        }
    }
}
