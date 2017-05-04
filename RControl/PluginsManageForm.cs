using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gnllk.RControl
{
    public partial class PluginsManageForm : Form
    {
        private DataGridViewCellStyle _rowStyleNormal;
        private DataGridViewCellStyle _rowStyleAlternate;

        public event Action<object, DataGridViewCellEventArgs> OnRowEnter;

        public event Action<object, DataGridViewCellEventArgs> OnCellClick;

        public event Action<object, EventArgs> OnInstallClick;

        public event Action<object, DataGridViewCellEventArgs, bool> OnEnableChange;

        public PluginsManageForm()
        {
            InitializeComponent();
            SetRowStyle();
            pluginsList.AutoGenerateColumns = false;
        }

        public DataGridView PluginsListView { get { return pluginsList; } }

        public Label LabName { get { return labName; } }

        public Label LabVersion { get { return labVersion; } }

        public Label LabDate { get { return labDate; } }

        public Label LabDescription { get { return labDescription; } }

        public Label LabAuthor { get { return labAuthor; } }

        public bool InstallButtonVisible
        {
            get { return btnInstall.Visible; }
            set { btnInstall.Visible = value; }
        }

        private void SetRowStyle()
        {
            _rowStyleNormal = new DataGridViewCellStyle();
            _rowStyleNormal.BackColor = Color.LightBlue;
            _rowStyleNormal.SelectionBackColor = Color.LightSteelBlue;

            _rowStyleAlternate = new DataGridViewCellStyle();
            _rowStyleAlternate.BackColor = Color.LightGray;
            _rowStyleAlternate.SelectionBackColor = Color.LightSlateGray;
        }

        private void pluginsList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (OnRowEnter == null) return;
            OnRowEnter(sender, e);
        }

        private void pluginsList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (OnCellClick == null) return;
            OnCellClick(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (OnInstallClick == null) return;
            OnInstallClick(sender, e);
        }

        private void pluginsList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                pluginsList.Rows[e.RowIndex].DefaultCellStyle = _rowStyleNormal;
            else
                pluginsList.Rows[e.RowIndex].DefaultCellStyle = _rowStyleAlternate;
        }

        private void pluginsList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 3 || OnEnableChange == null) return;
            var view = sender as DataGridView;
            if (view == null) return;

            var cell = view.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var check = Convert.ToBoolean(cell.EditedFormattedValue);
            OnEnableChange(sender, e, check);
        }
    }
}
