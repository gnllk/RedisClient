using System;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace Gnllk.RControl
{
    public partial class EditForm : Form
    {
        public readonly string[] EncodingNames = new string[] { "gb2312", "utf-16", "unicodeFFFE", "Windows-1252", "x-mac-korean", "x-mac-chinesesimp", "utf-32", "utf-32BE", "us-ascii", "x-cp20936", "x-cp20949", "iso-8859-1", "iso-8859-8", "iso-8859-8-i", "iso-2022-jp", "csISO2022JP", "iso-2022-kr", "x-cp50227", "euc-jp", "EUC-CN", "euc-kr", "hz-gb-2312", "GB18030", "x-iscii-de", "x-iscii-be", "x-iscii-ta", "x-iscii-te", "x-iscii-as", "x-iscii-or", "x-iscii-ka", "x-iscii-ma", "x-iscii-gu", "x-iscii-pa", "utf-7", "utf-8" };

        public const string DefaultEncodingName = "utf-8";

        public Encoding SelectedEncoding { get; protected set; } = Encoding.UTF8;

        public byte[] FileData { get; protected set; }

        protected byte[] OriginalValue { get; set; }

        private bool _initialized = false;

        public EditForm()
        {
            InitializeComponent();
            cbbEncoding.DataSource = EncodingNames;
            cbbEncoding.Text = DefaultEncodingName;
            cbbEncoding.SelectedItem = DefaultEncodingName;
            _initialized = true;
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

        public byte[] EditValue
        {
            get { return SelectedEncoding.GetBytes(txtValue.Text); }
            set
            {
                OriginalValue = value;
                txtValue.Text = SelectedEncoding.GetString(value);
            }
        }

        public string EditValueString
        {
            get { return txtValue.Text; }
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
                            FileData = reader.ReadBytes((int)fs.Length);
                            cbbEncoding.Enabled = false;
                            txtValue.Enabled = false;
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

        private void EditForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnCancel_Click(sender, new EventArgs());
            }
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            KeyPreview = true;
        }

        private void cbbEncoding_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!_initialized) return;
                var encodingName = cbbEncoding.SelectedValue.ToString();
                SelectedEncoding = Encoding.GetEncoding(encodingName);

                txtValue.Text = SelectedEncoding.GetString(OriginalValue);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
