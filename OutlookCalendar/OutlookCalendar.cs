using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.Office.Interop.Outlook;
using Fonlow.SyncML.Common;

namespace Fonlow.SyncML.OutlookSync
{
    public class OutlookCalendar : OutlookItems<AppointmentItem>
    {
        public OutlookCalendar(Application app, CalendarPeriod filter)
            : base(app.GetNamespace("MAPI").GetDefaultFolder(OlDefaultFolders.olFolderCalendar) as Folder)
        {
            this.filter = filter;
        }

        CalendarPeriod filter;

        protected override void DeleteItem(AppointmentItem item)
        {
            item.Delete();
        }

        protected override DateTime GetItemCreationTime(AppointmentItem item)
        {
            return item.CreationTime;
        }

        protected override DateTime GetItemLastModificationTime(AppointmentItem item)
        {
            return item.LastModificationTime;
        }

        protected override string GetEntryId(AppointmentItem item)
        {
            return item.EntryID;
        }

        public override string GetItemName(AppointmentItem item)
        {
            return item.Subject;
        }

        protected override bool IsItemIncludedInSync(AppointmentItem item)
        {
            switch (filter)
            {
                case CalendarPeriod.All:
                    return true;
                case CalendarPeriod.FutureOnly:
                    return item.Start >= DateTime.Now;
                case CalendarPeriod.SinceLastWeek:
                    return item.Start >= DateFunctions.StartDateOfLastWeek;
                case CalendarPeriod.SinceLast2Weeks:
                    return item.Start >= DateFunctions.StartDateOfLast2Weeks;
                case CalendarPeriod.SinceLastMonth:
                    return item.Start >= DateFunctions.StartDateOfLastMonth;
                case CalendarPeriod.SinceLast3Months:
                    return item.Start >= DateFunctions.StartDateOfLast3Months;
                case CalendarPeriod.SinceLast6Months:
                    return item.Start >= DateFunctions.StartDateOfLast6Months;
            }

            return false;
        }
    }

}
