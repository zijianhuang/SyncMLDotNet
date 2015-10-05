using System;
using Microsoft.Office.Interop.Outlook;

namespace Fonlow.SyncML.OutlookSync
{
    public class OutlookTasks : OutlookItems<TaskItem>
    {
        public OutlookTasks(Application app)
            : base(app.GetNamespace("MAPI").GetDefaultFolder(OlDefaultFolders.olFolderTasks) as Folder)
        {

        }

        protected override void DeleteItem(TaskItem item)
        {
            item.Delete();
        }

        protected override DateTime GetItemCreationTime(TaskItem item)
        {
            return item.CreationTime;
        }

        protected override DateTime GetItemLastModificationTime(TaskItem item)
        {
            return item.LastModificationTime;
        }

        protected override string GetEntryId(TaskItem item)
        {
            return item.EntryID;
        }

        public override string GetItemName(TaskItem item)
        {
            return item.Subject;
        }

    }

}
