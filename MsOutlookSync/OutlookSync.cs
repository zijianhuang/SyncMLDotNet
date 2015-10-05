using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Outlook;
using Fonlow.SyncML.Common;
using Fonlow.SyncML.MultiSync;
using System.Reflection;

namespace Fonlow.SyncML.OutlookSync
{
    /// <summary>
    /// Aggregate syncml core and the components for Outlok.
    /// </summary>
    public class MSOutlookSync : IDisposable
    {
        public MSOutlookSync()
        {
        }

        void syncForm_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            syncForm = null;
        }

        System.Windows.Forms.Form syncForm;
        
        void LoadSyncForm()
        {
            if (syncForm == null)
            {
                syncForm = UcMultiSyncMain.CreateMainForm(OutlookSyncSettings.Default);
                syncForm.FormClosed += new System.Windows.Forms.FormClosedEventHandler(syncForm_FormClosed);
            }

        }

        public void Show()
        {
            LoadSyncForm();
            syncForm.Show();
            syncForm.BringToFront();
        }

        public void ShowModal(System.Windows.Forms.IWin32Window owner)
        {
            LoadSyncForm();
            syncForm.ShowDialog(owner);
        }

        public System.Windows.Forms.Form SyncForm { get { return syncForm; } }

        bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {

                    if (syncForm != null)
                        syncForm.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }

    /// <summary>
    /// Load multiple deletion monitors for outlook items accounding to
    /// OutlookSyncSettings.
    /// </summary>
    public static class DeletionMonitorsLoader
    {
        public static void RegisterDeletionLogs(Application app)
        {
           IList<string> syncItemNames = OutlookSyncSettings.Default.GetSyncItemNames();
            foreach (string itemName in syncItemNames)
            {
                SyncItem item = OutlookSyncSettings.Default[itemName] as SyncItem;
                System.Diagnostics.Debug.Assert(item != null, "Why not SyncItem?"+syncItemNames.Count.ToString());

                if (item.Enabled)
                {
                    object monitor = LoadDeletionMonitor(item.LocalDataSourceAssembly, app); //a monitor is hooked with a Outlook folder through event handling, the monitor will be kept all the time.
                    if (monitor == null)
                        System.Diagnostics.Trace.TraceInformation("Deletion monitor can not be created.");
                    else
                        System.Diagnostics.Trace.TraceInformation("Deletion monitor created.");
                }
            }
        }

        private static object LoadDeletionMonitor(string localAssembly, Application app)
        {
            Type monitorType;
            monitorType = FindDeletionMonitorInAssembly(localAssembly);

            if (monitorType == null)
            {
                return null;
            }

            try
            {
                object handle = Activator.CreateInstance(monitorType, new object[] { app });
                System.Diagnostics.Trace.TraceInformation("Deletion minitor is created.");
                return handle;
            }
            catch (TargetInvocationException)
            {
                return null;
            }
        }

        static readonly Type typeofDeletionMonitorAttribute = typeof(DeletionMonitorAttribute);

        private static Type FindDeletionMonitorInAssembly(string localAssembly)
        {
            Assembly assembly = Assembly.Load(localAssembly);
            if (assembly == null)
            {
                return null;
            }

            foreach (Type t in assembly.GetTypes())
            {
                if (t.IsClass && (t.GetCustomAttributes(typeofDeletionMonitorAttribute, true).Count() > 0))
                {
                    return t;
                }
            }
            return null;
        }
       
    }
}
