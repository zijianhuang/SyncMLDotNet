using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.Office.Interop.Outlook;
using Fonlow.SyncML.Common;
using Fonlow.SyncML.Elements;
using System.Xml.Xsl;
using System.Collections.Specialized;

namespace Fonlow.SyncML.OutlookSync
{
    public class OutlookToSyncMLSifN : OutlookToSyncMLX<NoteItem>
    {
        public OutlookToSyncMLSifN(Application app)
            : base(new OutlookNotes(app), new OutlookNotesWithSifN(app), DeletionLogForNotes.Default)
        {
        }

        /// <summary>
        /// Get Notes data as Sif-N
        /// </summary>
        /// <param name="NoteItem"></param>
        /// <returns></returns>
        protected override string GetItemData(NoteItem item)
        {
            return "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + SifAgent.ReadItemToText(item);
        }

        protected override void AddContentToCommand(SyncMLUpdateBase command, NoteItem contact)
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

        static readonly XElement typeOfText= XElement.Parse("<Type xmlns='syncml:metinf'>text/x-s4j-SifN</Type>");

        protected override XElement TypeOfText
        {
            get
            {
                return typeOfText;
            }
        }

    }

}
