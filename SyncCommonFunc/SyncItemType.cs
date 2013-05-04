using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Diagnostics;

namespace Fonlow.SyncML.Common
{
    [TypeConverter(typeof(SyncItemTypeExpandableConverter))]
    public class SyncItem
    {
        [DisplayName("Content type")]
        [ReadOnly(true)]//not to be changed in PropertyGrid
        public string DisplayName { get; set; }

        /// <summary>
        /// An internal identifier to tell client codes what type of data to sync.
        /// It may be Contacts, Calendar, Note etc., up to the application to interpret.
        /// </summary>
        [Browsable(false)]
        public string ItemType {get;set;}

        /// <summary>
        /// Define the qualified name of the assembly with ILocalDataSource implementation.
        /// </summary>
        [Browsable(false)]
        public string LocalDataSourceAssembly { get; set; }

        [DisplayName("Remote name")]
        public string RemoteName { get; set; }

        /// <summary>
        /// For the same itemType, there might be more than one data format to represent in meta data.
        /// A derived class may override this property in order to provider a StandardValuesConverter which
        /// makes this property to be represented in datagrid as a dropdown list.
        /// </summary>
        [DisplayName("Data format")]
         public virtual string DataFormat { get; set; }

        [DisplayName("Sync Direction")]
        public SyncType SyncDirection { get; set; }

        [Browsable(false)]
        public string LastAnchor { get; set; }

        [DisplayName("Enabled")]
        public bool Enabled { get; set; }

        [ReadOnly(true)]
        [DisplayName("Last Anchor Time")]
        public DateTime LastAnchorTime { get; set; }

        /// <summary>
        /// Client codes may use this to sort items for visual presentation.
        /// </summary>
        [ReadOnly(true)]
        [Browsable(false)]
        public int OrderNo { get; set; }

        /// <summary>
        /// Some sync item type might have some sort of filter defined.
        /// It is up the the client codes to interpret what the object is.
        /// </summary>
        /// <returns>Null by default meaning there's no filter.</returns>
        public virtual object GetFilter()
        {
            return null;
        }
    }

    public class ContactsSyncItem : SyncItem
    {
        [TypeConverter(typeof(ContactsDataFormatConverter))]
        public override string DataFormat
        {
            get
            {
                return base.DataFormat;
            }
            set
            {
                base.DataFormat = value;
            }
        }
    }

    public class NotesSyncItem : SyncItem
    {
        [TypeConverter(typeof(NotesDataFormatConverter))]
        public override string DataFormat
        {
            get
            {
                return base.DataFormat;
            }
            set
            {
                base.DataFormat = value;
            }
        }
    }

    public class CalendarSyncItem : SyncItem
    {
        [TypeConverter(typeof(CalendarDataFormatConverter))]
        public override string DataFormat
        {
            get
            {
                return base.DataFormat;
            }
            set
            {
                base.DataFormat = value;
            }
        }


        public CalendarPeriod Filter { get; set; }

        public override object GetFilter()
        {
            return Filter;
        }
    }

    public class TasksSyncItem : SyncItem
    {
        [TypeConverter(typeof(CalendarDataFormatConverter))]
        public override string DataFormat
        {
            get
            {
                return base.DataFormat;
            }
            set
            {
                base.DataFormat = value;
            }
        }

    }

    internal class SyncItemTypeExpandableConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(
            ITypeDescriptorContext context,
            CultureInfo culture,
            object value,
            Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value == null)
                {
                    Trace.TraceWarning("itemValue is null. Bad.");
                    return "??";
                }

                SyncItem itemType = value as SyncItem;
                System.Diagnostics.Debug.Assert(itemType != null, "itemType null");
                return itemType.Enabled ? "Enabled" : "Disabled";
            }

            return base.ConvertTo(
                context,
                culture,
                value,
                destinationType);
        }
    }

    /// <summary>
    /// When decorating a property of a string type with a derived class of this class,
    /// in DataGrid, the property will have a dropdown list with standard values.
    /// A derived class should have a constructor without parameters, and initialize
    /// values through the constructor of the base.
    /// </summary>
    public class StandardValuesConverter : StringConverter
    {
        protected StandardValuesConverter(string csvValues)
        {
            if (String.IsNullOrEmpty(csvValues))
            {
                values=new string[]{};
            }
            else
            {
                values = csvValues.Split(',');
            }
        }

        string[] values;

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        /// <summary>
        /// Make sure in propertyGrid only items in the drop list can be used.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(values);
        }
    }

    public class ContactsDataFormatConverter : StandardValuesConverter
    {
        public ContactsDataFormatConverter()
            : base("SIF,vCard")
        {

        }
    }

    public class CalendarDataFormatConverter : StandardValuesConverter
    {
        public CalendarDataFormatConverter()
            : base("SIF,vCalendar")
        {

        }
    }

    public class NotesDataFormatConverter : StandardValuesConverter
    {
        public NotesDataFormatConverter()
            : base("SIF")
        {

        }
    }
}
