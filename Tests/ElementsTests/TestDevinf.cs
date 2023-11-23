using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.XmlDiffPatch;
using Fonlow.SyncML;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Xunit;
using Fonlow.SyncML.Elements;

namespace Fonlow.TestSyncMLElements
{
    /// <summary>
    /// Test DevInf elements.
    /// </summary>
     
    public class TestDevInf : TestBase
    {

        [Fact]
        public void TestDevInfCTType()
        {
            TestElement<DevinfCTType>.Test(CaseFile("DevInfCTType.xml"));
        }

        [Fact]
        public void TestDevinfDevID()
        {
            TestElement<DevinfDevID>.Test(CaseFile("DevinfDevID.xml"));
        }

        [Fact]
        public void TestDevinfDevTyp()
        {
            TestElement<DevinfDevTyp>.Test(CaseFile("DevinfDevTyp.xml"));
        }

        [Fact]
        public void TestDevinfDisplayName()
        {
            TestElement<DevinfDisplayName>.Test(CaseFile("DevinfDisplayName.xml"));
        }

        [Fact]
        public void TestDevinfFwV()
        {
            TestElement<DevinfFwV>.Test(CaseFile("DevinfFwV.xml"));
        }

        [Fact]
        public void TestDevinfHwV()
        {
            TestElement<DevinfHwV>.Test(CaseFile("DevinfHwV.xml"));
        }

        [Fact]
        public void TestDevinfMan()
        {
            TestElement<DevinfMan>.Test(CaseFile("DevinfMan.xml"));
        }

        [Fact]
        public void TestDevinfMaxGUIDSize()
        {
            TestElement<DevinfMaxGUIDSize>.Test(CaseFile("DevinfMaxGUIDSize.xml"));
        }

        [Fact]
        public void TestDevinfMaxID()
        {
            TestElement<DevinfMaxID>.Test(CaseFile("DevinfMaxID.xml"));
        }

        [Fact]
        public void TestDevinfMaxMem()
        {
            TestElement<DevinfMaxMem>.Test(CaseFile("DevinfMaxMem.xml"));
        }

        [Fact]
        public void TestDevinfMod()
        {
            TestElement<DevinfMod>.Test(CaseFile("DevinfMod.xml"));
        }

        [Fact]
        public void TestDevinfOEM()
        {
            TestElement<DevinfOEM>.Test(CaseFile("DevinfOEM.xml"));
        }

        [Fact]
        public void TestDevinfParamName()
        {
            TestElement<DevinfParamName>.Test(CaseFile("DevinfParamName.xml"));
        }

        [Fact]
        public void TestDevinfPropName()
        {
            TestElement<DevinfPropName>.Test(CaseFile("DevinfPropName.xml"));
        }

        [Fact]
        public void TestDevinfSharedMem()
        {
            TestElement<DevinfSharedMem>.Test(CaseFile("DevinfSharedMem.xml"));
        }

        [Fact]
        public void TestDevinfSize()
        {
            TestElement<DevinfSize>.Test(CaseFile("DevinfSize.xml"));
        }

        [Fact]
        public void TestDevinfSourceRef()
        {
            TestElement<DevinfSourceRef>.Test(CaseFile("DevinfSourceRef.xml"));
        }

        [Fact]
        public void TestDevinfSupportLargeObjs()
        {
            TestElement<DevinfSupportLargeObjs>.Test(CaseFile("DevinfSupportLargeObjs.xml"));
        }

        [Fact]
        public void TestDevinfSupportNumberOfChanges()
        {
            TestElement<DevinfSupportNumberOfChanges>.Test(CaseFile("DevinfSupportNumberOfChanges.xml"));
        }

        [Fact]
        public void TestDevinfSwV()
        {
            TestElement<DevinfSwV>.Test(CaseFile("DevinfSwV.xml"));
        }

        [Fact]
        public void TestDevinfSyncType()
        {
            TestElement<DevinfSyncType>.Test(CaseFile("DevinfSyncType.xml"));
        }

        [Fact]
        public void TestDevinfValEnum()
        {
            TestElement<DevinfValEnum>.Test(CaseFile("DevinfValEnum.xml"));
        }

        [Fact]
        public void TestDevinfVerCT()
        {
            TestElement<DevinfVerCT>.Test(CaseFile("DevinfVerCT.xml"));
        }

        [Fact]
        public void TestDevinfVerDTD()
        {
            TestElement<DevinfVerDTD>.Test(CaseFile("DevinfVerDTD.xml"));
        }

        [Fact]
        public void TestDevinfXNam()
        {
            TestElement<DevinfXNam>.Test(CaseFile("DevinfXNam.xml"));
        }

        [Fact]
        public void TestDevinfXVal()
        {
            TestElement<DevinfXVal>.Test(CaseFile("DevinfXVal.xml"));
        }

        [Fact]
        public void TestSyncCap()
        {
            XElement nav = XElement.Load(CaseFile("DevinfSyncCap.xml"));

            DevinfSyncCap f = DevinfSyncCap.Create(nav);
            Assert.True(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [Fact]
        public void TestDevinfDSMem()
        {
            XElement nav = XElement.Load(CaseFile("DevinfDSMem.xml"));

            DevinfDSMem f = DevinfDSMem.Create(nav);
            Assert.True(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [Fact]
        public void TestDevinfTx()
        {
            XElement nav = XElement.Load(CaseFile("DevinfTx.xml"));

            DevinfTx f = DevinfTx.Create(nav);
            Assert.True(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [Fact]
        public void TestDevinfTx_Pref()
        {
            XElement nav = XElement.Load(CaseFile("DevinfTxPref.xml"));

            DevinfTx_Pref f = DevinfTx_Pref.Create(nav);
            Assert.True(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [Fact]
        public void TestDevinfRx()
        {
            XElement nav = XElement.Load(CaseFile("DevinfRx.xml"));

            DevinfRx f = DevinfRx.Create(nav);
            Assert.True(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [Fact]
        public void TestDevinfRx_Pref()
        {
            XElement nav = XElement.Load(CaseFile("DevinfRxPref.xml"));

            DevinfRx_Pref f = DevinfRx_Pref.Create(nav);
            Assert.True(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [Fact]
        public void TestDevinfExt()
        {
            XElement nav = XElement.Load(CaseFile("DevinfExt.xml"));

            DevinfExt f = DevinfExt.Create(nav);
            Assert.True(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [Fact]
        public void TestDevinfFilterRx()
        {
            XElement nav = XElement.Load(CaseFile("DevinfFilterRx.xml"));

            DevinfFilter_Rx f = DevinfFilter_Rx.Create(nav);
            Assert.True(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [Fact]
        public void TestDevinfFilterCap()
        {
            XElement nav = XElement.Load(CaseFile("DevinfFilterCap.xml"));

            DevinfFilterCap f = DevinfFilterCap.Create(nav);
            Assert.True(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [Fact]
        public void TestDevinfDataType()
        {
            TestElement<DevinfDataType>.Test(CaseFile("DevinfDataType.xml"));
        }

        [Fact]
        public void TestDevinfProperty()
        {
            XElement nav = XElement.Load(CaseFile("DevinfProperty.xml"));

            DevinfProperty f = DevinfProperty.Create(nav);
            Assert.True(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [Fact]
        public void TestDevinfProperty2()
        {
            XElement nav = XElement.Load(CaseFile("DevinfProperty2.xml"));

            DevinfProperty f = DevinfProperty.Create(nav);
            Assert.True(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [Fact]
        public void TestDevinfPropParam()
        {
            XElement nav = XElement.Load(CaseFile("DevinfPropParam.xml"));

            DevinfPropParam f = DevinfPropParam.Create(nav);
            Assert.True(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [Fact]
        public void TestDevinfDataStore()
        {
            XElement nav = XElement.Load(CaseFile("DevinfDataStore.xml"));

            DevinfDataStore f = DevinfDataStore.Create(nav);
            Assert.True(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [Fact]
        public void TestDevinfDataStoreMobiledit()
        {
            XElement nav = XElement.Load(CaseFile("DevinfDataStoreMobiledit.xml"));

            DevinfDataStore f = DevinfDataStore.Create(nav);
            Assert.True(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [Fact]
        public void TestDevinfCTCap()
        {
            XElement nav = XElement.Load(CaseFile("DevinfCTCap.xml"));

            DevinfCTCap f = DevinfCTCap.Create(nav);
            Assert.True(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [Fact]
        public void TestDevinf()
        {
            XElement nav = XElement.Load(CaseFile("Devinf.xml"));

            DevInf f = DevInf.Create(nav);
            Assert.True(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [Fact]
        public void TestDevinfMobiledit()
        {
            XElement nav = XElement.Load(CaseFile("DevinfMobiledit.xml"));

            DevInf f = DevInf.Create(nav);
            Assert.True(CompareXml(nav, f.Xml), f.Xml.ToString());
        }

        [Fact]
        public void TestDevinfFunambol()
        {
            XElement nav = XElement.Load(CaseFile("DevinfFunambol.xml"));

            DevInf f = DevInf.Create(nav);
            Assert.True(CompareXml(nav, f.Xml), f.Xml.ToString());
        }



    }
}
