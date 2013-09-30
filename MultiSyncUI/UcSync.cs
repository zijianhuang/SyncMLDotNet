using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Fonlow.SyncML;
using Fonlow.SyncML.Windows;
using Fonlow.SyncML.Common;
using System.Reflection;


namespace Fonlow.SyncML.MultiSync
{
    /// <summary>
    /// Represent a self-contained UI for synchronizing a sync item.
    /// This UC can not be used at design time as the Load event handling
    /// uses some runtime elements.
    /// </summary>
    public partial class UcSync : UserControl
    {
        public UcSync()
        {
            InitializeComponent();
            btnSync.Click += new EventHandler(btnSync_Click);
            btnStop.Click += new EventHandler(btnStop_Click);
            Load += new EventHandler(UcSync_Load);

            //  LogBox = edStatus;
        }

        void btnStop_Click(object sender, EventArgs e)
        {
            StopSync();
        }

        ProgressBarLabel barLabelReceiving;
        ProgressBarLabel barLabelSending;

        void UcSync_Load(object sender, EventArgs e)
        {
            barLabelReceiving = new ProgressBarLabel(progressBarReceiving);
            barLabelSending = new ProgressBarLabel(progressBarSending);

            Bitmap stopImage = Properties.Resources.Stop;
            stopImage.MakeTransparent(stopImage.GetPixel(0, 0));
            btnStop.Image = stopImage;

            toolTip1.SetToolTip(btnSync, TypeDescriptor.GetConverter(syncItem.SyncDirection).ConvertToString(syncItem.SyncDirection));
        }


        /// <summary>
        /// Create a mainform as a component of client codes. 
        /// Client codes should give an assembly with an ILocalDataSource class.
        /// </summary>
        public UcSync(SyncItem syncItem, SyncSettingsBase syncSettings)
            : this()
        {
            if (syncSettings == null)
                throw new ArgumentNullException("syncSettings");

            this.syncItem = syncItem;
            this.syncSettings = syncSettings;
            
            ShowLastSyncInfo();
            ButtonLabel = syncItem.DisplayName;
        }

        SyncItem syncItem;

        SyncSettingsBase syncSettings;

        public string ButtonLabel
        {
            get { return btnSync.Text; }
            set { btnSync.Text = value; }
        }

        public void SetButtonIcon(Image image)
        {
            btnSync.Image = image;
        }


        void ShowLastSyncInfo()
        {
            if (labelStatus.InvokeRequired)
            {
                Action d = ShowLastSyncInfo;
                this.Invoke(d);
            }
            else
            {
                labelStatus.Text = "Last sync: " + syncItem.LastAnchorTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

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

            Trace.TraceInformation("DeviceInfo: " + builder.ToString());
        }

        private SyncMLFacade facade;

        

        private SyncMLFacade CreateFacadeForSyncSession()
        {
            SyncMLFacade r;
            ILocalDataSource localDataSource;

            try
            {
                localDataSource = CreateLocalDataSource();
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show(this.ParentForm, e.Message, "Warning");
                AppendStatusText(e.Message);
                return null;
            }

            if (localDataSource == null)
                return null;

            r = new SyncMLFacade(localDataSource);

            return InitSyncMLFacade(r) ? r : null;
        }

         bool InitSyncMLFacade(SyncMLFacade  r)
        {
            r.User = syncSettings.User;
            //    facade.ContactMediumType = ContactExchangeType.Vcard21;
            string password = ObtainPasswordFromSettingsOrUx();
            if (String.IsNullOrEmpty(password))
                return false;
            else
                r.Password = password;

            r.BasicUriText = syncSettings.BasicUri;

            r.DeviceAddress = syncSettings.DeviceAddress;

            r.LastAnchorTime = syncItem.LastAnchorTime;
            r.LastAnchor = syncItem.LastAnchor; // Next anchor is generated so before alert
            r.VendorName = syncSettings.VendorName;
            r.ModelName = syncSettings.ModelName;
            r.ModelVersion = syncSettings.ModelVersion;
            r.ContactDataSourceAtServer = syncItem.RemoteName;

            r.LastAnchorChangedEvent += HandleLastAnchorChanged;


            #region Wire event handlings to Facade's events regarding to visual effects
            r.StartOperationEvent += HandleStartOperation;
            r.EndOperationEvent += HandleEndOperation;
            r.OperationStatusEvent += HandleOperationMessage;
            r.ServerDeviceInfoEvent += HandleServerDeviceInfo;

            r.InitProgressBarEvent += HandleInitProgressBar;
            r.IncrementProgressBarEvent += HandleIncrementProgressBar;
            r.StageMessageEvent += new EventHandler<StatusEventArgs>(HandleStageMessageEvent);

            r.InitProgressBarReceivingEvent += HandleInitProgressBarReceiving;
            r.IncrementProgressBarReceivingEvent += HandleIncrementProgressBarReceiving;
            r.StageMessageReceivingEvent += HandleStageMessageReceivingEvent;
            r.GracefulStopEvent += new EventHandler<EventArgs>(facade_GracefulStopEvent);
            #endregion

            System.Net.WebProxy proxy;
            if (syncSettings.UseProxy)
            {
                proxy = new System.Net.WebProxy(syncSettings.Proxy);
                proxy.UseDefaultCredentials = true;
            }
            else
                proxy = null;

            r.Proxy = proxy;

            return true;
        }

        /// <summary>
        /// Check LocalDataSourceAssembly to find a class type which supports ILocalDataSource.
        /// </summary>
        /// <returns></returns>
        private Type FindLocalDataSourceTypeInAssembly()
        {
            Assembly assembly;
            try
            {
                assembly = Assembly.Load(syncItem.LocalDataSourceAssembly);
            }
            catch (System.IO.FileLoadException e)
            {
                AppendStatusText("When loading LocalDataSourceAssembly: " + e.Message);
                return null;
            }
            catch (System.IO.FileNotFoundException e)
            {
                AppendStatusText("When loading LocalDataSourceAssembly: " + e.Message);
                return null;
            }

            Trace.TraceInformation("Local data source is from this assembly: " + syncItem.LocalDataSourceAssembly);
            //           Trace.Assert(assembly != null, "Assembly with LocalDataSourceAssembly must be defined");
            if (assembly == null)
            {
                return null;
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
        private ILocalDataSource CreateLocalDataSource()
        {
            Type localDataSourceType=null;

            if (!String.IsNullOrEmpty(syncItem.LocalDataSourceAssembly))
            {
                localDataSourceType = FindLocalDataSourceTypeInAssembly();

                if (localDataSourceType == null)
                {
                    return null;
                }
            }


            try
            {
                object filter = syncItem.GetFilter();
                object handle;
                if (filter == null)
                {
                    handle = Activator.CreateInstance(localDataSourceType, new object[] { syncItem.DataFormat });
                }
                else
                {
                    handle = Activator.CreateInstance(localDataSourceType, new object[] { syncItem.DataFormat, filter });
                }
                ILocalDataSource source = (ILocalDataSource)handle;
                //  source.ExchangeType = syncItem.DataFormat;
                return source;
            }
            catch (TargetInvocationException e)
            {
                Trace.TraceInformation("When LoadLocalDataSource: " + e.Message);
                return null;
            }
        }

        private string ObtainPasswordFromSettingsOrUx()
        {
            if (!String.IsNullOrEmpty(syncSettings.Password))
            {
                return syncSettings.Password;
            }

            using (CommonForm fm = new CommonForm())
            {
                UCPassword ucPassword = new UCPassword();
                fm.AddControlForDialog(ucPassword, "Password");
                if (fm.ShowDialog() == DialogResult.OK)
                    return ucPassword.Password;
                else
                    return null;
            }

        }

        void facade_GracefulStopEvent(object sender, EventArgs e)
        {
            AppendStatusText("!!! Being stopped by user gracefully !!!");
            ShowDefaultCursor();
            SetControlVisible(btnStop, false);
        }

        private void StopSync()
        {
            facade.StopSync();
        }

        #region ProgressBars and stage messages of sync progress

        private void HandleStageMessageEvent(object sender, StatusEventArgs e)
        {
            DisplayStageMessage(e.Text);
        }

        private void DisplayStageMessage(string text)
        {
            if (barLabelSending.Label.InvokeRequired)
            {
                ProcessTextHandler d = new ProcessTextHandler(DisplayStageMessage);
                this.Invoke(d, new object[] { text });
            }
            else
                barLabelSending.Label.Text = text;

        }

        private void HandleStageMessageReceivingEvent(object sender, StatusEventArgs e)
        {
            DisplayStageMessageReceiving(e.Text);
        }

        private void DisplayStageMessageReceiving(string text)
        {
            if (barLabelReceiving.Label.InvokeRequired)
            {
                ProcessTextHandler d = new ProcessTextHandler(DisplayStageMessageReceiving);
                this.Invoke(d, new object[] { text });
            }
            else
                barLabelReceiving.Label.Text = text;

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
            if (progressBarSending.InvokeRequired)
            {
                InitProgressBarHandler d = new InitProgressBarHandler(InitProgressBar);
                this.Invoke(d, new object[] { e });
            }
            else
            {
                progressBarSending.Value = 0;
                progressBarSending.Minimum = e.MinValue;
                progressBarSending.Maximum = e.MaxValue;
                progressBarSending.Step = e.Step;
                progressBarSending.Visible = true;
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
            }

        }

        private void SetControlVisible(Control control, bool visible)
        {
            if (control.InvokeRequired)
            {
                Action<Control, bool> d = new Action<Control, bool>(SetControlVisible);
                this.Invoke(d, new object[] { control, visible });
            }
            else
            {
                control.Visible = visible;
            }
        }

        private void SetControlEnabled(Control control, bool enabled)
        {
            if (control.InvokeRequired)
            {
                Action<Control, bool> d = new Action<Control, bool>(SetControlEnabled);
                this.Invoke(d, new object[] { control, enabled });
            }
            else
            {
                control.Enabled = enabled;
            }
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
            if (progressBarSending.InvokeRequired)
            {
                ProcessIntegerHandler d = new ProcessIntegerHandler(IncrementProgressBar);
                this.Invoke(d, new object[] { amount });
            }
            else
            {
                progressBarSending.Increment(amount);
            }

        }
        #endregion

        /// <summary>
        /// Optional text box to display status text.
        /// </summary>
        public TextBox TextBoxStatus { get; set; }

        /// <summary>
        /// Display text asynchroneously in logBox;
        /// </summary>
        /// <param name="s"></param>
        private void AppendStatusText(string s)
        {
            if (TextBoxStatus == null)
                return;

            if (s == null)
                return;

            if (TextBoxStatus.InvokeRequired)
            {
                ProcessTextHandler d = new ProcessTextHandler(AppendStatusText);
                this.Invoke(d, new object[] { s });
            }
            else
            {
                // statusLabel.Text = s;
                TextBoxStatus.AppendText(DateTime.Now.ToLongTimeString() + ": " +
                    s + Environment.NewLine);
            }
        }

        #region Facade event handlers

        private void HandleOperationMessage(object sender, StatusEventArgs e)
        {//todo: this may be replaced by TraceSource
            AppendStatusText(e.Text);
        }


        private void HandleStartOperation(object sender, StatusEventArgs e)
        {
            AppendStatusText(e.Text);
            ShowWaitCursor();

            SetControlVisible(progressBarSending, false);  // the progress bar will be displayed when the model initialize the progress bar.
            SetControlVisible(progressBarReceiving, false);
        }

        private void HandleLastAnchorChanged(object sender, AnchorChangedEventArgs e)
        {
            ///Apparetly reference to complex type of ApplicationSettingsBase works properly only in the same class and in the same thread
            ///otherwise, the changes to properties of the complex type can not be detected by ApplicationSettingsBase.
            ///This function is called in another thread of the Facade. So I need to use propertyName to make the dirty flag work properly.
            syncItem.LastAnchorTime = e.Time;
            syncItem.LastAnchor = e.LastAnchor;
            syncSettings.Save();
            AppendStatusText("Last sync: " + syncItem.LastAnchorTime.ToString("yyyy-MM-dd HH:mm:ss"));
            ShowLastSyncInfo();
        }

        private void HandleEndOperation(object sender, StatusEventArgs e)
        {
            AppendStatusText(e.Text);
            ShowDefaultCursor();
            SetControlEnabled(btnSync, true);
            SetControlVisible(btnStop, false);
        }




        private void ShowWaitCursor()
        {
            if (this.InvokeRequired)
            {
                Action d = () => { ShowWaitCursor(); };
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
                Action d = () => { ShowDefaultCursor(); };
                this.Invoke(d);
            }
            else
            {
                Cursor = Cursors.Default;
            }
        }

        #endregion

         private void btnSync_Click(object sender, EventArgs e)
        {
            Sync();
        }

        public IAsyncResult Sync()
        {
            SyncType syncType = syncItem.SyncDirection;
            switch (syncType)
            {
                case SyncType.TwoWay:
                    return TwoWaySync();
                case SyncType.Slow:
                    return SlowSync();
                case SyncType.OneWayFromClient:
                    if (MessageBox.Show("This operation will send modified content to the server, and will not receive modifications from server. Both sides might then lost sync. Do you want to continue?",
                "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        return SyncFromClient();
                    break;
                case SyncType.OneWayFromServer:
                    if (MessageBox.Show("This operation will get modified content from the server, and will not send local updates to server. Both sides might then lost sync. Do you want to continue?",
              "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        return SyncFromServer();
                    break;
                case SyncType.RefreshFromServer:
                    if (MessageBox.Show("This operation will download all content from the server, which will overwrite all data in local database. Do you want to continue?",
                          "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        return RefreshFromServer();
                    break;
                case SyncType.RefreshFromClient:
                    if (MessageBox.Show("This operation will send all content to the server, and overwrite all data in server. Do you want to continue?",
                        "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        return RefreshFromClient();
                    break;
                default:
                    Debug.Assert(false, "What synctype?");
                    break;
            }
            return null;
        }

        private IAsyncResult Sync(SyncType syncType)
        {
            SetControlEnabled(btnSync, false);//will be enabled when sync is finished.
            SetControlVisible(btnStop, true);

            facade = CreateFacadeForSyncSession();
            if (facade != null)
            {
                return facade.LogOnAndSyncAsync(syncType);
            }
            else
            {
                SetControlEnabled(btnSync, true);
                SetControlVisible(btnStop, false);
            }
            return null;
        }

        private IAsyncResult TwoWaySync()
        {
            return Sync(SyncType.TwoWay);
        }

        private IAsyncResult SlowSync()
        {
            return Sync(SyncType.Slow);
        }

        private IAsyncResult SyncFromClient()
        {
            return Sync(SyncType.OneWayFromClient);
        }

        /// <summary>
        /// Basiclly RefreshFromClient with empty data in order to delete all on server,
        /// then run slow sync to add to the server.
        /// This is because some SyncML servers do not support Refresh from Client.
        /// </summary>
        private IAsyncResult RefreshFromClient()
        {
            return Sync(SyncType.RefreshFromClient);
        }

        private IAsyncResult SyncFromServer()
        {
            return Sync(SyncType.OneWayFromServer);
        }

        private IAsyncResult RefreshFromServer()
        {
            return Sync(SyncType.RefreshFromServer);
        }

    }

    internal class ProgressBarLabel
    {
        public ProgressBarLabel(ProgressBar bar)
        {
            this.bar = bar;
            Label = new Label();
            Label.AutoSize = true;
            Label.FlatStyle = FlatStyle.Flat;
            Label.Top = (bar.Height - Label.Font.Height) / 2;
            Label.TextChanged += new EventHandler(Label_TextChanged);

            bar.Controls.Add(Label);
            bar.SizeChanged += new EventHandler(bar_SizeChanged);
        }

        void Label_TextChanged(object sender, EventArgs e)
        {
            AdjustLabelLeft();
        }

        void bar_SizeChanged(object sender, EventArgs e)
        {
            AdjustLabelLeft();
        }

        ProgressBar bar;

        public Label Label { get; private set; }

        void AdjustLabelLeft()
        {
            Size size = TextRenderer.MeasureText(Label.Text, Label.Font);
            Label.Left = (bar.Width - size.Width) / 2;
        }

    }
}
