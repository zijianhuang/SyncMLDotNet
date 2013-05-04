using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.Office.Interop.Outlook;
using Fonlow.SyncML.Common;
using Fonlow.SyncML.Elements;
using System.Xml.Xsl;
using System.Collections.Specialized;

namespace Fonlow.SyncML.OutlookSync
{
    /// <summary>
    /// Common logic for providing Outlook data as ISyncCommandsSource.
    /// This class is not used as an interface in the abstract framework, but soly 
    /// an encalsulation of common logic for derived classes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class OutlookToSyncMLX<T> : ISyncCommandsSource
    {
        protected OutlookToSyncMLX(OutlookItems<T> outlookItems, OutlookItemsWithSyncContent<T> sifAgent, DeletionLog deletionLog)
        {
            SifAgent = sifAgent;
            this.outlookItems = outlookItems;
            this.deletionLog = deletionLog;
        }

        OutlookItems<T> outlookItems;

        DeletionLog deletionLog;

        public DateTime AnchorTime { get; set; }

        protected OutlookItemsWithSyncContent<T> SifAgent { get; private set; }

        #region ISyncCommandsSource Members

        void Prepare()
        {
            if (!commandsParepared)
            {
                commandsBuffer = new List<SyncMLCommand>();
                PrepareCommands(commandsBuffer);
                commandsParepared = true;
            }
        }

        public bool ExtractNextCommands(IList<SyncMLCommand> commands)
        {
            if (commands == null)
            {
                throw new ArgumentNullException("commands");
            } 
            
            Prepare();

            int c = 0;
            while (bufferPosition < commandsBuffer.Count)
            {
                commands.Add(commandsBuffer[bufferPosition]);
                bufferPosition++;
                c++;
                if (c == SyncConstants.MaxCommandsPerBatch)
                    break;
            }

            return (bufferPosition < commandsBuffer.Count);
        }

        bool commandsParepared;

        IList<SyncMLCommand> commandsBuffer;

        int bufferPosition;

        private void PrepareCommands(IList<SyncMLCommand> commands)
        {
            IEnumerable<T> newContacts = outlookItems.GetNewItems(AnchorTime);
            if (forSlowSync)
            {
                foreach (T contact in newContacts)
                {
                    SyncMLReplace updateCommand = SyncMLReplace.Create();
                    AddContentToCommand(updateCommand, contact);
                    commands.Add(updateCommand);
                }
            }
            else
            {
                foreach (T contact in newContacts)
                {
                    SyncMLAdd addCommand = SyncMLAdd.Create();
                    AddContentToCommand(addCommand, contact);
                    commands.Add(addCommand);
                }
            }

            IEnumerable<T> updatedContacts = outlookItems.GetUpdatedItems(AnchorTime);
            foreach (T contact in updatedContacts)
            {
                SyncMLReplace updateCommand = SyncMLReplace.Create();
                AddContentToCommand(updateCommand, contact);
                commands.Add(updateCommand);
            }

            string[] ids = deletionLog.ReadAll();
            if (ids != null)
            {
                foreach (string id in ids)
                {
                    SyncMLDelete deleteCommand = SyncMLDelete.Create();
                    SyncMLItem item = SyncMLItem.Create();
                    item.Source.LocURI.Content = id;
                    deleteCommand.ItemCollection.Add(item);
                    commands.Add(deleteCommand);
                }
            }
        }

        /// <summary>
        /// Add content to a AddCommand or a ReplaceCommand, with currentNode of SifCChangeLogXML.
        /// </summary>
        /// <param name="command">Command object to be manipulated.</param>
        /// <param name="currentNode">A C element under either New or Update element.</param>
        protected abstract void AddContentToCommand(SyncMLUpdateBase command, T content);

        /// <summary>
        /// Get contact data in vCard or Sif-C.
        /// </summary>
        /// <param name="contactItem"></param>
        /// <returns></returns>
        protected abstract string GetItemData(T item);

        static readonly XElement formatOfBase64= XElement.Parse("<Format xmlns='syncml:metinf'>b64</Format>");

        protected XElement FormatOfBase64
        {
            get
            {
                return formatOfBase64;

            }
        }

        protected abstract XElement TypeOfText
        {
            get;
        }

        public int NumberOfChanges
        {
            get
            {
                Prepare();
                return commandsBuffer.Count();
            }
        }

        #endregion

        bool forSlowSync;

        public void PrepareForSlowSync()
        {
            forSlowSync = true;
        }
    }
}
