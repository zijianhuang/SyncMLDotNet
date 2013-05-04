using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using Fonlow.SyncML.Elements;
using Fonlow.SyncML.Common;
using Microsoft.Office.Interop.Outlook;
using System.Collections.Specialized;

namespace Fonlow.SyncML.OutlookSync
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class OutlookDataSourceBase<T> : ILocalDataSource, IDisposable
    {
        /// <summary>
        /// An outlook data source needs components to:
        /// 1. Convert outlook item to SyncML content of certain data format, say SIF or vCard
        /// 2. Convert SyncML content to outlook item
        /// 3. Get common outlook functions of outlook items of certain item type.
        /// 4. Deletion log for certain item type.
        /// 
        /// This constructor basically wire all needed components. A derived class should deside what concrete components to use.
        /// </summary>
        ///<param name="app">Outllok application generally from Outlook plug-in.</param>
        protected OutlookDataSourceBase(Application app, 
            OutlookToSyncMLX<T> outlookToSyncMLX, 
            SyncMLXToOutlook<T> syncMLToOutlook,
            OutlookItems<T> outlookContacts, 
            DeletionLog deletionLog)
        {
            OutlookApp = app;
            this.outlookItems = outlookContacts;
            this.syncMLToOutlook = syncMLToOutlook;
            this.outlookToSyncMLX = outlookToSyncMLX;
            this.deletionLog = deletionLog;
        }

        DeletionLog deletionLog;

        protected Application OutlookApp { get; set; }

        private SyncMLXToOutlook<T> syncMLToOutlook;
        private OutlookToSyncMLX<T> outlookToSyncMLX;
        private OutlookItems<T> outlookItems;

    //    public string ExchangeType { get; set; } //dummy

        public NameValueCollection ApplySyncCommands(IList<SyncMLCommand> commands, string lastAnchorTimeText)
        {
            if (commands == null)
            {
                throw new ArgumentNullException("commands");
            } 
            
            if (commands.Count > 0)
            {
                //Step2: Apply the change log XML to local address book
                string localIDServerIDPairs = syncMLToOutlook.ApplySyncCommands(commands);
                System.Diagnostics.Debug.WriteLine("localIDServerIDPairs: " + localIDServerIDPairs);

                NameValueCollection localIDServerIDCollection = ConvertStringToPairs(localIDServerIDPairs);

                return localIDServerIDCollection;

            }

            return null;
        }

        public bool BeginSync(string message)
        {
            deletionLog.Enabled = false;
            return true;
        }

        public void EndSync(string message, OperationStatus status)
        {
            deletionLog.Enabled = true;
            switch (status)
            {
                case OperationStatus.Unspecified:
                    break;
                case OperationStatus.Successful:
                    deletionLog.RemoveLog();
                    break;
                case OperationStatus.Failed:
                    break;
                default:
                    break;
            }
        }

        public string GetItemName(string localId)
        {
            return outlookItems.GetItemName(localId);
        }

        public string DataSourceName
        {
            get { return "MSOutlook"; }
        }

        public string ErrorMessage
        {
            get { return null; }
        }

        public string NamesOfDeletedItems
        {
            get { return null; }
        }

        public string NamesOfAddedItems
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                IEnumerable<T> items = outlookItems.GetNewItems(AnchorTime);
                foreach (T item in items)
                {
                    builder.AppendLine(outlookItems.GetItemName(item));
                }
                return builder.ToString();
            }
        }

        public string NamesOfUpdatedItems
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                IEnumerable<T> items = outlookItems.GetUpdatedItems(AnchorTime);
                foreach (T item in items)
                {
                    builder.AppendLine(outlookItems.GetItemName(item));
                }
                return builder.ToString();
            }
        }

        protected static NameValueCollection ConvertStringToPairs(string text)
        {
            NameValueCollection r = new NameValueCollection();
            if (!String.IsNullOrEmpty(text))
            {
                string[] lines = text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in lines)
                {
                    string[] nv = s.Split(new char[] { '=' });
                    if (!String.IsNullOrEmpty(s.Trim()))
                        r.Add(nv[0], nv[1]);
                }
            }
            return r;
        }

        public ISyncCommandsSource GenerateSyncCommandsSource(DateTime lastAnchorTime)
        {
            this.AnchorTime = lastAnchorTime;
            outlookToSyncMLX.AnchorTime = lastAnchorTime;
            return outlookToSyncMLX;
        }

        protected DateTime AnchorTime { get; set; }

        public string SummaryOfGeneratedSyncCommands { get { return null; } }//no need at the moment

        public bool DeleteAllItems()
        {
            outlookItems.DeleteAllItems();
            return true;
        }

        bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //         deletionLog.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
