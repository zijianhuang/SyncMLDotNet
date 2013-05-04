using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Fonlow.SyncML.Windows;

namespace Fonlow.SyncML.MultiSync
{
    public partial class UcOptions : UserControl
    {
        public UcOptions()
        {
            InitializeComponent();
            Load += new EventHandler(UcOptions_Load);
        }

        void UcOptions_Load(object sender, EventArgs e)
        {
            btnOK.DialogResult = DialogResult.OK;
            btnCancel.DialogResult = DialogResult.Cancel;
            ParentForm.AcceptButton = btnOK;
            ParentForm.CancelButton = btnCancel;
            propertyGrid1.ExpandAllGridItems();
        }

        public UcOptions(object property) : this()
        {
            propertyGrid1.SelectedObject = property;
        }

        public static bool ShowModal(ApplicationSettingsBase settings)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");

            using (CommonForm fm = new CommonForm())
            {
                UcOptions ucOptions = new UcOptions(settings);
                fm.AddControlForDialog(ucOptions, "Options");
                if (fm.ShowDialog() == DialogResult.OK)
                {
                    settings.Save();
                    return true;
                }
                else
                {
                    settings.Reload();
                    return false;
                }
            }

        }
    }
}
