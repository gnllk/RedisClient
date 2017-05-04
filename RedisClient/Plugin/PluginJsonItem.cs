using System;
using System.Drawing;
using Gnllk.RControl;

namespace Gnllk.RedisClient.Plugin
{
    public class PluginJsonItem : IPluginItem
    {
        private readonly string PluginJsonString;

        public PluginJsonItem(string pluginJson)
        {
            PluginJsonString = pluginJson;
        }

        public string Author
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public DateTime CreateDate
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Description
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool Enable
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Icon Icon
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Guid Identity
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IShowInPlugin Plugin
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Version Version
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
