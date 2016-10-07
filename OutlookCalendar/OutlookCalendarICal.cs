using Ical.Net;
using Ical.Net.DataTypes;
using Ical.Net.Interfaces.DataTypes;
using Ical.Net.Utility;
using Ical.Net.Serialization;
using Ical.Net.Serialization.iCalendar;

using Ical.Net.Serialization.iCalendar.Serializers;

using Fonlow.SyncML.Common;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Linq;

namespace Fonlow.SyncML.OutlookSync
{
    /// <summary>
    /// Remote name should then be "event" with Funambol server.
    /// </summary>
    public class OutlookCalendarWithICal : OutlookItemsWithSyncContent<AppointmentItem>
    {
        public OutlookCalendarWithICal(Application app)
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
            using (Calendar calendar = new Calendar())
            {
                calendar.ProductId = SyncConstants.ProdId;
                Event icalEvent = new Event();

                try
                {
                    icalEvent.Description = item.Body;
                    icalEvent.Summary = item.Subject;
                    icalEvent.Start = new CalDateTime(DateTime.SpecifyKind(item.StartUTC, DateTimeKind.Utc)); // item.StartUTC does not have Kind specified.
                    icalEvent.End = new CalDateTime(DateTime.SpecifyKind(item.EndUTC, DateTimeKind.Utc));
                    icalEvent.Duration = new TimeSpan(0, item.Duration, 0);
                    icalEvent.IsAllDay = item.AllDayEvent;
                    if (!String.IsNullOrEmpty(item.Categories))
                        icalEvent.Categories = item.Categories.Split(new string[] { ",  ", ", ", "," }, StringSplitOptions.RemoveEmptyEntries);//the order of the separator is significant
                    icalEvent.Location = item.Location;
                    if (item.ReminderSet)
                    {
                        Alarm alarm = new Alarm();
                        alarm.Trigger = new Trigger(new TimeSpan(0, -item.ReminderMinutesBeforeStart, 0)); //Alarm.Trigger is null by default.
                        icalEvent.Alarms.Add(alarm);
                    }
                    icalEvent.AddProperty("X-FONLOW-BILLINGINFO", item.BillingInformation);
                    icalEvent.AddProperty("X-FONLOW-COMPANIES", item.Companies);
                    icalEvent.AddProperty("X-FONLOW-MILEAGE", item.Mileage);
                    icalEvent.AddProperty("X-FONLOW-NOAGING", item.NoAging ? "1" : "0");
                    calendar.Events.Add(icalEvent);
                }
                catch (System.NullReferenceException e)
                {
                    System.Diagnostics.Trace.TraceInformation("ReadItemToText: " + e.ToString() + "~" + e.Message);
                }
                catch (System.ArgumentNullException e)
                {
                    System.Diagnostics.Trace.TraceInformation("ReadItemToText: " + e.ToString() + "~" + e.Message);
                }

                //iCalendarSerializer serializer = new iCalendarSerializer(calendar);
                //return serializer.SerializeToString();
                CalendarSerializer serializer = new CalendarSerializer();
                return serializer.SerializeToString(calendar);
            }
        }

        protected override void WriteMetaToItem(string meta, AppointmentItem item)
        {
            try
            {
                // System.Diagnostics.Trace.TraceInformation("metaData: " + metaData);
                using (var reader = new System.IO.StringReader(meta))
                {
                    var calendarCollection = Calendar.LoadFromStream(reader);
                    var calendar = calendarCollection.FirstOrDefault();
                    if (calendar == null)
                    {
                        System.Diagnostics.Trace.TraceWarning("Hey, calendarCollection is empty.");
                        return;

                    }

                    var icalEvent = calendar.Events[0] as Event;//1.0 is harder to use. The returned interface rather than object is really bad, so I have to type cast to obtain properties not included in IEvent, or 
                    //I have to use a few interface references.

                    item.Subject = icalEvent.Summary;
                    item.Body = icalEvent.Description;
                    item.StartUTC = icalEvent.Start.AsUtc;
                    item.EndUTC = icalEvent.End.AsUtc;

                    // item.Duration =  no need to define as duration is End - Start
                    item.AllDayEvent = icalEvent.IsAllDay;
                    item.Categories = String.Join(",", icalEvent.Categories);
                    item.Location = icalEvent.Location;

                    if (icalEvent.Alarms.Count > 0)
                    {
                        var alarm = icalEvent.Alarms.First();
                        item.ReminderSet = true;
                        item.ReminderMinutesBeforeStart = -(alarm.Trigger.Duration.Value.Minutes + alarm.Trigger.Duration.Value.Hours * 60);
                    }


                    if (icalEvent.Properties["X-FONLOW-BILLINGINFO"] != null)
                        item.BillingInformation = icalEvent.Properties["X-FONLOW-BILLINGINFO"].Value.ToString();
                    if (icalEvent.Properties["X-FONLOW-MILEAGE"] != null)
                        item.Mileage = icalEvent.Properties["X-FONLOW-MILEAGE"].Value.ToString();
                    if (icalEvent.Properties["X-FONLOW-COMPANIES"] != null)
                        item.Companies = icalEvent.Properties["X-FONLOW-COMPANIES"].Value.ToString();
                    if (icalEvent.Properties["X-FONLOW-NOAGING"] != null)
                        item.NoAging = icalEvent.Properties["X-FONLOW-NOAGING"].Value.ToString() == "1";
                    /* no need to check  if (icalEvent.RRule != null)
                              {
                                  DDay.iCal.DataTypes.RecurrencePattern pattern = icalEvent.RRule[0];
                                  Microsoft.Office.Interop.Outlook.RecurrencePattern itemPattern = item.GetRecurrencePattern();
                                  itemPattern.RecurrenceType = iCalFrequencyToOutlookRecurrenceType(pattern.Frequency);
                                  switch (itemPattern.RecurrenceType)
                                  {
                                      case OlRecurrenceType.olRecursDaily:
                                          itemPattern.Interval = pattern.Interval;
                                          itemPattern.DayOfWeekMask = iCalDaysOfWeekToOutlookDaysOfWeek(pattern.ByDay);
                                          break;
                           //           case OlRecurrenceType.olRecursMonthNth:
                           //               break;
                                      case OlRecurrenceType.olRecursMonthly:
                                          itemPattern.Interval = pattern.Interval;
                                          itemPattern.DayOfWeekMask = iCalDaysOfWeekToOutlookDaysOfWeek(pattern.ByDay);
                                          break;
                                      case OlRecurrenceType.olRecursWeekly:
                                          itemPattern.Interval = pattern.Interval;
                                          itemPattern.DayOfWeekMask = iCalDaysOfWeekToOutlookDaysOfWeek(pattern.ByDay);
                                          break;
                               //       case OlRecurrenceType.olRecursYearNth:
                               //           break;
                                      case OlRecurrenceType.olRecursYearly:
                                          itemPattern.DayOfWeekMask = iCalDaysOfWeekToOutlookDaysOfWeek(pattern.ByDay);
                            
                                          break;
                                      default:
                                          break;
                                  }
                              }*/
                }
            }
            catch (System.NullReferenceException e)
            {
                System.Diagnostics.Trace.TraceInformation("WriteMetaToItem: " + e.ToString() + "~" + e.Message);
            }
        }

        /*private static OlDaysOfWeek iCalDaysOfWeekToOutlookDaysOfWeek(List<DaySpecifier> days)
        {
            int k = 1;
            int r = 0;
            foreach (DaySpecifier d in days)
            {
                r |= k << (int)d.DayOfWeek;
            }
            return (OlDaysOfWeek)r;
        }*/

        /*   private static OlRecurrenceType iCalFrequencyToOutlookRecurrenceType(FrequencyType frequencyType)
           {
               switch (frequencyType)
               {
                   case FrequencyType.Daily:
                       return OlRecurrenceType.olRecursDaily;
                   case FrequencyType.Hourly:
                       return OlRecurrenceType.olRecursDaily;
                   case FrequencyType.Minutely:
                    return OlRecurrenceType.olRecursDaily;
                   case FrequencyType.Monthly:
                       return OlRecurrenceType.olRecursMonthly;
                   case FrequencyType.None:
                       return OlRecurrenceType.olRecursDaily;
                   case FrequencyType.Secondly:
                       return OlRecurrenceType.olRecursDaily;
                   case FrequencyType.Weekly:
                       return OlRecurrenceType.olRecursWeekly;
                   case FrequencyType.Yearly:
                       return OlRecurrenceType.olRecursYearly;
                   default:
                       return OlRecurrenceType.olRecursDaily;
               }
           }*/


    }

}
