namespace Fonlow.SyncML.Windows
{
    partial class SyncMLForm
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
                logMemoListerner.Dispose(); 
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyncMLForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.syncToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.syncToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.slowSyncToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopSyncToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.syncFromClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshFromClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.syncFromServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshFromServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayLogWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.edStatus = new System.Windows.Forms.TextBox();
            this.btnSync = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lbProgress = new System.Windows.Forms.Label();
            this.lbProgressReceiving = new System.Windows.Forms.Label();
            this.progressBarReceiving = new System.Windows.Forms.ProgressBar();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.syncToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(355, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // syncToolStripMenuItem
            // 
            this.syncToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.syncToolStripMenuItem1,
            this.slowSyncToolStripMenuItem,
            this.pingToolStripMenuItem,
            this.stopSyncToolStripMenuItem,
            this.syncFromClientToolStripMenuItem,
            this.refreshFromClientToolStripMenuItem,
            this.syncFromServerToolStripMenuItem,
            this.refreshFromServerToolStripMenuItem});
            this.syncToolStripMenuItem.Name = "syncToolStripMenuItem";
            this.syncToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.syncToolStripMenuItem.Text = "Sync";
            // 
            // syncToolStripMenuItem1
            // 
            this.syncToolStripMenuItem1.Name = "syncToolStripMenuItem1";
            this.syncToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.syncToolStripMenuItem1.Size = new System.Drawing.Size(312, 22);
            this.syncToolStripMenuItem1.Text = "Sync";
            this.syncToolStripMenuItem1.Click += new System.EventHandler(this.syncToolStripMenuItem1_Click);
            // 
            // slowSyncToolStripMenuItem
            // 
            this.slowSyncToolStripMenuItem.Name = "slowSyncToolStripMenuItem";
            this.slowSyncToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.slowSyncToolStripMenuItem.Size = new System.Drawing.Size(312, 22);
            this.slowSyncToolStripMenuItem.Text = "Slow Sync";
            this.slowSyncToolStripMenuItem.Click += new System.EventHandler(this.slowSyncToolStripMenuItem_Click_1);
            // 
            // pingToolStripMenuItem
            // 
            this.pingToolStripMenuItem.Name = "pingToolStripMenuItem";
            this.pingToolStripMenuItem.Size = new System.Drawing.Size(312, 22);
            this.pingToolStripMenuItem.Text = "Reset Last Anchor";
            this.pingToolStripMenuItem.Visible = false;
            this.pingToolStripMenuItem.Click += new System.EventHandler(this.pingToolStripMenuItem_Click);
            // 
            // stopSyncToolStripMenuItem
            // 
            this.stopSyncToolStripMenuItem.Enabled = false;
            this.stopSyncToolStripMenuItem.Name = "stopSyncToolStripMenuItem";
            this.stopSyncToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.stopSyncToolStripMenuItem.Size = new System.Drawing.Size(312, 22);
            this.stopSyncToolStripMenuItem.Text = "Stop Sync";
            this.stopSyncToolStripMenuItem.Click += new System.EventHandler(this.stopSyncToolStripMenuItem_Click);
            // 
            // syncFromClientToolStripMenuItem
            // 
            this.syncFromClientToolStripMenuItem.Name = "syncFromClientToolStripMenuItem";
            this.syncFromClientToolStripMenuItem.Size = new System.Drawing.Size(312, 22);
            this.syncFromClientToolStripMenuItem.Text = "One-Way Sync from Client";
            this.syncFromClientToolStripMenuItem.Click += new System.EventHandler(this.syncFromClientToolStripMenuItem_Click);
            // 
            // refreshFromClientToolStripMenuItem
            // 
            this.refreshFromClientToolStripMenuItem.Name = "refreshFromClientToolStripMenuItem";
            this.refreshFromClientToolStripMenuItem.Size = new System.Drawing.Size(312, 22);
            this.refreshFromClientToolStripMenuItem.Text = "Replace All of the Server Data with Local Data";
            this.refreshFromClientToolStripMenuItem.Click += new System.EventHandler(this.refreshFromClientToolStripMenuItem_Click);
            // 
            // syncFromServerToolStripMenuItem
            // 
            this.syncFromServerToolStripMenuItem.Name = "syncFromServerToolStripMenuItem";
            this.syncFromServerToolStripMenuItem.Size = new System.Drawing.Size(312, 22);
            this.syncFromServerToolStripMenuItem.Text = "One-Way Sync from Server";
            this.syncFromServerToolStripMenuItem.Click += new System.EventHandler(this.syncFromServerToolStripMenuItem_Click);
            // 
            // refreshFromServerToolStripMenuItem
            // 
            this.refreshFromServerToolStripMenuItem.Name = "refreshFromServerToolStripMenuItem";
            this.refreshFromServerToolStripMenuItem.Size = new System.Drawing.Size(312, 22);
            this.refreshFromServerToolStripMenuItem.Text = "Replace All Local Data with  the Server Data";
            this.refreshFromServerToolStripMenuItem.Click += new System.EventHandler(this.refreshFromServerToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.displayLogWindowToolStripMenuItem,
            this.testFormToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            this.toolsToolStripMenuItem.Click += new System.EventHandler(this.toolsToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.settingsToolStripMenuItem.Text = "Preferences...";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // displayLogWindowToolStripMenuItem
            // 
            this.displayLogWindowToolStripMenuItem.Name = "displayLogWindowToolStripMenuItem";
            this.displayLogWindowToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.displayLogWindowToolStripMenuItem.Text = "Display Log Window";
            this.displayLogWindowToolStripMenuItem.Click += new System.EventHandler(this.displayLogWindowToolStripMenuItem_Click);
            // 
            // testFormToolStripMenuItem
            // 
            this.testFormToolStripMenuItem.Name = "testFormToolStripMenuItem";
            this.testFormToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.testFormToolStripMenuItem.Text = "TestForm";
            this.testFormToolStripMenuItem.Visible = false;
            this.testFormToolStripMenuItem.Click += new System.EventHandler(this.testFormToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // contentToolStripMenuItem
            // 
            this.contentToolStripMenuItem.Name = "contentToolStripMenuItem";
            this.contentToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.contentToolStripMenuItem.Text = "Contents";
            this.contentToolStripMenuItem.Click += new System.EventHandler(this.contentToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.aboutToolStripMenuItem.Text = "About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(89, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 58);
            this.button1.TabIndex = 9;
            this.button1.Text = "Preferences";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // edStatus
            // 
            this.edStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edStatus.Location = new System.Drawing.Point(6, 92);
            this.edStatus.Multiline = true;
            this.edStatus.Name = "edStatus";
            this.edStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.edStatus.Size = new System.Drawing.Size(342, 198);
            this.edStatus.TabIndex = 10;
            // 
            // btnSync
            // 
            this.btnSync.Image = ((System.Drawing.Image)(resources.GetObject("btnSync.Image")));
            this.btnSync.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSync.Location = new System.Drawing.Point(6, 28);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(77, 58);
            this.btnSync.TabIndex = 6;
            this.btnSync.Text = "Sync";
            this.btnSync.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // progressBar
            // 
            this.progressBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.progressBar.Location = new System.Drawing.Point(172, 74);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(171, 12);
            this.progressBar.TabIndex = 11;
            this.progressBar.Visible = false;
            // 
            // lbProgress
            // 
            this.lbProgress.AutoSize = true;
            this.lbProgress.Location = new System.Drawing.Point(173, 58);
            this.lbProgress.Name = "lbProgress";
            this.lbProgress.Size = new System.Drawing.Size(34, 13);
            this.lbProgress.TabIndex = 12;
            this.lbProgress.Text = "         ";
            this.lbProgress.Visible = false;
            // 
            // lbProgressReceiving
            // 
            this.lbProgressReceiving.AutoSize = true;
            this.lbProgressReceiving.Location = new System.Drawing.Point(173, 28);
            this.lbProgressReceiving.Name = "lbProgressReceiving";
            this.lbProgressReceiving.Size = new System.Drawing.Size(34, 13);
            this.lbProgressReceiving.TabIndex = 14;
            this.lbProgressReceiving.Text = "         ";
            this.lbProgressReceiving.Visible = false;
            // 
            // progressBarReceiving
            // 
            this.progressBarReceiving.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.progressBarReceiving.Location = new System.Drawing.Point(172, 44);
            this.progressBarReceiving.Name = "progressBarReceiving";
            this.progressBarReceiving.Size = new System.Drawing.Size(171, 12);
            this.progressBarReceiving.TabIndex = 13;
            this.progressBarReceiving.Visible = false;
            // 
            // SyncMLForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 297);
            this.Controls.Add(this.lbProgressReceiving);
            this.Controls.Add(this.progressBarReceiving);
            this.Controls.Add(this.lbProgress);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.edStatus);
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SyncMLForm";
            this.Text = "SyncML Client for XXX";
            this.Load += new System.EventHandler(this.FrmSyncML_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSyncML_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem syncToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem syncToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayLogWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox edStatus;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.ToolStripMenuItem testFormToolStripMenuItem;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lbProgress;
        private System.Windows.Forms.Label lbProgressReceiving;
        private System.Windows.Forms.ProgressBar progressBarReceiving;
        private System.Windows.Forms.ToolStripMenuItem slowSyncToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopSyncToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem syncFromClientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshFromClientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem syncFromServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshFromServerToolStripMenuItem;
    }
}

