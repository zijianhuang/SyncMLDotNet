using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fonlow.SyncML.Common;
using System.Linq;

namespace Fonlow.SyncML.Windows
{
    /// <summary>
    /// Host custom user control that will fill entire form. It can be used for non-modal and modal windows.
    /// The size of the form will adapt the size of the user control docked.
    /// The client codes should generally call the user control to get modal result.
    /// </summary>
    public partial class CommonForm : Form
    {
        public CommonForm()
        {
            InitializeComponent();
        }

        void FmCommon_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        /// <summary>
        /// Add a UserControl and dock.Fill. This should be called once.
        /// More calls attempting to add controls will be ignored.
        /// </summary>
        /// <param name="uc"></param>
        public void AddControl(UserControl uc, string title)
        {
            if (uc == null)
                return;
            if (Controls.Count > 0)
                return;

            Text = title;
            //    if (ClientSize.Width < uc.Size.Width)
            this.ClientSize = uc.Size;

            Controls.Add(uc);
            uc.Dock = DockStyle.Fill;

        }

        /// <summary>
        /// Add a UserControl and dock.Fill, change style to conventional dialog. This should be called once.
        /// More calls attempting to add controls will be ignored.
        /// </summary>
        public void AddControlForDialog(UserControl uc, string title)
        {
            if (uc == null)
                return;
            if (Controls.Count > 0)
                return;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            StartPosition = FormStartPosition.CenterParent;

            AddControl(uc, title);

            FormClosed += new FormClosedEventHandler(FmCommon_FormClosed);

        }

    }

    public static class MultiScreenHelper
    {
        //public static void DisplayForm(Form form, Point location, string deviceName)
        //{
        //    RefineLocation(form, location, deviceName);
        //    form.Show();
        //}

        public static void RefineLocation(Form form, Point location, string deviceName)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }

            Screen myScreen = Screen.FromPoint(location);
            if (myScreen.DeviceName == deviceName)
            {
                form.Location = location;
            }
            else
            {
                form.Left = Screen.PrimaryScreen.WorkingArea.Left + 10;
                form.Top = Screen.PrimaryScreen.WorkingArea.Top + 10;
            }
        }

        
    }
}