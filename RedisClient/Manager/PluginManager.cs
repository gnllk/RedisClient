using Gnllk.RControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Gnllk.RedisClient.Manager
{
    public class PluginItem
    {
        public PluginItem(IPluginManager plugin, string path)
        {
            Plugin = plugin;
            Path = path;
        }

        public IPluginManager Plugin { get; }

        public string Path { get; }

        public bool Enable { get; set; }
    }

    public interface IPluginManager
    {
        Config AppConfig { get; set; }

        ICollection<IShowAsPlugin> Plugins { get; }

        IShowAsPlugin DefaultPlugin { get; }

        void ReloadPlugins();

        void DisposePlugins();

        void InstallPlugin(string pluginZipFileName);

        void RemovePlugin(string pluginDirectoryName);

        event Action<ICollection<IShowAsPlugin>> OnPluginsInitialized;
    }

    public class PluginManager : ManagerBase<PluginManager>, IPluginManager
    {
        public const string DefaultPluginTypeName = "ShowAsText";

        private ICollection<IShowAsPlugin> _plugins;

        public ICollection<IShowAsPlugin> Plugins
        {
            get
            {
                if (_plugins == null)
                {
                    lock (this)
                    {
                        if (_plugins == null)
                        {
                            _plugins = PluginLoader.Instance.LoadPlugins();
                            InitializePlugins(_plugins);
                            OnPluginsInitialized?.Invoke(_plugins);
                        }
                    }
                }
                return _plugins;
            }
        }

        public IShowAsPlugin DefaultPlugin { get; protected set; }

        public Config AppConfig { get; set; }

        public event Action<ICollection<IShowAsPlugin>> OnPluginsInitialized;

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
                    p.OnAppClosing();
                }
                catch { }
                try
                {
                    AppConfig.PluginsConfigString[p.GetType().Name] = p.GetConfig();
                }
                catch { }
                p.Dispose();
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

        protected virtual void InitializePlugins(ICollection<IShowAsPlugin> plugins)
        {
            foreach (var plugin in plugins)
            {
                if (plugin is Control)
                {
                    (plugin as Control).Dock = DockStyle.Fill;
                }
                var pluginTypeName = plugin.GetType().Name;
                if (pluginTypeName == DefaultPluginTypeName)
                {
                    DefaultPlugin = plugin;
                }
                var pluginConfigString = string.Empty;
                if (AppConfig != null && AppConfig.PluginsConfigString.ContainsKey(pluginTypeName))
                {
                    pluginConfigString = AppConfig.PluginsConfigString[pluginTypeName];
                }
                try
                {
                    plugin.OnSetConfig(pluginConfigString);
                }
                catch { }
            }
            if (DefaultPlugin == null && _plugins.Count > 0)
            {
                DefaultPlugin = _plugins.First();
            }
        }
    }
}
