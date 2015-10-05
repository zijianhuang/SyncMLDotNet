using System;
using System.Linq;
using Fonlow.SyncML.Common;
using Microsoft.Office.Interop.Outlook;
using DDay.iCal;
using DDay.iCal.Serialization.iCalendar;

namespace Fonlow.SyncML.OutlookSync
{
    public class OutlookTasksWithICal : OutlookItemsWithSyncContent<TaskItem>
    {
        public OutlookTasksWithICal(Application app)
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
            using (iCalendar calendar = new iCalendar())
            {
                calendar.ProductID = SyncConstants.ProdId;
                Todo todo = new Todo();
                try
                {
                    todo.Summary = item.Subject;
                    todo.Description = item.Body;
                    todo.Completed = new iCalDateTime(item.DateCompleted);
                    todo.Due = new iCalDateTime(item.DueDate);
                    todo.Start = new iCalDateTime(item.StartDate);
                    if (!String.IsNullOrEmpty(item.Categories))
                        todo.Categories = item.Categories.Split(new string[] { ",", ", ", ",  " }, StringSplitOptions.RemoveEmptyEntries);
                    //todo.Priority = item.SchedulePlusPriority;
                    todo.PercentComplete = item.PercentComplete;
                    //   todo.Class.Value = item.Sensitivity
                    if (item.ReminderSet)
                    {
                        Alarm alarm = new Alarm();
                        alarm.Trigger = new Trigger(); //Alarm.Trigger is null by default.
                        alarm.Trigger.DateTime = new iCalDateTime(item.ReminderTime);
                        todo.Alarms.Add(alarm);
                    }

                    todo.AddProperty("X-FONLOW-ACTUALWORK", item.ActualWork.ToString());
                    todo.AddProperty("X-FONLOW-TOTALWORK", item.TotalWork.ToString());
                    todo.AddProperty("X-FONLOW-BILLINGINFO", item.BillingInformation);
                    todo.AddProperty("X-FONLOW-COMPANIES", item.Companies);
                    todo.AddProperty("X-FONLOW-MILEAGE", item.Mileage);
                    todo.AddProperty("X-FONLOW-TEAMTASK", item.TeamTask ? "1" : "0");

                    calendar.Todos.Add(todo);
                    iCalendarSerializer serializer = new iCalendarSerializer();
                    return serializer.SerializeToString(calendar);
                }
                catch (System.NullReferenceException e)
                {
                    System.Diagnostics.Trace.TraceWarning("ReadItemToText: " + e.ToString());
                    return String.Empty;
                }

            }
        }

        protected override void WriteMetaToItem(string meta, TaskItem item)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("metaData: " + meta);
                using (var reader = new System.IO.StringReader(meta))
                {
                    var calendarCollection = iCalendar.LoadFromStream(reader);
                    var calendar = calendarCollection.FirstOrDefault();
                    if (calendar == null)
                    {
                        System.Diagnostics.Trace.TraceWarning("Hey, calendarCollection is empty.");
                        return;

                    }

                    //Event icalEvent = calendar.Events[0];
                    Todo todo = calendar.Todos.FirstOrDefault() as Todo;
                    item.Subject = todo.Summary;
                    item.Body = todo.Description;
                    item.StartDate = todo.Start.Value.Date;
                    item.DueDate = todo.Due.Value.Date; //TaskItem use date only anyway.
                    if (todo.Completed != null)
                        item.DateCompleted = todo.Completed.Value;
                    item.PercentComplete = todo.PercentComplete;
                    item.Categories = String.Join(",", todo.Categories);
                    if (todo.Alarms.Count > 0)
                    {
                        Alarm alarm = todo.Alarms.FirstOrDefault() as Alarm;
                        item.ReminderSet = true;
                        if (alarm.Trigger.DateTime != null)
                            item.ReminderTime = alarm.Trigger.DateTime.Value;
                        else if (alarm.Trigger.Duration != null)
                            item.ReminderTime = item.DueDate.Add(alarm.Trigger.Duration.Value);
                    }


                    if (todo.Properties["X-FONLOW-ACTUALWORK"] != null)
                        item.ActualWork = int.Parse(todo.Properties["X-FONLOW-ACTUALWORK"].Value.ToString());
                    if (todo.Properties["X-FONLOW-TOTALWORK"] != null)
                        item.TotalWork = int.Parse(todo.Properties["X-FONLOW-TOTALWORK"].Value.ToString());
                    if (todo.Properties["X-FONLOW-BILLINGINFO"] != null)
                        item.BillingInformation = todo.Properties["X-FONLOW-BILLINGINFO"].Value.ToString();
                    if (todo.Properties["X-FONLOW-MILEAGE"] != null)
                        item.Mileage = todo.Properties["X-FONLOW-MILEAGE"].Value.ToString();
                    if (todo.Properties["X-FONLOW-COMPANIES"] != null)
                        item.Companies = todo.Properties["X-FONLOW-COMPANIES"].Value.ToString();
                    if (todo.Properties["X-FONLOW-TEAMTASK"] != null)
                        item.TeamTask = bool.Parse(todo.Properties["X-FONLOW-TEAMTASK"].Value.ToString());

                    todo.AddProperty("X-FONLOW-TEAMTASK", item.TeamTask ? "1" : "0");
                }

            }
            catch (System.NullReferenceException e)
            {
                System.Diagnostics.Trace.TraceInformation("WriteMetaToItem: " + e.ToString() + "~" + e.Message);
            }
            catch (System.FormatException e)
            {
                System.Diagnostics.Trace.TraceInformation("WriteMetaToItem: " + e.ToString() + "~" + e.Message);
            }
        }


    }
}
