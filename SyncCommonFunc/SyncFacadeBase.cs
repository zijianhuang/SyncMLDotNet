using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections.Specialized;
using System.Diagnostics;
using Fonlow.CommonSync;
using System.Runtime.Remoting.Messaging;

[assembly: CLSCompliant(false)]
namespace Fonlow.CommonSync
{
    /// <summary>
    /// For event 
    /// </summary>
    public class TextChangedEventArgs : StatusEventArgs
    {
        public TextChangedEventArgs(string text)
            : base(text)
        {

        }
    }

    /// <summary>
    /// Basic facade framework for OcPlaxo and SyncML
    /// </summary>
    public class SyncFacadeBase
    {
        /// <summary>
        /// construct the facade and wire with platformProvider
        /// </summary>
        /// <param name="platformProvider"></param>
        public SyncFacadeBase(ILocalDataSource platformProvider)
        {
            this.localDataSource = platformProvider;
        }

        /// <summary>
        /// Generally for unit testing
        /// </summary>
        public SyncFacadeBase()
        {

        }

        private System.Net.WebProxy proxy;

        /// <summary>
        /// Proxy for the web connection, external.
        /// </summary>
        public System.Net.WebProxy Proxy
        {
            get { return proxy; }
            set { proxy = value; }
        }


        private TraceSwitch traceSwitch;

        /// <summary>
        /// External trace switch
        /// </summary>
        public TraceSwitch TraceSwitch
        {
            get { return traceSwitch; }
            set { traceSwitch = value; }
        }


        private string basicUriStr;
        /// <summary>
        /// Basic URI connection to a sync server.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Str")]
        public string BasicUriStr
        {
            get { return basicUriStr; }
            set { basicUriStr = value; }
        }


        private DateTime lastAnchorTime;
        /// <summary>
        /// Last anchor.
        /// </summary>
        public DateTime LastAnchorTime
        {
            get { return lastAnchorTime; }
            set { lastAnchorTime = value; }
        }

        private int lastAnchor;
        public int LastAnchor { get { return lastAnchor; } set { lastAnchor = value; } }

        public int NextAnchor { get { return lastAnchor + 1; } }

        public string LastAnchorTimeStr
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


        /// <summary>
        /// Provide an internal namespace to group strings which will be used to represent categories of trace.
        /// </summary>
        protected sealed class TraceCategory
        {
            private TraceCategory()
            {

            }
            public const string OperationStatus = "OperationStatus";
            public const string Error = "Error";
            public const string Warning = "private const string";
            public const string PositiveError = "PositiveError";
            public const string Exception = "Exception";
        }

        private bool firstSync;

        /// <summary>
        /// Indicate whether sync was done before. This flag is generally initialized by checking some persistent settings.
        /// </summary>
        public bool FirstSync
        {
            get { return firstSync; }
            set { firstSync = value; }
        }


        /// <summary>
        /// Fire when a major operation start, with message telling the world. The client codes will start hourglass cursor.
        /// </summary>
        public event EventHandler<StatusEventArgs> StartOperationEvent;

        /// <summary>
        /// Tell application's GUI about the start of the sync operation through wired StartOperationEvent.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TellStartOperation(object sender, StatusEventArgs e)
        {
            if (StartOperationEvent != null)
                StartOperationEvent(sender, e);
        }

        /// <summary>
        /// Fire when a major operation end normally or abnormally, with message telling the world. The client codes will show default cursor.
        /// </summary>
        public event EventHandler<StatusEventArgs> EndOperationEvent;

        /// <summary>
        /// Tell application's GUI that the sync is completed, through the wired EndOperationEvent.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TellEndOperation(object sender, StatusEventArgs e)
        {
            if (EndOperationEvent != null)
                EndOperationEvent(sender, e);
        }

        /// <summary>
        /// Fire to show operation message to user.
        /// </summary>
        public event EventHandler<StatusEventArgs> OperationStatusEvent;

        /// <summary>
        /// Fire event for displaying status of an operation in application's GUI. 
        /// This function will be used by inherited classes and
        /// some worker classes inside the same assembly.
        /// This function is to tell important message that may concern the end users.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DisplayOperationStatus(object sender, StatusEventArgs e)
        {
            if (OperationStatusEvent != null)
                OperationStatusEvent(sender, e);
        }

        public void DisplayOperationMessage(string message)
        {
            DisplayOperationStatus(this, new StatusEventArgs(message));
        }

        /// <summary>
        /// Fire when last anchor gets updated
        /// </summary>
        public event EventHandler<AnchorChangedEventArgs> LastAnchorChangedEvent;
        protected void NotifyLastAnchorChanged(DateTime anchorTime, int anchor)
        {
            if (LastAnchorChangedEvent != null)
                LastAnchorChangedEvent(this, new AnchorChangedEventArgs(anchorTime, anchor));
        }

        #region Functions of reporting errors
        /// <summary>
        /// Trace with custom error message, under category Error.
        /// </summary>
        /// <param name="text"></param>
        protected static void HandleErrorStatus(string text)
        {
            Trace.WriteLine(text, TraceCategory.Error);
        }

        /*private static void HandleErrorPositive(string text)
        {
            Trace.WriteLine(text, TraceCategory.positiveError);
        }*/
        /// <summary>
        /// Trace Error message of exception, under category Exception
        /// </summary>
        /// <param name="exception"></param>
        protected static void WriteErrorException(Exception exception)
        {
            Trace.WriteLine(exception.Message, TraceCategory.Exception);
        }


        /// <summary>
        /// Provide general handling of async call back
        /// </summary>
        /// <param name="asyncResult"></param>
        protected static void GeneralCompletionCallback(IAsyncResult asyncResult)
        {
            AsyncResult resultObj = (AsyncResult)asyncResult;
            ActionHandler d = (ActionHandler)resultObj.AsyncDelegate;
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


    }
}
