using System;
using System.Windows.Forms;
using Gnllk.RControl;
using Gnllk.JCommon.Helper;
using System.Text;
using System.Linq;
using System.ComponentModel.Composition;
using System.IO;
using System.Drawing;

namespace ShowAsTextPlugin
{
    [Export(typeof(IShowInPlugin))]
    public partial class ShowAsText : UserControl, IShowInPlugin
    {
        private static readonly Guid PluginId = new Guid("f53f939a-cdf7-449c-9807-2b53471a466c");

        public readonly string[] EncodingName = new string[] { "us-ascii", "gb2312", "GB18030", "hz-gb-2312", "utf-7", "utf-8", "utf-16", "utf-32", "utf-32BE", "unicodeFFFE", "Windows-1252", "x-mac-korean", "x-mac-chinesesimp", "x-cp20936", "x-cp20949", "iso-8859-1", "iso-8859-8", "iso-8859-8-i", "iso-2022-jp", "csISO2022JP", "iso-2022-kr", "x-cp50227", "euc-jp", "EUC-CN", "euc-kr", "x-iscii-de", "x-iscii-be", "x-iscii-ta", "x-iscii-te", "x-iscii-as", "x-iscii-or", "x-iscii-ka", "x-iscii-ma", "x-iscii-gu", "x-iscii-pa" };

        public const string DefaultEncodingName = "utf-8";

        public const int MAX_STRING_LENGTH = 300;

        public const int SHOW_ALL = -1;

        protected ShowAsTextConfig Config { get; set; }

        protected Encoding CurrentEncoding { get; set; }

        protected ShowData Data { get; set; }

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

        public virtual void OnShowAs(ShowData data)
        {
            Data = data;
            ShowTextValue(Data.Key, Data.Value, MAX_STRING_LENGTH);
        }

        public virtual void OnBlur()
        {

        }

        public virtual bool ShouldShowAs(ShowData data)
        {
            if (data == null || string.IsNullOrEmpty(data.Key)) return false;

            return data.Key.EndsWith(".txt", StringComparison.OrdinalIgnoreCase)
                || data.Key.EndsWith(".log", StringComparison.OrdinalIgnoreCase);
        }

        protected virtual void ShowTextValue(string key, byte[] data, int lengthToShow = SHOW_ALL)
        {
            try
            {
                if (data != null && data.Length > 0)
                {
                    var text = txtValue.Text = CurrentEncoding.GetString(data);
                    if (lengthToShow == SHOW_ALL || text.Length < lengthToShow)
                        txtValue.Text = text;
                    else
                        txtValue.Text = text.Substring(0, lengthToShow) + "...";
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
            ShowTextValue(Data.Key, Data.Value);
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
                if (Data != null)
                    ShowTextValue(Data.Key, Data.Value);
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
                var key = Data.Key;
                byte[] val = Data.Value;

                if (val != null && val.Length > 0)
                {
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.Filter = "Text file (*.txt)|*.txt|Raw file (*.*)|*.*";
                    dialog.FileName = key ?? "Default1";
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
                                    file.Write(val, 0, val.Length);
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

        public Guid GetId()
        {
            return PluginId;
        }

        public Icon GetIcon()
        {
            return Resources.ShowAsText;
        }

        public string GetAuthor()
        {
            return "www.gnllk.com";
        }

        public bool ShouldShowConnection(ConnectionData data)
        {
            return false;
        }

        public bool ShouldShowDatabase(DatabaseData data)
        {
            return false;
        }

        public void OnShowConnection(ConnectionData data)
        {
            throw new NotImplementedException();
        }

        public void OnShowDatabase(DatabaseData data)
        {
            throw new NotImplementedException();
        }

        private void txtValue_MouseEnter(object sender, EventArgs e)
        {
            txtValue.Focus();
            txtValue.Select(0, 0);
        }

        private void cbbShowAs_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selected = cbbEncoding.SelectedValue.ToString();
                if (selected == "TXT")
                {
                }
                else if (selected == "XML")
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
