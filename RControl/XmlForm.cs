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
    public partial class XmlForm : Form
    {
        public XmlForm()
        {
            InitializeComponent();
        }

        public string Data
        {
            get { return xmlBrowser.DocumentText; }
            set { xmlBrowser.DocumentText = value; }
        }

        private void XmlForm_Load(object sender, EventArgs e)
        {
            KeyPreview = true;
        }

        private void XmlForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
