using Gnllk.RControl;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;

namespace Gnllk.RedisClient.Manager
{
    internal sealed class PluginLoader : ManagerBase<PluginLoader>
    {
        [ImportMany]
        private IEnumerable<IShowAsPlugin> _plugins;

        public ICollection<IShowAsPlugin> LoadPlugins()
        {
            var catalog = new AggregateCatalog();
            var baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");
            catalog.Catalogs.Add(new DirectoryCatalog(baseDir));
            foreach (var subDir in Directory.EnumerateDirectories(baseDir))
            {
                catalog.Catalogs.Add(new DirectoryCatalog(subDir));
            }
            var _container = new CompositionContainer(catalog);

            try
            {
                _container.ComposeParts(this);
                return _plugins.ToList();
            }
            catch
            {
                return new List<IShowAsPlugin>();
            }
        }
    }
}
