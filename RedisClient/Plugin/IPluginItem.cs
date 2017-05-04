using Gnllk.RControl;
using System;
using System.Drawing;

namespace Gnllk.RedisClient.Plugin
{
    public interface IPluginItem
    {
        Guid Identity { get; }

        Icon Icon { get; }

        string Name { get; }

        string Description { get; }

        string Author { get; }

        Version Version { get; }

        DateTime CreateDate { get; }

        IShowInPlugin Plugin { get; }

        bool Enable { get; set; }
    }
}
