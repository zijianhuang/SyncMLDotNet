using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Runtime.Remoting.Messaging;
using System.Diagnostics;
using Fonlow.SyncML.Common;
using System.Collections.ObjectModel;
using Fonlow.SyncML.Elements;

namespace Fonlow.SyncML
{
#pragma warning disable 0067
    /// <summary>
    /// Interfaces for frequent used sync commands by end users. Aggregrated functions to
    /// complete a sync task.
    /// The Facade should be used only once for each sync session because of the initialization of flags.
    /// </summary>
    public class SyncMLFacade
    {
        /// <summary>
        /// Major constructor. Connect to external platform provider.
        /// </summary>
        public SyncMLFacade(ILocalDataSource localDataSource)
        {
            SyncType = SyncType.TwoWay;
            if (localDataSource == null)
            {
                throw new ArgumentNullException("localDataSource", "LocalDataSource was not created.");
            }
            this.localDataSource = localDataSource;
        }

        #region SyncFacadeBase

        private System.Net.WebProxy proxy;

        /// <summary>
        /// Proxy for the web connection, external.
        /// </summary>
        public System.Net.WebProxy Proxy
        {
            get { return proxy; }
            set { proxy = value; }
        }


        /// <summary>
        /// Basic URI connection to a sync server. Defined in configuration.
        /// </summary>
        public string BasicUriText
        {
            get;
            set;
        }


        /// <summary>
        /// Last anchor.
        /// </summary>
        public DateTime LastAnchorTime
        {
            get;
            set;
        }

        /// <summary>
        /// The Last sync anchor describes the last event (e.g., time) when the database was synchronized from the point of
        /// sending device
        /// </summary>
        public string LastAnchor { get; set; }

        /// <summary>
        /// Next sync anchor describes the current event of sync from the point of sending device.
        /// </summary>
        public string NextAnchor { get; set; }

        public string LastAnchorTimeText
        {
            get { return LastAnchorTime.ToString("yyyyMMddTHHmmssZ"); }
        }

        private ILocalDataSource localDataSource;
        /// <summary>
        /// External address book platform provider
        /// </summary>
        public ILocalDataSource LocalDataSource
        {
            get
            {
                return localDataSource;
            }
        }


        public ISyncCommandsSource GenerateSyncCommandsSource(DateTime lastAnchor)
        {
            ISyncCommandsSource commandSource = LocalDataSource.GenerateSyncCommandsSource(lastAnchor);
            if ((SyncType == SyncType.Slow) || (SyncType == SyncType.RefreshFromClient))
            {
                commandSource.PrepareForSlowSync();
            }
            return commandSource;
        }

        private string contactDataSourceAtServer = "scard"; //scard is used by Funambol for Sif-C.
        /// <summary>
        /// card (vCard), or scard (sifc) at the SymcML server, generally defined in configuration,
        /// by the end user after acquiring such info from the service web page.
        /// </summary>
        public string ContactDataSourceAtServer
        {
            get { return contactDataSourceAtServer; }
            set { contactDataSourceAtServer = value; }
        }

        /// <summary>
        /// Fire when a major operation start, with message telling the world. The client codes will start hourglass cursor.
        /// </summary>
        public event EventHandler<StatusEventArgs> StartOperationEvent;


        /// <summary>
        /// Fire when a major operation end normally or abnormally, with message telling the world. The client codes will show default cursor.
        /// </summary>
        public event EventHandler<StatusEventArgs> EndOperationEvent;

        /// <summary>
        /// Fire to show operation message to user.
        /// </summary>
        public event EventHandler<StatusEventArgs> OperationStatusEvent;

        public void DisplayOperationMessage(string message)
        {
            EventsHelper.Fire(OperationStatusEvent, this, new StatusEventArgs(message));
        }

        /// <summary>
        /// Fire when last anchor gets updated
        /// </summary>
        public event EventHandler<AnchorChangedEventArgs> LastAnchorChangedEvent;

        protected void NotifyLastAnchorChanged(DateTime anchorTime, string anchor)
        {
            EventsHelper.Fire(LastAnchorChangedEvent, this, new AnchorChangedEventArgs(anchorTime, anchor));
        }

        #region Functions of reporting errors
        /// <summary>
        /// Trace with custom error message, under category Error.
        /// </summary>
        /// <param name="text"></param>
        protected static void HandleErrorStatus(string text)
        {
            Trace.TraceWarning(text);
        }

        /*private static void HandleErrorPositive(string text)
        {
            Trace.TraceInformation(text, TraceCategory.positiveError);
        }*/
        /// <summary>
        /// Trace Error message of exception, under category Exception
        /// </summary>
        /// <param name="exception"></param>
        protected static void WriteErrorException(Exception exception)
        {
            Trace.TraceWarning("Exception thrown in the thread: " + exception);
        }


        /// <summary>
        /// Provide general handling of async call back
        /// </summary>
        /// <param name="asyncResult"></param>
        protected static void GeneralCompletionCallback(IAsyncResult asyncResult)
        {
            AsyncResult resultObj = (AsyncResult)asyncResult;
            Action d = resultObj.AsyncDelegate as Action;
            try
            {
                d.EndInvoke(asyncResult);
            }
            catch (Exception e)
            {
                WriteErrorException(e);
                throw;
            }
        }


        #endregion

        #endregion

        private MessagesInPairs statusMessages;
        /// <summary>
        /// Provide messages of different status codes.
        /// The object of the property should generally be assigned by client codes.
        /// If the client codes do not assign one, this class will create one, and the client codes
        /// still nedd to initialize it for loading a file with the mapping between codes and messages.
        /// </summary>
        public MessagesInPairs StatusMessages
        {
            get
            {
                if (statusMessages == null)
                {
                    statusMessages = new MessagesInPairs();
                    statusMessages.LoadMessages(Properties.Resources.StatusMessages);
                }

                return statusMessages;
            }
            set { statusMessages = value; }
        }

        #region Parameters for sync

        private string user;
        /// <summary>
        /// User name of the sync account
        /// </summary>
        public string User
        {
            get { return user; }
            set { user = value; }
        }

        private string password;
        /// <summary>
        /// Password of the sync account
        /// </summary>
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string deviceAddress;
        /// <summary>
        /// Devices, which are, for example, connected temporarily, MAY prefer to identify themselves with an
        /// own identification mechanism.
        /// Refer to:http://www.syncml.org/docs/syncml_represent_v11_20020215.pdf 4.6 Identifiers page 13
        /// Refer to:http://en.wikipedia.org/wiki/Uniform_Resource_Name
        /// </summary>
        public string DeviceAddress
        {
            get { return deviceAddress; }
            set { deviceAddress = value; }
        }

        private string databaseAddress;
        /// <summary>
        /// The database addressing within the SyncML operations is done by using the URI scheme defined in
        /// the SyncML Representation protocol. Absolute or relative URI's can be used for the server and
        /// client databases.
        /// </summary>
        public string DatabaseAddress
        {
            get { return databaseAddress; }
            set { databaseAddress = value; }
        }

        private string vendorName;
        /// <summary>
        /// Manufacturer of sync client
        /// </summary>
        public string VendorName
        {
            get { return vendorName; }
            set { vendorName = value; }
        }

        private string modelName;
        /// <summary>
        /// Model. In workstation, it is the application name of local data source.
        /// </summary>
        public string ModelName
        {
            get { return modelName; }
            set { modelName = value; }
        }

        private string modelVersion;
        /// <summary>
        /// Software version.
        /// </summary>
        public string ModelVersion
        {
            get { return modelVersion; }
            set { modelVersion = value; }
        }

        #endregion

        /// <summary>
        /// Sync type. Default TwoWay.
        /// </summary>
        public SyncType SyncType
        {
            get; set;
        }


        /// <summary>
        /// Base64 encoded user password
        /// </summary>
        protected string UserPasswordBase64Encoded
        {
            get { return Utility.ConvertUtf8TextToBase64(User + ":" + Password); }
        }

        /// <summary>
        /// This ID is initialized when the first time connecting to the sync server.
        /// </summary>
        private string currentSessionID;
        internal string CurrentSessionID { get { return currentSessionID; } }

        private int currentMsgID = 1;
        /// <summary>
        /// Increasing the ID must be before ProcessResponse of a syncStep, which might result in new message.
        /// </summary>
        internal int CurrentMsgID { get { return currentMsgID; } set { currentMsgID = value; } }

        private SyncMLConnections connections;
        /// <summary>
        /// This is a factory that create a SyncMLConnection for each web request. 
        /// By default, HttpWebRequest uses the same persisten http connection.
        /// The startup URI and follow-up URI are different with each connection.
        /// </summary>
        internal SyncMLConnections Connections
        {
            get
            {
                if (connections == null)
                    connections = new SyncMLConnections(BasicUriText, Proxy);

                return connections;
            }
        }

        /// <summary>
        /// Logon to syncml server.
        /// </summary>
        public bool LogOn()
        {
            AuthenticationTypeOfNextMessage = SyncMLAuthenticationType.Base64;
            EventsHelper.Fire(StartOperationEvent, this, new StatusEventArgs("Start Logon"));

            currentSessionID = DateTime.Now.ToString("ddHHmmss");
            logonAttempts = 0;
            return SendLogonMessage();
        }

        int logonAttempts;

        internal SyncMLAuthenticationType AuthenticationTypeOfNextMessage { get; set; }

        private bool SendLogonMessage()
        {
            LogOnMessage logonMessage = new LogOnMessage(this);
            logonMessage.Send();
            logonAttempts++;

            if ((!logonMessage.LoggedOn) && (logonAttempts < 2))// fail then recursively sendLogMessage again.
            {
                switch (logonMessage.AuthenticationStatus)
                {
                    case SyncMLAuthenticationStatus.Base64Chal:
                        return SendLogonMessage();
                    case SyncMLAuthenticationStatus.MD5Chal: //this was not yet tested, since I don't know how to make the server challenge with MD5.
                        return SendLogonMessage();
                    default: // until the server does not ask for authentication
                        Debug.WriteLine(false, "AuthenticationStatus is " + logonMessage.AuthenticationStatus.ToString());
                        SoFarOK = false;
                        return false;

                }
            }

            EventsHelper.Fire(EndOperationEvent, this, new StatusEventArgs(null));  // just change back to default cursor, but not display message.
            return logonMessage.LoggedOn;
        }

        ///// <summary>
        ///// RespUri to get remote source.
        ///// </summary>
        //internal string CurrentRespUri { get { return Connections.Uri; } }

        /// <summary>
        /// SyncML command pool shared by all sync steps, to store commands for next request.
        /// A sync step should pick up commands in the beginning when forming a syncml message, then clear.
        /// </summary>
        private Collection<SyncMLCommand> responseCommandPool = new Collection<SyncMLCommand>();
        internal Collection<SyncMLCommand> ResponseCommandPool { get { return responseCommandPool; } }



        /// <summary>
        /// Begin a sync session.
        /// </summary>
        private void Sync()
        {
            if (LocalDataSource.BeginSync("")) // PlatformProvider permits sync
            {
                EventsHelper.Fire(StartOperationEvent, this, new StatusEventArgs("Start Sync"));
                if (LogOn())
                {
                    LocalDataSource.EndSync("", OperationStatus.Unspecified);  // PlatformProvider will generally refresh GUI.
                }
            }
            else
            {
                DisplayOperationMessage("Local data source rejects sync. Please check the settings of Open Contacts to enable sync, or if Fonlow.OpenContacts.Interop.dll is installed.");
                SoFarOK = false;
            }
        }

        public IAsyncResult LogOnAndSyncAsync(SyncType syncType)
        {
            SyncType = syncType;
            Action d = () => { Sync(); };
            return d.BeginInvoke(SyncCompletionCallback, null);
        }

        private bool soFarOK = true;

        /// <summary>
        /// Each sync steps message sent should report when sending message is so far OK.
        /// </summary>
        internal bool SoFarOK
        {
            get
            {
                lock (this)
                {
                    return soFarOK;
                }
            }

            set
            {
                lock (this)
                {
                    soFarOK = value;
                }
            }
        }


        /// <summary>
        /// Decoded next nonce sent from the server.
        /// </summary>
        internal byte[] Md5NextNonceFromServer
        {
            get;
            set;
        }

        /// <summary>
        /// Invoked by SyncAsync.
        /// </summary>
        /// <param name="asyncResult"></param>
        private void SyncCompletionCallback(IAsyncResult asyncResult)
        {
            try
            {
                GeneralCompletionCallback(asyncResult);
            }
            catch (Exception e)//exception was actually caugh and handled.
            {
                Trace.TraceWarning("When calling GeneralCompletionCallback (General Exception caught), " + e);
                soFarOK = false;
            }

            if (GracefulStop)
            {
                DisplayOperationMessage("Stopped by user gracefully.");
                return;
            }

            if (SoFarOK)
            {
                SyncComplete();
                EventsHelper.Fire(EndOperationEvent, this, new StatusEventArgs("End Sync", OperationStatus.Successful));
                localDataSource.EndSync("End Sync", OperationStatus.Successful);
            }
            else
            {
                EventsHelper.Fire(EndOperationEvent, this, new StatusEventArgs("Sync is not complete.", OperationStatus.Failed));
                localDataSource.EndSync("Sync is not complete.", OperationStatus.Failed);
            }
        }

        static readonly DevinfDataStore devinfDataStore = DevinfDataStore.Create(XElement.Parse(Properties.Resources.DataStoreSIFC));
        static readonly DevinfDataStore vCard21DataStore = DevinfDataStore.Create(XElement.Parse(Properties.Resources.DataStoreVCard21));
        private DevInf devinf;
        /// <summary>
        /// Device info.
        /// </summary>
        public DevInf LocalDevinf
        {
            get
            {
                if (devinf == null)
                {
                    devinf = DevInf.Create();
                    devinf.VerDTD = SyncMLSimpleElementFactory.Create<DevinfVerDTD>("1.2");
                    devinf.Man = SyncMLSimpleElementFactory.Create<DevinfMan>(VendorName);
                    devinf.Mod = SyncMLSimpleElementFactory.Create<DevinfMod>(ModelName);
                    devinf.OEM = SyncMLSimpleElementFactory.Create<DevinfOEM>(User);
                    //     devinf.FwV = SyncMLSimpleElementFactory.Create<DevinfFwV>("20080108");//may not be needed. not significant for OC
                    //     devinf.HwV = SyncMLSimpleElementFactory.Create<DevinfHwV>("20080108");//might not be needed for workstation.

                    devinf.SwV = SyncMLSimpleElementFactory.Create<DevinfSwV>(ModelVersion);//maybe OC or SyncML Client version later
                    devinf.DevID = SyncMLSimpleElementFactory.Create<DevinfDevID>(DeviceAddress); //might not be needed
                    devinf.DevTyp = SyncMLSimpleElementFactory.Create<DevinfDevTyp>("workstation");
                    DevinfSupportNumberOfChanges supportNumberOfChanges = devinf.SupportNumberOfChanges; // just to create this propery through referencing
                    DevinfSupportLargeObjs supportLargeObjs = devinf.SupportLargeObjs; // just to create this property by referencing to it

                    devinf.DataStoreCollection.Add(devinfDataStore);
                    devinf.DataStoreCollection.Add(vCard21DataStore);


                }

                return devinf;

            }

        }

        internal int totalNumberOfChangesReceiving;

        internal int numberOfChangesReceived;

        internal int totalNumberOfChangesSending;

        internal bool clientRequestMore;

        #region events for visual effects to the end users
        /// <summary>
        /// Fired after receiving device info of the server
        /// </summary>
        public event EventHandler<DeviceInfoEventArgs> ServerDeviceInfoEvent;//VS 2008 falsely reports this event is not used. so I have to add #pragma warning disable 0067 for this class

        /// <summary>
        /// Initialize a progress bar of GUI to show progress of sending
        /// </summary>
        public event EventHandler<InitProgressBarEventArgs> InitProgressBarEvent;

        /// <summary>
        /// Fire event of InitProgressBarEvent.
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="step"></param>
        internal void InitProgressBar(int minValue, int maxValue, int step)
        {
            EventsHelper.Fire(InitProgressBarEvent, this, new InitProgressBarEventArgs(minValue, maxValue, step));
        }

        /// <summary>
        /// Fired when starting to receive sync content from the server, and initialize a progress bar of GUI to show progress of receiving.
        /// </summary>
        public event EventHandler<InitProgressBarEventArgs> InitProgressBarReceivingEvent;

        /// <summary>
        /// Fire event of InitProgressBarReceiving.
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="step"></param>
        internal void InitProgressBarReceiving(int minValue, int maxValue, int step)
        {
            EventsHelper.Fire(InitProgressBarReceivingEvent, this, new InitProgressBarEventArgs(minValue, maxValue, step));
        }
        /// <summary>
        /// Fired during receiving sync content. The number of sync commands will be passed to the progress bar.
        /// </summary>
        public event EventHandler<IncrementProgressBarEventArgs> IncrementProgressBarReceivingEvent;

        internal void IncrementProgressBarReceiving(int amount)
        {
            EventsHelper.Fire(IncrementProgressBarReceivingEvent, this, new IncrementProgressBarEventArgs(amount));
        }

        /// <summary>
        /// Fired during sending sync content. The number of sync commands in each message will be passed to the progress bar.
        /// </summary>
        public event EventHandler<IncrementProgressBarEventArgs> IncrementProgressBarEvent;

        internal void IncrementProgressBar(int amount)
        {
            EventsHelper.Fire(IncrementProgressBarEvent, this, new IncrementProgressBarEventArgs(amount));
        }

        /// <summary>
        /// Tell the world to display a stage message along with the progress bar. The GUI generally will display the message in a label.
        /// </summary>
        public event EventHandler<StatusEventArgs> StageMessageEvent;

        /// <summary>
        /// Fire event of StageMessageEvent.
        /// </summary>
        /// <param name="text"></param>
        internal void DisplayStageMessage(string text)
        {
            EventsHelper.Fire(StageMessageEvent, this, new StatusEventArgs(text));
        }

        public event EventHandler<StatusEventArgs> StageMessageReceivingEvent;

        internal void DisplayStageMessageReceiving(string text)
        {
            EventsHelper.Fire(StageMessageReceivingEvent, this, new StatusEventArgs(text));
        }

        #endregion

        /// <summary>
        /// Fired when gracefulStop is marked and the codes just return gracefully.
        /// </summary>
        public event EventHandler<EventArgs> GracefulStopEvent;
        internal void NotifyStoppedGracefully()
        {
            EventsHelper.Fire(GracefulStopEvent, this, new EventArgs());
        }

        /// <summary>
        /// After sync is completed, tell the world to save lastAnchor.
        /// </summary>
        internal void SyncComplete()
        {
            LastAnchor = NextAnchor;
            LastAnchorTime = DateTime.Now.AddSeconds(1);
            //The timestamps saved for local contacts records may have a few ms latency, resulting unnecessary updates
            //for next sync, so adding 1 second to LastAnchorTime to compensate the latency.

            //Tell the world to save lastAnchor
            NotifyLastAnchorChanged(LastAnchorTime, LastAnchor);
        }

        private ISyncCommandsSource commandsToSend;
        /// <summary>
        /// Shared buffer by multiple SyncML messages to be sent to the server.
        /// </summary>
        internal ISyncCommandsSource CommandsToSend { get { return commandsToSend; } set { commandsToSend = value; } }

        internal bool ExtractNextCommands(IList<Fonlow.SyncML.Elements.SyncMLCommand> commands)
        {
            MoreSyncCommands = commandsToSend.ExtractNextCommands(commands);
            return MoreSyncCommands;
        }

        internal bool MoreSyncCommands { get; private set; }

        private bool gracefulStop;
        public bool GracefulStop
        {
            get
            {
                lock (this)
                {
                    return gracefulStop;
                }
            }

            private set
            {
                lock (this)
                {
                    gracefulStop = value;
                    if (gracefulStop)
                        NotifyStoppedGracefully();
                }
            }
        }

        /// <summary>
        /// Signal Facade to stop gracefully
        /// </summary>
        public void StopSync()
        {
            GracefulStop = true;
            DisplayOperationMessage("The user tried to stop sync gracefully. Both the server and the client might be partially updated. Please wait for confirmation of the graceful exit.");
        }
    }



    /// <summary>
    /// Register which commands have been issued in a syncML message, 
    /// and the status codes returned from server.
    /// </summary>
    public class CommandResponseRegister
    {
        private Collection<CommandResponsePair> pairs;
        public CommandResponseRegister()
        {
            pairs = new Collection<CommandResponsePair>();
        }
        /// <summary>
        /// Add a command to the register.
        /// </summary>
        /// <param name="command"></param>
        public void Add(SyncMLCommand command)
        {
            pairs.Add(CommandResponsePair.Create(command));
        }

        public void Clear()
        {
            pairs.Clear();
        }

        public int Count
        {
            get { return pairs.Count; }
        }

        /// <summary>
        /// Marked a command registered with a status code from server.
        /// </summary>
        /// <param name="cmdRef">Index to locate the command to register.</param>
        /// <param name="statusCode">Status code.</param>
        /// <returns>Command if located.</returns>
        public SyncMLCommand RegisterStatus(string cmdRef, string statusCode)
        {
            SyncMLCommand r = null;
            foreach (CommandResponsePair pair in pairs)
            {
                if (pair.CmdRef == cmdRef)
                {
                    pair.StatusCode = statusCode;
                    r = pair.Command;
                    break;
                }
            }

            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>True if all commands have status.</returns>
        public bool IsAllCommandsReturnedWithStatus()
        {
            foreach (CommandResponsePair pair in pairs)
            {
                if (String.IsNullOrEmpty(pair.StatusCode))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Generally client codes will display commands without status after checking IsAllCommandsReturnedWithStatus().
        /// </summary>
        public Collection<SyncMLCommand> CommandsWithoutStatus
        {
            get
            {
                Collection<SyncMLCommand> outstandingPairs = new Collection<SyncMLCommand>();
                foreach (CommandResponsePair pair in pairs)
                {
                    if (String.IsNullOrEmpty(pair.StatusCode))
                    {
                        outstandingPairs.Add(pair.Command);
                    }

                }

                return outstandingPairs;
            }
        }

        /// <summary>
        /// Return XML of commands without status, separated by new line.
        /// </summary>
        public string CommandsXmlWithoutStatus
        {
            get
            {
                string s = "";
                foreach (SyncMLCommand command in CommandsWithoutStatus)
                {
                    s += command.Xml + Environment.NewLine;
                }
                return s;
            }
        }

    }

    /// <summary>
    /// Record what command has been issued in a SyncML message, and the statusCode returned from server.
    /// </summary>
    public class CommandResponsePair
    {
        private string cmdRef;
        /// <summary>
        /// Record the CmdID of command sent to server. CmdRef will be the key to look up.
        /// </summary>
        public string CmdRef
        {
            get { return cmdRef; }
        }

        private string statusCode;
        /// <summary>
        /// Status code returned from the server
        /// </summary>
        public string StatusCode
        {
            get { return statusCode; }
            set { statusCode = value; }
        }

        private SyncMLCommand command;
        /// <summary>
        /// Point to the respective command in syncml
        /// </summary>
        public SyncMLCommand Command
        {
            get { return command; }
        }

        private CommandResponsePair()
        {

        }

        /// <summary>
        /// Create a pair.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static CommandResponsePair Create(SyncMLCommand command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            CommandResponsePair r = new CommandResponsePair();
            r.command = command;
            r.cmdRef = command.CmdID.Content;
            return r;
        }

    }

}
