using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Fonlow.SyncML.Common;

namespace Fonlow.SyncML.Windows
{
    /// <summary>
    /// UI for inputting password.
    /// </summary>
    public partial class UCPassword : UserControl
    {
        public UCPassword()
        {
            InitializeComponent();
        }


        private string password ;
        /// <summary>
        /// Return null if cancelled.
        /// </summary>
        public string Password
        {
            get { return password; }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            password = maskedTextBox1.Text;
            ParentForm.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            password = null;
            ParentForm.DialogResult = DialogResult.Cancel;
        }

        private void UCPassword_ParentChanged(object sender, EventArgs e)
        {
            ParentForm.AcceptButton = button1;
            ParentForm.CancelButton = button2;
        }
    }
}
