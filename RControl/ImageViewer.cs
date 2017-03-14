using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gnllk.RControl
{
    public partial class ImageViewer : UserControl
    {
        private int _picX = 0;

        private int _picY = 0;

        private bool _mouseDragMove = false;

        private const int MinRetain = 30;

        public ImageViewer()
        {
            InitializeComponent();
            picBox.MouseWheel += PicBox_MouseWheel;
        }

        public Image Image
        {
            get { return picBox.Image; }
            set { picBox.Image = value; }
        }

        public PictureBox PictureBox
        {
            get { return picBox; }
        }

        public void ShowImageInCenter()
        {
            var img = picBox.Image;
            if (img != null)
            {
                picBox.Width = img.Width;
                picBox.Height = img.Height;
                picBox.Top = (int)((Parent.Height - picBox.Height) / 2d);
                picBox.Left = (int)((Parent.Width - picBox.Width) / 2d);
            }
        }

        public void ClearImage()
        {
            picBox.Image = null;
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

        private void picBox_MouseEnter(object sender, EventArgs e)
        {
            picBox.Focus();
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

        private void ImageViewer_Resize(object sender, EventArgs e)
        {
            picBox.Top = (int)((Parent.Height - picBox.Height) / 2d);
            picBox.Left = (int)((Parent.Width - picBox.Width) / 2d);
        }
    }
}
