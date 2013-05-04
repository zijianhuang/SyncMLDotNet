using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using Fonlow.SyncML.Common;

namespace Fonlow.SyncML.Common
{
    /// <summary>
    /// Server settints of sync. A derived class should have sync items defined.
    /// </summary>
    public class SyncSettingsBase : ApplicationSettingsBase
    {
        protected SyncSettingsBase()
        {
        }

        /// <summary>
        /// Put all properties of SyncItemType into a list for the sack of query. Items are sorted according OrderNo
        /// </summary>
        /// <returns>A list of items. If no one, the list is empty but not null.</returns>
        public IList<SyncItem> GetSyncItems()
        {
            Type typeOfSyncItem = typeof(SyncItem);
            List<SyncItem> items = new List<SyncItem>();
            foreach (SettingsPropertyValue propertyValue in PropertyValues)
            {
                if (typeOfSyncItem.IsAssignableFrom(propertyValue.Property.PropertyType))
                {
                    SyncItem syncItem = propertyValue.PropertyValue as SyncItem;
                    System.Diagnostics.Debug.Assert(syncItem != null, "What syncitem?");
                    items.Add(syncItem);
                }
            }
            //PropertyValues does not give syncItems as they appear in the Settings object
            //so I have to sort it with SyncItem.OrderNo.
            items.Sort(CompareSyncItemsByOrderNo);
            return items;
        }

        public IList<string> GetSyncItemNames()
        {
            Type typeOfSyncItem = typeof(SyncItem);
            List<string> items = new List<string>();
            foreach (SettingsProperty property in Properties)
            {
                if (typeOfSyncItem.IsAssignableFrom(property.PropertyType))
                {
                    items.Add(property.Name);
                }
            }
            items.Sort(CompareSyncItemNamesByOrderNo);
            return items;
        }

        private int CompareSyncItemsByOrderNo(SyncItem item1, SyncItem item2)
        {
            if (item1.OrderNo < item2.OrderNo)
                return -1;
            if (item1.OrderNo > item2.OrderNo)
                return 1;
            return 0;
        }

        private int CompareSyncItemNamesByOrderNo(string name1, string name2)
        {
            SyncItem item1 = this[name1] as SyncItem;
            SyncItem item2 = this[name2] as SyncItem;
            return CompareSyncItemsByOrderNo(item1, item2);
        }

        const string GeneralCategory = "Server";

        #region ApplicationScopedSettingsMayBe
        [ApplicationScopedSetting]
        [Browsable(false)]
        [DefaultSettingValue("Fonlow IT")]
        public string VendorName
        {
            get
            {
                return ((string)(this["VendorName"]));
            }
        }

        [ApplicationScopedSetting]
        [Browsable(false)]
        [DefaultSettingValue("999")]
        public string ModelVersion
        {
            get
            {
                return ((string)(this["ModelVersion"]));
            }
        }

        [ApplicationScopedSetting]
        [Browsable(false)]
        [DefaultSettingValue("Fonlow SyncML Client")]
        public string ModelName
        {
            get
            {
                return ((string)(this["ModelName"]));
            }
        }
        #endregion

        [UserScopedSettingAttribute()]
        [Category(GeneralCategory)]
        [DisplayName("Location")]
        [Description("Service provider's SyncML server location")]
        [DefaultSettingValue("http://localhost:8080/funambol/ds")]
        public string BasicUri
        {
            get
            {
                return ((string)(this["BasicUri"]));
            }
            set
            {
                this["BasicUri"] = value;
            }
        }

        [UserScopedSettingAttribute()]
        [Category(GeneralCategory)]
        [DisplayName("Username")]
        [DefaultSettingValue("test")]
        public string User
        {
            get
            {
                return ((string)(this["User"]));
            }
            set
            {
                this["User"] = value;
            }
        }

        [UserScopedSettingAttribute()]
        [Category(GeneralCategory)]
        [DisplayName("Password")]
        [PasswordPropertyText(true)]
        public string Password
        {
            get
            {
                return ((string)(this["Password"]));
            }
            set
            {
                //if (RememberPassword)
                    this["Password"] = value;
                //else
                //    this["Password"] = String.Empty;
            }
        }

        //[UserScopedSettingAttribute()]
        //[Category(GeneralCategory)]
        //[DisplayName("Remember Password")]
        //[DefaultSettingValue("False")]
        //public bool RememberPassword
        //{
        //    get
        //    {
        //        return (bool)this["RememberPassword"];
        //    }
        //    set
        //    {
        //        this["RememberPassword"] = value;
        //        if (!value)
        //        {
        //            Password = String.Empty;
        //        }
        //    }
        //}

        [UserScopedSettingAttribute()]
        [Category(GeneralCategory)]
        [DisplayName("Use proxy")]
        [DefaultSettingValue("false")]
        public bool UseProxy
        {
            get
            {
                return (bool)this["UseProxy"];
            }
            set
            {
                this["UseProxy"] = value;
            }
        }

        [UserScopedSettingAttribute()]
        [Category(GeneralCategory)]
        [DisplayName("Proxy location")]
        [DefaultSettingValue("")]
        public string Proxy
        {
            get
            {
                return ((string)(this["Proxy"]));
            }
            set
            {
                this["Proxy"] = value;
            }
        }

        [UserScopedSettingAttribute()]
        [Browsable(false)]
        [DefaultSettingValue("FonlowSyncMLClientGuid")]
        public string DeviceAddress
        {
            get
            {
                return ((string)(this["DeviceAddress"]));
            }
            set
            {
                this["DeviceAddress"] = value;
            }
        }

    }

  /*  /// <summary>
    /// Contain sync settings for multiple types of meta data.
    /// The application should initialize those non-browsable properties.
    /// </summary>
    public class MultiSyncSettings : SyncSettingsBase
    {
        #region Singleton
        public static MultiSyncSettings Default
        {
            get { return Nested.instance; }
        }

        class Nested
        {
            static Nested() { }
            internal static readonly MultiSyncSettings instance = new MultiSyncSettings();
        }
        #endregion;

        protected MultiSyncSettings()
        {
            //SyncItems = new SyncItems();
        }

        [UserScopedSettingAttribute()]
        [DisplayName("Items")]
        [Category("Sync")]
        public SyncItems SyncItems
        {
            get
            {
                SyncItems items = this["SyncItems"] as SyncItems;
                if (items == null)//When Reloading, property SyncItems may be assigned null if not in the persistent storage.
                {
                    SyncItems = new SyncItems();
                }
                return items;
            }
            set
            {
                if (value == null)
                {
                    SyncItems.Clear();
                }
                else
                {
                    this["SyncItems"] = value;
                }
            }
        }
    }*/

    public class ProgramSettings : ApplicationSettingsBase
    {
        private static ProgramSettings defaultInstance = ((ProgramSettings)(ApplicationSettingsBase.Synchronized(new ProgramSettings())));

        public static ProgramSettings Default
        {
            get
            {
                return defaultInstance;
            }
        }

        protected ProgramSettings()
        {

        }

        const string ProgramCategory = "Program";

        #region ApplicationScopedMayBe
        /// <summary>
        /// In theory these properties should be in ApplicationScoped, however, because the settings
        /// may be used in addin hosted by an Win32 program like Outlook, it is not convenient to
        /// install or change ApplicationScoped settings in such scenario. So, a workaround is to
        /// make all settings in UserScoped.
        /// </summary>
        [ApplicationScopedSetting]
        [Browsable(false)]
        [Category(ProgramCategory)]
        [DefaultSettingValue("Copyright ©  2008-2013  Fonlow IT")]
        public string Copyright
        {
            get
            {
                return ((string)(this["Copyright"]));
            }
        }

        [ApplicationScopedSetting]
        [Browsable(false)]
        [Category(ProgramCategory)]
        [DefaultSettingValue("SyncML Client for XXX")]
        public string ProductName
        {
            get
            {
                return ((string)(this["ProductName"]));
            }
        }

        [ApplicationScopedSetting]
        [Browsable(false)]
        [Category(ProgramCategory)]
        [DefaultSettingValue(@"http://www.fonlow.com/nowhere")]
        public string WebSite
        {
            get
            {
                return ((string)(this["WebSite"]));
            }
            set
            {
                this["WebSite"] = value;
            }
        }

        [ApplicationScopedSetting]
        [Browsable(false)]
        [Category(ProgramCategory)]
        [DefaultSettingValue(@"http://www.fonlow.com/nowhere2")]
        public string HelpUrl
        {
            get
            {
                return ((string)(this["HelpUrl"]));
            }

        }

        /// <summary>
        /// If not set, the About box of the UI will use version numbers of respective assemblies.
        /// </summary>
        [ApplicationScopedSetting]
        [Browsable(false)]
        [Category(ProgramCategory)]
        public string Version
        {
            get
            {
                return ((string)(this["Version"]));
            }
        }
        #endregion

        [UserScopedSetting()]
        [DefaultSettingValue("10, 10")]
        public Point Location
        {
            get { return (Point)this["Location"]; }
            set { this["Location"] = value; }
        }

        [UserScopedSetting()]
        public string ScreenDeviceName
        {
            get { return (string)this["ScreenDeviceName"]; }
            set { this["ScreenDeviceName"] = value; }
        }

        [UserScopedSetting()]
        public string LogScreenDeviceName
        {
            get { return (string)this["LogScreenDeviceName"]; }
            set { this["LogScreenDeviceName"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("20, 20")]
        public Point LogWinLocation
        {
            get { return (Point)this["LogWinLocation"]; }
            set { this["LogWinLocation"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("300, 380")]
        public Size LogWinSize
        {
            get { return (Size)this["LogWinSize"]; }
            set { this["LogWinSize"] = value; }
        }



    }

}
