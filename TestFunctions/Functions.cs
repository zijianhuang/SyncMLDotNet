using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fonlow.SyncML.Common;

namespace TestFunctions
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class TestFunctions
    {
        public TestFunctions()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
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
        public void TestSyncMLMd5()
        {
            string r = Utility.GenerateSyncMLMD5("Bruce2", "OhBehave", Encoding.UTF8.GetBytes("Nonce"));
            Assert.AreEqual(r, "Zz6EivR3yeaaENcRN6lpAQ==");
            DateTime dt = DateTime.Now;
            dt.AddHours(3);
        }

        [TestMethod]
        public void TestFromBase64First()
        {
            string r = Utility.ConvertFromBase64("Tm9uY2U=");
            Assert.AreEqual(r, "Nonce");
        }

        [TestMethod]
        public void TestStatusMessages()
        {
            string text = "status1=message1" + Environment.NewLine +
                "status2=message2";
            MessagesInPairs agent = new MessagesInPairs();
            agent.LoadMessages(text);
            Assert.AreEqual(agent.GetMessage("Status2"), "message2");
        }

        [TestMethod]
        public void TestStatusMessages2()
        {
            string text = "status1=message1" + "\n" +
                Environment.NewLine+
                "status2=message2"+"\n";
            MessagesInPairs agent = new MessagesInPairs();
            agent.LoadMessages(text);
            Assert.AreEqual(agent.GetMessage("Status2"), "message2");
        }

        [TestMethod]
        public void TestDateFunctions()
        {
            DateTime today = DateTime.Parse("2010-03-26");
            Assert.AreEqual(DateFunctions.GetStartDateOfLastWeek(today),
                DateTime.Parse("2010-03-14"));
            Assert.AreEqual(DateFunctions.GetStartDateOfLast2Weeks(today),
                DateTime.Parse("2010-03-07"));
            Assert.AreEqual(DateFunctions.GetStartDateOfLastMonth(today),
                DateTime.Parse("2010-02-01"));
            Assert.AreEqual(DateFunctions.GetStartDateOfLast3Months(today),
                DateTime.Parse("2009-12-01"));
            Assert.AreEqual(DateFunctions.GetStartDateOfLast6Months(today),
                DateTime.Parse("2009-09-01"));


        }
    }
}
