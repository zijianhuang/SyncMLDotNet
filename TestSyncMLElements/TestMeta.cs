using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.XmlDiffPatch;
using Fonlow.SyncML;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fonlow.SyncML.Elements;

namespace Fonlow.TestSyncMLElements
{
    [TestClass]
    public class TestMeta : TestBase
    {
    /*    [TestMethod]
        public void TestSyncMLMeta1()
        {
            XElement x = XElement.Parse(@"<Meta>Something</Meta>");
            SyncMLMeta f = new SyncMLMeta();
            f.Content = "Something";
            Assert.IsTrue(CompareXml(x, f.Xml), f.Xml.ToString());
        }

        [TestMethod]
        public void TestSyncMLMeta2()
        {
            XElement nav = XElement.Load(CaseFile("Meta.xml"));
            SyncMLMeta f = new SyncMLMeta();
            f.Content = @"<DevInf xmlns='syncml:devinf'><Man xmlns='syncml:devinf'>IBM</Man>";

            Assert.IsTrue(CompareXml(nav, f.Xml), f.Xml.ToString());
        }*/

        [TestMethod]
        public void TestAnchor()
        {
            XElement nav = XElement.Load(CaseFile("MetaAnchor.xml"));
            MetaAnchor f = MetaAnchor.Create(nav);
            Assert.IsTrue(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [TestMethod]
        public void TestMem()
        {
            XElement nav = XElement.Load(CaseFile("MetaMem.xml"));
            MetaMem f = MetaMem.Create(nav);
            Assert.IsTrue(CompareXml(nav, f.Xml), f.Xml.ToString());
        }


        [TestMethod]
        public void TestMetaFieldLevel()
        {
            TestElement<MetaFieldLevel>.Test(CaseFile("MetaFieldLevel.xml"));
        }

        [TestMethod]
        public void TestMetaFormat()
        {
            TestElement<MetaFormat>.Test(CaseFile("MetaFormat.xml"));
        }

        [TestMethod]
        public void TestMetaFreeID()
        {
            TestElement<MetaFreeID>.Test(CaseFile("MetaFreeID.xml"));
        }

        [TestMethod]
        public void TestMetaFreeMem()
        {
            TestElement<MetaFreeMem>.Test(CaseFile("MetaFreeMem.xml"));
        }

        [TestMethod]
        public void TestMetaMark()
        {
            TestElement<MetaMark>.Test(CaseFile("MetaMark.xml"));
        }

        [TestMethod]
        public void TestMetaMaxMsgSize()
        {
            TestElement<MetaMaxMsgSize>.Test(CaseFile("MetaMaxMsgSize.xml"));
        }

        [TestMethod]
        public void TestMetaMaxObjSize()
        {
            TestElement<MetaMaxObjSize>.Test(CaseFile("MetaMaxObjSize.xml"));
        }

        [TestMethod]
        public void TestMetaNextNonce()
        {
            TestElement<MetaNextNonce>.Test(CaseFile("MetaNextNonce.xml"));
        }

        [TestMethod]
        public void TestMetaSize()
        {
            TestElement<MetaSize>.Test(CaseFile("MetaSize.xml"));
        }

        [TestMethod]
        public void TestMetaType()
        {
            TestElement<MetaType>.Test(CaseFile("MetaType.xml"));
        }

        [TestMethod]
        public void TestMetaVersion()
        {
            TestElement<MetaVersion>.Test(CaseFile("MetaVersion.xml"));
        }

        [TestMethod]
        public void TestMetaContent()
        {
            XElement nav = XElement.Load(CaseFile("MetaContent.xml"));
            SyncMLMeta meta = SyncMLSimpleElementFactory.Create<SyncMLMeta>(nav);

            Assert.IsTrue(CompareXml(meta.Xml, nav));

        }


    }
}
