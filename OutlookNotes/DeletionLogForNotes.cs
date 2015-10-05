using Microsoft.Office.Interop.Outlook;

namespace Fonlow.SyncML.OutlookSync
{
    public sealed class DeletionLogForNotes : DeletionLog
    {
        private DeletionLogForNotes()
            : base("DeletedNotes.db")
        {
        }
        public static DeletionLogForNotes Default
        {
            get { return Nested.instance; }
        }

        class Nested
        {
            static Nested() { }
            internal static readonly DeletionLogForNotes instance = new DeletionLogForNotes();
        }
    }

    public class DeletionMonitorForNotes : DeletionMonitor<NoteItem>
    {
        public DeletionMonitorForNotes(Application app)
            : base(app.GetNamespace("MAPI").GetDefaultFolder(OlDefaultFolders.olFolderNotes) as Folder,
            DeletionLogForNotes.Default)
        {

        }

        protected override string GetItemEntryId(NoteItem item)
        {
            return item.EntryID;
        }
    }
}