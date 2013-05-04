using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using System.Diagnostics;
using Fonlow.SyncML.Elements;
using Fonlow.SyncML.Common;
using System.Xml.Linq;
using System.Linq;
using System.Reflection;

namespace Fonlow.SyncML.OpenContacts
{
    /// <summary>
    /// Interface to Open Contacts provider, 
    /// or other address book providers which use the same methods of
    /// managing change log info, such as Contacts Express
    /// </summary>
    public interface IOpenContactsProvider
    {
        /// <summary>
        /// For change log xml from client address book
        /// </summary>
        /// <param name="lastAnchor">Last anchor.</param>
        /// <returns>Change log XML</returns>
        string GetChangeLogXml(DateTime lastAnchor);

        /// <summary>
        /// Apply change log to client address book, and return localID/serverID pair
        /// </summary>
        /// <param name="xml">Change log XML to update local address book</param>
        /// <returns>Each pair is in localID=ServerID format, separated by line break.</returns>
        string ApplyChangeLogXml(string xml);

        /// <summary>
        /// Obtain sync permission from local address book.
        /// </summary>
        /// <param name="message">The message from sync program to local address book</param>
        /// <returns>True if local address book allow to sync.</returns>
        bool BeginSync(string message);

        /// <summary>
        /// Notify local address book that the sync is finished.
        /// </summary>
        /// <param name="message"></param>
        void EndSync(string message);

        string GetContactName(string localId);

        /// <summary>
        /// Get error message from the provider.
        /// </summary>
        string ErrorMessage
        {
            get;
        }

        string DeletedNames
        { get; }

        string AddedNames { get; }

        string UpdatedNames { get; }

        bool DeleteAllContacts();
    }

    /// <summary>
    /// 
    /// </summary>
    public class OCLocalDataSource : ILocalDataSource
    {
        /// <summary>
        /// This is used by a client which will provider an OcProvider.
        /// </summary>
        public OCLocalDataSource(IOpenContactsProvider ocProvider, string exchangeType)
        {
            this.ocProvider = ocProvider;
            this.contactMediumType = exchangeType;
            StyleSheetFromChangeLogXmlToSifc = "ChangeLogXMLToSifc.xsl";
            StyleSheetFromSifcToChangeLogXml = "SifcToChangeLogXML.xsl";
        }



        private IOpenContactsProvider ocProvider;



        /// <summary>
        /// File name of stylesheet to convert SIF-C to Change Log XML
        /// </summary>
        public string StyleSheetFromSifcToChangeLogXml
        {
            get;
            set;
        }

        /// <summary>
        /// File name of stylesheet to convert Change Log XML to SIF-C.
        /// </summary>
        public string StyleSheetFromChangeLogXmlToSifc
        {
            get;
            set;
        }

        private string contactMediumType = "SIF";

        public string ExchangeType
        {
            get { return contactMediumType; }
            set { contactMediumType = value; }
        }

        public NameValueCollection ApplySyncCommands(IList<SyncMLCommand> commands, string lastAnchorTimeText)
        {
            if (commands == null)
            {
                return null;
            }

            if (commands.Count > 0)
            {
                //Step1: Create Change log XML from sync commands
                string changeLogXml = SyncMLSyncCommandsToXml.GenerateXml(commands,
                    StyleSheetFromSifcToChangeLogXml, lastAnchorTimeText);
                Debug.WriteLine("Converted to OC Change Log XML:");
                Debug.WriteLine(changeLogXml);

                //Step2: Apply the change log XML to local address book
                string localIDServerIDPairs = ocProvider.ApplyChangeLogXml(changeLogXml);
                System.Diagnostics.Debug.WriteLine("localIDServerIDPairs: " + localIDServerIDPairs);

                NameValueCollection localIDServerIDCollection = ConvertStringToPairs(localIDServerIDPairs);

                return localIDServerIDCollection;

            }

            return null;
        }

        public bool BeginSync(string message)
        {
            if (ocProvider == null)
            {
                Trace.TraceInformation("OcProvider can not be created. Please check config.");
                return false;
            }
            return ocProvider.BeginSync(message);
        }

        public void EndSync(string message, OperationStatus status)
        {
            if (ocProvider == null)
            {
                Trace.TraceInformation("OcProvider was not created. Please check config.");
                return;
            }
            ocProvider.EndSync(message);
            //todo: may need to clean up oc log according to status
        }

        public string GetItemName(string localId)
        {
            return ocProvider.GetContactName(localId);
        }

        public string DataSourceName
        {
            get { return "Open Contacts"; }
        }

        public string ErrorMessage
        {
            get { return ocProvider.ErrorMessage; }
        }

        public string NamesOfDeletedItems
        {
            get { return ocProvider.DeletedNames; }
        }

        public string NamesOfAddedItems
        {
            get { return ocProvider.AddedNames; }
        }

        public string NamesOfUpdatedItems
        {
            get { return ocProvider.UpdatedNames; }
        }

        protected static NameValueCollection ConvertStringToPairs(string text)
        {
            NameValueCollection r = new NameValueCollection();
            if (!String.IsNullOrEmpty(text))
            {
                string[] lines = text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in lines)
                {
                    string[] nv = s.Split(new char[] { '=' });
                    if (!String.IsNullOrEmpty(s.Trim()))
                        r.Add(nv[0], nv[1]);
                }
            }
            return r;
        }

        /// <summary>
        /// Get commands source from the provider.
        /// </summary>
        /// <param name="lastAnchorTime"></param>
        /// <returns>Null if something wrong in the provider</returns>
        public ISyncCommandsSource GenerateSyncCommandsSource(DateTime lastAnchorTime)
        {
            XElement doc;
            try
            {
                doc = XElement.Parse(ocProvider.GetChangeLogXml(lastAnchorTime));
                Debug.WriteLine("Change log from Open Contacts:");
                Debug.WriteLine(doc.ToString());
            }
            catch (System.Runtime.InteropServices.COMException e)//Open Contacts as COM server may raise this.
            {
                Trace.TraceInformation("When GenerateSyncCommandsSource: "+e.Message);
                return null;
            }
            
            summaryOfGeneratedSyncCommands = ReportChangeLogContent(doc).ToString();

            if (contactMediumType == "SIF")
                return new XmlToSyncMLSyncCommands(doc, StyleSheetFromChangeLogXmlToSifc, SyncConstants.MaxCommandsPerBatch);
            else if (contactMediumType == "vCard")
                return new XmlToSyncMLSyncCommandsForVcard(doc, StyleSheetFromChangeLogXmlToSifc, SyncConstants.MaxCommandsPerBatch, false);
            else
                throw new LocalDataSourceException("ContactMediumType is incorrect.");
        }

        private string summaryOfGeneratedSyncCommands;
        public string SummaryOfGeneratedSyncCommands { get { return summaryOfGeneratedSyncCommands; } }

        /// <summary>
        /// Safely query multiple levels of Xml path. If the path could not return any element,
        /// return an empty list rather than null. This is convenient for Linq to Xml.
        /// </summary>
        /// <param name="topElement"></param>
        /// <param name="elementNames"></param>
        /// <returns>Elements or empty list.</returns>
        private static IEnumerable<XElement> SafeElementsQuery(XElement topElement, params string[] elementNames)
        {
            IEnumerable<XElement> firstLevelElements = topElement.Elements(elementNames[0]);
            IEnumerable<XElement> r= firstLevelElements;
            if (firstLevelElements != null)
            {
                for (int i = 1; i < elementNames.Length; i++)
                {
                    IEnumerable<XElement> thisLevelElements = r.Elements(elementNames[i]);
                    if (thisLevelElements != null)
                    {
                        r = thisLevelElements;
                    }
                    else
                        return new List<XElement>();
                }

            }
            else
                return new List<XElement>();

            return r;
        }

        /// <summary>
        /// Report change log's content to GUI. This is generally called before sending changes to the server.
        /// </summary>
        /// <param name="changes">The root element should be Changes</param>
        private static StringBuilder ReportChangeLogContent(XElement changes)
        {
            StringBuilder builder = new StringBuilder();

            IEnumerable<string> namesAdded =
                from element in SafeElementsQuery(changes, "New", "C", "Field")
                where element.Attribute("Name").Value == "FullName" && element.Attribute("Section").Value == String.Empty
                select element.Value;

            if (namesAdded.Count() > 0)
            {
                builder.AppendLine("These records will be added to the server.");
                foreach (string s in namesAdded)
                    builder.Append(s + ", ");

                if (builder.Length > 3)
                    builder.Remove(builder.Length - 2, 2); // remove the last comma and space.

                builder.AppendLine();
            }

            IEnumerable<string> namesReplaced =
                from element in SafeElementsQuery(changes, "Update", "C", "Field")
                where element.Attribute("Name").Value == "FullName" && element.Attribute("Section").Value == String.Empty
                select element.Value;

            if (namesReplaced.Count() > 0)
            {
                builder.AppendLine("These records will be updated on the server.");
                foreach (string s in namesReplaced)
                    builder.Append(s + ", ");

                if (builder.Length > 3)
                    builder.Remove(builder.Length - 2, 2); // remove the last comma and space.

                builder.AppendLine();
            }
            /*XPathNodeIterator deleteNodes = changes.Select("Delete/D");
            int deletedCount = deleteNodes.Count; may be better not to report.*/

            return builder;
        }

        public bool DeleteAllItems()
        {
            return ocProvider.DeleteAllContacts();
        }

    }

    /// <summary>
    /// Exception type for LocalDataSource assembly.
    /// </summary>
    [Serializable]
    public class LocalDataSourceException : Exception
    {
        public LocalDataSourceException()
            : base()
        {

        }

        public LocalDataSourceException(string message)
            : base(message)
        {

        }

        public LocalDataSourceException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}
