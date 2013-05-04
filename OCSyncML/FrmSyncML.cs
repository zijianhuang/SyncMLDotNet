using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fonlow.SyncML.Common;
using Fonlow.SyncML.Elements;
using System.Diagnostics;
using Fonlow.SyncML.Windows.Properties;
using System.Reflection;

namespace Fonlow.SyncML.Windows
{
    /// <summary>
    /// This main UI of Sync will acquire an ILocalDataSource through reflection with info defined in configuration,
    /// or from client codes.
    /// </summary>
    public partial class SyncMLForm : Form
    {
        /// <summary>
        /// Standard way of create the main UI. ILocalDataSource object is defined in configuration, and created
        /// using reflection.
        /// </summary>
        public SyncMLForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Create a mainform as a component of client codes. 
        /// Client codes will create ILocalDataSource object.
        /// </summary>
        /// <param name="syncDataSource"></param>
        public SyncMLForm(ILocalDataSource syncDataSource, AboutInfo aboutInfo)
            : this()
        {
            localDataSource = syncDataSource;
            this.aboutInfo = aboutInfo;
        }

        /// <summary>
        /// Create a mainform as a component of client codes. 
        /// Client codes should give an assembly with an ILocalDataSource class.
        /// </summary>
        public SyncMLForm(string syncDataSourceAssembly, AboutInfo aboutInfo)
            : this()
        {
            localDataSourceAssembly = syncDataSourceAssembly;
            this.aboutInfo = aboutInfo;
        }

        /// <summary>
        /// This is mostly used for constructing with a known localDataSource in an integrated solution.
        /// An example is CX.
        /// </summary>
        public SyncMLForm(Type localDataSourceType, AboutInfo aboutInfo)
            : this()
        {
            this.localDataSourceType = localDataSourceType;
            this.aboutInfo = aboutInfo;
        }

        Type localDataSourceType;

        string localDataSourceAssembly;

        private void HandleServerDeviceInfo(object sender, DeviceInfoEventArgs args)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Server info:");
            builder.AppendLine("============");
            builder.AppendLine("Manufacturer: " + args.Manufacturer);
            builder.AppendLine("Model: " + args.Model);
            builder.AppendLine("Software Version: " + args.SoftwareVersion);
            builder.AppendLine("Device ID: " + args.DeviceId);
            builder.AppendLine("Device Type: " + args.DeviceType);
            builder.AppendLine("============");

            Trace.WriteLine("DeviceInfo: "+builder.ToString());
        }

        private SyncMLFacade facade;
        private string programPath;

        ILocalDataSource localDataSource;

        private SyncMLFacade CreateFacadeForSyncSession()
        {
            SyncMLFacade facade;

            try
            {
                localDataSource = LoadLocalDataSource();
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show(this, e.Message, "Warning");
                UpdateStatusLogText(e.Message);
                return null;
            }

            if (localDataSource == null)
                return null;

            facade = new SyncMLFacade(localDataSource);

            facade.User = Settings.Default.User;
        //    facade.ContactMediumType = ContactExchangeType.Vcard21;
            string password = ObtainPassword();
            if (String.IsNullOrEmpty(password))
                return null;
            else
                facade.Password = password;
            facade.BasicUriText = Settings.Default.BasicURI;

            facade.DeviceAddress = Settings.Default.DeviceAddress;

            facade.LastAnchorTime = Settings.Default.LastAnchorTime;
            facade.LastAnchor = Settings.Default.LastAnchor; // Next anchor is generated so before alert
            facade.VendorName = Settings.Default.VendorName;
            facade.ModelName = Settings.Default.ModelName;
            facade.ModelVersion = Settings.Default.ModelVersion;
            facade.ContactDataSourceAtServer = Settings.Default.ServerContactSource;

            facade.LastAnchorChangedEvent += HandleLastAnchorChanged;


            #region Wire event handlings to Facade's events regarding to visual effects
            facade.StartOperationEvent += HandleStartOperation;
            facade.EndOperationEvent += HandleEndOperation;
            facade.OperationStatusEvent += HandleOperationMessage;
            facade.ServerDeviceInfoEvent += HandleServerDeviceInfo;

            facade.InitProgressBarEvent += HandleInitProgressBar;
            facade.IncrementProgressBarEvent += HandleIncrementProgressBar;
            facade.StageMessageEvent += new EventHandler<StatusEventArgs>(HandleStageMessageEvent);

            facade.InitProgressBarReceivingEvent += HandleInitProgressBarReceiving;
            facade.IncrementProgressBarReceivingEvent += HandleIncrementProgressBarReceiving;
            facade.StageMessageReceivingEvent += HandleStageMessageReceivingEvent;
            facade.GracefulStopEvent += new EventHandler<EventArgs>(facade_GracefulStopEvent);
            #endregion


            System.Net.WebProxy proxy;
            if (Settings.Default.UseProxy)
            {
                proxy = new System.Net.WebProxy(Settings.Default.Proxy);
                proxy.UseDefaultCredentials = true;
            }
            else
                proxy = null;

            facade.Proxy = proxy;


            return facade;
        }

        /// <summary>
        /// Check LocalDataSourceAssembly to find a class type which supports ILocalDataSource.
        /// </summary>
        /// <returns></returns>
        private Type FindLocalDataSourceTypeInAssembly()
        {
            Assembly assembly = Assembly.Load(localDataSourceAssembly);
            Trace.WriteLine("Local data source is from this assembly: " + localDataSourceAssembly);
            //           Trace.Assert(assembly != null, "Assembly with LocalDataSourceAssembly must be defined");
            if (assembly == null)
            {
                throw new NullReferenceException("Assembly with LocalDataSourceAssembly must be defined in Configuration");
            }

            foreach (Type t in assembly.GetTypes())
            {
                if (t.IsClass && (typeof(ILocalDataSource).IsAssignableFrom(t)))
                {
                    return t;
                }
            }
            return null;
        }

        /// <summary>
        /// Create an instance of ILocalDataSource through reflection on the LocalDataSourceAssembly.
        /// Or return an external ILocalDataSource object given by a client.
        /// </summary>
        /// <returns></returns>
        private ILocalDataSource LoadLocalDataSource()
        {
            if (localDataSource != null)
            {
                Debug.WriteLine("ILocalDataSource object is created by client codes.");
             //   localDataSource.ExchangeType = Settings.Default.ContactExchangeType;
                return localDataSource;
            }

            if (!String.IsNullOrEmpty(localDataSourceAssembly))
            {
                localDataSourceType = FindLocalDataSourceTypeInAssembly();
            }

            //            Trace.Assert(t != null, "LocalDataSource class must be defined in the assembly");
            if (localDataSourceType == null)
            {
                throw new NullReferenceException("LocalDataSource class must be defined in the assembly");
            }

         //   Object[] args = { Settings.Default.ContactExchangeType };
            try
            {
                object handle = Activator.CreateInstance(localDataSourceType, new object[]{Settings.Default.ContactExchangeType});
                ILocalDataSource source = (ILocalDataSource)handle;
             //   source.ExchangeType = Settings.Default.ContactExchangeType;
                return source;
            }
            catch (TargetInvocationException e)
            {
                Trace.WriteLine("When LoadLocalDataSource: "+e.Message);
                return null;
            }
        }

        private static string ObtainPassword()
        {
            if (Settings.Default.RememberPassword)
                return Settings.Default.Password;
            else
            {
                UCPassword ucPassword = new UCPassword();
                using (CommonForm fm = new CommonForm())
                {
                    fm.AddControlForDialog(ucPassword, "Password");
                    if (fm.ShowDialog() == DialogResult.OK)
                        return ucPassword.Password;
                    else
                        return null;
                }

            }
        }

        void facade_GracefulStopEvent(object sender, EventArgs e)
        {
            UpdateStatusLogText("!!! Being stopped by user gracefully !!!");
            ShowDefaultCursor();
            DisableStopFunction();
        }

        #region ProgressBars and stage messages of sync progress

        private void HandleStageMessageEvent(object sender, StatusEventArgs e)
        {
            DisplayStageMessage(e.Text);
        }

        private void DisplayStageMessage(string text)
        {
            if (lbProgress.InvokeRequired)
            {
                ProcessTextHandler d = new ProcessTextHandler(DisplayStageMessage);
                this.Invoke(d, new object[] { text });
            }
            else
                lbProgress.Text = text;

        }

        private void HandleStageMessageReceivingEvent(object sender, StatusEventArgs e)
        {
            DisplayStageMessageReceiving(e.Text);
        }

        private void DisplayStageMessageReceiving(string text)
        {
            if (lbProgressReceiving.InvokeRequired)
            {
                ProcessTextHandler d = new ProcessTextHandler(DisplayStageMessageReceiving);
                this.Invoke(d, new object[] { text });
            }
            else
                lbProgressReceiving.Text = text;

        }

        private void HandleInitProgressBar(object sender, InitProgressBarEventArgs e)
        {
            InitProgressBar(e);
        }

        private void HandleInitProgressBarReceiving(object sender, InitProgressBarEventArgs e)
        {
            InitProgressBarReceiving(e);
        }

        private void InitProgressBar(InitProgressBarEventArgs e)
        {
            if (progressBar.InvokeRequired)
            {
                InitProgressBarHandler d = new InitProgressBarHandler(InitProgressBar);
                this.Invoke(d, new object[] { e });
            }
            else
            {
                progressBar.Value = 0;
                progressBar.Minimum = e.MinValue;
                progressBar.Maximum = e.MaxValue;
                progressBar.Step = e.Step;
                progressBar.Visible = true;
                lbProgress.Visible = true;
            }

        }

        private void InitProgressBarReceiving(InitProgressBarEventArgs e)
        {
            if (progressBarReceiving.InvokeRequired)
            {
                InitProgressBarHandler d = new InitProgressBarHandler(InitProgressBarReceiving);
                this.Invoke(d, new object[] { e });
            }
            else
            {
                progressBarReceiving.Value = 0;
                progressBarReceiving.Minimum = e.MinValue;
                progressBarReceiving.Maximum = e.MaxValue;
                progressBarReceiving.Step = e.Step;
                progressBarReceiving.Visible = true;
                lbProgressReceiving.Visible = true;
            }

        }

        private void HideProgressBar()
        {
            if (progressBar.InvokeRequired)
            {
                ActionHandler d = new ActionHandler(HideProgressBar);
                this.Invoke(d, new object[] { });
            }
            else
                progressBar.Hide();

        }

        private void HideProgressBarReceiving()
        {
            if (progressBar.InvokeRequired)
            {
                ActionHandler d = new ActionHandler(HideProgressBarReceiving);
                this.Invoke(d, new object[] { });
            }
            else
                progressBarReceiving.Hide();

        }

        private void HideProgressLabel()
        {
            if (lbProgress.InvokeRequired)
            {
                ActionHandler d = new ActionHandler(HideProgressLabel);
                this.Invoke(d, new object[] { });
            }
            else
                lbProgress.Hide();

        }

        private void HideProgressLabelReceiving()
        {
            if (lbProgress.InvokeRequired)
            {
                ActionHandler d = new ActionHandler(HideProgressLabelReceiving);
                this.Invoke(d, new object[] { });
            }
            else
                lbProgressReceiving.Hide();

        }

        private void ShowProgressLabel()
        {
            if (lbProgress.InvokeRequired)
            {
                ActionHandler d = new ActionHandler(ShowProgressLabel);
                this.Invoke(d, new object[] { });
            }
            else
                lbProgress.Show();

        }

        private void ShowProgressLabelReceiving()
        {
            if (lbProgress.InvokeRequired)
            {
                ActionHandler d = new ActionHandler(ShowProgressLabelReceiving);
                this.Invoke(d, new object[] { });
            }
            else
                lbProgressReceiving.Show();

        }

        private void HandleIncrementProgressBarReceiving(object sender, IncrementProgressBarEventArgs e)
        {
            IncrementProgressBarReceiving(e.IncrementAmount);
        }

        private void IncrementProgressBarReceiving(int amount)
        {
            if (progressBarReceiving.InvokeRequired)
            {
                ProcessIntegerHandler d = new ProcessIntegerHandler(IncrementProgressBarReceiving);
                this.Invoke(d, new object[] { amount });
            }
            else
            {
                progressBarReceiving.Increment(amount);
            }

        }

        private void HandleIncrementProgressBar(object sender, IncrementProgressBarEventArgs e)
        {
            IncrementProgressBar(e.IncrementAmount);
        }

        private void IncrementProgressBar(int amount)
        {
            if (progressBar.InvokeRequired)
            {
                ProcessIntegerHandler d = new ProcessIntegerHandler(IncrementProgressBar);
                this.Invoke(d, new object[] { amount });
            }
            else
            {
                progressBar.Increment(amount);
            }

        }
        #endregion

        private void HandleOperationMessage(object sender, StatusEventArgs e)
        {
            UpdateStatusLogText(e.Text);
        }

        /// <summary>
        /// Display text asynchroneously in edStatus
        /// </summary>
        /// <param name="s"></param>
        private void UpdateStatusLogText(string s)
        {
            if (s == null)
                return;

            if (edStatus.InvokeRequired)
            {
                ProcessTextHandler d = new ProcessTextHandler(UpdateStatusLogText);
                this.Invoke(d, new object[] { s });
            }
            else
            {
                // statusLabel.Text = s;
                edStatus.AppendText(DateTime.Now.ToLongTimeString() + ": " +
                    s + Environment.NewLine);
            }
        }

        private void HandleStartOperation(object sender, StatusEventArgs e)
        {
            UpdateStatusLogText(e.Text);
            ShowWaitCursor();

            HideProgressBar();  // the progress bar will be displayed when the model initialize the progress bar.
            HideProgressLabel();
            HideProgressBarReceiving();
            HideProgressLabelReceiving();

            EnableStopFunction();
        }

        private void HandleLastAnchorChanged(object sender, AnchorChangedEventArgs e)
        {
            Settings.Default.LastAnchorTime = e.Time;
            Settings.Default.LastAnchor = e.LastAnchor;
            Settings.Default.Save();
            UpdateStatusLogText("Last sync time saved: " + e.Time.ToString());
        }

        private void HandleEndOperation(object sender, StatusEventArgs e)
        {

            UpdateStatusLogText(e.Text);
            ShowDefaultCursor();
            DisableStopFunction();
        }

        private void ShowWaitCursor()
        {
            if (this.InvokeRequired)
            {
                ActionHandler d = new ActionHandler(ShowWaitCursor);
                this.Invoke(d);
            }
            else
            {
                Cursor = Cursors.WaitCursor;
            }
        }

        private void ShowDefaultCursor()
        {
            if (this.InvokeRequired)
            {
                ActionHandler d = new ActionHandler(ShowDefaultCursor);
                this.Invoke(d);
            }
            else
            {
                Cursor = Cursors.Default;
            }
        }



        private void pingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Reseting last anchor will result in slow sync next time. Do you want to continue?",
                "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                ResetLastAnchor();
        }

        /// <summary>
        /// This is generally for testing to force to slow sync.
        /// </summary>
        private static void ResetLastAnchor()
        {
            Settings.Default.LastAnchorTime = DateTime.MinValue;
            Settings.Default.LastAnchor = "0";
            Settings.Default.Save();
        }

        private void displayLogWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayLogWindow();
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
                System.Drawing.Size size = Settings.Default.LogWinSize;
                System.Drawing.Point position = Settings.Default.LogWinPosition;
                if (size.Height < 250)
                    size.Height = 250;

                if (size.Width < 250)
                    size.Width = 250;

                logUC.Location = position;
                logUC.Size = size;
                logUC.Show();
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
                Settings.Default.LogWinPosition = ev.Location;
                Settings.Default.LogWinSize = ev.Size;
                Settings.Default.Save();
            }
        }

        private void testFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestForm f = new TestForm();
            f.Show();
        }

        private void syncToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Sync();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAbout();
        }

        readonly AboutInfo aboutInfo;

        private void ShowAbout()
        {
            UCAbout ucAbout = new UCAbout();
            ucAbout.SetDeviceId("Device ID: " + Settings.Default.DeviceAddress);
            ucAbout.SetAboutInfo(aboutInfo);
            using (CommonForm fm = new CommonForm())
            {
                fm.AddControlForDialog(ucAbout, "About");
                fm.ShowDialog();
            }
        }

        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void FrmSyncML_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (logUC != null) // to save the settings of logUC.
                logUC.Close();

        }

        private void slowSyncToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            Sync();
        }

        private void Sync()
        {
            facade = CreateFacadeForSyncSession();
            if (facade != null)
            {
                facade.SyncType = SyncType.TwoWay;
                facade.LogOnAndSyncAsync();
            }
        }

        private void SlowSync()
        {
            facade = CreateFacadeForSyncSession();
            if (facade != null)
            {
                facade.SyncType = SyncType.Slow;
                facade.LogOnAndSyncAsync();
            }
        }

        private void EnableStopFunction()
        {
            if (menuStrip1.InvokeRequired)
            {
                ActionHandler d = new ActionHandler(EnableStopFunction);
                this.Invoke(d, new object[] { });
            }
            else
            {
                stopSyncToolStripMenuItem.Enabled = true;
            }
        }

        private void DisableStopFunction()
        {
            if (menuStrip1.InvokeRequired)
            {
                ActionHandler d = new ActionHandler(DisableStopFunction);
                this.Invoke(d, new object[] { });
            }
            else
            {
                stopSyncToolStripMenuItem.Enabled = false;
            }

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayOptions();
        }

        private static void DisplayOptions()
        {
            UcOptions ucOptions = new UcOptions();
            using (CommonForm fm = new CommonForm())
            {
                fm.AddControlForDialog(ucOptions, "Preferences");
                fm.ShowDialog();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DisplayOptions();
        }

        private void contentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayHelp();
        }

        private void DisplayHelp()
        {
            try
            {
                //System.Diagnostics.Process.Start(programPath + "Help\\index.htm");
                System.Diagnostics.Process.Start("http://www.fonlow.com/opencontacts/SyncMLClient/");
            }
            catch (Win32Exception)
            {
                MessageBox.Show("Can not locate the help file.");
            }
        }

        private void FrmSyncML_Load(object sender, EventArgs e)
        {
            Text = aboutInfo.ProductName;

            programPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\";

            if ((Settings.Default.DeviceAddress == String.Empty) ||
               (Settings.Default.DeviceAddress == "FonlowSyncMLClientGuid"))
            {
                Settings.Default.DeviceAddress = "FonlowSyncMLClient" + Guid.NewGuid().ToString("N");
                Settings.Default.Save();
            }
        }

        private void slowSyncToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ExecuteSlowSync();
        }

        private void ExecuteSlowSync()
        {
            if (MessageBox.Show(this, "Slow Sync will exchange all info between the local and the server. Do you want to continue?",
    "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ResetLastAnchor();
                SlowSync();
            }
        }

        private void stopSyncToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopSync();
        }

        private void StopSync()
        {
            facade.StopSync();
        }

        private void syncFromClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This operation will send modified content to the server, and will not receive modifications from server. Both sides might then lost sync. Do you want to continue?",
                "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                SyncFromClient();
        }

        private void SyncFromClient()
        {
            facade = CreateFacadeForSyncSession();
            if (facade != null)
            {
                facade.SyncType = SyncType.OneWayFromClient;
                facade.LogOnAndSyncAsync();
            }

        }

        private void refreshFromClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This operation will send all content to the server, and overwrite all data in server. Do you want to continue?",
                "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                RefreshFromClient();
        }

        private void RefreshFromClient()
        {
            facade = CreateFacadeForSyncSession();
            if (facade != null)
            {
                facade.LogOnAndRefreshFromClientAsync();
            }

        }

        private void syncFromServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This operation will get modified content from the server, and will not send local updates to server. Both sides might then lost sync. Do you want to continue?",
                "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                SyncFromServer();
        }

        private void SyncFromServer()
        {
            facade = CreateFacadeForSyncSession();
            if (facade != null)
            {
                facade.SyncType = SyncType.OneWayFromServer;
                facade.LogOnAndSyncAsync();
            }

        }

        private void refreshFromServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This operation will download all content from the server, which will overwrite all data in local database. Do you want to continue?",
                "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                RefreshFromServer();
        }

        private void RefreshFromServer()
        {
            facade = CreateFacadeForSyncSession();
            if (facade != null)
            {
                facade.SyncType = SyncType.RefreshFromServer;
                facade.LogOnAndSyncAsync();
            }

        }

    }

}