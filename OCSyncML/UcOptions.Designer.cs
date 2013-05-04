namespace Fonlow.SyncML.Windows
{
    partial class UcOptions
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabAccount = new System.Windows.Forms.TabPage();
            this.chkRememberPwd = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.edPassword = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.edLastTime = new System.Windows.Forms.TextBox();
            this.edUser = new System.Windows.Forms.TextBox();
            this.edServer = new System.Windows.Forms.TextBox();
            this.tabConnection = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.edProxy = new System.Windows.Forms.TextBox();
            this.chkUseProxy = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.comboStorageType = new System.Windows.Forms.ComboBox();
            this.edContactsStorage = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabAccount.SuspendLayout();
            this.tabConnection.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabAccount);
            this.tabControl1.Controls.Add(this.tabConnection);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(403, 227);
            this.tabControl1.TabIndex = 0;
            // 
            // tabAccount
            // 
            this.tabAccount.Controls.Add(this.label7);
            this.tabAccount.Controls.Add(this.label6);
            this.tabAccount.Controls.Add(this.edContactsStorage);
            this.tabAccount.Controls.Add(this.comboStorageType);
            this.tabAccount.Controls.Add(this.chkRememberPwd);
            this.tabAccount.Controls.Add(this.label5);
            this.tabAccount.Controls.Add(this.edPassword);
            this.tabAccount.Controls.Add(this.label3);
            this.tabAccount.Controls.Add(this.label2);
            this.tabAccount.Controls.Add(this.label1);
            this.tabAccount.Controls.Add(this.edLastTime);
            this.tabAccount.Controls.Add(this.edUser);
            this.tabAccount.Controls.Add(this.edServer);
            this.tabAccount.Location = new System.Drawing.Point(4, 22);
            this.tabAccount.Name = "tabAccount";
            this.tabAccount.Padding = new System.Windows.Forms.Padding(3);
            this.tabAccount.Size = new System.Drawing.Size(395, 201);
            this.tabAccount.TabIndex = 0;
            this.tabAccount.Text = "Account";
            this.tabAccount.UseVisualStyleBackColor = true;
            // 
            // chkRememberPwd
            // 
            this.chkRememberPwd.AutoSize = true;
            this.chkRememberPwd.Location = new System.Drawing.Point(90, 84);
            this.chkRememberPwd.Name = "chkRememberPwd";
            this.chkRememberPwd.Size = new System.Drawing.Size(126, 17);
            this.chkRememberPwd.TabIndex = 3;
            this.chkRememberPwd.Text = "Remember Password";
            this.chkRememberPwd.UseVisualStyleBackColor = true;
            this.chkRememberPwd.CheckedChanged += new System.EventHandler(this.chkRememberPwd_CheckedChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 163);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Last sync time:";
            // 
            // edPassword
            // 
            this.edPassword.Location = new System.Drawing.Point(90, 58);
            this.edPassword.Name = "edPassword";
            this.edPassword.PasswordChar = '*';
            this.edPassword.Size = new System.Drawing.Size(281, 20);
            this.edPassword.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "User:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Password:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Server:";
            // 
            // edLastTime
            // 
            this.edLastTime.Location = new System.Drawing.Point(90, 160);
            this.edLastTime.Name = "edLastTime";
            this.edLastTime.ReadOnly = true;
            this.edLastTime.Size = new System.Drawing.Size(281, 20);
            this.edLastTime.TabIndex = 6;
            // 
            // edUser
            // 
            this.edUser.Location = new System.Drawing.Point(90, 32);
            this.edUser.Name = "edUser";
            this.edUser.Size = new System.Drawing.Size(281, 20);
            this.edUser.TabIndex = 1;
            // 
            // edServer
            // 
            this.edServer.Location = new System.Drawing.Point(90, 6);
            this.edServer.Name = "edServer";
            this.edServer.Size = new System.Drawing.Size(281, 20);
            this.edServer.TabIndex = 0;
            // 
            // tabConnection
            // 
            this.tabConnection.Controls.Add(this.label4);
            this.tabConnection.Controls.Add(this.edProxy);
            this.tabConnection.Controls.Add(this.chkUseProxy);
            this.tabConnection.Location = new System.Drawing.Point(4, 22);
            this.tabConnection.Name = "tabConnection";
            this.tabConnection.Padding = new System.Windows.Forms.Padding(3);
            this.tabConnection.Size = new System.Drawing.Size(395, 201);
            this.tabConnection.TabIndex = 1;
            this.tabConnection.Text = "Connection";
            this.tabConnection.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Proxy:";
            // 
            // edProxy
            // 
            this.edProxy.Location = new System.Drawing.Point(62, 42);
            this.edProxy.Name = "edProxy";
            this.edProxy.Size = new System.Drawing.Size(281, 20);
            this.edProxy.TabIndex = 7;
            // 
            // chkUseProxy
            // 
            this.chkUseProxy.AutoSize = true;
            this.chkUseProxy.Location = new System.Drawing.Point(62, 19);
            this.chkUseProxy.Name = "chkUseProxy";
            this.chkUseProxy.Size = new System.Drawing.Size(73, 17);
            this.chkUseProxy.TabIndex = 0;
            this.chkUseProxy.Text = "Use proxy";
            this.chkUseProxy.UseVisualStyleBackColor = true;
            this.chkUseProxy.CheckedChanged += new System.EventHandler(this.chkUseProxy_CheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(112, 233);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(219, 233);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // comboStorageType
            // 
            this.comboStorageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboStorageType.FormattingEnabled = true;
            this.comboStorageType.Items.AddRange(new object[] {
            "SIF-C",
            "vCard"});
            this.comboStorageType.Location = new System.Drawing.Point(90, 133);
            this.comboStorageType.Name = "comboStorageType";
            this.comboStorageType.Size = new System.Drawing.Size(121, 21);
            this.comboStorageType.TabIndex = 5;
            // 
            // edContactsStorage
            // 
            this.edContactsStorage.Location = new System.Drawing.Point(90, 107);
            this.edContactsStorage.Name = "edContactsStorage";
            this.edContactsStorage.Size = new System.Drawing.Size(281, 20);
            this.edContactsStorage.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(37, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Storage:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 136);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Exchange type:";
            // 
            // UcOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tabControl1);
            this.Name = "UcOptions";
            this.Size = new System.Drawing.Size(403, 268);
            this.Load += new System.EventHandler(this.UcOptions_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabAccount.ResumeLayout(false);
            this.tabAccount.PerformLayout();
            this.tabConnection.ResumeLayout(false);
            this.tabConnection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabAccount;
        private System.Windows.Forms.TabPage tabConnection;
        private System.Windows.Forms.MaskedTextBox edPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox edLastTime;
        private System.Windows.Forms.TextBox edUser;
        private System.Windows.Forms.TextBox edServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox edProxy;
        private System.Windows.Forms.CheckBox chkUseProxy;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkRememberPwd;
        private System.Windows.Forms.ComboBox comboStorageType;
        private System.Windows.Forms.TextBox edContactsStorage;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
    }
}
