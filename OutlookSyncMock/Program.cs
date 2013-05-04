using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Fonlow.SyncML.Windows;
using Fonlow.SyncML.OutlookSync;
using Fonlow.SyncML.Common;

namespace OutlookSyncMock
{
    /// <summary>
    /// This exe mock is only for debugging. It might has conflicit with the Add-in installed
    /// because both will monitor Outlook's events, and write deletion log resulting in duplicated
    /// entries in the log.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AboutInfo aboutInfo;

            aboutInfo.Copyright = "Copyright ©  2008-2010  Fonlow IT";
            aboutInfo.ProductName = "SyncML Client for MS Outlook";
            aboutInfo.WebSite = @"http://www.fonlow.com/opencontacts/";
            aboutInfo.Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();


            SyncMLForm syncForm = new SyncMLForm(new OutlookDataSourceContacts(), aboutInfo);
            Application.Run(syncForm);
        }
    }
}
