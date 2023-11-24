using Fonlow.SyncML.OutlookSync;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Office.Interop.Outlook;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fonlow.SyncML.Elements;
using System.IO;
using Fonlow.VCard;

namespace TestOutlookSync
{
    /// <summary>
    /// Summary description for OutlookContactsIntegrationTests
    /// </summary>
    [TestClass]
    public class ContactsIntegrationTests
    {
        public ContactsIntegrationTests()
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

        //bool RemoveAllItemsInOutlook()
        //{
            
        //}


        [TestMethod]
        public void TestMethod1()
        {
            //
            // TODO: Add test logic here
            //
        }
    }
}
