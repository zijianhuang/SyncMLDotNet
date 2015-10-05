using Microsoft.Office.Interop.Outlook;

namespace Fonlow.SyncML.OutlookSync
{
    public sealed class DeletionLogForCalendar : DeletionLog
    {
        private DeletionLogForCalendar()
            : base("DeletedCalendar.db")
        {
        }
        public static DeletionLogForCalendar Default
        {
            get { return Nested.instance; }
        }

        class Nested
        {
            static Nested() { }
            internal static readonly DeletionLogForCalendar instance = new DeletionLogForCalendar();
        }
    }

    public class DeletionMonitorForCalendar : DeletionMonitor<AppointmentItem>
    {
        public DeletionMonitorForCalendar(Application app)
            : base(app.GetNamespace("MAPI").GetDefaultFolder(OlDefaultFolders.olFolderCalendar) as Folder,
            DeletionLogForCalendar.Default)
        {

        }

        protected override string GetItemEntryId(AppointmentItem item)
        {
            return item.EntryID;
        }
    }
}