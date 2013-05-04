using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Fonlow.SyncML.Windows.Properties;

[assembly: CLSCompliant(true)]
namespace Fonlow.SyncML.Windows
{
    public partial class UcOptions : UserControl
    {
        public UcOptions()
        {
            InitializeComponent();
            LoadSettings();
        }
        void BindForTextBox(TextBoxBase box, string property)
        {
            box.DataBindings.Add("Text", Settings.Default, property);
        }

        private void LoadSettings()
        {
            BindForTextBox(edServer, "BasicURI");
            BindForTextBox(edUser, "User");

            if (Settings.Default.RememberPassword)
            {
                BindForTextBox(edPassword, "Password");
            }
            else
            {
                edPassword.Enabled = false;
            }

            BindForTextBox(edProxy, "Proxy");
            chkUseProxy.DataBindings.Add("Checked", Settings.Default, "UseProxy");
            edProxy.Enabled = chkUseProxy.Checked;

            edLastTime.Text = Settings.Default.LastAnchorTime.ToString();//no need to bind

            chkRememberPwd.DataBindings.Add("Checked", Settings.Default, "RememberPassword");
            BindForTextBox(edContactsStorage, "ServerContactSource");

            //hard to bind enum with SelectedIndex, and I don't want to write parse just for one property.
            if (Settings.Default.ContactExchangeType == "vCard")
                comboStorageType.Text = "vCard";
            else
                comboStorageType.Text = "SIF-C";
        }

        private void SaveSettings()
        {
            if (comboStorageType.SelectedIndex == 0)
                Settings.Default.ContactExchangeType = "SIF";
            else
                Settings.Default.ContactExchangeType = "vCard";

            Settings.Default.Save();
        }

        private void chkUseProxy_CheckedChanged(object sender, EventArgs e)
        {
            edProxy.Enabled = chkUseProxy.Checked;
        }

        private void UcOptions_Load(object sender, EventArgs e)
        {
            ParentForm.CancelButton = btnCancel;
            ParentForm.AcceptButton = btnOK;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveSettings();
            ParentForm.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ParentForm.Close();
        }

        private void chkRememberPwd_CheckedChanged(object sender, EventArgs e)
        {
            edPassword.Enabled = chkRememberPwd.Checked;
        }
    }
}
