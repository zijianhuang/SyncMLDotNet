using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Fonlow.SyncML.Windows
{
    public partial class UCAbout : UserControl
    {
        public UCAbout()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ParentForm.Close();
        }

        private void FmAbout_Load(object sender, EventArgs e)
        {
            ParentForm.CancelButton = button1;
        }

        public void SetDeviceId(string deviceId)
        {
                lbDeviceId.Text = deviceId;
        }

        public void SetAboutInfo(AboutInfo aboutInfo)
        {
            lbProductName.Text = aboutInfo.ProductName;
            lbWeb.Text = aboutInfo.Website;
            lbCopyright.Text = aboutInfo.Copyright;
            if (String.IsNullOrEmpty(aboutInfo.Version))
            {
                AssemblyName mainUIAssemblyName = Assembly.GetAssembly(typeof(UCAbout)).GetName();
                lbVersion2.Text = "Core Version " + mainUIAssemblyName.Version;

                Assembly exeAssembly = Assembly.GetEntryAssembly();
                if (exeAssembly != null)
                {
                    lbVersion.Text = "Version " + exeAssembly.GetName().Version;
                }
                else
                {
                    lbVersion.Text = "Version " +lbVersion2.Text;
                }
            }
            else
            {
                lbVersion.Text = aboutInfo.Version;
                lbVersion2.Hide();
            }
        }
    }

    public struct AboutInfo
    {
        public string ProductName;
        public string Website;
        public string Copyright;
        public string Version;
    }

}
