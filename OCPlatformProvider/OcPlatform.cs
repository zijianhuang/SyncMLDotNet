using System;
using System.Collections.Generic;
using System.Text;
using Fonlow.OpenContacts.Interop; // interop of Open Contacts COM
using System.Diagnostics;

namespace Fonlow.SyncML.OpenContacts
{
    /// <summary>
    /// Provide interaction between sync client and Open Contacts. This class will
    /// be wired to SyncFacade. Open Contacts won't be loaded until the first request.
    /// </summary>
    public class OCPlatformProvider : IOpenContactsProvider
    {
        private ISimpleContacts ocSync;
        /// <summary>
        /// Connection to Open Contacts when requested
        /// </summary>
        internal ISimpleContacts OCSync
        {
            get
            {
                if (ocSync == null)
                    ocSync = new SimpleContactsClass();

                try
                {
                    // In case OC got closed, create a new instance.
                    ocSync.Test();
                }
                catch (Exception e) //todo: a lazy way of re-claiming OC. Catch more specific exception later on.
                {
                    Trace.TraceWarning("General exception caught: "+e.ToString());
                    ocSync = new SimpleContactsClass();
                }
                return ocSync;
            }
        }

        public string GetChangeLogXml(DateTime lastAnchor)
        {
            return OCSync.GetChangeLogXML(lastAnchor);
        }

        public string ApplyChangeLogXml(string xml)
        {
            if (String.IsNullOrEmpty(xml))
            {
                Trace.TraceWarning("Xml is empty, and there's no point to continue.");
                throw new ArgumentNullException("xml", "xml is empty.");
            }

            try
            {
                return OCSync.UpdateOCWithChangeLogXML(xml, out errorMessage, out addedNames, out updatedNames, out deletedNames);

            }
            catch (System.Runtime.InteropServices.COMException e)
            {
                Trace.TraceWarning("ApplyChangeLogXml having problem with OC: " + e);
                Trace.TraceInformation("xml is: " + xml);
                throw;
            }
            catch (Exception e)
            {
                Trace.TraceWarning("ApplyChangeLogXml: " + e);
                throw;
            }

        }

        private string errorMessage;
        /// <summary>
        /// Error message generated by the platform
        /// </summary>
        public string ErrorMessage
        {
            get { return errorMessage; }
        }

        public bool BeginSync(string message)
        {
          return   OCSync.BeginSync(message);
        }

        public void EndSync(string message)
        {
            OCSync.EndSync(message);
        }

        private string deletedNames;
        public string DeletedNames
        {
            get { return deletedNames; }
        }

        private string addedNames;
        public string AddedNames
        {
            get { return addedNames; }
        }

        private string updatedNames;
        public string UpdatedNames
        {
            get { return updatedNames; }
        }

        public string GetContactName(string localId)
        {
            return ocSync.GetContactName(localId);
        }

        public bool DeleteAllContacts()
        {
            return ocSync.DeleteAllContacts();
        }

    }
}