using System;
using System.Drawing;

namespace Gnllk.RedisClient
{
    internal class PluginGridViewItem
    {
        public Guid Identity { get; set; }

        public Icon Icon { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public string[] DownloadUrls { get; set; }

        public string[] ScreenshotUrls { get; set; }

        public Version Version { get; set; }

        public DateTime UpdateTime { get; set; }

        public PluginInstallState InstallState { get; set; }
    }

    internal enum PluginInstallState
    {
        NotInstalled,
        Installing,
        Installed,
        Uninstalling,
        Uninstalled
    }
}
