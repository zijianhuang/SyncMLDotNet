using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fonlow.SyncML.Common;

namespace Fonlow.SyncML.Windows
{
    /// <summary>
    /// Log window for sync projects.
    /// </summary>
    public partial class FormLog : Form
    {
        public FormLog()
        {
            InitializeComponent();
        }

        public event EventHandler<LocationSizeChangedEventArgs> DisposeEvent;

        public TextBox Box
        {
            get { return edLog; }
        }

        private void CopyToClipboard()
        {
            edLog.SelectAll();
            edLog.Copy();           
        }

        private void Clear()
        {
            edLog.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CopyToClipboard();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clear();
        }

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

            if (DisposeEvent != null)
            {
                DisposeEvent(this, new LocationSizeChangedEventArgs(Location, Size));
            }

            base.Dispose(disposing);
        }

    }
}