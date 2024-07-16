namespace WFASystemInformation
{
    partial class FormSimpleSettings
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
            this.clb_Simple = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // clb_Simple
            // 
            this.clb_Simple.CheckOnClick = true;
            this.clb_Simple.FormattingEnabled = true;
            this.clb_Simple.Items.AddRange(new object[] {
            "CPU",
            "GPU",
            "RAM",
            "Disk",
            "Operating System",
            "Operation System(Licence Key)",
            "Installed Apps",
            "Services",
            "MAC",
            "IP"});
            this.clb_Simple.Location = new System.Drawing.Point(12, 12);
            this.clb_Simple.Name = "clb_Simple";
            this.clb_Simple.Size = new System.Drawing.Size(179, 154);
            this.clb_Simple.TabIndex = 0;
            this.clb_Simple.MouseUp += new System.Windows.Forms.MouseEventHandler(this.clb_Simple_MouseUp);
            // 
            // FormSimpleSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(203, 177);
            this.Controls.Add(this.clb_Simple);
            this.Name = "FormSimpleSettings";
            this.Text = "FormSimpleSettings";
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.CheckedListBox clb_Simple;
    }
}