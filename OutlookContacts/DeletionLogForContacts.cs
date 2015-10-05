using Microsoft.Office.Interop.Outlook;

namespace Fonlow.SyncML.OutlookSync
{
    public sealed class DeletionLogForContacts : DeletionLog
    {
        private DeletionLogForContacts():base("DeletedContacts.db")
        {
        }

        public static DeletionLogForContacts Default
        {
            get { return Nested.instance; }
        }

        class Nested
        {
            static Nested() { }
            internal static readonly DeletionLogForContacts instance = new DeletionLogForContacts();
        }
    }

    public class DeletionMonitorForContacts : DeletionMonitor<ContactItem>
    {
        public DeletionMonitorForContacts(Application app)
            : base(app.GetNamespace("MAPI").GetDefaultFolder(OlDefaultFolders.olFolderContacts) as Folder,
            DeletionLogForContacts.Default)
        {

        }

        protected override string GetItemEntryId(ContactItem item)
        {
            return item.EntryID;
        }
    }

}
