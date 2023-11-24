using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TestOutlookSync
{
    [DeploymentItem(@"..\..\..\..\TestCase\Outlook", @"Outlook")]
    public class OutlookItemsTestBase
    {
        protected OutlookItemsTestBase()
        {
            MockPath = "Outlook\\";
        }

        protected string MockPath { get; set; }


    }
}
