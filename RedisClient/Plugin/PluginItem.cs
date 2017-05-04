using Gnllk.RControl;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Gnllk.RedisClient.Plugin
{
    public class PluginItem : IPluginItem
    {
        protected static readonly string[] InnerPluginsNames = { "ShowAsText", "ShowAsImage" };

        public PluginItem() { }

        public PluginItem(IShowInPlugin plugin)
        {
            if (plugin == null)
                throw new ArgumentNullException("plugin", "plugin could not be null");

            var type = plugin.GetType();
            Plugin = plugin;
            PluginTypeName = type.Name;
            PluginFileName = type.Assembly.Location;
            PluginPath = Path.GetDirectoryName(PluginFileName);
            CreateDate = new FileInfo(PluginFileName).CreationTime;
            IsInner = InnerPluginsNames.Contains(PluginTypeName);
            Enable = true;
        }

        public string Author
        {
            get
            {
                return Plugin.GetAuthor();
            }
        }

        public string Description
        {
            get
            {
                return Plugin.GetDescription();
            }
        }

        public Icon Icon
        {
            get
            {
                return Plugin.GetIcon();
            }
        }

        public Guid Identity
        {
            get
            {
                var assembly = Assembly.GetAssembly(Plugin.GetType());
                var guidAttr = assembly.GetCustomAttributes(typeof(GuidAttribute), false);
                return guidAttr.Any() ? Guid.Parse(((GuidAttribute)guidAttr.First()).Value)
                    : Guid.Empty;
            }
        }

        public string Name
        {
            get
            {
                return Plugin.GetName();
            }
        }

        public Version Version
        {
            get
            {
                return Assembly.GetAssembly(Plugin.GetType()).GetName().Version;
            }
        }

        public IShowInPlugin Plugin { get; }

        public string PluginTypeName { get; }

        public string PluginPath { get; }

        public string PluginFileName { get; }

        public bool Enable { get; set; }

        public bool IsInner { get; }

        public DateTime CreateDate { get; }

        public override string ToString()
        {
            return PluginTypeName;
        }
    }
}
