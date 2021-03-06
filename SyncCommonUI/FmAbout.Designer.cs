namespace Fonlow.SyncML.Windows
{
    partial class UCAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCAbout));
            this.button1 = new System.Windows.Forms.Button();
            this.lbCopyright = new System.Windows.Forms.Label();
            this.lbWeb = new System.Windows.Forms.LinkLabel();
            this.lbProductName = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbVersion = new System.Windows.Forms.Label();
            this.lbDeviceId = new System.Windows.Forms.Label();
            this.lbVersion2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(177, 205);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbCopyright
            // 
            this.lbCopyright.AutoSize = true;
            this.lbCopyright.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCopyright.Location = new System.Drawing.Point(60, 148);
            this.lbCopyright.Name = "lbCopyright";
            this.lbCopyright.Size = new System.Drawing.Size(227, 17);
            this.lbCopyright.TabIndex = 10;
            this.lbCopyright.Text = "Copyright ©  2008-2009  Fonlow IT";
            // 
            // lbWeb
            // 
            this.lbWeb.AutoSize = true;
            this.lbWeb.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbWeb.Location = new System.Drawing.Point(60, 113);
            this.lbWeb.Name = "lbWeb";
            this.lbWeb.Size = new System.Drawing.Size(234, 17);
            this.lbWeb.TabIndex = 9;
            this.lbWeb.TabStop = true;
            // 
            // lbProductName
            // 
            this.lbProductName.AutoSize = true;
            this.lbProductName.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProductName.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lbProductName.Location = new System.Drawing.Point(51, 13);
            this.lbProductName.Name = "lbProductName";
            this.lbProductName.Size = new System.Drawing.Size(307, 22);
            this.lbProductName.TabIndex = 8;
            this.lbProductName.Text = "SyncML Client for LocalDatasource";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(6, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(39, 41);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // lbVersion
            // 
            this.lbVersion.AutoSize = true;
            this.lbVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbVersion.Location = new System.Drawing.Point(252, 58);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(68, 15);
            this.lbVersion.TabIndex = 12;
            this.lbVersion.Text = "Version 1.3";
            // 
            // lbDeviceId
            // 
            this.lbDeviceId.AutoSize = true;
            this.lbDeviceId.Location = new System.Drawing.Point(60, 182);
            this.lbDeviceId.Name = "lbDeviceId";
            this.lbDeviceId.Size = new System.Drawing.Size(35, 13);
            this.lbDeviceId.TabIndex = 13;
            this.lbDeviceId.Text = "label1";
            // 
            // lbVersion2
            // 
            this.lbVersion2.AutoSize = true;
            this.lbVersion2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbVersion2.Location = new System.Drawing.Point(252, 83);
            this.lbVersion2.Name = "lbVersion2";
            this.lbVersion2.Size = new System.Drawing.Size(68, 15);
            this.lbVersion2.TabIndex = 14;
            this.lbVersion2.Text = "Version 1.3";
            // 
            // UCAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Controls.Add(this.lbVersion2);
            this.Controls.Add(this.lbDeviceId);
            this.Controls.Add(this.lbVersion);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbCopyright);
            this.Controls.Add(this.lbWeb);
            this.Controls.Add(this.lbProductName);
            this.Controls.Add(this.pictureBox1);
            this.Name = "UCAbout";
            this.Size = new System.Drawing.Size(429, 241);
            this.Load += new System.EventHandler(this.FmAbout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbCopyright;
        private System.Windows.Forms.LinkLabel lbWeb;
        private System.Windows.Forms.Label lbProductName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbVersion;
        private System.Windows.Forms.Label lbDeviceId;
        private System.Windows.Forms.Label lbVersion2;
    }
}
