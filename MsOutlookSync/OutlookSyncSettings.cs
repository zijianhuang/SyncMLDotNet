using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Configuration;
using Fonlow.SyncML.MultiSync;
using Fonlow.SyncML.Common;
using Fonlow.Configuration;

namespace Fonlow.SyncML.OutlookSync
{

    //public class OutlookSyncMLProgramSettingsProvider : UserSettingsFileProviderBase
    //{
    //    protected override string SectionName
    //    {
    //        get { return "Fonlow.SyncML.OutlookSync.OutlookSyncSettings"; }
    //    }

    //    protected override string UserConfigFilePath
    //    {
    //        get { return @"C:\Users\AndySuperCo\AppData\Roaming\Fonlow\SyncMLAddinForOutlook\user.config"; }
    //    }
    //}

    //[SettingsProvider(typeof(OutlookSyncMLProgramSettingsProvider))]
    public class OutlookSyncSettings : SyncSettingsBase
    {
        
        #region Singleton
        private static OutlookSyncSettings defaultInstance = ((OutlookSyncSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new OutlookSyncSettings())));

        public static OutlookSyncSettings Default
        {
            get
            {
                return defaultInstance;
            }
        }
        #endregion;

         OutlookSyncSettings()
        {

        }

        [UserScopedSettingAttribute()]
        [Category("Sync Content")]
        [DisplayName("Contacts")]
        public ContactsSyncItem ContactsSyncItem
        {
            get
            {
                return this["ContactsSyncItem"] as ContactsSyncItem;
            }
            set
            {
                this["ContactsSyncItem"] = value;
            }
        }

        [UserScopedSettingAttribute()]
        [Category("Sync Content")]
        [DisplayName("Calendar")]
        public CalendarSyncItem CalendarSyncItem
        {
            get
            {
                return this["CalendarSyncItem"] as CalendarSyncItem;
            }
            set
            {
                this["CalendarSyncItem"] = value;
            }
        }

        [UserScopedSettingAttribute()]
        [Category("Sync Content")]
        [DisplayName("Tasks")]
        public TasksSyncItem TasksSyncItem
        {
            get
            {
                return this["TasksSyncItem"] as TasksSyncItem;
            }
            set
            {
                this["TasksSyncItem"] = value;
            }
        }

        [UserScopedSettingAttribute()]
        [Category("Sync Content")]
        [DisplayName("Notes")]
        public NotesSyncItem NotesSyncItem
        {
            get
            {
                return this["NotesSyncItem"] as NotesSyncItem;
            }
            set
            {
                this["NotesSyncItem"] = value;
            }
        }

    }

}
