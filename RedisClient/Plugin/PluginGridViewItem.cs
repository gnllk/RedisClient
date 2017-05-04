using System;
using System.Drawing;
using Gnllk.RControl;

namespace Gnllk.RedisClient.Plugin
{
    public class PluginGridViewItem : IPluginItem
    {
        private readonly IPluginItem _pluginItem;

        public PluginGridViewItem(IPluginItem pluginItem)
        {
            if (pluginItem == null)
                throw new ArgumentNullException("pluginItem");

            _pluginItem = pluginItem;
            InstallState = PluginInstallState.Installed;
        }

        public PluginInstallState InstallState { get; set; }

        public string Author { get { return _pluginItem.Author; } }

        public DateTime CreateDate { get { return _pluginItem.CreateDate; } }

        public string Description { get { return _pluginItem.Description; } }

        public Icon Icon { get { return _pluginItem.Icon; } }

        public Guid Identity { get { return _pluginItem.Identity; } }

        public string Name { get { throw new NotImplementedException(); } }

        public IShowInPlugin Plugin { get { return _pluginItem.Plugin; } }

        public Version Version { get { return _pluginItem.Version; } }

        public bool Enable { get; set; }
    }
}
