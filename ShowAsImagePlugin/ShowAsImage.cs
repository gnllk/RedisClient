using System;
using System.Drawing;
using System.Windows.Forms;
using Gnllk.RControl;
using System.ComponentModel.Composition;
using System.IO;

namespace ShowAsImagePlugin
{
    [Export(typeof(IShowAsPlugin))]
    public partial class ShowAsImage : UserControl, IShowAsPlugin
    {
        public ShowAsImage()
        {
            InitializeComponent();
        }

        private string OriginalKey { get; set; }

        private byte[] OriginalValue { get; set; }

        public virtual string GetConfig()
        {
            return string.Empty;
        }

        public virtual string GetDescription()
        {
            return "Support jpg, png, bmp";
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

        public virtual void OnShowAs(string key, byte[] data)
        {
            OriginalKey = key;
            OriginalValue = data;
            ShowValue(OriginalKey, OriginalValue);
        }

        public virtual bool ShouldShowAs(string key, byte[] data)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                var isImg = key.EndsWith(".png", StringComparison.OrdinalIgnoreCase)
                    || key.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase)
                    || key.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
                    || key.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase);

                if (isImg) return true;
            }

            if (data != null && data.Length > 9)
            {
                var isPng = data[0] == 0x89
                && data[1] == 0x50
                && data[2] == 0x4E
                && data[3] == 0x47;

                if (isPng) return true;

                var isJpg = data[0] == 0xFF
                && data[1] == 0xD8
                && data[2] == 0xFF
                && data[3] == 0xE0
                && data[4] == 0x00
                && data[5] == 0x10
                && data[6] == 0x4A
                && data[7] == 0x46
                && data[8] == 0x49
                && data[9] == 0x46;

                if (isJpg) return true;

                var isBmp = data[0] == 0x42
                && data[1] == 0x4D;

                if (isBmp) return true;
            }

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
