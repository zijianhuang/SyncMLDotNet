using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fonlow.SyncML.Common;
using Fonlow.SyncML.Elements;

namespace DemoContact
{
    public class ContactsDataSource : ILocalDataSource
    {
        public string DataSourceName
        {
            get { return "Demo Contacts"; }

        }

        public string ErrorMessage
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string NamesOfAddedItems
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string NamesOfDeletedItems
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string NamesOfUpdatedItems
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string SummaryOfGeneratedSyncCommands
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public NameValueCollection ApplySyncCommands(IList<SyncMLCommand> commands, string lastAnchorTimeText)
        {
            throw new NotImplementedException();
        }

        public bool BeginSync(string message)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllItems()
        {
            throw new NotImplementedException();
        }

        public void EndSync(string message, OperationStatus status)
        {
            throw new NotImplementedException();
        }

        public ISyncCommandsSource GenerateSyncCommandsSource(DateTime lastAnchorTime)
        {
            throw new NotImplementedException();
        }

        public string GetItemName(string localId)
        {
            throw new NotImplementedException();
        }
    }
}
