using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System;

namespace Fonlow.Configuration
{
    public abstract class UserSettingsFileProviderBase : SettingsProvider
    {
        protected UserSettingsFileProviderBase()
            : base()
        {
        }

        ClientSettingsSection GetConfigurationSection()
        {
            //string targetDir = (string)Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Office\Outlook\Addins\FonlowSyncML.Addin", "TargetDir", String.Empty);
            //if (String.IsNullOrEmpty(targetDir))
            //{
            //    Trace.TraceWarning("Cannot locate TargetDir in key FonlowSyncML.Addin, and the plugin will might not be loaded properly.");
            //    return null;
            //}
            ConfigurationFileMap fileMap = new ConfigurationFileMap(UserConfigFilePath);
            System.Configuration.Configuration config = ConfigurationManager.OpenMappedMachineConfiguration(fileMap);

            UserSettingsGroup userSectionGroup = config.SectionGroups["userSettings"] as UserSettingsGroup;
            if (userSectionGroup == null)
            {
                Trace.TraceWarning("No userSettings found in {0}.", UserConfigFilePath);
                return null;
            }
            return userSectionGroup.Sections[SectionName] as ClientSettingsSection;
        }

        ClientSettingsSection section;

        protected abstract string SectionName { get; }

        protected abstract string UserConfigFilePath { get; }

        public override string ApplicationName
        {
            get
            {
                return "Addin";
            }
            set
            {

            }
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(this.ApplicationName, config);
            section = GetConfigurationSection();
        }

        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            SettingsPropertyValueCollection values = new SettingsPropertyValueCollection();
            // Iterate through the settings to be retrieved
            foreach (SettingsProperty setting in collection)
            {
                SettingsPropertyValue value = new SettingsPropertyValue(setting);
                value.IsDirty = false;
                SettingElement element = section.Settings.Get(setting.Name);
                Trace.TraceInformation(string.Format("Retrieve for {0}.", setting.Name));
                if (element != null)
                {
                    value.PropertyValue = element.Value;
                    values.Add(value);
                }
            }

            return values;
        }

        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            foreach (SettingsPropertyValue propertyValue in collection)
            {
                if (propertyValue.IsDirty)
                {
                    section.ElementInformation.Properties[propertyValue.Name].Value = propertyValue.PropertyValue;
                }
            }
        }


    }

}
