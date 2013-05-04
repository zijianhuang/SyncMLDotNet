using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.Office.Interop.Outlook;
using Fonlow.SyncML.Common;

namespace Fonlow.SyncML.OutlookSync
{
    public class OutlookCalendarWithSifE : OutlookItemsWithSyncContent<AppointmentItem>
    {
        public OutlookCalendarWithSifE(Application app )
            : base(app.GetNamespace("MAPI").GetDefaultFolder(OlDefaultFolders.olFolderCalendar) as Folder)
        {
        }

        protected override AppointmentItem AddItemToItemsFolder()
        {
            return ItemsFolder.Items.Add(OlItemType.olAppointmentItem) as AppointmentItem;
        }

        protected override string GetEntryId(AppointmentItem item)
        {
            return item.EntryID;
        }

        protected override void SaveItem(AppointmentItem item)
        {
            item.Save();
        }

        const string isoFormat = "yyyyMMddTHHmmssZ";

        public override string ReadItemToText(AppointmentItem item)
        {
            XElement element = new XElement("appointment",
                new XElement("SIFVersion", "1.0"),
                     DataUtility.CreateElement("AllDayEvent", item.AllDayEvent ? "1" : "0"),
                     DataUtility.CreateElement("BusyStatus", ((int)item.BusyStatus).ToString()),
                     DataUtility.CreateElement("Categories", item.Categories),
                     DataUtility.CreateElement("Companies", item.Companies),
                 DataUtility.CreateElement("Importance", ((int)item.Importance).ToString()),
                 DataUtility.CreateElement("IsRecurring", item.IsRecurring ? "1" : "0"),
                 DataUtility.CreateElement("Location", item.Location),
                 DataUtility.CreateElement("MeetingStatus", ((int)item.MeetingStatus).ToString()),
                 DataUtility.CreateElement("Mileage", item.Mileage),
                 DataUtility.CreateElement("ReminderMinutesBeforeStart", item.ReminderMinutesBeforeStart.ToString()),
              DataUtility.CreateElement("ReminderSet", item.ReminderSet ? "1" : "0"),
              DataUtility.CreateElement("ReminderSoundFile", item.ReminderSoundFile),
              DataUtility.CreateElement("ReplyTime", item.ReplyTime.ToUniversalTime().ToString(isoFormat)),
              DataUtility.CreateElement("Sensitivity", ((int)item.Sensitivity).ToString()),
                 DataUtility.CreateElement("Subject", item.Subject),
            DataUtility.CreateElement("Body", item.Body),
            DataUtility.CreateElement("Start", item.StartUTC.ToString(isoFormat)),
            DataUtility.CreateElement("End", item.EndUTC.ToString(isoFormat))
                     );

          /* no need to check  if (item.IsRecurring)
            {
                RecurrencePattern pattern = item.GetRecurrencePattern();
                element.Add(
                    DataUtility.CreateElement("DayOfMonth", pattern.DayOfMonth.ToString()),
                    DataUtility.CreateElement("DayOfWeekMask", ((int)pattern.DayOfWeekMask).ToString()),
                    DataUtility.CreateElement("Interval", pattern.Interval.ToString()),
                    DataUtility.CreateElement("Instance", pattern.Instance.ToString()),
                    DataUtility.CreateElement("MonthOfYear", pattern.MonthOfYear.ToString()),
                    DataUtility.CreateElement("NoEndDate", pattern.NoEndDate ? "1" : "0"),
                    DataUtility.CreateElement("Occurrences", pattern.Occurrences.ToString()),
                    DataUtility.CreateElement("PatternStartDate", pattern.PatternStartDate.ToUniversalTime().ToString(isoFormat)),
                    DataUtility.CreateElement("PatternEndDate", pattern.PatternEndDate.ToUniversalTime().ToString(isoFormat)),
                    DataUtility.CreateElement("RecurrenceType", ((int)pattern.RecurrenceType).ToString())
                    );
            }*/
            
            return element.ToString(SaveOptions.DisableFormatting);

        }

        protected override void WriteMetaToItem(string meta, AppointmentItem item)
        {
            try
            {
                XElement sif = XElement.Parse(meta);
                item.Subject = DataUtility.GetXElementValueSafely(sif, "Subject");
                item.Body = DataUtility.GetXElementValueSafely(sif, "Body");
                DateTime dt = DataUtility.ParseDateTime(DataUtility.GetXElementValueSafely(sif, "Start"));
                if (dt.Kind == DateTimeKind.Utc)
                    item.StartUTC = dt;
                else
                    item.Start = dt;

                dt = DataUtility.ParseDateTime(DataUtility.GetXElementValueSafely(sif, "End"));
                if (dt.Kind == DateTimeKind.Utc)
                    item.EndUTC = dt;
                else
                    item.End = dt;

                item.AllDayEvent = DataUtility.ParseBool(DataUtility.GetXElementValueSafely(sif, "AllDayEvent"));
                item.BusyStatus = (OlBusyStatus)DataUtility.ParseInt(DataUtility.GetXElementValueSafely(sif, "BusyStatus"));
                item.Categories = DataUtility.GetXElementValueSafely(sif, "Categories");
                item.Companies = DataUtility.GetXElementValueSafely(sif, "Companies");
                item.Importance = (OlImportance)DataUtility.ParseInt(DataUtility.GetXElementValueSafely(sif, "Importance"));
               // item.IsRecurring = DataUtility.ParseBool(DataUtility.GetXElementValueSafely(sif, "IsRecurring"));no need to check in sync for recurrence events.
                item.Location = DataUtility.GetXElementValueSafely(sif, "Location");
                item.MeetingStatus = (OlMeetingStatus)DataUtility.ParseInt(DataUtility.GetXElementValueSafely(sif, "MeetingStatus"));
                item.Mileage = DataUtility.GetXElementValueSafely(sif, "Mileage");
                item.ReminderMinutesBeforeStart = DataUtility.ParseInt(DataUtility.GetXElementValueSafely(sif, "ReminderMinutesBeforeStart"));
                item.ReminderSet = DataUtility.ParseBool(DataUtility.GetXElementValueSafely(sif, "ReminderSet"));
                item.ReminderSoundFile = DataUtility.GetXElementValueSafely(sif, "ReminderSoundFile");
                dt = DataUtility.ParseDateTime(DataUtility.GetXElementValueSafely(sif, "ReplyTime"));
                if (dt.Kind == DateTimeKind.Utc)
                    item.ReplyTime = dt.ToLocalTime();
                else
                    item.ReplyTime = dt;

                item.Sensitivity = (OlSensitivity)DataUtility.ParseInt(DataUtility.GetXElementValueSafely(sif, "Sensitivity"));

             /* no need to check  if (DataUtility.ParseBool(sif, "IsRecurring")) //Task has similar pattern
                {
                    RecurrencePattern pattern = item.GetRecurrencePattern();
                    pattern.RecurrenceType = (OlRecurrenceType)DataUtility.ParseInt(sif, "RecurrenceType");//this must be first
                    switch (pattern.RecurrenceType)
                    {
                        case OlRecurrenceType.olRecursDaily:
                            pattern.DayOfWeekMask = (OlDaysOfWeek)DataUtility.ParseInt(sif, "DayOfWeekMask");
                            pattern.Interval = DataUtility.ParseInt(sif, "Interval");
                            break;
                        case OlRecurrenceType.olRecursMonthNth:
                            pattern.Interval = DataUtility.ParseInt(sif, "Interval");
                            pattern.Instance = DataUtility.ParseInt(sif, "Instance"); //say the 2nd Sunday in March
                            pattern.DayOfMonth = DataUtility.ParseInt(sif, "DayOfMonth");
                            break;
                        case OlRecurrenceType.olRecursMonthly:
                            pattern.Interval = DataUtility.ParseInt(sif, "Interval");
                            pattern.DayOfWeekMask = (OlDaysOfWeek)DataUtility.ParseInt(sif, "DayOfWeekMask");
                            break;
                        case OlRecurrenceType.olRecursWeekly:
                            pattern.Interval = DataUtility.ParseInt(sif, "Interval");
                            pattern.DayOfWeekMask = (OlDaysOfWeek)DataUtility.ParseInt(sif, "DayOfWeekMask");
                            break;
                        case OlRecurrenceType.olRecursYearNth:
                            pattern.Interval = DataUtility.ParseInt(sif, "Interval");
                            pattern.Instance = DataUtility.ParseInt(sif, "Instance");
                            pattern.MonthOfYear = DataUtility.ParseInt(sif, "MonthOfYear");
                            break;
                        case OlRecurrenceType.olRecursYearly:
                            pattern.DayOfWeekMask = (OlDaysOfWeek)DataUtility.ParseInt(sif, "DayOfWeekMask");
                            pattern.MonthOfYear = DataUtility.ParseInt(sif, "MonthOfYear");
                            break;
                        default:
                            break;
                    }

                    //                pattern.NoEndDate = DataUtility.ParseBool(sif, "NoEndDate"); No need to set
                    //If the PatternEndDate property or the Occurrences property is set, the pattern is considered to be finite 
                    //and the NoEndDate property is False. If neither PatternEndDate nor Occurrences is set, the pattern is considered 
                    //infinite and NoEndDate is True.
                    //pattern.Occurrences = DataUtility.ParseInt(sif, "Occurrences"); no need to set

                    pattern.PatternStartDate = DataUtility.ParseDateTime(sif, "PatternStartDate");
                    DateTime d = DataUtility.ParseDateTime(sif, "PatternEndDate");
                    if (d != new DateTime(4501, 1, 1))
                        pattern.PatternEndDate = d;//must be the end
                   
                } */
            }
            catch (System.NullReferenceException e)
            {
                System.Diagnostics.Trace.TraceInformation("WirteMetaToItem: " + e.ToString() + "~" + e.Message);
            }
        }

    }

}
