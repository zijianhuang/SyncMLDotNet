using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.Office.Interop.Outlook;

namespace Fonlow.SyncML.OutlookSync
{
    public class OutlookNotes : OutlookItems<NoteItem>
    {
        public OutlookNotes(Application app)
            : base(app.GetNamespace("MAPI").GetDefaultFolder(OlDefaultFolders.olFolderNotes) as Folder)
        {

        }

        protected override void DeleteItem(NoteItem item)
        {
            item.Delete();
        }

        protected override DateTime GetItemCreationTime(NoteItem item)
        {
            return item.CreationTime;
        }

        protected override DateTime GetItemLastModificationTime(NoteItem item)
        {
            return item.LastModificationTime;
        }

        protected override string GetEntryId(NoteItem item)
        {
            return item.EntryID;
        }

        public override string GetItemName(NoteItem item)
        {
            return item.Subject;
        }

    }

}
