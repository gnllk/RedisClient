using System;
using System.Windows.Forms;
using System.IO;
using Gnllk.RControl.Properties;

namespace Gnllk.RControl
{
    public partial class AddForm : Form
    {
        byte[] mByteData = null;

        bool mIsValid = true;

        public string AddName
        {
            get { return txtName.Text; }
            set { txtName.Text = value; }
        }

        public string AddValue
        {
            get { return txtValue.Text; }
            set { txtValue.Text = value; }
        }

        public byte[] ByteData
        {
            get { return mByteData; }
        }

        public int DbIndex
        {
            get
            {
                int result = 0;
                if (int.TryParse(cbb_db.Text.Trim(), out result))
                {
                    return result;
                }
                throw new Exception("Cannot parse the db number to integer.");
            }
            set { cbb_db.Text = value.ToString(); }
        }

        public AddForm()
        {
            InitializeComponent();
        }

        public AddForm(int dbIndex)
        {
            InitializeComponent();
            DbIndex = dbIndex;
        }

        public AddForm(string addName)
        {
            InitializeComponent();
            AddName = addName;
        }

        public AddForm(int dbIndex, string addName)
        {
            InitializeComponent();
            DbIndex = dbIndex;
            AddName = addName;
        }

        private void AddForm_Load(object sender, EventArgs e)
        {
            txtName.Text = Resources.LabName;
            txtValue.Text = Resources.LabValue;
            KeyPreview = true;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtName_Enter(object sender, EventArgs e)
        {
            if (txtName.Text == Resources.LabName)
            {
                txtName.Text = string.Empty;
            }
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                txtName.Text = Resources.LabName;
            }
        }

        private void txtValue_Enter(object sender, EventArgs e)
        {
            if (txtValue.Text == Resources.LabValue)
            {
                txtValue.Text = string.Empty;
            }
        }

        private void txtValue_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtValue.Text))
            {
                mByteData = null;
                txtValue.Text = Resources.LabValue;
            }
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
                        if (string.IsNullOrWhiteSpace(txtName.Text)
                            || txtName.Text == Resources.LabName)
                        {
                            txtName.Text = Path.GetFileName(dialog.FileName);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text == Resources.LabName)
            {
                txtName.Text = string.Empty;
            }
            if (txtValue.Text == Resources.LabValue)
            {
                txtValue.Text = string.Empty;
            }
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                mIsValid = false;
                MessageBox.Show("Name can not be null or empty");
            }
            else
            {
                mIsValid = true;
                DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mIsValid = true;
            DialogResult = DialogResult.Cancel;
        }

        private void AddForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!mIsValid)
            {
                e.Cancel = true;
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

        private void AddForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnCancel_Click(sender, new EventArgs());
            }
        }
    }
}
