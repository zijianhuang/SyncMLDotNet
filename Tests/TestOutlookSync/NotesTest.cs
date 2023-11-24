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
using System.Configuration;

namespace TestOutlookSync
{
    /// <summary>
    /// Summary description for NotesTest
    /// </summary>
    [TestClass]
    public class NotesTest : OutlookItemsTestBase
    {
        public NotesTest()
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
        public void TestAddNotes()
        {
            XElement x = XElement.Load(MockPath + "SifN.xml");
            Application app = new Application();
            OutlookNotesWithSifN agent = new OutlookNotesWithSifN(app);
            OutlookNotes outlookAgent = new OutlookNotes(app);

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

        const string subject = "New Note the first note";

        [TestMethod]
        public void TestReplaceNotes()
        {
            Application app = new Application();
            OutlookNotesWithSifN agent = new OutlookNotesWithSifN(app);
            OutlookNotes outlookAgent = new OutlookNotes(app);
            TestAddNotes();
            string existingEntryId = outlookAgent.GetEntryIdByDisplayName(subject);

            XElement x = XElement.Load(MockPath + "SifN.xml");
            string entryId = agent.ReplaceItem(x.ToString(), existingEntryId);
            Assert.IsTrue(entryId == existingEntryId);

            NoteItem note = outlookAgent.GetItemByEntryId(entryId);
            Assert.IsTrue(String.Compare(note.Body, x.Element("Body").Value, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.CompareOptions.IgnoreSymbols)==0);
            Assert.AreEqual(note.Subject, x.Element("Subject").Value);
               Assert.AreEqual(note.Categories, x.Element("Categories").Value);
            Assert.AreEqual(((int)note.Color).ToString(), x.Element("Color").Value);

        }

        //[TestMethod]
        //public void TestRegistry()
        //{
        //    //string manifestPath = (string)Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Office\Outlook\Addins\FonlowSyncML.Addin", "Manifest", String.Empty);
        //    //System.Diagnostics.Trace.TraceInformation("value: "+manifestPath);
        //    //string userConfigPath = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        //    //System.Diagnostics.Trace.TraceInformation("dir: " + userConfigPath);

        //    ExeConfigurationFileMap configFileMap =
        //  new ExeConfigurationFileMap();
        //    configFileMap.RoamingUserConfigFilename = @"C:\Users\AndySuperCo\AppData\Roaming\Fonlow\SyncMLAddinForOutlook\user.config";

        //    // Get the mapped configuration file

        //   Configuration  config =
        //       ConfigurationManager.OpenMappedExeConfiguration(
        //         configFileMap, ConfigurationUserLevel.PerUserRoaming);
  
           
        //}

        [TestMethod]
        public void TestSettingsProvider()
        {
            Assert.IsNotNull(OutlookSyncSettings.Default.DeviceAddress);
        }
    }
}
