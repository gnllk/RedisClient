namespace Gnllk.RControl
{
    partial class ObjectViewer
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.objectTreeView = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // objectTreeView
            // 
            this.objectTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectTreeView.Location = new System.Drawing.Point(0, 0);
            this.objectTreeView.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.objectTreeView.Name = "objectTreeView";
            this.objectTreeView.Size = new System.Drawing.Size(320, 338);
            this.objectTreeView.TabIndex = 0;
            this.objectTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.objectTreeView_NodeMouseClick);
            this.objectTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.objectTreeView_NodeMouseDoubleClick);
            this.objectTreeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.objectTreeView_KeyDown);
            this.objectTreeView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.objectTreeView_KeyPress);
            // 
            // ObjectViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.objectTreeView);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "ObjectViewer";
            this.Size = new System.Drawing.Size(320, 338);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView objectTreeView;
    }
}
