using Fonlow.SyncML.OutlookSync;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Office.Interop.Outlook;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fonlow.SyncML.Elements;
using Fonlow.SyncML.Common;
using System.IO;
using System;
using DDay.iCal.DataTypes;
using DDay.iCal.Components;
using DDay.iCal;

namespace TestOutlookSync
{
    /// <summary>
    /// Summary description for TasksTest
    /// </summary>
    [TestClass]
    public class TasksTest : OutlookItemsTestBase
    {
        public TasksTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestAddTasks()
        {
            XElement x = XElement.Load(MockPath + "SifT.xml");
            Application app = new ApplicationClass();
            OutlookTasksWithSifT agent = new OutlookTasksWithSifT(app);
            OutlookTasks outlookAgent = new OutlookTasks(app);

            string entryId = outlookAgent.GetEntryIdByDisplayName(subject);
            if (entryId != null)
            {
                bool deletionOK = outlookAgent.DeleteItem(entryId);
                Assert.IsTrue(deletionOK);
            }
            agent.AddItem(x.ToString());
            entryId = outlookAgent.GetEntryIdByDisplayName(subject);
            Assert.IsTrue(entryId != null);
        }

        const string subject = "Something you need to do";

        [TestMethod]
        public void TestReplaceTasks()
        {
            Application app = new ApplicationClass();
            OutlookTasksWithSifT agent = new OutlookTasksWithSifT(app);
            OutlookTasks outlookAgent = new OutlookTasks(app);
            TestAddTasks();
            string existingEntryId = outlookAgent.GetEntryIdByDisplayName(subject);

            XElement x = XElement.Load(MockPath + "SifT.xml");
            string entryId = agent.ReplaceItem(x.ToString(), existingEntryId);
            Assert.AreEqual(entryId, existingEntryId);

            //Compare added item with SIFT.xml
            TaskItem task = outlookAgent.GetItemByEntryId(entryId);
            Assert.AreEqual(task.Body, x.Element("Body").Value);
            Assert.AreEqual(task.Categories, x.Element("Categories").Value);
            Assert.AreEqual(task.Companies, x.Element("Companies").Value);
            Assert.AreEqual(((int)task.Importance).ToString(), x.Element("Importance").Value);
            Assert.AreEqual(task.Complete, x.Element("Complete").Value == "0" ? false : true);
            Assert.AreEqual(DataUtility.DateTimeToIsoText(task.DueDate), x.Element("DueDate").Value);
            Assert.AreEqual(task.BillingInformation, x.Element("BillingInformation").Value);
            Assert.AreEqual(task.ActualWork.ToString(), x.Element("ActualWork").Value);
            Assert.AreEqual(task.IsRecurring, x.Element("IsRecurring").Value == "0" ? false : true);
            Assert.AreEqual(task.Mileage, x.Element("Mileage").Value);
            Assert.AreEqual(task.PercentComplete.ToString(), x.Element("PercentComplete").Value);
            Assert.AreEqual(task.ReminderSet, x.Element("ReminderSet").Value == "0" ? false : true);
            Assert.AreEqual(DataUtility.DateTimeToIsoText(task.ReminderTime.ToUniversalTime()), x.Element("ReminderTime").Value);
            Assert.AreEqual(((int)task.Sensitivity).ToString(), x.Element("Sensitivity").Value);
            // Assert.AreEqual(DataUtility.DateTimeToISOText(task.StartDate), x.Element("StartDate").Value); StartDate may be altered by PatternStartDate and RecurrencePattern
            Assert.AreEqual(((int)task.Status).ToString(), x.Element("Status").Value);
            Assert.AreEqual(task.Subject, x.Element("Subject").Value);
            Assert.AreEqual(task.TeamTask, x.Element("TeamTask").Value == "0" ? false : true);
            Assert.AreEqual(task.TotalWork.ToString(), x.Element("TotalWork").Value);
            if (task.IsRecurring)
            {
                Microsoft.Office.Interop.Outlook.RecurrencePattern pattern = task.GetRecurrencePattern();
                Assert.AreEqual(pattern.Interval.ToString(), x.Element("Interval").Value);
                Assert.AreEqual(pattern.MonthOfYear.ToString(), x.Element("MonthOfYear").Value);
                Assert.AreEqual(pattern.DayOfMonth.ToString(), x.Element("DayOfMonth").Value);
                Assert.AreEqual(((int)pattern.DayOfWeekMask).ToString(), x.Element("DayOfWeekMask").Value);
                Assert.AreEqual(pattern.Instance.ToString(), x.Element("Instance").Value);
                Assert.AreEqual(DataUtility.DateTimeToIsoText(pattern.PatternStartDate), x.Element("PatternStartDate").Value);
                Assert.AreEqual(DataUtility.DateTimeToIsoText(pattern.PatternEndDate), x.Element("PatternEndDate").Value);
                Assert.AreEqual(pattern.NoEndDate, x.Element("NoEndDate").Value == "0" ? false : true);
                //     Assert.AreEqual(pattern.Occurrences.ToString(), x.Element("Occurrences").Value); no need to test
            }

        }

        [TestMethod]
        public void TestAddTaskICal()
        {
            string x = File.ReadAllText(MockPath + "todo.ics");
            Application app = new ApplicationClass();
            OutlookTasksWithICal agent = new OutlookTasksWithICal(app);
            OutlookTasks outlookAgent = new OutlookTasks(app);

            string entryId = outlookAgent.GetEntryIdByDisplayName("Demo Task Sunbird");
            if (entryId != null)
            {
                bool deletionOK = outlookAgent.DeleteItem(entryId);
                Assert.IsTrue(deletionOK);
            }
            agent.AddItem(x); //also test ReadTextToItem()
            entryId = outlookAgent.GetEntryIdByDisplayName("Demo Task Sunbird");
            Assert.IsTrue(entryId != null);

            TaskItem task = outlookAgent.GetItemByEntryId(entryId);

            iCalendar calendar = iCalendar.LoadFromFile(MockPath + "todo.ics");
            Todo todo = calendar.Todos[0];
            CompareIcalTodoAndAppointment(todo, task);
        }

        void CompareIcalTodoAndAppointment(Todo todo, TaskItem task)
        {
            Assert.AreEqual(todo.Start.Date, task.StartDate);
            Assert.AreEqual(todo.Due.Date, task.DueDate);
            Assert.IsTrue(todo.Description.Value.Replace('\n', ' ') == task.Body.Replace("\r\n", " "));
            Assert.AreEqual(todo.Percent_Complete.Value, task.PercentComplete);
            Alarm alarm = todo.Alarms.FirstOrDefault();
            Assert.IsTrue((alarm != null) == task.ReminderSet);
            //    if (alarm != null)
            //       Assert.Equals(alarm.Trigger.DateTime.Value.ToLocalTime(), task.ReminderTime);


        }

        [TestMethod]
        public void TestReplaceTaskICal()
        {
            Application app = new ApplicationClass();
            OutlookTasksWithICal agent = new OutlookTasksWithICal(app);
            OutlookTasks outlookAgent = new OutlookTasks(app);
            TestAddTaskICal();
            string existingEntryId = outlookAgent.GetEntryIdByDisplayName("Demo Task Sunbird");

            string x = File.ReadAllText(MockPath + "todo.ics");
            string entryId = agent.ReplaceItem(x, existingEntryId);
            Assert.IsTrue(entryId == existingEntryId);

            TaskItem task = outlookAgent.GetItemByEntryId(entryId);

            iCalendar calendar = iCalendar.LoadFromFile(MockPath + "todo.ics");
            Todo todo = calendar.Todos[0];
            CompareIcalTodoAndAppointment(todo, task);

            //Test ReadItemToText()
            string icalText = agent.ReadItemToText(task);
            calendar = iCalendar.LoadFromStream(new StringReader(icalText));
            todo = calendar.Todos[0];
            CompareIcalTodoAndAppointment(todo, task);
        }

    }
}
