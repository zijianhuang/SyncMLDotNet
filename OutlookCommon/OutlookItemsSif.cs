using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Office.Interop.Outlook;

namespace Fonlow.SyncML.OutlookSync
{
    /// <summary>
    /// Provide common logic to convert outlook items into meta data, and
    /// save meta data into outlook items. The derived classes will define what
    /// meta data is.
    /// </summary>
    /// <typeparam name="T">type of outlook item.</typeparam>
    public abstract class OutlookItemsWithSyncContent<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="itemsFolder">An derived class generally initialize this in base().</param>
        protected OutlookItemsWithSyncContent(Folder itemsFolder)
        {
            ItemsFolder = itemsFolder;
        }

        protected Folder ItemsFolder { get; private set; }

        /// <summary>
        /// Read all items to a list of strings. This function currently is used only for testing.
        /// </summary>
        public IList<string> ReadAllItemsToTexts()
        {
            IList<string> r = new List<string>();
            Items folderItems = ItemsFolder.Items;
            IEnumerable<T> contactItems = folderItems.OfType<T>();
            foreach (T item in contactItems)
            {
                r.Add(ReadItemToText(item));
            }
            return r;
        }

        /// <summary>
        /// Read an item to a SyncML content to sync. 
        /// </summary>
        public abstract string ReadItemToText(T item);

        /// <summary>
        /// 
        /// </summary>
        /// <returns>EntryID of new added item, of null for existing entry</returns>
        public string AddItem(string meta)
        {
            T item = AddItemToItemsFolder();
            WriteMetaToItem(meta, item);
            SaveItem(item);
            return GetEntryId(item);
        }

        protected abstract T AddItemToItemsFolder();

        /// <summary>
        /// Update item. If item does not exist, create one.
        /// </summary>
        /// <param name="entryId">entryId of existing contact</param>
        /// <returns>Entry Id which may be of the existing or new one.</returns>
        public string ReplaceItem(string meta, string entryId)
        {
            T existingItem;

            if (String.IsNullOrEmpty(entryId))
            {
                return AddItem(meta);
            }
            else
            {
                IEnumerable<T> items = ItemsFolder.Items.OfType<T>().Where(item =>
                    (GetEntryId(item) == entryId));

                if (items.Count() > 0)
                {
                    existingItem = items.First();
                }
                else
                {
                    return AddItem(meta);
                }
            }

            WriteMetaToItem(meta, existingItem);
            SaveItem(existingItem);

            return GetEntryId(existingItem);
        }

        protected abstract string GetEntryId(T item);

        protected abstract void SaveItem(T item);

        protected abstract void WriteMetaToItem(string meta, T item);

    }


}
