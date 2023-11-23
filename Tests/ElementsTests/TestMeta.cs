using Fonlow.SyncML.Elements;
using System.Xml.Linq;

namespace Fonlow.TestSyncMLElements
{

	public class TestMeta : TestBase
	{
		[Fact]
		public void TestAnchor()
		{
			XElement nav = XElement.Load(CaseFile("MetaAnchor.xml"));
			MetaAnchor f = MetaAnchor.Create(nav);
			Assert.True(CompareXml(nav, f.Xml), f.Xml.ToString());
		}

		[Fact]
		public void TestMem()
		{
			XElement nav = XElement.Load(CaseFile("MetaMem.xml"));
			MetaMem f = MetaMem.Create(nav);
			Assert.True(CompareXml(nav, f.Xml), f.Xml.ToString());
		}


		[Fact]
		public void TestMetaFieldLevel()
		{
			TestElement<MetaFieldLevel>.Test(CaseFile("MetaFieldLevel.xml"));
		}

		[Fact]
		public void TestMetaFormat()
		{
			TestElement<MetaFormat>.Test(CaseFile("MetaFormat.xml"));
		}

		[Fact]
		public void TestMetaFreeID()
		{
			TestElement<MetaFreeID>.Test(CaseFile("MetaFreeID.xml"));
		}

		[Fact]
		public void TestMetaFreeMem()
		{
			TestElement<MetaFreeMem>.Test(CaseFile("MetaFreeMem.xml"));
		}

		[Fact]
		public void TestMetaMark()
		{
			TestElement<MetaMark>.Test(CaseFile("MetaMark.xml"));
		}

		[Fact]
		public void TestMetaMaxMsgSize()
		{
			TestElement<MetaMaxMsgSize>.Test(CaseFile("MetaMaxMsgSize.xml"));
		}

		[Fact]
		public void TestMetaMaxObjSize()
		{
			TestElement<MetaMaxObjSize>.Test(CaseFile("MetaMaxObjSize.xml"));
		}

		[Fact]
		public void TestMetaNextNonce()
		{
			TestElement<MetaNextNonce>.Test(CaseFile("MetaNextNonce.xml"));
		}

		[Fact]
		public void TestMetaSize()
		{
			TestElement<MetaSize>.Test(CaseFile("MetaSize.xml"));
		}

		[Fact]
		public void TestMetaType()
		{
			TestElement<MetaType>.Test(CaseFile("MetaType.xml"));
		}

		[Fact]
		public void TestMetaVersion()
		{
			TestElement<MetaVersion>.Test(CaseFile("MetaVersion.xml"));
		}

		[Fact]
		public void TestMetaContent()
		{
			XElement nav = XElement.Load(CaseFile("MetaContent.xml"));
			SyncMLMeta meta = SyncMLSimpleElementFactory.Create<SyncMLMeta>(nav);

			Assert.True(CompareXml(meta.Xml, nav));

		}


	}
}
