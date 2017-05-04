namespace ShowAsTextPlugin
{
    partial class ShowAsText
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableMain = new System.Windows.Forms.TableLayoutPanel();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.tableToolBar = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnSaveFile = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnShowAllText = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbbEncoding = new System.Windows.Forms.ComboBox();
            this.cbbShowAs = new System.Windows.Forms.ComboBox();
            this.tableMain.SuspendLayout();
            this.tableToolBar.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableMain
            // 
            this.tableMain.ColumnCount = 1;
            this.tableMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableMain.Controls.Add(this.txtValue, 0, 1);
            this.tableMain.Controls.Add(this.tableToolBar, 0, 0);
            this.tableMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableMain.Location = new System.Drawing.Point(0, 0);
            this.tableMain.Name = "tableMain";
            this.tableMain.RowCount = 2;
            this.tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableMain.Size = new System.Drawing.Size(680, 400);
            this.tableMain.TabIndex = 0;
            // 
            // txtValue
            // 
            this.txtValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtValue.Location = new System.Drawing.Point(0, 40);
            this.txtValue.Margin = new System.Windows.Forms.Padding(0);
            this.txtValue.Multiline = true;
            this.txtValue.Name = "txtValue";
            this.txtValue.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtValue.Size = new System.Drawing.Size(680, 360);
            this.txtValue.TabIndex = 0;
            this.txtValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtValue_KeyDown);
            this.txtValue.MouseEnter += new System.EventHandler(this.txtValue_MouseEnter);
            // 
            // tableToolBar
            // 
            this.tableToolBar.ColumnCount = 4;
            this.tableToolBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableToolBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableToolBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableToolBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableToolBar.Controls.Add(this.panel2, 0, 0);
            this.tableToolBar.Controls.Add(this.panel8, 2, 0);
            this.tableToolBar.Controls.Add(this.panel7, 1, 0);
            this.tableToolBar.Controls.Add(this.panel1, 0, 0);
            this.tableToolBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableToolBar.Location = new System.Drawing.Point(3, 3);
            this.tableToolBar.Name = "tableToolBar";
            this.tableToolBar.RowCount = 1;
            this.tableToolBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableToolBar.Size = new System.Drawing.Size(674, 34);
            this.tableToolBar.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbbShowAs);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(171, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(162, 28);
            this.panel2.TabIndex = 4;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.btnSaveFile);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(507, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(164, 28);
            this.panel8.TabIndex = 3;
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSaveFile.Location = new System.Drawing.Point(0, 0);
            this.btnSaveFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(164, 28);
            this.btnSaveFile.TabIndex = 1;
            this.btnSaveFile.Text = "Save Text";
            this.btnSaveFile.UseVisualStyleBackColor = true;
            this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.btnShowAllText);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(339, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(162, 28);
            this.panel7.TabIndex = 2;
            // 
            // btnShowAllText
            // 
            this.btnShowAllText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnShowAllText.Location = new System.Drawing.Point(0, 0);
            this.btnShowAllText.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnShowAllText.Name = "btnShowAllText";
            this.btnShowAllText.Size = new System.Drawing.Size(162, 28);
            this.btnShowAllText.TabIndex = 0;
            this.btnShowAllText.Text = "Show all";
            this.btnShowAllText.UseVisualStyleBackColor = true;
            this.btnShowAllText.Click += new System.EventHandler(this.btnShowAllText_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbbEncoding);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(162, 28);
            this.panel1.TabIndex = 0;
            // 
            // cbbEncoding
            // 
            this.cbbEncoding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbEncoding.Font = new System.Drawing.Font("SimSun", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbbEncoding.FormattingEnabled = true;
            this.cbbEncoding.Location = new System.Drawing.Point(0, 0);
            this.cbbEncoding.Name = "cbbEncoding";
            this.cbbEncoding.Size = new System.Drawing.Size(162, 26);
            this.cbbEncoding.TabIndex = 1;
            this.cbbEncoding.SelectedIndexChanged += new System.EventHandler(this.cbbEncoding_SelectedIndexChanged);
            // 
            // cbbShowAs
            // 
            this.cbbShowAs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbShowAs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbShowAs.Font = new System.Drawing.Font("SimSun", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbbShowAs.FormattingEnabled = true;
            this.cbbShowAs.Items.AddRange(new object[] {
            "TXT",
            "XML"});
            this.cbbShowAs.Location = new System.Drawing.Point(0, 0);
            this.cbbShowAs.Name = "cbbShowAs";
            this.cbbShowAs.Size = new System.Drawing.Size(162, 26);
            this.cbbShowAs.TabIndex = 2;
            this.cbbShowAs.SelectedIndexChanged += new System.EventHandler(this.cbbShowAs_SelectedIndexChanged);
            // 
            // ShowAsText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableMain);
            this.Name = "ShowAsText";
            this.Size = new System.Drawing.Size(680, 400);
            this.tableMain.ResumeLayout(false);
            this.tableMain.PerformLayout();
            this.tableToolBar.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableMain;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.TableLayoutPanel tableToolBar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbbEncoding;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnShowAllText;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btnSaveFile;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbbShowAs;
    }
}
