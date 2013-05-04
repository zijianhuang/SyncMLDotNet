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
    ///This is a test class for XmlToSyncMLSyncCommandsForVcardTest and is intended
    ///to contain all XmlToSyncMLSyncCommandsForVcardTest Unit Tests
    ///</summary>
    [TestClass()]
    [DeploymentItem(@"TestCase\OcChangeLog.xml")]
    [DeploymentItem(@"TestCase\SifcToChangeLogXML.xsl")]
    [DeploymentItem(@"TestCase\SifcChangeLog.xml")]
    [DeploymentItem(@"TestCase\ChangeLogXMLToSifc.xsl")]
    [DeploymentItem(@"TestCase\AddAndyWillCommandVcard.xml")]
    public class XmlToSyncMLSyncCommandsForVcardTest
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
        ///A test for AddContentToCommand
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Fonlow.SyncML.OCLocalDataSource.dll")]
        public void AddContentToCommandTest()
        {
            XElement ocChangeLogXml = XElement.Load(@"OcChangeLog.xml");

            XmlToSyncMLSyncCommandsForVcard_Accessor target = new XmlToSyncMLSyncCommandsForVcard_Accessor(ocChangeLogXml, 
@"ChangeLogXMLToSifc.xsl", 100, false);

            Collection<SyncMLCommand> commands = new Collection<SyncMLCommand>();
            Assert.AreEqual(4, target.NumberOfChanges);
   //         target.PrepareSifcChangeLogXml();
            target.ExtractNextCommands(commands);
            System.Diagnostics.Debug.WriteLine("Totol commands: " + commands.Count.ToString());
            foreach (SyncMLCommand command in commands)
            {
                System.Diagnostics.Debug.WriteLine(command.Xml.ToString());
            }


            XElement addAndyWillXml = XElement.Load(@"AddAndyWillCommandVcard.xml");

            Assert.IsTrue(CompareXml(addAndyWillXml, commands[3].Xml));
        }

    }
}
