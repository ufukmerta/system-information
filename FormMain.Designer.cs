namespace WFASystemInformation
{
    partial class FormMain
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.rTxt_Information = new System.Windows.Forms.RichTextBox();
            this.btn_Detailed = new System.Windows.Forms.Button();
            this.btn_Simple = new System.Windows.Forms.Button();
            this.btn_AllSettings = new System.Windows.Forms.Button();
            this.btn_SimpleSettings = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rTxt_Information
            // 
            this.rTxt_Information.Location = new System.Drawing.Point(12, 12);
            this.rTxt_Information.Name = "rTxt_Information";
            this.rTxt_Information.Size = new System.Drawing.Size(375, 343);
            this.rTxt_Information.TabIndex = 13;
            this.rTxt_Information.Text = "Get System Information";
            // 
            // btn_Detailed
            // 
            this.btn_Detailed.Location = new System.Drawing.Point(12, 422);
            this.btn_Detailed.Name = "btn_Detailed";
            this.btn_Detailed.Size = new System.Drawing.Size(314, 55);
            this.btn_Detailed.TabIndex = 14;
            this.btn_Detailed.Text = "Get Detailed Information";
            this.btn_Detailed.UseVisualStyleBackColor = true;
            this.btn_Detailed.Click += new System.EventHandler(this.btn_Detailed_Click);
            // 
            // btn_Simple
            // 
            this.btn_Simple.Location = new System.Drawing.Point(12, 361);
            this.btn_Simple.Name = "btn_Simple";
            this.btn_Simple.Size = new System.Drawing.Size(314, 55);
            this.btn_Simple.TabIndex = 15;
            this.btn_Simple.Text = "Get Simple Information";
            this.btn_Simple.UseVisualStyleBackColor = true;
            this.btn_Simple.Click += new System.EventHandler(this.btn_Simple_Click);
            // 
            // btn_AllSettings
            // 
            this.btn_AllSettings.Image = global::WFASystemInformation.Properties.Resources.setting;
            this.btn_AllSettings.Location = new System.Drawing.Point(332, 422);
            this.btn_AllSettings.Name = "btn_AllSettings";
            this.btn_AllSettings.Size = new System.Drawing.Size(55, 55);
            this.btn_AllSettings.TabIndex = 17;
            this.btn_AllSettings.UseVisualStyleBackColor = true;
            this.btn_AllSettings.Click += new System.EventHandler(this.btn_DetailedSettings_Click);
            // 
            // btn_SimpleSettings
            // 
            this.btn_SimpleSettings.Image = global::WFASystemInformation.Properties.Resources.setting;
            this.btn_SimpleSettings.Location = new System.Drawing.Point(332, 361);
            this.btn_SimpleSettings.Name = "btn_SimpleSettings";
            this.btn_SimpleSettings.Size = new System.Drawing.Size(55, 55);
            this.btn_SimpleSettings.TabIndex = 16;
            this.btn_SimpleSettings.UseVisualStyleBackColor = true;
            this.btn_SimpleSettings.Click += new System.EventHandler(this.btn_SimpleSettings_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 489);
            this.Controls.Add(this.btn_AllSettings);
            this.Controls.Add(this.btn_SimpleSettings);
            this.Controls.Add(this.btn_Simple);
            this.Controls.Add(this.btn_Detailed);
            this.Controls.Add(this.rTxt_Information);
            this.Name = "FormMain";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.RichTextBox rTxt_Information;
        private System.Windows.Forms.Button btn_Detailed;
        private System.Windows.Forms.Button btn_Simple;
        private System.Windows.Forms.Button btn_SimpleSettings;
        private System.Windows.Forms.Button btn_AllSettings;
    }
}

