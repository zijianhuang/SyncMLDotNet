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
    /// <summary>
    /// Test DevInf elements.
    /// </summary>
    [TestClass]
    public class TestDevInf : TestBase
    {

        [TestMethod]
        public void TestDevInfCTType()
        {
            TestElement<DevinfCTType>.Test(CaseFile("DevInfCTType.xml"));
        }

        [TestMethod]
        public void TestDevinfDevID()
        {
            TestElement<DevinfDevID>.Test(CaseFile("DevinfDevID.xml"));
        }

        [TestMethod]
        public void TestDevinfDevTyp()
        {
            TestElement<DevinfDevTyp>.Test(CaseFile("DevinfDevTyp.xml"));
        }

        [TestMethod]
        public void TestDevinfDisplayName()
        {
            TestElement<DevinfDisplayName>.Test(CaseFile("DevinfDisplayName.xml"));
        }

        [TestMethod]
        public void TestDevinfFwV()
        {
            TestElement<DevinfFwV>.Test(CaseFile("DevinfFwV.xml"));
        }

        [TestMethod]
        public void TestDevinfHwV()
        {
            TestElement<DevinfHwV>.Test(CaseFile("DevinfHwV.xml"));
        }

        [TestMethod]
        public void TestDevinfMan()
        {
            TestElement<DevinfMan>.Test(CaseFile("DevinfMan.xml"));
        }

        [TestMethod]
        public void TestDevinfMaxGUIDSize()
        {
            TestElement<DevinfMaxGUIDSize>.Test(CaseFile("DevinfMaxGUIDSize.xml"));
        }

        [TestMethod]
        public void TestDevinfMaxID()
        {
            TestElement<DevinfMaxID>.Test(CaseFile("DevinfMaxID.xml"));
        }

        [TestMethod]
        public void TestDevinfMaxMem()
        {
            TestElement<DevinfMaxMem>.Test(CaseFile("DevinfMaxMem.xml"));
        }

        [TestMethod]
        public void TestDevinfMod()
        {
            TestElement<DevinfMod>.Test(CaseFile("DevinfMod.xml"));
        }

        [TestMethod]
        public void TestDevinfOEM()
        {
            TestElement<DevinfOEM>.Test(CaseFile("DevinfOEM.xml"));
        }

        [TestMethod]
        public void TestDevinfParamName()
        {
            TestElement<DevinfParamName>.Test(CaseFile("DevinfParamName.xml"));
        }

        [TestMethod]
        public void TestDevinfPropName()
        {
            TestElement<DevinfPropName>.Test(CaseFile("DevinfPropName.xml"));
        }

        [TestMethod]
        public void TestDevinfSharedMem()
        {
            TestElement<DevinfSharedMem>.Test(CaseFile("DevinfSharedMem.xml"));
        }

        [TestMethod]
        public void TestDevinfSize()
        {
            TestElement<DevinfSize>.Test(CaseFile("DevinfSize.xml"));
        }

        [TestMethod]
        public void TestDevinfSourceRef()
        {
            TestElement<DevinfSourceRef>.Test(CaseFile("DevinfSourceRef.xml"));
        }

        [TestMethod]
        public void TestDevinfSupportLargeObjs()
        {
            TestElement<DevinfSupportLargeObjs>.Test(CaseFile("DevinfSupportLargeObjs.xml"));
        }

        [TestMethod]
        public void TestDevinfSupportNumberOfChanges()
        {
            TestElement<DevinfSupportNumberOfChanges>.Test(CaseFile("DevinfSupportNumberOfChanges.xml"));
        }

        [TestMethod]
        public void TestDevinfSwV()
        {
            TestElement<DevinfSwV>.Test(CaseFile("DevinfSwV.xml"));
        }

        [TestMethod]
        public void TestDevinfSyncType()
        {
            TestElement<DevinfSyncType>.Test(CaseFile("DevinfSyncType.xml"));
        }

        [TestMethod]
        public void TestDevinfValEnum()
        {
            TestElement<DevinfValEnum>.Test(CaseFile("DevinfValEnum.xml"));
        }

        [TestMethod]
        public void TestDevinfVerCT()
        {
            TestElement<DevinfVerCT>.Test(CaseFile("DevinfVerCT.xml"));
        }

        [TestMethod]
        public void TestDevinfVerDTD()
        {
            TestElement<DevinfVerDTD>.Test(CaseFile("DevinfVerDTD.xml"));
        }

        [TestMethod]
        public void TestDevinfXNam()
        {
            TestElement<DevinfXNam>.Test(CaseFile("DevinfXNam.xml"));
        }

        [TestMethod]
        public void TestDevinfXVal()
        {
            TestElement<DevinfXVal>.Test(CaseFile("DevinfXVal.xml"));
        }

        [TestMethod]
        public void TestSyncCap()
        {
            XElement nav = XElement.Load(CaseFile("DevinfSyncCap.xml"));

            DevinfSyncCap f = DevinfSyncCap.Create(nav);
            Assert.IsTrue(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [TestMethod]
        public void TestDevinfDSMem()
        {
            XElement nav = XElement.Load(CaseFile("DevinfDSMem.xml"));

            DevinfDSMem f = DevinfDSMem.Create(nav);
            Assert.IsTrue(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [TestMethod]
        public void TestDevinfTx()
        {
            XElement nav = XElement.Load(CaseFile("DevinfTx.xml"));

            DevinfTx f = DevinfTx.Create(nav);
            Assert.IsTrue(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [TestMethod]
        public void TestDevinfTx_Pref()
        {
            XElement nav = XElement.Load(CaseFile("DevinfTxPref.xml"));

            DevinfTx_Pref f = DevinfTx_Pref.Create(nav);
            Assert.IsTrue(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [TestMethod]
        public void TestDevinfRx()
        {
            XElement nav = XElement.Load(CaseFile("DevinfRx.xml"));

            DevinfRx f = DevinfRx.Create(nav);
            Assert.IsTrue(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [TestMethod]
        public void TestDevinfRx_Pref()
        {
            XElement nav = XElement.Load(CaseFile("DevinfRxPref.xml"));

            DevinfRx_Pref f = DevinfRx_Pref.Create(nav);
            Assert.IsTrue(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [TestMethod]
        public void TestDevinfExt()
        {
            XElement nav = XElement.Load(CaseFile("DevinfExt.xml"));

            DevinfExt f = DevinfExt.Create(nav);
            Assert.IsTrue(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [TestMethod]
        public void TestDevinfFilterRx()
        {
            XElement nav = XElement.Load(CaseFile("DevinfFilterRx.xml"));

            DevinfFilter_Rx f = DevinfFilter_Rx.Create(nav);
            Assert.IsTrue(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [TestMethod]
        public void TestDevinfFilterCap()
        {
            XElement nav = XElement.Load(CaseFile("DevinfFilterCap.xml"));

            DevinfFilterCap f = DevinfFilterCap.Create(nav);
            Assert.IsTrue(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [TestMethod]
        public void TestDevinfDataType()
        {
            TestElement<DevinfDataType>.Test(CaseFile("DevinfDataType.xml"));
        }

        [TestMethod]
        public void TestDevinfProperty()
        {
            XElement nav = XElement.Load(CaseFile("DevinfProperty.xml"));

            DevinfProperty f = DevinfProperty.Create(nav);
            Assert.IsTrue(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [TestMethod]
        public void TestDevinfProperty2()
        {
            XElement nav = XElement.Load(CaseFile("DevinfProperty2.xml"));

            DevinfProperty f = DevinfProperty.Create(nav);
            Assert.IsTrue(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [TestMethod]
        public void TestDevinfPropParam()
        {
            XElement nav = XElement.Load(CaseFile("DevinfPropParam.xml"));

            DevinfPropParam f = DevinfPropParam.Create(nav);
            Assert.IsTrue(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [TestMethod]
        public void TestDevinfDataStore()
        {
            XElement nav = XElement.Load(CaseFile("DevinfDataStore.xml"));

            DevinfDataStore f = DevinfDataStore.Create(nav);
            Assert.IsTrue(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [TestMethod]
        public void TestDevinfDataStoreMobiledit()
        {
            XElement nav = XElement.Load(CaseFile("DevinfDataStoreMobiledit.xml"));

            DevinfDataStore f = DevinfDataStore.Create(nav);
            Assert.IsTrue(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [TestMethod]
        public void TestDevinfCTCap()
        {
            XElement nav = XElement.Load(CaseFile("DevinfCTCap.xml"));

            DevinfCTCap f = DevinfCTCap.Create(nav);
            Assert.IsTrue(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [TestMethod]
        public void TestDevinf()
        {
            XElement nav = XElement.Load(CaseFile("Devinf.xml"));

            DevInf f = DevInf.Create(nav);
            Assert.IsTrue(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [TestMethod]
        public void TestDevinfMobiledit()
        {
            XElement nav = XElement.Load(CaseFile("DevinfMobiledit.xml"));

            DevInf f = DevInf.Create(nav);
            Assert.IsTrue(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [TestMethod]
        public void TestDevinfFunambol()
        {
            XElement nav = XElement.Load(CaseFile("DevinfFunambol.xml"));

            DevInf f = DevInf.Create(nav);
            Assert.IsTrue(CompareXml(nav, f.Xml), f.Xml.ToString());
        }



    }
}
