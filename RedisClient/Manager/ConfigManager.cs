using System.Text;
using System.IO;
using Gnllk.JCommon.Helper;

namespace Gnllk.RedisClient.Manager
{
    public class ConfigManager : ManagerBase<ConfigManager>
    {
        protected const string CONFIG_FILENAME = @".\Config.json";

        protected Config mConfig = null;

        public Config Config
        {
            get
            {
                if (mConfig == null) mConfig = new Config();
                return mConfig;
            }
            set
            {
                mConfig = value;
            }
        }

        public void LoadConfig()
        {
            using (FileStream file = new FileStream(CONFIG_FILENAME, FileMode.Open, FileAccess.Read))
            {
                TextReader reader = new StreamReader(file, Encoding.UTF8);
                Config = JsonHelper.FromJson<Config>(reader.ReadToEnd());
            }
        }

        public void SaveConfig()
        {
            using (FileStream file = new FileStream(CONFIG_FILENAME, FileMode.Create, FileAccess.Write))
            {
                TextWriter writer = new StreamWriter(file, Encoding.UTF8);
                string json = JsonHelper.ToJson(Config);
                writer.Write(json);
                writer.Flush();
            }
        }

        public void FreeConnection()
        {
            if (Config != null && Config.Connections != null)
            {
                foreach (var item in Config.Connections)
                {
                    if (item.Value != null) item.Value.Dispose();
                }
            }
        }
    }
}
