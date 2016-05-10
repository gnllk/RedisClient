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
            this.xmlBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // xmlBrowser
            // 
            this.xmlBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xmlBrowser.Location = new System.Drawing.Point(0, 0);
            this.xmlBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.xmlBrowser.Name = "xmlBrowser";
            this.xmlBrowser.Size = new System.Drawing.Size(784, 442);
            this.xmlBrowser.TabIndex = 0;
            // 
            // XmlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 442);
            this.Controls.Add(this.xmlBrowser);
            this.Name = "XmlForm";
            this.ShowIcon = false;
            this.Text = "Xml";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser xmlBrowser;
    }
}