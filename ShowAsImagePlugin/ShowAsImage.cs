using System;
using System.Drawing;
using System.Windows.Forms;
using Gnllk.RControl;
using System.ComponentModel.Composition;
using System.IO;

namespace ShowAsImagePlugin
{
    [Export(typeof(IShowInPlugin))]
    public partial class ShowAsImage : UserControl, IShowInPlugin
    {
        private static readonly Guid PluginId = new Guid("03bb1b0e-ee3c-46b5-88db-4253c089741e");

        public ShowAsImage()
        {
            InitializeComponent();
        }

        protected ShowData Data { get; set; }

        public string GetAuthor()
        {
            return "www.gnllk.com";
        }

        public virtual string GetConfig()
        {
            return string.Empty;
        }

        public virtual string GetDescription()
        {
            return "Support jpg, png, bmp";
        }

        public Icon GetIcon()
        {
            return Resources.ShowAsImage;
        }

        public Guid GetId()
        {
            return PluginId;
        }

        public virtual string GetName()
        {
            return "Show as image";
        }

        public virtual void OnAppClosing()
        {
        }

        public virtual void OnBlur()
        {
        }

        public virtual void OnSetConfig(string config)
        {
        }

        public virtual void OnShowAs(ShowData data)
        {
            Data = data;
            ShowValue(Data.Key, Data.Value);
        }

        public void OnShowConnection(ConnectionData data)
        {
            throw new NotImplementedException();
        }

        public void OnShowDatabase(DatabaseData data)
        {
            throw new NotImplementedException();
        }

        public virtual bool ShouldShowAs(ShowData data)
        {
            if (data == null) return false;

            var key = data.Key;
            var val = data.Value;

            if (string.IsNullOrWhiteSpace(key))
            {
                var isImg = key.EndsWith(".png", StringComparison.OrdinalIgnoreCase)
                    || key.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase)
                    || key.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
                    || key.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase);

                if (isImg) return true;
            }

            if (val != null && val.Length > 9)
            {
                var isPng = val[0] == 0x89
                && val[1] == 0x50
                && val[2] == 0x4E
                && val[3] == 0x47;

                if (isPng) return true;

                var isJpg = val[0] == 0xFF
                && val[1] == 0xD8
                && val[2] == 0xFF
                && val[3] == 0xE0
                && val[4] == 0x00
                && val[5] == 0x10;

                if (isJpg) return true;

                var isBmp = val[0] == 0x42
                && val[1] == 0x4D;

                if (isBmp) return true;
            }

            return false;
        }

        public virtual bool ShouldShowConnection(ConnectionData data)
        {
            return false;
        }

        public virtual bool ShouldShowDatabase(DatabaseData data)
        {
            return false;
        }

        protected virtual void ShowValue(string key, byte[] data)
        {
            try
            {
                if (data == null || data.Length == 0) return;

                using (var stream = new MemoryStream(data))
                {
                    imgViewer.Image = Image.FromStream(stream);
                    imgViewer.ShowImageInCenter();
                }
            }
            catch (Exception ex)
            {
                imgViewer.ClearImage();
                MessageBox.Show(string.Format("May be not a image, {0}", ex.Message));
            }
        }
    }
}
