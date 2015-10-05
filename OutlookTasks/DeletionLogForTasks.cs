using Microsoft.Office.Interop.Outlook;

namespace Fonlow.SyncML.OutlookSync
{
    public sealed class DeletionLogForTasks : DeletionLog
    {
        private DeletionLogForTasks()
            : base("DeletedTasks.db")
        {
        }
        public static DeletionLogForTasks Default
        {
            get { return Nested.instance; }
        }

        class Nested
        {
            static Nested() { }
            internal static readonly DeletionLogForTasks instance = new DeletionLogForTasks();
        }
    }

    public class DeletionMonitorForTasks : DeletionMonitor<TaskItem>
    {
        public DeletionMonitorForTasks(Application app)
            : base(app.GetNamespace("MAPI").GetDefaultFolder(OlDefaultFolders.olFolderTasks) as Folder,
            DeletionLogForTasks.Default)
        {

        }

        protected override string GetItemEntryId(TaskItem item)
        {
            return item.EntryID;
        }
    }
}