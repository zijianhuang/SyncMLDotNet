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
using DDay.iCal;

namespace TestOutlookSync
{
    /// <summary>
    /// Summary description for CalendarTest
    /// </summary>
    [TestClass]
    public class CalendarTest : OutlookItemsTestBase
    {
        public CalendarTest()
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
        public void TestAddCalendar()
        {
            AddCalendar();
        }

        string AddCalendar()
        {
            XElement x = XElement.Load(MockPath + "SifE.xml");
            Application app = new Application();
            OutlookCalendarWithSifE agent = new OutlookCalendarWithSifE(app);
            OutlookCalendar outlookAgent = new OutlookCalendar(app, CalendarPeriod.All);

            string entryId = outlookAgent.GetEntryIdByDisplayName(subject);
            if (entryId != null)
            {
                bool deletionOK = outlookAgent.DeleteItem(entryId);
                Assert.IsTrue(deletionOK);
            }
            agent.AddItem(x.ToString());
            entryId = outlookAgent.GetEntryIdByDisplayName(subject);
            Assert.IsTrue(entryId != null);
            return entryId;
        }

        const string subject = "Meeting with Maga developers";
        
        [TestMethod]
        public void TestReplaceCalendar()
        {
            Application app = new Application();
            OutlookCalendarWithSifE agent = new OutlookCalendarWithSifE(app);
            OutlookCalendar outlookAgent = new OutlookCalendar(app, CalendarPeriod.All);


            string existingEntryId = AddCalendar();

            XElement x = XElement.Load(MockPath + "SifE.xml");
            string entryId = agent.ReplaceItem(x.ToString(), existingEntryId);
            Assert.IsTrue(entryId == existingEntryId);

            AppointmentItem appointment = outlookAgent.GetItemByEntryId(entryId);
            Assert.AreEqual(appointment.Body, x.Element("Body").Value);
            Assert.AreEqual(appointment.Subject, x.Element("Subject").Value);
            Assert.AreEqual(((int)appointment.BusyStatus).ToString(), x.Element("BusyStatus").Value);
            Assert.AreEqual(appointment.Categories, x.Element("Categories").Value);
            Assert.AreEqual(appointment.Companies, x.Element("Companies").Value);
            Assert.AreEqual(((int)appointment.Importance).ToString(), x.Element("Importance").Value);
            Assert.AreEqual(appointment.AllDayEvent, x.Element("AllDayEvent").Value=="0"?false:true);
         //   Assert.AreEqual(appointment.IsRecurring, x.Element("IsRecurring").Value=="0"?false:true);no need to check recurring events in sync.
            Assert.AreEqual(appointment.Location, x.Element("Location").Value);
            Assert.AreEqual(((int)appointment.MeetingStatus).ToString(), x.Element("MeetingStatus").Value);
            Assert.AreEqual(appointment.Mileage, x.Element("Mileage").Value);
            Assert.AreEqual(appointment.ReminderMinutesBeforeStart.ToString(), x.Element("ReminderMinutesBeforeStart").Value);
            Assert.AreEqual(appointment.ReminderSet, x.Element("ReminderSet").Value=="0"?false:true);
            Assert.AreEqual(appointment.ReminderSoundFile, x.Element("ReminderSoundFile").Value);
            Assert.AreEqual(DataUtility.DateTimeToIsoText(appointment.ReplyTime.ToUniversalTime()), x.Element("ReplyTime").Value);
       //     Assert.AreEqual(((int)appointment.Sensitivity).ToString(), x.Element("Sensitivity").Value);

            if (appointment.IsRecurring)
            {
                Microsoft.Office.Interop.Outlook.RecurrencePattern pattern = appointment.GetRecurrencePattern();
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
            else
            {
                Assert.AreEqual(appointment.Start.ToUniversalTime().ToString("yyyyMMddTHHmmssZ"), x.Element("Start").Value);
                Assert.AreEqual(appointment.End.ToUniversalTime().ToString("yyyyMMddTHHmmssZ"), x.Element("End").Value);
            }

        }

        [TestMethod]
        public void TestTryParseDateText()
        {
            DateTime r;
            const string text = "20040930T133000Z";
            if (DateTime.TryParseExact(text, new string[] { "yyyyMMddTHHmmssZ", "yyyy-MM-dd", "yyyyMMdd" },
                    System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out r))
            {
                Assert.AreEqual(r.Year, 2004);
            }
            else
            {
                Assert.IsTrue(false, "Can not convert");
            }

        }

        /*[TestMethod]
        public void TestConvertingICalDaysOfWeek()
        {
            List<DaySpecifier> days = new List<DaySpecifier>();
            days.Add(new DaySpecifier(DayOfWeek.Sunday));
            days.Add(new DaySpecifier(DayOfWeek.Monday));
            days.Add(new DaySpecifier(DayOfWeek.Thursday));
            days.Add(new DaySpecifier(DayOfWeek.Friday));

            OlDaysOfWeek marks = OutlookCalendarWithICal_Accessor.iCal.DaysOfWeekToOutlookDaysOfWeek(days);
            Assert.IsTrue((int)marks == 51);
        }*/

        const string iCalSubject = "Outlook demo event";

        [TestMethod]
        public void TestAddCalendarICal()
        {
            string x = File.ReadAllText(MockPath + "Outlook demo event.ics");
            Application app = new Application();
            OutlookCalendarWithICal agent = new OutlookCalendarWithICal(app);
            OutlookCalendar outlookAgent = new OutlookCalendar(app, CalendarPeriod.All);

            string entryId = outlookAgent.GetEntryIdByDisplayName(iCalSubject);
            if (entryId != null)
            {
                bool deletionOK = outlookAgent.DeleteItem(entryId);
                Assert.IsTrue(deletionOK);
            }
            agent.AddItem(x); //also test ReadTextToItem()
            entryId = outlookAgent.GetEntryIdByDisplayName(iCalSubject);
            Assert.IsTrue(entryId != null);

            AppointmentItem appointment = outlookAgent.GetItemByEntryId(entryId);

            var calendarCollection =  iCalendar.LoadFromFile(MockPath + "Outlook demo event.ics");
            var calendar = calendarCollection.FirstOrDefault();
            var icalEvent = calendar.Events[0];
            CompareIcalEventAndAppointment(icalEvent, appointment);
        }

        void CompareIcalEventAndAppointment(IEvent icalEvent, AppointmentItem appointment)
        {
            Assert.IsTrue(icalEvent.Start.UTC == appointment.StartUTC);
            Assert.IsTrue(icalEvent.End.UTC == appointment.EndUTC);
            Assert.IsTrue(icalEvent.IsAllDay == appointment.AllDayEvent);
            Assert.IsTrue(icalEvent.Location == appointment.Location);
            Assert.IsTrue(icalEvent.Description.Replace('\n', ' ') == appointment.Body.Replace("\r\n", " "));
            //     Assert.IsTrue(icalEvent == appointment);

            var alarm = icalEvent.Alarms.FirstOrDefault();
            Assert.IsTrue((alarm != null) == appointment.ReminderSet);
            if (alarm != null)
                Assert.IsTrue(-(alarm.Trigger.Duration.Value.Minutes + alarm.Trigger.Duration.Value.Hours * 60) == appointment.ReminderMinutesBeforeStart);
            

        }

        [TestMethod]
        public void TestReplaceCalendarICal()
        {
            Application app = new Application();
            OutlookCalendarWithICal agent = new OutlookCalendarWithICal(app);
            OutlookCalendar outlookAgent = new OutlookCalendar(app, CalendarPeriod.All);
            TestAddCalendarICal();
            string existingEntryId = outlookAgent.GetEntryIdByDisplayName(iCalSubject);

            string x = File.ReadAllText(MockPath + "Outlook demo event.ics");
            string entryId = agent.ReplaceItem(x, existingEntryId);
            Assert.IsTrue(entryId == existingEntryId);

            AppointmentItem appointment = outlookAgent.GetItemByEntryId(entryId);

            var calendarCollection = iCalendar.LoadFromFile(MockPath + "Outlook demo event.ics");
            var calendar = calendarCollection.FirstOrDefault();
            var icalEvent = calendar.Events[0];
            CompareIcalEventAndAppointment(icalEvent, appointment);

            //Test ReadItemToText()
            string icalText = agent.ReadItemToText(appointment);
            calendarCollection = iCalendar.LoadFromStream(new StringReader(icalText));
            calendar = calendarCollection.FirstOrDefault();
            icalEvent = calendar.Events[0];
            CompareIcalEventAndAppointment(icalEvent, appointment);
        }

        [TestMethod]
        public void TestSettings()
        {
            CalendarSyncItem syncItem = OutlookSyncSettings.Default.CalendarSyncItem;
            DateTime now = DateTime.Now;
            syncItem.LastAnchorTime = now;
            OutlookSyncSettings.Default.Save();

            System.Threading.Thread.Sleep(100);
            OutlookSyncSettings.Default.Reload();
            Assert.AreEqual(now, OutlookSyncSettings.Default.CalendarSyncItem.LastAnchorTime);
        }

        [TestMethod]
        public void TestSettingsInThread()
        {
            DateTime now = DateTime.Now;

            SyncItem syncItem;

            System.Action saveAnchor = () =>
            {
                SyncSettingsBase settings = OutlookSyncSettings.Default;
                syncItem = OutlookSyncSettings.Default.CalendarSyncItem;
                syncItem.LastAnchorTime = now;
                settings.Save();
            };

            IAsyncResult result = saveAnchor.BeginInvoke(null, null);
            saveAnchor.EndInvoke(result);

            System.Threading.Thread.Sleep(100);
            OutlookSyncSettings.Default.Reload();
            Assert.AreEqual(now, OutlookSyncSettings.Default.CalendarSyncItem.LastAnchorTime);

            now = DateTime.Now;
            IAsyncResult result2 = saveAnchor.BeginInvoke(null, null);
            saveAnchor.EndInvoke(result2);

            System.Threading.Thread.Sleep(100);
            OutlookSyncSettings.Default.Reload();
            Assert.AreEqual(now, OutlookSyncSettings.Default.CalendarSyncItem.LastAnchorTime);


        }



    }
}
