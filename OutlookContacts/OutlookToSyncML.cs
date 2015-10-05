using System;
using System.Text;
using System.Xml.Linq;
using Microsoft.Office.Interop.Outlook;
using Fonlow.SyncML.Elements;
using Fonlow.VCard;


namespace Fonlow.SyncML.OutlookSync
{
    public class OutlookToSyncMLSifC : OutlookToSyncMLX<ContactItem>
    {
        public OutlookToSyncMLSifC(Application app)
            : base(new OutlookContacts(app), new OutlookContactsWithSifC(app), DeletionLogForContacts.Default)
        {
        }

        /// <summary>
        /// Get contact data as Sif-C
        /// </summary>
        /// <param name="contactItem"></param>
        /// <returns></returns>
        protected override string GetItemData(ContactItem item)
        {
            return "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + SifAgent.ReadItemToText(item);
        }

        protected override void AddContentToCommand(SyncMLUpdateBase command, ContactItem contact)
        {
            command.Meta.Xml.Add(FormatOfBase64);
            command.Meta.Xml.Add(TypeOfText);

            SyncMLItem item = SyncMLItem.Create();
            item.Source.LocURI.Content = contact.EntryID;

            UTF8Encoding byteConverter = new UTF8Encoding();
            byte[] buffer = byteConverter.GetBytes(GetItemData(contact));
            item.Data.Content = Convert.ToBase64String(buffer);

            command.ItemCollection.Add(item);
        }

        static readonly XElement typeOfText = XElement.Parse("<Type xmlns='syncml:metinf'>text/x-s4j-sifc</Type>");

        protected override XElement TypeOfText
        {
            get
            {
                return typeOfText;
            }
        }

    }

    public class OutlookToSyncMLVcard : OutlookToSyncMLX<ContactItem>
    {
        public OutlookToSyncMLVcard(Application app)
            : base(new OutlookContacts(app), new OutlookContactsWithSifC(app), DeletionLogForContacts.Default)
        {
        }

        /// <summary>
        /// Get contact data as vCard.
        /// </summary>
        /// <param name="contactItem"></param>
        /// <returns></returns>
        protected override string GetItemData(ContactItem item)
        {
            return VCardWriter.WriteToString(
                VCardSIFC.ConvertSifCToVCard(XElement.Parse(
                SifAgent.ReadItemToText(item))));
        }

        protected override void AddContentToCommand(SyncMLUpdateBase command, ContactItem contact)
        {
            command.Meta.Xml.Add(TypeOfText);

            SyncMLItem item = SyncMLItem.Create();
            item.Source.LocURI.Content = contact.EntryID;
            item.Data.Content = GetItemData(contact);

            command.ItemCollection.Add(item);
        }

        static readonly XElement typeOfText = XElement.Parse("<Type xmlns='syncml:metinf'>text/x-vcard</Type>");

        protected override XElement TypeOfText
        {
            get
            {
                return typeOfText;
            }
        }

    }
}
