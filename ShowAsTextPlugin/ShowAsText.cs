using System;
using System.Windows.Forms;
using Gnllk.RControl;
using Gnllk.JCommon.Helper;
using System.Text;
using System.Linq;
using System.ComponentModel.Composition;
using System.IO;

namespace ShowAsTextPlugin
{
    [Export(typeof(IShowAsPlugin))]
    public partial class ShowAsText : UserControl, IShowAsPlugin
    {
        public readonly string[] EncodingName = new string[] { "gb2312", "utf-16", "unicodeFFFE", "Windows-1252", "x-mac-korean", "x-mac-chinesesimp", "utf-32", "utf-32BE", "us-ascii", "x-cp20936", "x-cp20949", "iso-8859-1", "iso-8859-8", "iso-8859-8-i", "iso-2022-jp", "csISO2022JP", "iso-2022-kr", "x-cp50227", "euc-jp", "EUC-CN", "euc-kr", "hz-gb-2312", "GB18030", "x-iscii-de", "x-iscii-be", "x-iscii-ta", "x-iscii-te", "x-iscii-as", "x-iscii-or", "x-iscii-ka", "x-iscii-ma", "x-iscii-gu", "x-iscii-pa", "utf-7", "utf-8" };

        public const string DefaultEncodingName = "utf-8";

        public const int MAX_STRING_LENGTH = 100;

        protected ShowAsTextConfig Config { get; set; }

        protected Encoding CurrentEncoding { get; set; }

        private string OriginalKey { get; set; }

        private byte[] OriginalValue { get; set; }

        public ShowAsText()
        {
            InitializeComponent();
            Config = new ShowAsTextConfig();
            CurrentEncoding = Encoding.UTF8;
            cbbEncoding.DataSource = EncodingName;
            cbbEncoding.Text = DefaultEncodingName;
            cbbEncoding.SelectedItem = DefaultEncodingName;
        }

        public string GetConfig()
        {
            return JsonHelper.ToJson(Config);
        }

        public virtual string GetDescription()
        {
            return "Show data as text";
        }

        public virtual string GetName()
        {
            return "Show as text";
        }

        public virtual void OnAppClosing()
        {
        }

        public virtual void OnSetConfig(string config)
        {
            try
            {
                Config = string.IsNullOrWhiteSpace(config) ? new ShowAsTextConfig()
                    : JsonHelper.FromJson<ShowAsTextConfig>(config);
            }
            catch
            {
                Config = new ShowAsTextConfig();
            }
            finally
            {
                SetSelectedEncoding(Config);
                CurrentEncoding = Encoding.GetEncoding(Config.EncodingName);
            }
        }

        public virtual void OnShowAs(string name, byte[] data)
        {
            OriginalKey = name;
            OriginalValue = data;
            ShowValue(OriginalKey, OriginalValue);
        }

        public virtual void OnBlur()
        {

        }

        public virtual bool ShouldShowAs(string name, byte[] data)
        {
            if (string.IsNullOrEmpty(name)) return false;

            return name.EndsWith(".txt", StringComparison.OrdinalIgnoreCase)
                || name.EndsWith(".log", StringComparison.OrdinalIgnoreCase);
        }

        protected virtual void ShowValue(string key, byte[] data)
        {
            try
            {
                if (data != null && data.Length > 0)
                {
                    txtValue.Text = CurrentEncoding.GetString(data);
                }
                else
                {
                    txtValue.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                txtValue.Text = string.Empty;
                MessageBox.Show(ex.Message);
            }
        }

        private void btnShowAllText_Click(object sender, EventArgs e)
        {
        }

        private void btnClearText_Click(object sender, EventArgs e)
        {
            txtValue.Text = string.Empty;
        }

        private void cbbEncoding_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Config.EncodingName = cbbEncoding.SelectedValue.ToString();
                CurrentEncoding = Encoding.GetEncoding(Config.EncodingName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                CurrentEncoding = Encoding.UTF8;
                cbbEncoding.SelectedItem = DefaultEncodingName;
                Config.EncodingName = DefaultEncodingName;
            }
            finally
            {
                ShowValue(OriginalKey, OriginalValue);
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

        private void SetSelectedEncoding(ShowAsTextConfig config)
        {
            cbbEncoding.SelectedItem = string.IsNullOrWhiteSpace(config.EncodingName)
                || !EncodingName.Contains(config.EncodingName)
                ? DefaultEncodingName : config.EncodingName;
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] data = OriginalValue;
                if (data != null && data.Length > 0)
                {
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.Filter = "Text file (*.txt)|*.txt|Raw file (*.*)|*.*";
                    dialog.FileName = OriginalKey ?? "Default1";
                    string ext = Path.GetExtension(dialog.FileName).ToLower();
                    if (string.IsNullOrWhiteSpace(ext))
                    {
                        dialog.FilterIndex = 1;
                    }
                    else
                    {
                        dialog.FilterIndex = 2;
                    }
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        if (!string.IsNullOrWhiteSpace(dialog.FileName))
                        {
                            if (dialog.FilterIndex == 1)
                            {
                                using (FileStream file = new FileStream(dialog.FileName, FileMode.Create))
                                {
                                    var writer = new StreamWriter(file, CurrentEncoding);
                                    writer.Write(txtValue.Text);
                                    writer.Flush();
                                }
                            }
                            else
                            {
                                using (FileStream file = new FileStream(dialog.FileName, FileMode.Create))
                                {
                                    file.Write(data, 0, data.Length);
                                    file.Flush();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
