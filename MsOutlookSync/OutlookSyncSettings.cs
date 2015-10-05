using System.ComponentModel;
using System.Configuration;
using Fonlow.SyncML.Common;

namespace Fonlow.SyncML.OutlookSync
{
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
