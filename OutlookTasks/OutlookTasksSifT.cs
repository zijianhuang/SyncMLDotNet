using System;
using System.Xml.Linq;
using Microsoft.Office.Interop.Outlook;

namespace Fonlow.SyncML.OutlookSync
{
    public class OutlookTasksWithSifT : OutlookItemsWithSyncContent<TaskItem>
    {
        public OutlookTasksWithSifT(Application app)
            : base(app.GetNamespace("MAPI").GetDefaultFolder(OlDefaultFolders.olFolderTasks) as Folder)
        {
        }

        protected override TaskItem AddItemToItemsFolder()
        {
            return ItemsFolder.Items.Add(OlItemType.olTaskItem) as TaskItem;
        }

        protected override string GetEntryId(TaskItem item)
        {
            return item.EntryID;
        }

        protected override void SaveItem(TaskItem item)
        {
            item.Save();
        }

        const string isoFormat = "yyyyMMddTHHmmssZ";

        public override string ReadItemToText(TaskItem item)
        {
            XElement element = new XElement("note",
                new XElement("SIFVersion", "1.0"),
                DataUtility.CreateElement("ActualWork", item.ActualWork.ToString()),
                DataUtility.CreateElement("BillingInformation", item.BillingInformation),
                DataUtility.CreateElement("Companies", item.Companies),
                DataUtility.CreateElement("Categories", item.Categories),
                 DataUtility.CreateElement("Complete", item.Complete ? "1" : "0"),
                DataUtility.CreateElement("DueDate", item.DueDate.ToUniversalTime().ToString(isoFormat)),
                DataUtility.CreateElement("DateCompleted", item.DateCompleted.ToString(isoFormat)),
           DataUtility.CreateElement("Body", item.Body),
                           DataUtility.CreateElement("Importance", ((int)item.Importance).ToString()),
                DataUtility.CreateElement("Mileage", item.Mileage),
                DataUtility.CreateElement("PercentComplete", item.PercentComplete.ToString()),
                DataUtility.CreateElement("ReminderSet", item.ReminderSet ? "1" : "0"),
                DataUtility.CreateElement("ReminderTime", item.ReminderTime.ToUniversalTime().ToString(isoFormat)),
                DataUtility.CreateElement("ReminderSoundFile", item.ReminderSoundFile),
                DataUtility.CreateElement("IsRecurring", item.IsRecurring ? "1" : "0"),
                DataUtility.CreateElement("Sensitivity", ((int)item.Sensitivity).ToString()),
                DataUtility.CreateElement("StartDate", item.StartDate.ToUniversalTime().ToString(isoFormat)),
                DataUtility.CreateElement("Status", ((int)item.Status).ToString()),
                DataUtility.CreateElement("Subject", item.Subject),
                DataUtility.CreateElement("TeamTask", item.TeamTask ? "1" : "0"),
                DataUtility.CreateElement("TotalWork", item.TotalWork.ToString())
              );

            if (item.IsRecurring)
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
            }

            return element.ToString(SaveOptions.DisableFormatting);

        }

        protected override void WriteMetaToItem(string meta, TaskItem item)
        {
            XElement sif = XElement.Parse(meta);
            item.ActualWork = DataUtility.ParseInt(sif, "ActualWork");
            item.BillingInformation = DataUtility.GetXElementValueSafely(sif, "BillingInformation");
            item.Companies = DataUtility.GetXElementValueSafely(sif, "Companies");
            item.Categories = DataUtility.GetXElementValueSafely(sif, "Categories");
            item.Complete = DataUtility.ParseBool(sif, "Complete");
            item.DueDate = DataUtility.ParseDate(sif, "DueDate");
            item.DateCompleted = DataUtility.ParseDate(sif, "DateCompleted");
            item.Body = DataUtility.GetXElementValueSafely(sif, "Body");
            item.Importance = (OlImportance)DataUtility.ParseInt(sif, "Importance");
            item.Mileage = DataUtility.GetXElementValueSafely(sif, "Mileage");
            item.PercentComplete = DataUtility.ParseInt(sif, "PercentComplete");
            item.ReminderSet = DataUtility.ParseBool(sif, "ReminderSet");
            item.ReminderTime = DataUtility.ParseDateTime(sif, "ReminderTime");
            item.ReminderSoundFile = DataUtility.GetXElementValueSafely(sif, "ReminderSoundFile");
            // item.IsRecurring = DataUtility.ParseBool(sif, "IsRecurring");
            item.Sensitivity = (OlSensitivity)DataUtility.ParseInt(sif, "Sensitivity");
            item.StartDate = DataUtility.ParseDate(sif, "StartDate");
            item.Status = (OlTaskStatus)DataUtility.ParseInt(sif, "Status");
            item.Subject = DataUtility.GetXElementValueSafely(sif, "Subject");
            item.TotalWork = DataUtility.ParseInt(sif, "TotalWork");

            if (DataUtility.ParseBool(sif, "IsRecurring"))
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

            }

        }
    }
}
