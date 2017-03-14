namespace Gnllk.RedisClient
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.labCurrentKey = new System.Windows.Forms.ToolStripStatusLabel();
            this.labInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.tableDataTree = new System.Windows.Forms.TableLayoutPanel();
            this.tableAddConnect = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtEndPiont = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnAddConnect = new System.Windows.Forms.Button();
            this.panelTree = new System.Windows.Forms.Panel();
            this.tvDataTree = new System.Windows.Forms.TreeView();
            this.TreeImageList = new System.Windows.Forms.ImageList(this.components);
            this.tableDataEdit = new System.Windows.Forms.TableLayoutPanel();
            this.tableDataEditTop = new System.Windows.Forms.TableLayoutPanel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnAboutMe = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.cbbShowAs = new System.Windows.Forms.ComboBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnPluginsManage = new System.Windows.Forms.Button();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btnExportFile = new System.Windows.Forms.Button();
            this.pluginViewBox = new System.Windows.Forms.Panel();
            this.StatusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.tableDataTree.SuspendLayout();
            this.tableAddConnect.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panelTree.SuspendLayout();
            this.tableDataEdit.SuspendLayout();
            this.tableDataEditTop.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusBar
            // 
            this.StatusBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar,
            this.labCurrentKey,
            this.labInfo});
            this.StatusBar.Location = new System.Drawing.Point(0, 596);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Padding = new System.Windows.Forms.Padding(1, 0, 13, 0);
            this.StatusBar.Size = new System.Drawing.Size(1123, 25);
            this.StatusBar.TabIndex = 0;
            this.StatusBar.Text = "StatusBar";
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 19);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // labCurrentKey
            // 
            this.labCurrentKey.Name = "labCurrentKey";
            this.labCurrentKey.Size = new System.Drawing.Size(95, 20);
            this.labCurrentKey.Text = "Current Key";
            // 
            // labInfo
            // 
            this.labInfo.Name = "labInfo";
            this.labInfo.Size = new System.Drawing.Size(88, 20);
            this.labInfo.Text = "Infomation";
            // 
            // splitMain
            // 
            this.splitMain.BackColor = System.Drawing.Color.LightGray;
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.tableDataTree);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.tableDataEdit);
            this.splitMain.Size = new System.Drawing.Size(1123, 596);
            this.splitMain.SplitterDistance = 506;
            this.splitMain.TabIndex = 1;
            // 
            // tableDataTree
            // 
            this.tableDataTree.BackColor = System.Drawing.Color.LightGray;
            this.tableDataTree.ColumnCount = 1;
            this.tableDataTree.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableDataTree.Controls.Add(this.tableAddConnect, 0, 0);
            this.tableDataTree.Controls.Add(this.panelTree, 0, 1);
            this.tableDataTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableDataTree.Location = new System.Drawing.Point(0, 0);
            this.tableDataTree.Name = "tableDataTree";
            this.tableDataTree.RowCount = 2;
            this.tableDataTree.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableDataTree.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableDataTree.Size = new System.Drawing.Size(506, 596);
            this.tableDataTree.TabIndex = 0;
            // 
            // tableAddConnect
            // 
            this.tableAddConnect.ColumnCount = 5;
            this.tableAddConnect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableAddConnect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableAddConnect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableAddConnect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableAddConnect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableAddConnect.Controls.Add(this.panel1, 0, 0);
            this.tableAddConnect.Controls.Add(this.panel2, 1, 0);
            this.tableAddConnect.Controls.Add(this.panel3, 2, 0);
            this.tableAddConnect.Controls.Add(this.panel4, 3, 0);
            this.tableAddConnect.Controls.Add(this.panel5, 4, 0);
            this.tableAddConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableAddConnect.Location = new System.Drawing.Point(3, 3);
            this.tableAddConnect.Name = "tableAddConnect";
            this.tableAddConnect.RowCount = 1;
            this.tableAddConnect.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableAddConnect.Size = new System.Drawing.Size(500, 34);
            this.tableAddConnect.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(74, 28);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "EndPoint";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtEndPiont);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(83, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(134, 28);
            this.panel2.TabIndex = 1;
            // 
            // txtEndPiont
            // 
            this.txtEndPiont.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEndPiont.Location = new System.Drawing.Point(0, 0);
            this.txtEndPiont.Name = "txtEndPiont";
            this.txtEndPiont.Size = new System.Drawing.Size(134, 25);
            this.txtEndPiont.TabIndex = 0;
            this.txtEndPiont.Text = "localhost:6379";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(223, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(74, 28);
            this.panel3.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Password";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtPassword);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(303, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(94, 28);
            this.panel4.TabIndex = 3;
            // 
            // txtPassword
            // 
            this.txtPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPassword.Location = new System.Drawing.Point(0, 0);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(94, 25);
            this.txtPassword.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnAddConnect);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(403, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(94, 28);
            this.panel5.TabIndex = 4;
            // 
            // btnAddConnect
            // 
            this.btnAddConnect.BackColor = System.Drawing.Color.Transparent;
            this.btnAddConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddConnect.Location = new System.Drawing.Point(0, 0);
            this.btnAddConnect.Name = "btnAddConnect";
            this.btnAddConnect.Size = new System.Drawing.Size(94, 28);
            this.btnAddConnect.TabIndex = 0;
            this.btnAddConnect.Text = "Add";
            this.btnAddConnect.UseVisualStyleBackColor = false;
            this.btnAddConnect.Click += new System.EventHandler(this.btnAddConnect_Click);
            // 
            // panelTree
            // 
            this.panelTree.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelTree.Controls.Add(this.tvDataTree);
            this.panelTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTree.Location = new System.Drawing.Point(0, 40);
            this.panelTree.Margin = new System.Windows.Forms.Padding(0);
            this.panelTree.Name = "panelTree";
            this.panelTree.Size = new System.Drawing.Size(506, 556);
            this.panelTree.TabIndex = 1;
            // 
            // tvDataTree
            // 
            this.tvDataTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvDataTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvDataTree.ImageIndex = 0;
            this.tvDataTree.ImageList = this.TreeImageList;
            this.tvDataTree.Location = new System.Drawing.Point(0, 0);
            this.tvDataTree.Name = "tvDataTree";
            this.tvDataTree.SelectedImageIndex = 0;
            this.tvDataTree.Size = new System.Drawing.Size(502, 552);
            this.tvDataTree.TabIndex = 1;
            this.tvDataTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvDataTree_NodeMouseClick);
            this.tvDataTree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvDataTree_KeyDown);
            this.tvDataTree.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tvDataTree_KeyPress);
            // 
            // TreeImageList
            // 
            this.TreeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("TreeImageList.ImageStream")));
            this.TreeImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.TreeImageList.Images.SetKeyName(0, "redis_client.png");
            this.TreeImageList.Images.SetKeyName(1, "server.png");
            this.TreeImageList.Images.SetKeyName(2, "database.png");
            this.TreeImageList.Images.SetKeyName(3, "content.png");
            // 
            // tableDataEdit
            // 
            this.tableDataEdit.BackColor = System.Drawing.Color.LightGray;
            this.tableDataEdit.ColumnCount = 1;
            this.tableDataEdit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableDataEdit.Controls.Add(this.tableDataEditTop, 0, 0);
            this.tableDataEdit.Controls.Add(this.pluginViewBox, 0, 1);
            this.tableDataEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableDataEdit.Location = new System.Drawing.Point(0, 0);
            this.tableDataEdit.Name = "tableDataEdit";
            this.tableDataEdit.RowCount = 2;
            this.tableDataEdit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableDataEdit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableDataEdit.Size = new System.Drawing.Size(613, 596);
            this.tableDataEdit.TabIndex = 0;
            // 
            // tableDataEditTop
            // 
            this.tableDataEditTop.ColumnCount = 4;
            this.tableDataEditTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableDataEditTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableDataEditTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableDataEditTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableDataEditTop.Controls.Add(this.panel6, 0, 0);
            this.tableDataEditTop.Controls.Add(this.panel7, 1, 0);
            this.tableDataEditTop.Controls.Add(this.panel8, 2, 0);
            this.tableDataEditTop.Controls.Add(this.panel9, 3, 0);
            this.tableDataEditTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableDataEditTop.Location = new System.Drawing.Point(3, 3);
            this.tableDataEditTop.Name = "tableDataEditTop";
            this.tableDataEditTop.RowCount = 1;
            this.tableDataEditTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableDataEditTop.Size = new System.Drawing.Size(607, 34);
            this.tableDataEditTop.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnAboutMe);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(145, 28);
            this.panel6.TabIndex = 0;
            // 
            // btnAboutMe
            // 
            this.btnAboutMe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAboutMe.Location = new System.Drawing.Point(0, 0);
            this.btnAboutMe.Name = "btnAboutMe";
            this.btnAboutMe.Size = new System.Drawing.Size(145, 28);
            this.btnAboutMe.TabIndex = 0;
            this.btnAboutMe.Text = "About me";
            this.btnAboutMe.UseVisualStyleBackColor = true;
            this.btnAboutMe.Click += new System.EventHandler(this.btnAboutMe_Click);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.cbbShowAs);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(154, 4);
            this.panel7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 2);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(145, 28);
            this.panel7.TabIndex = 1;
            // 
            // cbbShowAs
            // 
            this.cbbShowAs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbShowAs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbShowAs.Font = new System.Drawing.Font("SimSun", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbbShowAs.FormattingEnabled = true;
            this.cbbShowAs.Items.AddRange(new object[] {
            "Auto"});
            this.cbbShowAs.Location = new System.Drawing.Point(0, 0);
            this.cbbShowAs.Name = "cbbShowAs";
            this.cbbShowAs.Size = new System.Drawing.Size(145, 26);
            this.cbbShowAs.TabIndex = 2;
            this.cbbShowAs.SelectedIndexChanged += new System.EventHandler(this.cbbShowAs_SelectedIndexChanged);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.btnPluginsManage);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(305, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(145, 28);
            this.panel8.TabIndex = 2;
            // 
            // btnPluginsManage
            // 
            this.btnPluginsManage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPluginsManage.Location = new System.Drawing.Point(0, 0);
            this.btnPluginsManage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnPluginsManage.Name = "btnPluginsManage";
            this.btnPluginsManage.Size = new System.Drawing.Size(145, 28);
            this.btnPluginsManage.TabIndex = 1;
            this.btnPluginsManage.Text = "Plugins manage";
            this.btnPluginsManage.UseVisualStyleBackColor = true;
            this.btnPluginsManage.Click += new System.EventHandler(this.btnPluginsManage_Click);
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.btnExportFile);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(456, 3);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(148, 28);
            this.panel9.TabIndex = 3;
            // 
            // btnExportFile
            // 
            this.btnExportFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExportFile.Location = new System.Drawing.Point(0, 0);
            this.btnExportFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnExportFile.Name = "btnExportFile";
            this.btnExportFile.Size = new System.Drawing.Size(148, 28);
            this.btnExportFile.TabIndex = 1;
            this.btnExportFile.Text = "Export file";
            this.btnExportFile.UseVisualStyleBackColor = true;
            this.btnExportFile.Click += new System.EventHandler(this.btnExportFile_Click);
            // 
            // pluginViewBox
            // 
            this.pluginViewBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pluginViewBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pluginViewBox.Location = new System.Drawing.Point(0, 40);
            this.pluginViewBox.Margin = new System.Windows.Forms.Padding(0);
            this.pluginViewBox.Name = "pluginViewBox";
            this.pluginViewBox.Size = new System.Drawing.Size(613, 556);
            this.pluginViewBox.TabIndex = 1;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1123, 621);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.StatusBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "Redis Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.tableDataTree.ResumeLayout(false);
            this.tableAddConnect.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panelTree.ResumeLayout(false);
            this.tableDataEdit.ResumeLayout(false);
            this.tableDataEditTop.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip StatusBar;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.TableLayoutPanel tableDataTree;
        private System.Windows.Forms.TableLayoutPanel tableAddConnect;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtEndPiont;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnAddConnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView tvDataTree;
        private System.Windows.Forms.TableLayoutPanel tableDataEdit;
        private System.Windows.Forms.TableLayoutPanel tableDataEditTop;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.ToolStripStatusLabel labCurrentKey;
        private System.Windows.Forms.ToolStripStatusLabel labInfo;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ImageList TreeImageList;
        private System.Windows.Forms.Panel pluginViewBox;
        private System.Windows.Forms.Button btnAboutMe;
        private System.Windows.Forms.Panel panelTree;
        private System.Windows.Forms.Button btnPluginsManage;
        private System.Windows.Forms.Button btnExportFile;
        private System.Windows.Forms.ComboBox cbbShowAs;
    }
}

