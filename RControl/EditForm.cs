using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Gnllk.RControl
{
    public partial class EditForm : Form
    {
        byte[] mByteData = null;

        public EditForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        public string EditName
        {
            get { return labName.Text; }
            set { labName.Text = value; }
        }

        public string EditValue
        {
            get { return txtValue.Text; }
            set { txtValue.Text = value; }
        }

        public byte[] ByteData
        {
            get { return mByteData; }
        }

        private void btnImportFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (Stream fs = dialog.OpenFile())
                    {
                        if (fs.Length > (long)Int32.MaxValue)
                        {
                            throw new Exception(string.Format("File is large than {0} KB", Math.Round(Int32.MaxValue / 1024.0, 3)));
                        }
                        txtValue.Text = string.Format("The content of file {0}", dialog.FileName);
                        using (BinaryReader reader = new BinaryReader(fs))
                        {
                            mByteData = reader.ReadBytes((int)fs.Length);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtValue_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Control && e.KeyCode == Keys.A)
                {
                    txtValue.SelectAll();
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
