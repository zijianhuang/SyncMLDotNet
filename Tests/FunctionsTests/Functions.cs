using System;
using System.Text;
using System.Collections.Generic;
using Xunit;
using Fonlow.SyncML.Common;

namespace TestFunctions
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>

	public class TestFunctions
	{

		[Fact]
		public void TestSyncMLMd5()
		{
			string r = Utility.GenerateSyncMLMD5("Bruce2", "OhBehave", Encoding.UTF8.GetBytes("Nonce"));
			Assert.Equal("Zz6EivR3yeaaENcRN6lpAQ==", r);
			DateTime dt = DateTime.Now;
			dt.AddHours(3);
		}

		[Fact]
		public void TestFromBase64First()
		{
			string r = Utility.ConvertFromBase64("Tm9uY2U=");
			Assert.Equal("Nonce", r);
		}

		[Fact]
		public void TestStatusMessages()
		{
			string text = "status1=message1" + Environment.NewLine +
				"status2=message2";
			MessagesInPairs agent = new MessagesInPairs();
			agent.LoadMessages(text);
			Assert.Equal("message2", agent.GetMessage("Status2"));
		}

		[Fact]
		public void TestStatusMessages2()
		{
			string text = "status1=message1" + "\n" +
				Environment.NewLine +
				"status2=message2" + "\n";
			MessagesInPairs agent = new MessagesInPairs();
			agent.LoadMessages(text);
			Assert.Equal("message2", agent.GetMessage("Status2"));
		}

		[Fact]
		public void TestDateFunctions()
		{
			DateTime today = DateTime.Parse("2010-03-26");
			Assert.Equal(DateFunctions.GetStartDateOfLastWeek(today),
				DateTime.Parse("2010-03-14"));
			Assert.Equal(DateFunctions.GetStartDateOfLast2Weeks(today),
				DateTime.Parse("2010-03-07"));
			Assert.Equal(DateFunctions.GetStartDateOfLastMonth(today),
				DateTime.Parse("2010-02-01"));
			Assert.Equal(DateFunctions.GetStartDateOfLast3Months(today),
				DateTime.Parse("2009-12-01"));
			Assert.Equal(DateFunctions.GetStartDateOfLast6Months(today),
				DateTime.Parse("2009-09-01"));


		}
	}
}
