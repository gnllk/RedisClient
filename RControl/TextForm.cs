using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gnllk.RControl
{
    public partial class TextForm : Form
    {
        public string Data
        {
            get { return txtValue.Text; }
            set { txtValue.Text = value; }
        }

        public TextForm()
        {
            InitializeComponent();
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

        private void TextForm_Load(object sender, EventArgs e)
        {
            KeyPreview = true;
        }

        private void TextForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
