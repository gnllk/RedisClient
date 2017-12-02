using System;
using System.Windows.Forms;
using System.IO;
using Gnllk.RControl.Properties;
using System.Text;

namespace Gnllk.RControl
{
    public partial class AddForm : Form
    {
        public readonly string[] EncodingNames = new string[] { "gb2312", "utf-16", "unicodeFFFE", "Windows-1252", "x-mac-korean", "x-mac-chinesesimp", "utf-32", "utf-32BE", "us-ascii", "x-cp20936", "x-cp20949", "iso-8859-1", "iso-8859-8", "iso-8859-8-i", "iso-2022-jp", "csISO2022JP", "iso-2022-kr", "x-cp50227", "euc-jp", "EUC-CN", "euc-kr", "hz-gb-2312", "GB18030", "x-iscii-de", "x-iscii-be", "x-iscii-ta", "x-iscii-te", "x-iscii-as", "x-iscii-or", "x-iscii-ka", "x-iscii-ma", "x-iscii-gu", "x-iscii-pa", "utf-7", "utf-8" };

        public const string DefaultEncodingName = "utf-8";

        private bool _isValid = true;

        public Encoding SelectedEncoding { get; protected set; } = Encoding.UTF8;

        public byte[] FileData { get; protected set; }

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

        public byte[] AddValueBytes
        {
            get { return SelectedEncoding.GetBytes(txtValue.Text); }
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
            cbbEncoding.DataSource = EncodingNames;
            cbbEncoding.Text = DefaultEncodingName;
            cbbEncoding.SelectedItem = DefaultEncodingName;
        }

        public AddForm(int dbIndex) : this()
        {
            DbIndex = dbIndex;
        }

        public AddForm(string addName) : this()
        {
            AddName = addName;
        }

        public AddForm(int dbIndex, string addName) : this()
        {
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
                            FileData = reader.ReadBytes((int)fs.Length);
                        }
                        cbbEncoding.Enabled = false;
                        txtValue.Enabled = false;
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
                _isValid = false;
                MessageBox.Show("Name can not be null or empty");
            }
            else
            {
                _isValid = true;
                DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _isValid = true;
            DialogResult = DialogResult.Cancel;
        }

        private void AddForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isValid)
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

        private void cbbEncoding_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var encodingName = cbbEncoding.SelectedValue.ToString();
                SelectedEncoding = Encoding.GetEncoding(encodingName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
