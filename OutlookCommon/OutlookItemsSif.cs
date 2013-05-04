using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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



    /// <summary>
    /// Provide some common function of reading and writing data
    /// </summary>
    public static class DataUtility
    {
        static DateTime outlookOKYear = DateTime.Today.AddYears(1000); 
        static DateTime outlookNullYear = new DateTime(4501, 01, 01);// Outlook Null year is actuall 4501-01-01 when being presented in C# codes.

        /// <summary>
        /// Create element or return null if content is null.
        /// </summary>
        public static XElement CreateElement(string elementName, string content)
        {
            return String.IsNullOrEmpty(content) ? null : new XElement(elementName, content);
        }

        /// <summary>
        /// Return element value or null if element does not exist.
        /// </summary>
        public static string GetXElementValueSafely(XElement parentElement, string elementName)
        {
            if (parentElement == null)
            {
                throw new ArgumentNullException("parentElement");
            }
            XElement ele = parentElement.Element(elementName);
            return (ele == null) ? null : ele.Value;
        }

        /// <summary>
        /// Read element value as enum. The caller should do the casting.
        /// </summary>
        public static object CreateEnum(XElement parentElement, string elementName, Type type)
        {
            try
            {
                string text = GetXElementValueSafely(parentElement, elementName);
                if (String.IsNullOrEmpty(text))
                {
                    return 0; // assuming None and unspecified or alike has value 0.
                }
                else
                {
                    return Enum.Parse(type, text, true);
                }
            }
            catch (ArgumentException e)
            {
                System.Diagnostics.Trace.TraceInformation("When CreateEnum: " + e.Message);
                return 0;
            }
        }

        /// <summary>
        /// Create an element of date in ISO format. It must be ISO, otherwise, getting data back from the server 
        /// will have odd things. Say, the Funambol server refuses to send back data.
        /// </summary>
        public static XElement CreateElementDate(string elementName, DateTime content)
        {
            return (content > outlookOKYear || content < DateTime.MinValue) ? null : new XElement(elementName, content.ToString("yyyy-MM-dd"));
        }

        /// <summary>
        /// Parse text in ISO format. Return OutlookNullYear if text is null or empty.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static DateTime ParseDateTime(string text)
        {
            if (String.IsNullOrEmpty(text))
                return outlookNullYear;
            DateTime r;
            if (DateTime.TryParse(text, out r))
            {
                return r;
            }
            else
            {
                if (DateTime.TryParseExact(text, new string[] { "yyyyMMddTHHmmssZ"},
                    System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out r))
                {
                    //r is already converted to local time, and it is more consistent to keep it as UTC.
                  //  r = r.ToUniversalTime();
                    if (r == outlookNullYear)
                    {
                        System.Diagnostics.Trace.TraceInformation("Invalid date text: " + text + "~Today is used for further analysis.");
                        return DateTime.Today;
               
                    }
                    else
                      return r;
                }
                else
                {
                    System.Diagnostics.Trace.TraceInformation("Unrecognized date text: " + text + "~Today is used for further analysis.");
                    return DateTime.Today;
                }
            }
        }

        /// <summary>
        /// Return yyyyMMddTHHmmssZ or Empty if dateTime is OutlookNullYear.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string DateTimeToIsoText(DateTime dateTime)
        {
            if (dateTime >= outlookNullYear.AddDays(-2))
                return String.Empty;

            if (dateTime.Kind == DateTimeKind.Local)
                return dateTime.ToUniversalTime().ToString("yyyyMMddTHHmmssZ");
            if (dateTime.Kind == DateTimeKind.Utc)
                return dateTime.ToString("yyyyMMddTHHmmssZ");
            else //if (dateTime.Date == dateTime) //so kind is unspecified
                return dateTime.ToString("yyyyMMddTHHmmssZ");
        }

        public static DateTime ParseDateTime(XElement parentElement, string elementName)
        {
            return ParseDateTime(GetXElementValueSafely(parentElement, elementName));
        }

        public static DateTime ParseDate(XElement parentElement, string elementName)
        {
            DateTime date = ParseDateTime(GetXElementValueSafely(parentElement, elementName));
            return (date == outlookNullYear)? outlookNullYear : date.Date;
        }

        public static bool ParseBool(string text)
        {
            return (String.IsNullOrEmpty(text) | text == "0") ? false : true;
        }

        public static bool ParseBool(XElement parentElement, string elementName)
        {
            return ParseBool(GetXElementValueSafely(parentElement, elementName));
        }

        public static int ParseInt(string text)
        {
            int r;
            return String.IsNullOrEmpty(text)?0:
                (int.TryParse(text, out r)?r:0); 
        }

        public static int ParseInt(XElement parentElement, string elementName)
        {
            return ParseInt(GetXElementValueSafely(parentElement, elementName));
        }
    }
}
