using System.Text;
using System.IO;
using Gnllk.JCommon.Helper;
using System;

namespace Gnllk.RedisClient.Manager
{
    public class ConfigManager : ManagerBase<ConfigManager>
    {
        protected const string APP_PATH = "RedisClient";

        protected const string CONFIG_FILE_NAME = "Config.json";

        protected string mConfigFilePath;

        protected string ConfigFilePath
        {
            get
            {
                if (mConfigFilePath == null)
                {
                    var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    mConfigFilePath = Path.Combine(appDataPath, APP_PATH);
                }
                return mConfigFilePath;
            }
        }

        protected string ConfigFileName
        {
            get
            {
                return Path.Combine(ConfigFilePath, CONFIG_FILE_NAME);
            }
        }

        protected Config mConfig;

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
            if (!File.Exists(ConfigFileName)) return;
            using (var file = new FileStream(ConfigFileName, FileMode.Open, FileAccess.Read))
            {
                var reader = new StreamReader(file, Encoding.UTF8);
                Config = JsonHelper.FromJson<Config>(reader.ReadToEnd());
            }
        }

        public void SaveConfig()
        {
            CreateDirIfNotExists(ConfigFilePath);
            using (var file = new FileStream(ConfigFileName, FileMode.Create, FileAccess.Write))
            {
                var writer = new StreamWriter(file, Encoding.UTF8);
                var json = JsonHelper.ToJson(Config);
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

        protected void CreateDirIfNotExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
