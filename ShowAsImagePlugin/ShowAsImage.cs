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
        private int _picX = 0;

        private int _picY = 0;

        private bool _mouseDragMove = false;

        private const int MinRetain = 30;

        public ShowAsImage()
        {
            InitializeComponent();
            picBox.MouseWheel += PicBox_MouseWheel;
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
            if (string.IsNullOrEmpty(key)) return false;

            return key.EndsWith(".png", StringComparison.OrdinalIgnoreCase)
                || key.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase)
                || key.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
                || key.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase);
        }

        protected virtual void ShowValue(string key, byte[] data)
        {
            try
            {
                if (data == null || data.Length == 0) return;

                using (var stream = new MemoryStream(data))
                {
                    var img = Image.FromStream(stream);
                    picBox.Image = img;
                    picBox.Width = img.Width;
                    picBox.Height = img.Height;
                    picBox.Top = (int)((Parent.Height - picBox.Height) / 2d);
                    picBox.Left = (int)((Parent.Width - picBox.Width) / 2d);
                }
            }
            catch (Exception ex)
            {
                picBox.Image = null;
                MessageBox.Show(string.Format("May be not a image, {0}", ex.Message));
            }
        }

        private void picBox_MouseEnter(object sender, EventArgs e)
        {
            picBox.Focus();
        }

        private void PicBox_MouseWheel(object sender, MouseEventArgs e)
        {
            var oldWith = picBox.Width;
            var oldHeight = picBox.Height;
            if (e.Delta >= 0)
            {
                picBox.Width = (int)(picBox.Width * 1.1);
                picBox.Height = (int)(picBox.Height * 1.1);
                var leftDelta = (int)((oldWith - picBox.Width) / 2d);
                var topDelta = (int)((oldHeight - picBox.Height) / 2d);
                picBox.Left += leftDelta;
                picBox.Top += topDelta;
            }
            else
            {
                picBox.Width = (int)(picBox.Width * 0.9);
                picBox.Height = (int)(picBox.Height * 0.9);
                var leftDelta = (int)((oldWith - picBox.Width) / 2d);
                var topDelta = (int)((oldHeight - picBox.Height) / 2d);
                picBox.Left += leftDelta;
                picBox.Top += topDelta;
            }
        }

        private void picBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseDragMove)
            {
                var left = picBox.Left + (e.X - _picX);
                var top = picBox.Top + (e.Y - _picY);
                if (left > MinRetain - picBox.Width && left < Parent.Parent.Width - MinRetain)
                    picBox.Left = left;
                if (top > MinRetain - picBox.Height && top < Parent.Height - MinRetain)
                    picBox.Top = top;
            }
        }

        private void picBox_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseDragMove = true;
            _picX = e.X;
            _picY = e.Y;
        }

        private void picBox_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseDragMove = false;
        }

        private void ShowAsImage_Resize(object sender, EventArgs e)
        {
            picBox.Top = (int)((Parent.Height - picBox.Height) / 2d);
            picBox.Left = (int)((Parent.Width - picBox.Width) / 2d);
        }
    }
}
