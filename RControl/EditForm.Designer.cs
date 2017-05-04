namespace Gnllk.RControl
{
    partial class EditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditForm));
            this.tableMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableControl = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnImportFile = new System.Windows.Forms.Button();
            this.cbbEncoding = new System.Windows.Forms.ComboBox();
            this.panelName = new System.Windows.Forms.Panel();
            this.labName = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.tableMain.SuspendLayout();
            this.tableControl.SuspendLayout();
            this.panelName.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableMain
            // 
            this.tableMain.ColumnCount = 1;
            this.tableMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableMain.Controls.Add(this.tableControl, 0, 2);
            this.tableMain.Controls.Add(this.panelName, 0, 0);
            this.tableMain.Controls.Add(this.txtValue, 0, 1);
            this.tableMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableMain.Location = new System.Drawing.Point(0, 0);
            this.tableMain.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableMain.Name = "tableMain";
            this.tableMain.RowCount = 3;
            this.tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableMain.Size = new System.Drawing.Size(782, 435);
            this.tableMain.TabIndex = 0;
            // 
            // tableControl
            // 
            this.tableControl.ColumnCount = 5;
            this.tableControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tableControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tableControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tableControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tableControl.Controls.Add(this.btnSave, 3, 0);
            this.tableControl.Controls.Add(this.btnCancel, 4, 0);
            this.tableControl.Controls.Add(this.btnImportFile, 2, 0);
            this.tableControl.Controls.Add(this.cbbEncoding, 0, 0);
            this.tableControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableControl.Location = new System.Drawing.Point(4, 401);
            this.tableControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableControl.Name = "tableControl";
            this.tableControl.RowCount = 1;
            this.tableControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableControl.Size = new System.Drawing.Size(774, 31);
            this.tableControl.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(512, 3);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(125, 25);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Location = new System.Drawing.Point(645, 3);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(125, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnImportFile
            // 
            this.btnImportFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnImportFile.Location = new System.Drawing.Point(378, 3);
            this.btnImportFile.Name = "btnImportFile";
            this.btnImportFile.Size = new System.Drawing.Size(127, 25);
            this.btnImportFile.TabIndex = 3;
            this.btnImportFile.Text = "Import file";
            this.btnImportFile.UseVisualStyleBackColor = true;
            this.btnImportFile.Click += new System.EventHandler(this.btnImportFile_Click);
            // 
            // cbbEncoding
            // 
            this.cbbEncoding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbEncoding.FormattingEnabled = true;
            this.cbbEncoding.Location = new System.Drawing.Point(3, 3);
            this.cbbEncoding.Name = "cbbEncoding";
            this.cbbEncoding.Size = new System.Drawing.Size(127, 23);
            this.cbbEncoding.TabIndex = 4;
            this.cbbEncoding.SelectedIndexChanged += new System.EventHandler(this.cbbEncoding_SelectedIndexChanged);
            // 
            // panelName
            // 
            this.panelName.Controls.Add(this.labName);
            this.panelName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelName.Location = new System.Drawing.Point(4, 3);
            this.panelName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelName.Name = "panelName";
            this.panelName.Size = new System.Drawing.Size(774, 31);
            this.panelName.TabIndex = 1;
            // 
            // labName
            // 
            this.labName.AutoSize = true;
            this.labName.Location = new System.Drawing.Point(4, 7);
            this.labName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(39, 15);
            this.labName.TabIndex = 0;
            this.labName.Text = "Name";
            // 
            // txtValue
            // 
            this.txtValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtValue.Location = new System.Drawing.Point(4, 40);
            this.txtValue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtValue.Multiline = true;
            this.txtValue.Name = "txtValue";
            this.txtValue.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtValue.Size = new System.Drawing.Size(774, 355);
            this.txtValue.TabIndex = 2;
            this.txtValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtValue_KeyDown);
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 435);
            this.Controls.Add(this.tableMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "EditForm";
            this.ShowInTaskbar = false;
            this.Text = "Edit";
            this.Load += new System.EventHandler(this.EditForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.EditForm_KeyUp);
            this.tableMain.ResumeLayout(false);
            this.tableMain.PerformLayout();
            this.tableControl.ResumeLayout(false);
            this.panelName.ResumeLayout(false);
            this.panelName.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableMain;
        private System.Windows.Forms.TableLayoutPanel tableControl;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panelName;
        private System.Windows.Forms.Label labName;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Button btnImportFile;
        private System.Windows.Forms.ComboBox cbbEncoding;
    }
}