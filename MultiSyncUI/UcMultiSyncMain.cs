using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fonlow.SyncML.Windows;
using Fonlow.SyncML.Common;
using System.Diagnostics;

namespace Fonlow.SyncML.MultiSync
{
    public partial class UcMultiSyncMain : UserControl
    {
        internal UcMultiSyncMain()
        {
            InitializeComponent();
            menuItemAbout.Click += new EventHandler(menuItemAbout_Click);
            btnLog.Click += new EventHandler(btnLog_Click);
            menuItemContent.Click += new EventHandler(menuItemContent_Click);
            Load += new EventHandler(UcMultiSyncMain_Load);
            btnSync.Click += delegate(object sender, EventArgs e) { SyncAllAsync(); };
        }

        internal UcMultiSyncMain(SyncSettingsBase settings)
            : this()
        {
            this.settings = settings;
        }

        SyncSettingsBase settings;

        /// <summary>
        /// Create a form for application containing UcMultiSyncMain.
        /// And this form can remember its own location in multiscreen.
        /// </summary>
        /// <returns></returns>
        public static Form CreateMainForm(SyncSettingsBase settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings", "How could settings be null?");
            }
            CommonForm form = new CommonForm();
            form.ShowInTaskbar = true;
            //  form.AutoSize = true;/// Because this form is used as main form.
            // AutoSize can not work properly probably the autosize takes effects
            // before UcMultiSyncMain is loaded.
            //   form.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            form.Icon = Properties.Resources.syncIcon;
            form.Load += delegate(object sender, EventArgs e)
            {
                try
                {
                    UcMultiSyncMain uc = new UcMultiSyncMain(settings);
                    form.AddControl(uc, ProgramSettings.Default.ProductName);
                    // form.AutoSize = true;//not working either
                }
                catch (System.Configuration.ConfigurationErrorsException exception)
                {
                    MessageBox.Show("Configuration has problem: " + e.ToString() + "~" + exception.Message);
                }

            };

            MultiScreenHelper.RefineLocation(form, ProgramSettings.Default.Location, ProgramSettings.Default.ScreenDeviceName);
            form.FormClosing += delegate(object sender, System.Windows.Forms.FormClosingEventArgs e)
            {
                ProgramSettings.Default.Location = form.Location;
                ProgramSettings.Default.ScreenDeviceName = System.Windows.Forms.Screen.FromControl(form).DeviceName;
                ProgramSettings.Default.Save();
            };
            return form;
        }

        void menuItemContent_Click(object sender, EventArgs e)
        {
            DisplayHelp();
        }

        private static void DisplayHelp()
        {
            try
            {
                System.Diagnostics.Process.Start(ProgramSettings.Default.HelpUrl);
            }
            catch (Win32Exception)
            {
                MessageBox.Show("Can not locate the help file.");
            }
        }

        void btnLog_Click(object sender, EventArgs e)
        {
            DisplayLogWindow();
        }

        void UcMultiSyncMain_Load(object sender, EventArgs e)
        {
            //Initialize DeviceAddress
            if (String.IsNullOrEmpty(settings.DeviceAddress) ||
         (settings.DeviceAddress == "FonlowSyncMLClientGuid"))
            {
                settings.DeviceAddress = "FonlowSyncMLClient" + Guid.NewGuid().ToString("N");
                settings.Save();
            }

            LoadSyncPanels();
        }

        void menuItemAbout_Click(object sender, EventArgs e)
        {
            ShowAbout();
        }

        private AboutInfo aboutInfo;

        private void ShowAbout()
        {
            aboutInfo.Copyright = ProgramSettings.Default.Copyright;
            aboutInfo.ProductName = ProgramSettings.Default.ProductName;
            aboutInfo.Version = ProgramSettings.Default.Version;
            aboutInfo.Website = ProgramSettings.Default.WebSite;
            using (CommonForm fm = new CommonForm())
            {
                UCAbout ucAbout = new UCAbout();
                ucAbout.SetDeviceId("Device ID: " + settings.DeviceAddress);
                ucAbout.SetAboutInfo(aboutInfo);
                fm.AddControlForDialog(ucAbout, "About");
                fm.ShowDialog();
            }
        }

        private void ShowOptions()
        {
            if (UcOptions.ShowModal(settings))
            {
                ReloadSyncPanels();
            }
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            ShowOptions();
        }
        /// <summary>
        /// Funambol SyncML server does not seem to like multiple log-on SyncML message
        /// at almost the same time, and occasionally send back status code 400.
        /// So I have to serialize these.
        /// </summary>
        void SyncAll()
        {
            foreach (Control control in panel1.Controls)
            {
                UcSync syncUc = control as UcSync;
                //Debug.Assert(syncUc != null, "What, not syncUc?");
                if (syncUc == null) //could be the status box
                    continue;

                IAsyncResult r = syncUc.Sync();
                while (!r.IsCompleted)
                {
                    System.Threading.Thread.Sleep(200);
                }

            }
        }
        /// <summary>
        /// Need to spin a new thread because System.Threading.Thread must not
        /// be the main thread, otherwise, the program will hang.
        /// </summary>
        void SyncAllAsync()
        {
            Action d = SyncAll;
            d.BeginInvoke(null, null);
        }

        private void ReloadSyncPanels()
        {
            panel1.Controls.Clear();
            LoadSyncPanels();
        }

        private void LoadSyncPanels()
        {
            //   IList<SyncItem> syncItems = settings.GetSyncItems();
            IList<string> syncItemNames = settings.GetSyncItemNames();
            if (syncItemNames.Count == 0)
            {
                MessageBox.Show("The user configuration has no SyncItems defined. Please check.");
                return;
            }

            TextBox statusBox = new TextBox();
            Trace.Assert(panel1 != null, "panel1 null");
            Trace.Assert(btnSync != null, "btuSync null");
            Trace.Assert(syncItemNames.Count > 0, "syncItemNames empty.");
            int i = 0;
            if (syncItemNames.Count == 1)
            {//Hide the Sync All button.
                i = 1;
                string syncItemPropertyName = syncItemNames[0];
                SyncItem syncItem = settings[syncItemPropertyName] as SyncItem;
                if (syncItem.Enabled)
                {
                    UcSync syncUc = new UcSync(syncItem, settings);
                    syncUc.Dock = DockStyle.None;
                    syncUc.Location = new Point(0, 0);
                    syncUc.Width = ClientSize.Width;
                    syncUc.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                    syncUc.TextBoxStatus = statusBox;
                    panel1.Controls.Add(syncUc);

                    syncUc.ButtonLabel = "Sync";
                    syncUc.SetButtonIcon(Properties.Resources.sync);
                    btnSync.Visible = false;
                    toolStripSeparator1.Visible = false;
                    panel1.Height = syncUc.Height;
                }
            }
            else
            {
                int y = 0;
                foreach (string itemName in syncItemNames)
                {
                    SyncItem item = settings[itemName] as SyncItem;
                    if (item.Enabled)
                    {
                        UcSync syncUc = new UcSync(item, settings);
                        syncUc.Dock = DockStyle.None;
                        syncUc.Location = new Point(0, y);
                        syncUc.Width = ClientSize.Width;
                        syncUc.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                        syncUc.TextBoxStatus = statusBox;
                        panel1.Controls.Add(syncUc);

                        i++;
                        y += syncUc.Height + 1; // add one to leave a line between
                    }
                }
            }

            const int ucPanelHeight = 71;//UcSync's default height
            const int ucPanelWidth = 372; //UcSync's default Width
            Height = (ucPanelHeight + 1) * i + toolStrip1.Height;
            Width = ucPanelWidth;

            //initialize the statusBox
            statusBox.Multiline = true;
            statusBox.ScrollBars = ScrollBars.Vertical;
            statusBox.Location = new Point(4, (ucPanelHeight + 1) * i + 4);
            const int statusBoxHeight = 125;
            statusBox.Size = new Size(ucPanelWidth - 8, statusBoxHeight);
            statusBox.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            panel1.Controls.Add(statusBox);
            Height += statusBoxHeight + 8;

            ParentForm.ClientSize = new Size(ParentForm.ClientSize.Width, Height);
        }

        private FormLog logUC;
        private MemoTraceListener logMemoListerner;
        private void DisplayLogWindow()
        {
            if (logUC == null)
            {
                logUC = new FormLog();
                logUC.DisposeEvent += HandleLogWindowClose;
                //To show trace on screen. Might need a switch later.
                logMemoListerner = new MemoTraceListener(logUC.Box);
                Trace.Listeners.Add(logMemoListerner);
                System.Drawing.Size size = ProgramSettings.Default.LogWinSize;
                if (size.Height < 250)
                    size.Height = 250;

                if (size.Width < 250)
                    size.Width = 250;

                MultiScreenHelper.RefineLocation(logUC, ProgramSettings.Default.LogWinLocation, ProgramSettings.Default.LogScreenDeviceName);

                logUC.Size = size;
                logUC.Show();
            }
            else
            {
                logUC.BringToFront();
            }
        }

        private void HandleLogWindowClose(object sender, EventArgs e)
        {
            LocationSizeChangedEventArgs ev = e as LocationSizeChangedEventArgs;
            logUC = null;
            Trace.Listeners.Remove(logMemoListerner);
            logMemoListerner = null;
            if (ev != null)
            {
                ProgramSettings.Default.LogWinLocation = ev.Location;
                ProgramSettings.Default.LogWinSize = ev.Size;
                ProgramSettings.Default.LogScreenDeviceName = Screen.FromPoint(ev.Location).DeviceName;
                ProgramSettings.Default.Save();
            }
        }



    }

}
