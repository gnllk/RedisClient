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
    public partial class OneValueForm : Form
    {
        public OneValueForm()
        {
            InitializeComponent();
        }

        public TextBox TextBox
        {
            get
            {
                return txtValue;
            }
        }

        public string Value
        {
            get { return this.txtValue.Text; }
            set { this.txtValue.Text = value; }
        }

        public bool EnablePassword
        {
            set
            {
                txtValue.PasswordChar = value ? '*' : '\0';
            }
            get
            {
                return txtValue.PasswordChar != '\0';
            }
        }

        private void btnComfirm_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void txtValue_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void txtValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
            }
        }

        private void OneValueForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void OneValueForm_Load(object sender, EventArgs e)
        {
            KeyPreview = true;
        }
    }
}
