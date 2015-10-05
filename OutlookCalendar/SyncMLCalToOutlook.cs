using Microsoft.Office.Interop.Outlook;
using Fonlow.SyncML.Common;

namespace Fonlow.SyncML.OutlookSync
{
    /// <summary>
    /// Apply SyncML commands to Outlook
    /// </summary>
    public class SyncMLSifEToOutlook : SyncMLXToOutlook<AppointmentItem>
    {
        public SyncMLSifEToOutlook(Application app, CalendarPeriod filter)
            : base(new OutlookCalendar(app, filter), new OutlookCalendarWithSifE(app))
        {
        }

        protected override string GetTextFromContent(bool isBase64, string contentType, string content)
        {
            if (contentType == "text/x-s4j-sife")
            {
                return Utility.ConvertFromBase64(content);
            }
            else if ((contentType == "text/x-vcalendar") || (contentType == "text/vcalendar"))
            {
                if (isBase64)
                    return Utility.ConvertFromBase64(content);
                else
                    return content;
            }
            else
                return null;
        }

    }

    public class SyncMLICalendarToOutlook : SyncMLXToOutlook<AppointmentItem>
    {
        public SyncMLICalendarToOutlook(Application app, CalendarPeriod filter)
            : base(new OutlookCalendar(app, filter), new OutlookCalendarWithICal(app))
        {
        }

        protected override string GetTextFromContent(bool isBase64, string contentType, string content)
        {
            if (contentType == "text/x-s4j-sife")
            {
                return Utility.ConvertFromBase64(content);
            }
            else if ((contentType == "text/x-vcalendar") || (contentType == "text/vcalendar"))
            {
                if (isBase64)
                    return Utility.ConvertFromBase64(content);
                else
                    return content;
            }
            else
                return null;
        }

    }
}
