namespace Gnllk.RControl
{
    partial class PluginsManageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginsManageForm));
            this.pluginsMenuStrip = new System.Windows.Forms.MenuStrip();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.pluginsList = new System.Windows.Forms.DataGridView();
            this.tableDetails = new System.Windows.Forms.TableLayoutPanel();
            this.panel22 = new System.Windows.Forms.Panel();
            this.btnInstall = new System.Windows.Forms.Button();
            this.panel20 = new System.Windows.Forms.Panel();
            this.labAuthor = new System.Windows.Forms.Label();
            this.panel17 = new System.Windows.Forms.Panel();
            this.panel18 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel15 = new System.Windows.Forms.Panel();
            this.labDate = new System.Windows.Forms.Label();
            this.panel13 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labName = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.labVersion = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.labDescription = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.picPluginSreenshot = new System.Windows.Forms.PictureBox();
            this.pluginStatusBar = new System.Windows.Forms.StatusStrip();
            this.PluginIcon = new System.Windows.Forms.DataGridViewImageColumn();
            this.PluginName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.State = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Enable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pluginsList)).BeginInit();
            this.tableDetails.SuspendLayout();
            this.panel22.SuspendLayout();
            this.panel20.SuspendLayout();
            this.panel17.SuspendLayout();
            this.panel18.SuspendLayout();
            this.panel15.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPluginSreenshot)).BeginInit();
            this.SuspendLayout();
            // 
            // pluginsMenuStrip
            // 
            this.pluginsMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.pluginsMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.pluginsMenuStrip.Name = "pluginsMenuStrip";
            this.pluginsMenuStrip.Size = new System.Drawing.Size(982, 24);
            this.pluginsMenuStrip.TabIndex = 1;
            this.pluginsMenuStrip.Text = "pluginsMenuStrip";
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 24);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.pluginsList);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.tableDetails);
            this.splitContainerMain.Size = new System.Drawing.Size(982, 531);
            this.splitContainerMain.SplitterDistance = 623;
            this.splitContainerMain.TabIndex = 2;
            // 
            // pluginsList
            // 
            this.pluginsList.AllowUserToAddRows = false;
            this.pluginsList.AllowUserToDeleteRows = false;
            this.pluginsList.AllowUserToResizeRows = false;
            this.pluginsList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.pluginsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.pluginsList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PluginIcon,
            this.PluginName,
            this.State,
            this.Enable});
            this.pluginsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pluginsList.Location = new System.Drawing.Point(0, 0);
            this.pluginsList.MultiSelect = false;
            this.pluginsList.Name = "pluginsList";
            this.pluginsList.RowHeadersVisible = false;
            this.pluginsList.RowHeadersWidth = 30;
            this.pluginsList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.pluginsList.RowTemplate.Height = 27;
            this.pluginsList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.pluginsList.Size = new System.Drawing.Size(623, 531);
            this.pluginsList.TabIndex = 1;
            this.pluginsList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.pluginsList_CellClick);
            this.pluginsList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.pluginsList_CellContentClick);
            this.pluginsList.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.pluginsList_RowEnter);
            this.pluginsList.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.pluginsList_RowsAdded);
            // 
            // tableDetails
            // 
            this.tableDetails.ColumnCount = 1;
            this.tableDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableDetails.Controls.Add(this.panel22, 0, 10);
            this.tableDetails.Controls.Add(this.panel20, 0, 9);
            this.tableDetails.Controls.Add(this.panel17, 0, 8);
            this.tableDetails.Controls.Add(this.panel15, 0, 7);
            this.tableDetails.Controls.Add(this.panel13, 0, 6);
            this.tableDetails.Controls.Add(this.panel3, 0, 1);
            this.tableDetails.Controls.Add(this.panel9, 0, 4);
            this.tableDetails.Controls.Add(this.panel7, 0, 3);
            this.tableDetails.Controls.Add(this.panel5, 0, 2);
            this.tableDetails.Controls.Add(this.panel1, 0, 0);
            this.tableDetails.Controls.Add(this.panel11, 0, 5);
            this.tableDetails.Controls.Add(this.panel2, 0, 11);
            this.tableDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableDetails.Location = new System.Drawing.Point(0, 0);
            this.tableDetails.Name = "tableDetails";
            this.tableDetails.RowCount = 12;
            this.tableDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableDetails.Size = new System.Drawing.Size(355, 531);
            this.tableDetails.TabIndex = 2;
            // 
            // panel22
            // 
            this.panel22.Controls.Add(this.btnInstall);
            this.panel22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel22.Location = new System.Drawing.Point(3, 323);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(349, 26);
            this.panel22.TabIndex = 39;
            // 
            // btnInstall
            // 
            this.btnInstall.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnInstall.Location = new System.Drawing.Point(0, 0);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(349, 26);
            this.btnInstall.TabIndex = 0;
            this.btnInstall.Text = "Install";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel20
            // 
            this.panel20.Controls.Add(this.labAuthor);
            this.panel20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel20.Location = new System.Drawing.Point(3, 291);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(349, 26);
            this.panel20.TabIndex = 37;
            // 
            // labAuthor
            // 
            this.labAuthor.AutoSize = true;
            this.labAuthor.Location = new System.Drawing.Point(5, 8);
            this.labAuthor.Name = "labAuthor";
            this.labAuthor.Size = new System.Drawing.Size(31, 15);
            this.labAuthor.TabIndex = 0;
            this.labAuthor.Text = "***";
            // 
            // panel17
            // 
            this.panel17.Controls.Add(this.panel18);
            this.panel17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel17.Location = new System.Drawing.Point(3, 259);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(349, 26);
            this.panel17.TabIndex = 35;
            // 
            // panel18
            // 
            this.panel18.Controls.Add(this.label5);
            this.panel18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel18.Location = new System.Drawing.Point(0, 0);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(349, 26);
            this.panel18.TabIndex = 30;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(5, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Author:";
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.labDate);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel15.Location = new System.Drawing.Point(3, 227);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(349, 26);
            this.panel15.TabIndex = 33;
            // 
            // labDate
            // 
            this.labDate.AutoSize = true;
            this.labDate.Location = new System.Drawing.Point(5, 8);
            this.labDate.Name = "labDate";
            this.labDate.Size = new System.Drawing.Size(31, 15);
            this.labDate.TabIndex = 0;
            this.labDate.Text = "***";
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.label4);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel13.Location = new System.Drawing.Point(3, 195);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(349, 26);
            this.panel13.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(5, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Date:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.labName);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 35);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(349, 26);
            this.panel3.TabIndex = 28;
            // 
            // labName
            // 
            this.labName.AutoSize = true;
            this.labName.Location = new System.Drawing.Point(5, 8);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(31, 15);
            this.labName.TabIndex = 0;
            this.labName.Text = "***";
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.label3);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(3, 131);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(349, 26);
            this.panel9.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(5, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Description:";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.labVersion);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(3, 99);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(349, 26);
            this.panel7.TabIndex = 14;
            // 
            // labVersion
            // 
            this.labVersion.AutoSize = true;
            this.labVersion.Location = new System.Drawing.Point(5, 8);
            this.labVersion.Name = "labVersion";
            this.labVersion.Size = new System.Drawing.Size(31, 15);
            this.labVersion.TabIndex = 0;
            this.labVersion.Text = "***";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 67);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(349, 26);
            this.panel5.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(5, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Version:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(349, 26);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(5, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.labDescription);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(3, 163);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(349, 26);
            this.panel11.TabIndex = 29;
            // 
            // labDescription
            // 
            this.labDescription.AutoSize = true;
            this.labDescription.Location = new System.Drawing.Point(5, 8);
            this.labDescription.Name = "labDescription";
            this.labDescription.Size = new System.Drawing.Size(31, 15);
            this.labDescription.TabIndex = 0;
            this.labDescription.Text = "***";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.picPluginSreenshot);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 355);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(349, 173);
            this.panel2.TabIndex = 40;
            // 
            // picPluginSreenshot
            // 
            this.picPluginSreenshot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPluginSreenshot.Location = new System.Drawing.Point(0, 0);
            this.picPluginSreenshot.Name = "picPluginSreenshot";
            this.picPluginSreenshot.Size = new System.Drawing.Size(349, 173);
            this.picPluginSreenshot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picPluginSreenshot.TabIndex = 0;
            this.picPluginSreenshot.TabStop = false;
            // 
            // pluginStatusBar
            // 
            this.pluginStatusBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.pluginStatusBar.Location = new System.Drawing.Point(0, 533);
            this.pluginStatusBar.Name = "pluginStatusBar";
            this.pluginStatusBar.Size = new System.Drawing.Size(982, 22);
            this.pluginStatusBar.TabIndex = 3;
            this.pluginStatusBar.Text = "statusStrip1";
            // 
            // PluginIcon
            // 
            this.PluginIcon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.PluginIcon.DataPropertyName = "Icon";
            this.PluginIcon.HeaderText = "Icon";
            this.PluginIcon.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.PluginIcon.Name = "PluginIcon";
            this.PluginIcon.ReadOnly = true;
            this.PluginIcon.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PluginIcon.Width = 60;
            // 
            // PluginName
            // 
            this.PluginName.DataPropertyName = "Name";
            this.PluginName.FillWeight = 24.81752F;
            this.PluginName.HeaderText = "Name";
            this.PluginName.Name = "PluginName";
            this.PluginName.ReadOnly = true;
            // 
            // State
            // 
            this.State.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.State.DataPropertyName = "State";
            this.State.FillWeight = 175.1825F;
            this.State.HeaderText = "State";
            this.State.Name = "State";
            this.State.ReadOnly = true;
            this.State.Width = 120;
            // 
            // Enable
            // 
            this.Enable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Enable.HeaderText = "Enable";
            this.Enable.Name = "Enable";
            this.Enable.Width = 60;
            // 
            // PluginsManageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 555);
            this.Controls.Add(this.pluginStatusBar);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.pluginsMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.pluginsMenuStrip;
            this.Name = "PluginsManageForm";
            this.Text = "Plugins Manager";
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pluginsList)).EndInit();
            this.tableDetails.ResumeLayout(false);
            this.panel22.ResumeLayout(false);
            this.panel20.ResumeLayout(false);
            this.panel20.PerformLayout();
            this.panel17.ResumeLayout(false);
            this.panel18.ResumeLayout(false);
            this.panel18.PerformLayout();
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPluginSreenshot)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip pluginsMenuStrip;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.DataGridView pluginsList;
        private System.Windows.Forms.TableLayoutPanel tableDetails;
        private System.Windows.Forms.Panel panel22;
        private System.Windows.Forms.Panel panel20;
        private System.Windows.Forms.Label labAuthor;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Panel panel18;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Label labDate;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label labName;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label labVersion;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label labDescription;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox picPluginSreenshot;
        private System.Windows.Forms.StatusStrip pluginStatusBar;
        private System.Windows.Forms.DataGridViewImageColumn PluginIcon;
        private System.Windows.Forms.DataGridViewTextBoxColumn PluginName;
        private System.Windows.Forms.DataGridViewTextBoxColumn State;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Enable;
    }
}