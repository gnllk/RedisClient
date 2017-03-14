namespace Gnllk.RControl
{
    partial class XmlForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XmlForm));
            this.xmlBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // xmlBrowser
            // 
            this.xmlBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xmlBrowser.Location = new System.Drawing.Point(0, 0);
            this.xmlBrowser.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.xmlBrowser.MinimumSize = new System.Drawing.Size(27, 23);
            this.xmlBrowser.Name = "xmlBrowser";
            this.xmlBrowser.Size = new System.Drawing.Size(782, 435);
            this.xmlBrowser.TabIndex = 0;
            // 
            // XmlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 435);
            this.Controls.Add(this.xmlBrowser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "XmlForm";
            this.Text = "Xml";
            this.Load += new System.EventHandler(this.XmlForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.XmlForm_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser xmlBrowser;
    }
}