using Fonlow.SyncML.Common;
using Microsoft.Office.Interop.Outlook;

namespace Fonlow.SyncML.OutlookSync
{
    public class OutlookDataSourceCalendar : OutlookDataSourceBase<AppointmentItem>
    {
        public OutlookDataSourceCalendar(Application app, string exchangeType, CalendarPeriod filter)
            : base(app,
            (exchangeType == "SIF") ? new OutlookToSyncMLSifE(app, filter) as OutlookToSyncMLX<AppointmentItem> : new OutlookToSyncMLICal(app, filter),
            (exchangeType == "SIF") ? new SyncMLSifEToOutlook(app, filter) as SyncMLXToOutlook<AppointmentItem> : new SyncMLICalendarToOutlook(app, filter),
            new OutlookCalendar(app, filter), DeletionLogForCalendar.Default)
        {
        }

        public OutlookDataSourceCalendar(string exchangeType, CalendarPeriod filter)
            : this(new Application(), exchangeType, filter)
        {

        }

        public OutlookDataSourceCalendar(string exchangeType) : this(exchangeType, CalendarPeriod.All)
        {

        }


    }
}
