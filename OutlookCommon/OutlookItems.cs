using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.Office.Interop.Outlook;

namespace Fonlow.SyncML.OutlookSync
{
    /// <summary>
    /// Aggregate some frequently used functions of accessing Outlook items.
    /// </summary>
    /// <typeparam name="T">The type of outlook items, such as contacts or calendar.</typeparam>
    public abstract class OutlookItems<T> 
    {
        protected OutlookItems(Folder itemsFolder)
        {
            ItemsFolder = itemsFolder;
        }

        public Folder ItemsFolder { get; private set; }
        
        public string GetItemName(string entryId)
        {
            IEnumerable<T> contactItems = ItemsFolder.Items.OfType<T>().Where(item =>
                GetEntryId(item) == entryId);
            if (contactItems.Count() > 0)
            {
                return GetItemName(contactItems.First());
            }
            return null;
        }

        public string GetEntryIdByDisplayName(string displayName)
        {
            IEnumerable<T> items = ItemsFolder.Items.OfType<T>().Where(item =>
                GetItemName(item) == displayName);
            if (items.Count() > 0)
            {
                return GetEntryId( items.First());
            }
            return null;
        }

        public abstract string GetItemName(T item);

        public void DeleteAllItems()
        {
            IEnumerable<T> contactItems = ItemsFolder.Items.OfType<T>();

            List<T> items = contactItems.ToList();// must use this buffer to hold pointer to contact items.
            foreach (T item in items)
                DeleteItem(item);

        }

        protected abstract void DeleteItem(T item);

        public bool DeleteItem(string entryId)
        {
            IEnumerable<T> contactItems = ItemsFolder.Items.OfType<T>().Where(item =>
                GetEntryId(item) == entryId);
            if (contactItems.Count() > 0)
            {
                T item = contactItems.First();
                DeleteItem(item);
                return true;
            }
            return false;
        }

        protected abstract string GetEntryId(T item);

        /// <summary>
        /// This is currently for unit test only.
        /// </summary>
        /// <param name="entryId"></param>
        /// <returns></returns>
        public T GetItemByEntryId(string entryId)
        {
            IEnumerable<T> items = ItemsFolder.Items.OfType<T>().Where(item =>
                GetEntryId(item) == entryId);

            if (items.Count() > 0)
            {
                return items.First();
            }
            return default(T); // a bit redundant to satisfy the compiler.
        }

        public IEnumerable<T> GetNewItems(DateTime anchor)
        {
            IEnumerable<T> newItems = ItemsFolder.Items.OfType<T>().Where(item =>
                (GetItemCreationTime(item) > anchor) && IsItemIncludedInSync(item));
            return newItems;
        }

        public IEnumerable<T> GetUpdatedItems(DateTime anchor)
        {
            IEnumerable<T> newItems = ItemsFolder.Items.OfType<T>().Where(item =>
                (
                (GetItemCreationTime(item) <= anchor )
                && (GetItemLastModificationTime(item) > anchor)
                && IsItemIncludedInSync(item)
                )
                );
            return newItems;
        }

        protected abstract DateTime GetItemLastModificationTime(T item);

        protected abstract DateTime GetItemCreationTime(T item);

        /// <summary>
        /// By default this function returns true always, unless there's a need to filter out.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected virtual bool IsItemIncludedInSync(T item)
        {
            return true;
        }



    }
}
