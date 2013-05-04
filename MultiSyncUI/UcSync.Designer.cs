namespace Fonlow.SyncML.MultiSync
{
    partial class UcSync
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
            this.components = new System.ComponentModel.Container();
            this.progressBarReceiving = new System.Windows.Forms.ProgressBar();
            this.progressBarSending = new System.Windows.Forms.ProgressBar();
            this.btnSync = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.labelStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBarReceiving
            // 
            this.progressBarReceiving.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarReceiving.Location = new System.Drawing.Point(190, 3);
            this.progressBarReceiving.Name = "progressBarReceiving";
            this.progressBarReceiving.Size = new System.Drawing.Size(157, 23);
            this.progressBarReceiving.TabIndex = 5;
            this.progressBarReceiving.Visible = false;
            // 
            // progressBarSending
            // 
            this.progressBarSending.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarSending.Location = new System.Drawing.Point(190, 32);
            this.progressBarSending.Name = "progressBarSending";
            this.progressBarSending.Size = new System.Drawing.Size(157, 23);
            this.progressBarSending.TabIndex = 6;
            this.progressBarSending.Visible = false;
            // 
            // btnSync
            // 
            this.btnSync.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSync.Location = new System.Drawing.Point(3, 3);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(135, 50);
            this.btnSync.TabIndex = 7;
            this.btnSync.Text = "Sync";
            this.btnSync.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSync.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(144, 3);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(26, 23);
            this.btnStop.TabIndex = 9;
            this.toolTip1.SetToolTip(this.btnStop, "Stop sync gracefully");
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Visible = false;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(24, 56);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(35, 13);
            this.labelStatus.TabIndex = 10;
            this.labelStatus.Text = "label1";
            // 
            // UcSync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.progressBarSending);
            this.Controls.Add(this.progressBarReceiving);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "UcSync";
            this.Size = new System.Drawing.Size(372, 71);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarReceiving;
        private System.Windows.Forms.ProgressBar progressBarSending;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label labelStatus;


    }
}
