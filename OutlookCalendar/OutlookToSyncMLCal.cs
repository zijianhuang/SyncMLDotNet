using System;
using System.Text;
using System.Xml.Linq;
using Microsoft.Office.Interop.Outlook;
using Fonlow.SyncML.Common;
using Fonlow.SyncML.Elements;


namespace Fonlow.SyncML.OutlookSync
{
    /// <summary>
    /// Provider Calendar implemenation of ISyncCommandsSource.
    /// </summary>
    public class OutlookToSyncMLSifE : OutlookToSyncMLX<AppointmentItem>
    {
        public OutlookToSyncMLSifE(Application app, CalendarPeriod filter)
            : base(new OutlookCalendar(app, filter), new OutlookCalendarWithSifE(app), DeletionLogForCalendar.Default)
        {
        }


        /// <summary>
        /// Get contact data as Sif-C
        /// </summary>
        /// <param name="AppointmentItem"></param>
        /// <returns></returns>
        protected override string GetItemData(AppointmentItem item)
        {
            return "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + SifAgent.ReadItemToText(item);
        }

        protected override void AddContentToCommand(SyncMLUpdateBase command, AppointmentItem content)
        {
            command.Meta.Xml.Add(FormatOfBase64);
            command.Meta.Xml.Add(TypeOfText);

            SyncMLItem item = SyncMLItem.Create();
            item.Source.LocURI.Content = content.EntryID;

            UTF8Encoding byteConverter = new UTF8Encoding();
            byte[] buffer = byteConverter.GetBytes(GetItemData(content));
            item.Data.Content = Convert.ToBase64String(buffer);

            command.ItemCollection.Add(item);
        }

        static readonly  XElement typeOfText=XElement.Parse("<Type xmlns='syncml:metinf'>text/x-s4j-sife</Type>");

        protected override XElement TypeOfText
        {
            get
            {
                return typeOfText;
            }
        }

    }

    public class OutlookToSyncMLICal : OutlookToSyncMLX<AppointmentItem>
    {
        public OutlookToSyncMLICal(Application app, CalendarPeriod filter)
            : base(new OutlookCalendar(app, filter), new OutlookCalendarWithICal(app), DeletionLogForCalendar.Default)
        {
        }

        /// <summary>
        /// Get contact data as vCard.
        /// </summary>
        /// <param name="AppointmentItem"></param>
        /// <returns></returns>
        protected override string GetItemData(AppointmentItem item)
        {
            return SifAgent.ReadItemToText(item);
        }

        protected override void AddContentToCommand(SyncMLUpdateBase command, AppointmentItem contact)
        {
            command.Meta.Xml.Add(TypeOfText);
            SyncMLItem item = SyncMLItem.Create();
            item.Source.LocURI.Content = contact.EntryID;
            item.Data.Content = GetItemData(contact);
            command.ItemCollection.Add(item);
        }

        static readonly XElement typeOfText =XElement.Parse("<Type xmlns='syncml:metinf'>text/x-vcalendar</Type>");

        protected override XElement TypeOfText
        {
            get
            {
                return typeOfText;
            }
        }

    }
}
