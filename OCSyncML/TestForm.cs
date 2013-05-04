using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fonlow.SyncML.Common;

namespace Fonlow.SyncML
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void base64ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox2.Text = Utility.ConvertUtf8TextToBase64(textBox1.Text);
        }

        private void fromBase64ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = Utility.ConvertFromBase64(textBox2.Text);
        }
    }
}