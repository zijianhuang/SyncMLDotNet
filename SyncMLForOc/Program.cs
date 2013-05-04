using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
//using Fonlow.SyncML.Windows;
using Fonlow.SyncML.MultiSync;
using Fonlow.SyncML.Common;
using System.Reflection;
using System.Configuration;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;

namespace Fonlow.SyncML.OpenContacts.App
{
    static class Program
    {
        /// <summary>
        /// MultiSyncUI, but with OC sync only.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException); 
            Mutex mutex = null;
            AppDomain.CurrentDomain.ProcessExit += new EventHandler((sender, e) => { if (mutex != null) mutex.Dispose(); });
            bool createdNew = true;
            mutex = new Mutex(true, "Fonlow.SyncMLClientOpenContacts", out createdNew);

            if (!createdNew)
            {
                MessageBox.Show("Another instance of SyncML Client for Open Contacts is running.", "SyncML Client for Open Contacts");
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            OcSyncSettings settings = OcSyncSettings.Default;
            Application.Run(UcMultiSyncMain.CreateMainForm(settings));

        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            //Optionally to release resource 
            //Windows still report e.ToString() by default to Windows Problem Reporter. 
            Trace.TraceWarning("Some problem, unhandled exception: " + args.ExceptionObject.ToString());
        }

    }



    public class OcSyncSettings : SyncSettingsBase
    {
        #region Singleton
        private static OcSyncSettings defaultInstance = ((OcSyncSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new OcSyncSettings())));

        public static OcSyncSettings Default
        {
            get
            {
                return defaultInstance;
            }
        }
        #endregion;

        OcSyncSettings()
        {

        }

        [UserScopedSettingAttribute()]
        [Category("Sync Content")]
        [DisplayName("Contacts")]
        public ContactsSyncItem ContactsSyncItem
        {
            get
            {
                return ((ContactsSyncItem)(this["ContactsSyncItem"]));
            }
            set
            {
                this["ContactsSyncItem"] = value;
            }
        }
    }
}
