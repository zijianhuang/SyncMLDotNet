using System;
using Microsoft.Office.Interop.Outlook;

namespace Fonlow.SyncML.OutlookSync
{
    /// <summary>
    /// Aggregate some frequently used functions of Outlook contacts.
    /// 
    /// </summary>
    public class OutlookContacts : OutlookItems<ContactItem>
    {
        /// <summary>
        /// Generally the plugin will give the Application object.
        /// </summary>
        /// <param name="app"></param>
        public OutlookContacts(Application app)
            :base(app.GetNamespace("MAPI").GetDefaultFolder(OlDefaultFolders.olFolderContacts) as Folder)
        {
        }


        protected override void DeleteItem(ContactItem item)
        {
            item.Delete();
        }

        protected override string GetEntryId(ContactItem item)
        {
            return item.EntryID;
        }


        public override string GetItemName(ContactItem item)
        {
            return item.FullName;
        }

        protected override DateTime GetItemCreationTime(ContactItem item)
        {
            return item.CreationTime;
        }

        protected override DateTime GetItemLastModificationTime(ContactItem item)
        {
            return item.LastModificationTime;
        }
    }

}
