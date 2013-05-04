using Fonlow.SyncML.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Specialized;
using System.Xml.Linq;
using System.Text;
using Fonlow.SyncML.OpenContacts;

namespace TestOcLocalDataSource
{
    
    
    /// <summary>
    ///This is a test class for OCLocalDataSourceTest and is intended
    ///to contain all OCLocalDataSourceTest Unit Tests
    ///</summary>
    [TestClass()]
    [DeploymentItem(@"TestCase\ChangeLog.xml")]
    [DeploymentItem(@"TestCase\ChangeLog2.xml")]
    [DeploymentItem(@"TestCase\ChangeLog3.xml")]
    public class OCLocalDataSourceTest
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

        /// <summary>
        ///A test for ReportChangeLogContent
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Fonlow.SyncML.OCLocalDataSource.dll")]
        public void ReportChangeLogContentTest()
        {
            XElement changes = XElement.Load(@"ChangeLog.xml").Element("Changes");
            StringBuilder expected = new StringBuilder();
            expected.AppendLine("These records will be added to the server.");
            expected.AppendLine("Andy Wong");
            expected.AppendLine("These records will be updated on the server.");
            expected.AppendLine("Super Big, René Haby");
            StringBuilder actual;
            actual = OCLocalDataSource_Accessor.ReportChangeLogContent(changes);
            Assert.AreEqual(expected.ToString(), actual.ToString());
            //   Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ReportChangeLogContent, without the New node. This also test SafeElementsQuery.
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Fonlow.SyncML.OCLocalDataSource.dll")]
        public void ReportChangeLogContentTest2()
        {
            XElement changes = XElement.Load(@"ChangeLog2.xml").Element("Changes");
            StringBuilder expected = new StringBuilder();
            expected.AppendLine("These records will be updated on the server.");
            expected.AppendLine("Super Big, René Haby");
            StringBuilder actual;
            actual = OCLocalDataSource_Accessor.ReportChangeLogContent(changes);
            Assert.AreEqual(expected.ToString(), actual.ToString());
            //   Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ReportChangeLogContent, with the New node but empty C nodes.
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Fonlow.SyncML.OCLocalDataSource.dll")]
        public void ReportChangeLogContentTest3()
        {
            XElement changes = XElement.Load(@"ChangeLog3.xml").Element("Changes");
            StringBuilder expected = new StringBuilder();
            expected.AppendLine("These records will be updated on the server.");
            expected.AppendLine("Super Big, René Haby");
            StringBuilder actual;
            actual = OCLocalDataSource_Accessor.ReportChangeLogContent(changes);
            Assert.AreEqual(expected.ToString(), actual.ToString());
            //   Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ConvertStringToPairs
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Fonlow.SyncML.OCLocalDataSource.dll")]
        public void ConvertStringToPairsTest()
        {
            string text = "ABC=123" + System.Environment.NewLine +
                "CDE=234";
            NameValueCollection expected = new NameValueCollection();
            expected.Add("ABC", "123");
            expected.Add("CDE", "234");
            NameValueCollection actual;
            actual = OCLocalDataSource_Accessor.ConvertStringToPairs(text);
            Assert.IsTrue(expected.Count  == actual.Count);
        }
    }
}
