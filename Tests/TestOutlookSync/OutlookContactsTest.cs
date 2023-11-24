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
    ///This is a test class for OutlookContactsOutlookTest and is intended
    ///to contain all OutlookContactsOutlookTest Unit Tests
    ///</summary>
    [TestClass()]
    public class OutlookContactsOutlookTest : OutlookItemsTestBase
    {

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        [TestMethod]
        [Description("Check Outlook contacts")]
        public void AddContactToOutlook()
        {
            XElement x = XElement.Load(MockPath+"sifc.xml");
            Application app = new Application();
            OutlookContactsWithSifC agent = new OutlookContactsWithSifC(app);
            OutlookContacts outlookAgent = new OutlookContacts(app);
            
            string entryId = outlookAgent.GetEntryIdByDisplayName("Ing John Patrick Doe Sn");//Delete it if exists
            if (entryId != null)
            {
                bool deletionOK = outlookAgent.DeleteItem(entryId);
                Assert.IsTrue(deletionOK);
            }
            agent.AddItem(x.ToString());//Add to outlook
            entryId = outlookAgent.GetEntryIdByDisplayName("Ing John Patrick Doe Sn");
            Assert.IsTrue(entryId != null);
        }

        [TestMethod]
        [Description("Add ContactItem with vCard; Convertion between ContactItem and vCard")]
        public void TestContactAndVCard()
        {
            string vcardText = File.ReadAllText(MockPath + "Contact of Outlook.vcf");
            VCard v = VCardReader.ParseText(vcardText);

            Application app = new Application();
            OutlookContactsWithVCard agent = new OutlookContactsWithVCard(app);
            OutlookContacts outlookAgent = new OutlookContacts(app);
            const string fullName = "Manager Contact Of Outlook Sr.";
            string entryId = outlookAgent.GetEntryIdByDisplayName(fullName);
            if (entryId != null)
            {
                bool deletionOK = outlookAgent.DeleteItem(entryId);
                Assert.IsTrue(deletionOK);
            }
            agent.AddItem(vcardText);
            entryId = outlookAgent.GetEntryIdByDisplayName(fullName);
            Assert.IsTrue(entryId != null);

            //test conversions
            ContactItem contactItem = outlookAgent.GetItemByEntryId(entryId);
            string text = agent.ReadItemToText(contactItem);
            VCard v2 = VCardReader.ParseText(text);
            Assert.IsTrue(v.Surname == v2.Surname &&
                v.GivenName == v2.GivenName &&
                v.MiddleName == v2.MiddleName &&
                v.Suffix == v2.Suffix && //full name will not be compared as Outlook treat fullname specially.
                v.Title == v2.Title &&
                v.Role == v2.Role &&
                v.Birthday == v2.Birthday &&
                v.Org == v2.Org &&
                v.Department == v2.Department &&
                v.Phones.Count == v2.Phones.Count &&
                v.Emails.Count == v2.Emails.Count &&
                v.Addresses.Count == v2.Addresses.Count&&
                v.URLs.Count == v2.URLs.Count);

            

        }

        [TestMethod]
        public void TestReplaceContact()
        {
            Application app = new Application();
            OutlookContactsWithSifC agent = new OutlookContactsWithSifC(app);
            OutlookContacts outlookAgent = new OutlookContacts(app);
            AddContactToOutlook();
            string existingEntryId = outlookAgent.GetEntryIdByDisplayName("Ing John Patrick Doe Sn");

            XElement x = XElement.Load(MockPath + "sifc.xml");
            string entryId = agent.ReplaceItem(x.ToString(), existingEntryId);
            Assert.IsTrue(entryId == existingEntryId);

        }


        /*  [TestMethod]
          public void WriteTest()
          {
              DeletionLogWriter writer = new DeletionLogWriter();
              writer.AddLog(System.DateTime.Now.ToString());
              writer.Close();
          }*/
    }
}
