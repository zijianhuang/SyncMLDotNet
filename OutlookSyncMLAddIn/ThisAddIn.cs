using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.Windows.Forms;
using System.Drawing;
using Fonlow.SyncML.Windows;
using Fonlow.SyncML.MultiSync;
using System.Diagnostics;


namespace Fonlow.SyncML.OutlookSync
{
    /// <summary>
    /// The entry point of the Outlook Addin.
    /// The addin has a Log button in DEBUG mode.
    /// References:
    /// about addin registry: http://msdn.microsoft.com/en-us/library/vstudio/bb386106.aspx
    /// about registry redirector: http://msdn.microsoft.com/en-us/library/windows/desktop/aa384232%28v=vs.85%29.aspx
    /// </summary>
    /// 
    public partial class FonlowOutlookAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {

            syncAgent = new MSOutlookSync();

            AddSyncBar();

            DeletionMonitorsLoader.RegisterDeletionLogs(Application);
        }

        MSOutlookSync syncAgent;

        Office.CommandBar syncBar;
        Office.CommandBarButton syncButton;
        Office.CommandBarButton optionsButton;

        const string syncBarName = "Fonlow SyncML";

        /// <summary>
        /// Add SyncBar if it is not there.
        /// When creating a command bar or a button, Outlook just has the meta data of the GUI updated. So even after the assembly of the addin
        /// is removed, the command bar along with the buttons are still there.
        /// http://www.add-in-express.com/docs/net-commandbar-tips.php#visibility-rules
        /// </summary>
        void AddSyncBar()
        {
            Outlook.Explorer explorer = Application.ActiveExplorer();

            IEnumerable<Office.CommandBar> bars = explorer.CommandBars.OfType<Office.CommandBar>().Where(item =>
                item.Name == syncBarName);
            syncBar = bars.FirstOrDefault();
            if (syncBar == null)
            {
                syncBar = explorer.CommandBars.Add(syncBarName, Office.MsoBarPosition.msoBarTop, false, false);
            }

#if DEBUG
            ClearButtons();
#endif
            syncButton = GetButton("Fonlow.SyncML.Sync", "Sync", "Display the sync center", OutlookSyncMLAddIn.Properties.Resources.sync16);
            syncButton.Click += new Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler(syncButton_Click);

            optionsButton = GetButton("Fonlow.SyncML.Options", "Options", "Display the Options of sync", OutlookSyncMLAddIn.Properties.Resources.Settings16);
            optionsButton.Click += new Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler(optionsButton_Click);

            syncBar.Visible = true;

#if DEBUG
            logButton = GetButton("Fonlow.SyncML.Log", "Log", "Log window", null);
            logButton.Click += new Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler(logButton_Click);
#endif
        }

#if DEBUG
        void logButton_Click(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            DisplayLogWindow();
        }
#endif

        void optionsButton_Click(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            UcOptions.ShowModal(OutlookSyncSettings.Default);
        }

        /// <summary>
        /// Get button or create one.
        /// </summary>
        Office.CommandBarButton GetButton(string buttonTag, string caption, string hint, Image image)
        {
            IEnumerable<Office.CommandBarButton> buttons = syncBar.Controls.OfType<Office.CommandBarButton>().Where(item =>
                item.Tag == buttonTag);

            Office.CommandBarButton btn = buttons.FirstOrDefault();
            if (btn == null)
            {
                btn = syncBar.Controls.Add(Office.MsoControlType.msoControlButton, missing, missing, missing, missing) as Office.CommandBarButton;
                btn.Style = Office.MsoButtonStyle.msoButtonIconAndCaption;
                btn.Tag = buttonTag;
                btn.Caption = caption;
                btn.TooltipText = hint;
                if (image != null)
                    btn.Picture = ConvertImage.Convert(image);

            }

            return btn;
        }

        void syncButton_Click(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            Sync();
        }

        void Sync()
        {
            syncAgent.Show();
        }

#if DEBUG
        /// <summary>
        /// This function is for hourse keeping during development, in order to clear the meta data of the GUI of Outlook.
        /// </summary>
        void ClearButtons()
        {
            IEnumerable<Office.CommandBarButton> buttons = syncBar.Controls.OfType<Office.CommandBarButton>();
            foreach (Office.CommandBarButton btn in buttons)
            {
                btn.Delete(false);
            }
        }
#endif

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            syncAgent.Dispose();
        }

        #region VSTO generated code


        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion

#if DEBUG
        Office.CommandBarButton logButton;

        private FormLog logUC;
        private MemoTraceListener logMemoListerner;
        private void DisplayLogWindow()
        {
            if (logUC == null)
            {
                logUC = new FormLog();
                //  logUC.DisposeEvent += HandleLogWindowClose;
                //To show trace on screen. Might need a switch later.
                logMemoListerner = new MemoTraceListener(logUC.Box);
                Trace.Listeners.Add(logMemoListerner);
                System.Drawing.Size size = new Size(300, 300);

                logUC.Size = size;
                logUC.Show();
            }
        }
#endif
        /*private void HandleLogWindowClose(object sender,  LocationSizeChangedEventArgs e)
        {
            logUC = null;
            Trace.Listeners.Remove(logMemoListerner);
            logMemoListerner = null;
        }*/

    }

    sealed internal class ConvertImage : System.Windows.Forms.AxHost
    {
        private ConvertImage()
            : base(null)
        {
        }
        public static stdole.IPictureDisp Convert
            (System.Drawing.Image image)
        {
            return (stdole.IPictureDisp)System.
                Windows.Forms.AxHost
                .GetIPictureDispFromPicture(image);
        }
    }

}
