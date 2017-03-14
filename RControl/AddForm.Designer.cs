namespace Gnllk.RControl
{
    partial class AddForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddForm));
            this.tableMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableControl = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnImportFile = new System.Windows.Forms.Button();
            this.pan_lab_db = new System.Windows.Forms.Panel();
            this.lab_db = new System.Windows.Forms.Label();
            this.cbb_db = new System.Windows.Forms.ComboBox();
            this.panelName = new System.Windows.Forms.Panel();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.tableMain.SuspendLayout();
            this.tableControl.SuspendLayout();
            this.pan_lab_db.SuspendLayout();
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
            this.tableMain.TabIndex = 1;
            // 
            // tableControl
            // 
            this.tableControl.ColumnCount = 6;
            this.tableControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 93F));
            this.tableControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tableControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tableControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tableControl.Controls.Add(this.btnSave, 4, 0);
            this.tableControl.Controls.Add(this.btnCancel, 5, 0);
            this.tableControl.Controls.Add(this.btnImportFile, 3, 0);
            this.tableControl.Controls.Add(this.pan_lab_db, 0, 0);
            this.tableControl.Controls.Add(this.cbb_db, 1, 0);
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
            this.btnImportFile.TabIndex = 2;
            this.btnImportFile.Text = "Import file";
            this.btnImportFile.UseVisualStyleBackColor = true;
            this.btnImportFile.Click += new System.EventHandler(this.btnImportFile_Click);
            // 
            // pan_lab_db
            // 
            this.pan_lab_db.Controls.Add(this.lab_db);
            this.pan_lab_db.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pan_lab_db.Location = new System.Drawing.Point(3, 3);
            this.pan_lab_db.Name = "pan_lab_db";
            this.pan_lab_db.Size = new System.Drawing.Size(34, 25);
            this.pan_lab_db.TabIndex = 4;
            // 
            // lab_db
            // 
            this.lab_db.AutoSize = true;
            this.lab_db.Location = new System.Drawing.Point(5, 5);
            this.lab_db.Name = "lab_db";
            this.lab_db.Size = new System.Drawing.Size(23, 15);
            this.lab_db.TabIndex = 3;
            this.lab_db.Text = "db";
            // 
            // cbb_db
            // 
            this.cbb_db.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbb_db.FormattingEnabled = true;
            this.cbb_db.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15"});
            this.cbb_db.Location = new System.Drawing.Point(43, 3);
            this.cbb_db.Name = "cbb_db";
            this.cbb_db.Size = new System.Drawing.Size(87, 23);
            this.cbb_db.TabIndex = 5;
            this.cbb_db.Text = "0";
            // 
            // panelName
            // 
            this.panelName.Controls.Add(this.txtName);
            this.panelName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelName.Location = new System.Drawing.Point(4, 3);
            this.panelName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelName.Name = "panelName";
            this.panelName.Size = new System.Drawing.Size(774, 31);
            this.panelName.TabIndex = 1;
            // 
            // txtName
            // 
            this.txtName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtName.Location = new System.Drawing.Point(0, 0);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(774, 25);
            this.txtName.TabIndex = 0;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            this.txtName.Enter += new System.EventHandler(this.txtName_Enter);
            this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
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
            this.txtValue.Enter += new System.EventHandler(this.txtValue_Enter);
            this.txtValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtValue_KeyDown);
            this.txtValue.Leave += new System.EventHandler(this.txtValue_Leave);
            // 
            // AddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 435);
            this.Controls.Add(this.tableMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddForm";
            this.Text = "Add";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddForm_FormClosing);
            this.Load += new System.EventHandler(this.AddForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.AddForm_KeyUp);
            this.tableMain.ResumeLayout(false);
            this.tableMain.PerformLayout();
            this.tableControl.ResumeLayout(false);
            this.pan_lab_db.ResumeLayout(false);
            this.pan_lab_db.PerformLayout();
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
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnImportFile;
        private System.Windows.Forms.Panel pan_lab_db;
        private System.Windows.Forms.Label lab_db;
        private System.Windows.Forms.ComboBox cbb_db;
    }
}