using System;
using System.IO;
using Microsoft.Office.Interop.Outlook;

namespace Fonlow.SyncML.OutlookSync
{
    /// <summary>
    /// Read and write deletion log for Outlook. 
    /// The log file in in ApplicationData\Fonlow\OutlookSyncPlugin
    /// The log basically record the entryId of each record deleted. One entryId per line.
    /// e.g. C:\Users\zijian\AppData\Roaming\Fonlow\OutlookSyncPlugin
    /// 
    /// Each derived class should generally be used as a singleton, in order to
    /// block writing to log sometimes when doing sync.
    /// </summary>
    public abstract class DeletionLog
    {
        string dir;
        string dbPath;

        protected DeletionLog(string logFileName)
        {
            System.Diagnostics.Debug.Assert(!String.IsNullOrEmpty(logFileName), "logFileName null");

            dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Fonlow\\OutlookSyncPlugin";
            dbPath = dir + "\\" + logFileName;
            Directory.CreateDirectory(dir);
            Enabled = true;
        }

        /// <summary>
        /// Log an entryId of deleted contacts.
        /// </summary>
        /// <param name="id"></param>
        public void AddLog(string id)
        {
            if (!Enabled)
                return;

            using (StreamWriter writer = new StreamWriter(dbPath, true))
            {
                System.Diagnostics.Trace.TraceInformation("write log " + id);
                writer.WriteLine(id);
            }
        }

        /// <summary>
        /// Enable or disable writing log.
        /// </summary>
        public bool Enabled { get; set; }

        public void RemoveLog()
        {
            try
            {
                File.Delete(dbPath);
            }
            catch (IOException e)
            {
                System.Diagnostics.Debug.WriteLine("When RemoveLog: "+e.Message);
            }
        }

        /// <summary>
        /// Read all Ids.
        /// </summary>
        /// <returns>each item has an Id saved. It might be null if the file does not exist.</returns>
        public string[] ReadAll()
        {
            if (File.Exists(dbPath))
            {
                using (StreamReader reader = new StreamReader(dbPath))
                {
                    return reader.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                }
            }
            else
            {
                return null;
            }
        }

    }

    public class ItemDeleteEventArgs : EventArgs
    {
        public ItemDeleteEventArgs(string entryId)
        {
            EntryId = entryId;
        }

        public string EntryId { get; private set; }
    }

    /// <summary>
    /// To mark a class is a Deletion Monitor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DeletionMonitorAttribute : Attribute
    {

    }

    /// <summary>
    /// Basic framework of monitoring a Folder. Reflection should find drived classes through checking this inherited attribute.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DeletionMonitor()]
    public abstract class DeletionMonitor<T>
    {
        protected DeletionMonitor(Folder itemsFolder, DeletionLog deletionLog)
        {
            ItemsFolder = itemsFolder;
            ItemsFolder.BeforeItemMove += new MAPIFolderEvents_12_BeforeItemMoveEventHandler(contactsFolder_BeforeItemMove);
            this.deletionLog = deletionLog;
        }

        DeletionLog deletionLog;

        protected Folder ItemsFolder { get; private set; }
        /// <summary>
        /// This handler will log the entryId of to-be-deleted contact.
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="MoveTo"></param>
        /// <param name="Cancel"></param>
        void contactsFolder_BeforeItemMove(object item, MAPIFolder moveTo, ref bool cancel)
        {
            if (item != null)
            {
                string entryId = GetItemEntryId((T)item);
                if (moveTo == null)// so the item is being deleted permenently
                {
                    deletionLog.AddLog(entryId);
                }
                else if (moveTo.Name == "Deleted Items")
                {
                    deletionLog.AddLog(entryId);
                }

            }
        }

        protected abstract string GetItemEntryId(T item);
    }

}
