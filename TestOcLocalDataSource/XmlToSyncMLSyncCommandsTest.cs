using Fonlow.SyncML.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using Fonlow.SyncML.Elements;
using System.Collections.ObjectModel;
using Microsoft.XmlDiffPatch;
using Fonlow.SyncML.OpenContacts;

namespace TestOcLocalDataSource
{
    
    
    /// <summary>
    ///This is a test class for XmlToSyncMLSyncCommandsTest and is intended
    ///to contain all XmlToSyncMLSyncCommandsTest Unit Tests
    ///</summary>
    [TestClass()]
    [DeploymentItem(@"TestCase\ChangeLogXMLToSifc.xsl")]
    [DeploymentItem(@"TestCase\ChangeLogXmlFromServer.xml")]
    [DeploymentItem(@"TestCase\OcChangeLog.xml")]
    [DeploymentItem(@"TestCase\PeopleOC.xml")]
    [DeploymentItem(@"TestCase\SifcChangeLog.xml")]
    [DeploymentItem(@"TestCase\AddAndyWillCommand.xml")]
    [DeploymentItem(@"TestCase\AddAndyWillCommandVcard.xml")]
    [DeploymentItem(@"TestCase\SifcToChangeLogXML.xsl")]


    public class XmlToSyncMLSyncCommandsTest
    {


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

        private bool CompareXml(XElement x1, XElement x2)
        {
            XmlDiff diff = new XmlDiff(XmlDiffOptions.IgnoreChildOrder | XmlDiffOptions.IgnoreComments
                | XmlDiffOptions.IgnoreWhitespace | XmlDiffOptions.IgnoreXmlDecl);

            return diff.Compare(x1.CreateReader(), x2.CreateReader());
        }

        /// <summary>
        ///A test for PrepareSifcChangeLogXml
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Fonlow.SyncML.OCLocalDataSource.dll")]
        [Description("Convert from Oc Change Log Xml to SIFCChangeLog")]
        public void PrepareSifcChangeLogXmlTest()
        {
            XElement ocChangeLogXml = XElement.Load(@"OcChangeLog.xml");

            XmlToSyncMLSyncCommands_Accessor target = new XmlToSyncMLSyncCommands_Accessor(ocChangeLogXml, @"ChangeLogXMLToSifc.xsl", 100);
            target.PrepareSifcChangeLogXml();

            XElement expectedXml = XElement.Load(@"SifcChangeLog.xml");
            System.Diagnostics.Debug.WriteLine(target.sifcChangeLogXml.ToString());
            Assert.IsTrue(CompareXml(expectedXml, target.sifcChangeLogXml));
        }

        /// <summary>
        ///A test for PrepareCommands: from OcChangeLogXml -> SifcChangeLogXml -> Commands
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Fonlow.SyncML.OCLocalDataSource.dll")]
        [Description("A test for PrepareCommands: from OcChangeLogXml -> SifcChangeLogXml -> Commands")]
        public void PrepareCommandsTest()
        {
            XElement ocChangeLogXml = XElement.Load(@"OcChangeLog.xml");

            XmlToSyncMLSyncCommands_Accessor target = new XmlToSyncMLSyncCommands_Accessor(ocChangeLogXml, @"ChangeLogXMLToSifc.xsl", 100);

            Collection<SyncMLCommand> commands = new Collection<SyncMLCommand>();
            target.PrepareSifcChangeLogXml();
            target.PrepareCommands(commands);
            System.Diagnostics.Debug.WriteLine("Totol commands: "+commands.Count.ToString());
            foreach (SyncMLCommand command in commands)
            {
                System.Diagnostics.Debug.WriteLine(command.Xml.ToString());
            }

            Assert.IsTrue(target.NumberOfChanges == 4);

            XElement addAndyWillXml = XElement.Load(@"AddAndyWillCommand.xml");

            Assert.IsTrue(CompareXml(addAndyWillXml, commands[3].Xml));
        }


        /// <summary>
        ///A test for GenerateXml: from SyncML commands to OC Change Long Xml.
        ///</summary>
        [TestMethod()]
        [Description("A test for GenerateXml: from SyncML commands to OC Change Long Xml.")]
        public void GenerateXmlTest()
        {
            Assert.IsTrue(System.IO.File.Exists("ChangeLogXMLToSifc.xsl"));
            XElement ocChangeLogXml = XElement.Load("OcChangeLog.xml");

            XmlToSyncMLSyncCommands_Accessor target = new XmlToSyncMLSyncCommands_Accessor(ocChangeLogXml, "ChangeLogXMLToSifc.xsl", 100);

            Collection<SyncMLCommand> commands = new Collection<SyncMLCommand>();
            target.PrepareSifcChangeLogXml();
            target.PrepareCommands(commands);
            System.Diagnostics.Debug.WriteLine("Totol commands: " + commands.Count.ToString());
            foreach (SyncMLCommand command in commands)
            {
                System.Diagnostics.Debug.WriteLine(command.Xml.ToString());
            }

            Assert.IsTrue(target.NumberOfChanges == 4);

            string styleSheetFileName = "SifcToChangeLogXML.xsl";
            string lastAnchor = "1899-12-30 00:00:00";
            XElement expected = ocChangeLogXml;
            string t = SyncMLSyncCommandsToXml.GenerateXml(commands, styleSheetFileName, lastAnchor);
          //  XElement actual = XElement.Parse( t );
         //   Assert.IsTrue(CompareXml(expected, actual));
        }



    }
}
