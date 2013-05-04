using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Globalization;

namespace Fonlow.SyncML.Common
{
    /// <summary>
    /// Contain common constants
    /// </summary>
    public sealed class SyncConstants
    {
        private SyncConstants()
        {

        }

        public const int MaxCommandsPerBatch = 50;

        /// <summary>
        /// Used in iCalendar
        /// </summary>
        public const string ProdId = "-//Fonlow//SyncML Client API.NET";
    }


    public enum ContactExchangeType : int
    {
        Sifc, Vcard21, Unknown
    }

    /// <summary>
    /// Sync type.
    /// </summary>
    [TypeConverter(typeof(EnumDescriptionConverter))]
    public enum SyncType
    {
        [Description("Two-way")] //DisplayNameAttribute can not be used.
        TwoWay, //200
        [Description("Slow Sync")]
        Slow, //201
        [Description("One way sync from client")]
        OneWayFromClient, //202
        [Description("Refresh from client")]
        RefreshFromClient, //203
        [Description("One way sync from server")]
        OneWayFromServer, //204
        [Description("Refresh from server")]
        RefreshFromServer  //205      

    }

    [TypeConverter(typeof(EnumDescriptionConverter))]
    public enum CalendarPeriod
    {
        [Description("All")]
        All, 
        [Description("Future Only")]
        FutureOnly,
        [Description("Since Last Week")]
        SinceLastWeek,
        [Description("Since Last 2 Weeks")]
        SinceLast2Weeks,
        [Description("Since Last Month")]
        SinceLastMonth,
        [Description("Since Last 3 Months")]
        SinceLast3Months,
        [Description("Since Last 6 Months")]
        SinceLast6Months
    }

    /// <summary>
    /// Decorate an enum type to give string representation in datagrid, with
    /// descriptions on each member of the enum.
    /// </summary>
    public class EnumDescriptionConverter : EnumConverter
    {
        private Type enumType;

        public EnumDescriptionConverter(Type type)
            : base(type)
        {
            enumType = type;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context,
            Type destinationType)
        {
            return destinationType == typeof(string);
        }

        /// <summary>
        /// Convert to string
        /// </summary>
        public override object ConvertTo(ITypeDescriptorContext context,
            CultureInfo culture,
            object value, Type destinationType)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            FieldInfo fi = enumType.GetField(Enum.GetName(enumType, value));
            DescriptionAttribute dna =
                Attribute.GetCustomAttribute(
                fi, typeof(DescriptionAttribute)) as DescriptionAttribute;

            if (dna != null)
                return dna.Description;
            else
                return value.ToString();
        }
       
        public override bool CanConvertFrom(ITypeDescriptorContext context,
            Type sourceType)
        {
            return sourceType == typeof(string);
        }

        /// <summary>
        /// Convert from string
        /// </summary>
        public override object ConvertFrom(ITypeDescriptorContext context,
            CultureInfo culture,
            object value)
        {
            string text = value as string;
            if (text == null)
                return null;

            foreach (FieldInfo fi in enumType.GetFields())
            {
                DescriptionAttribute dna =
                Attribute.GetCustomAttribute(
                fi, typeof(DescriptionAttribute)) as DescriptionAttribute;

                if ((dna != null) && (text == dna.Description))
                    return Enum.Parse(enumType, fi.Name);
            }
            return Enum.Parse(enumType, text);
        }

        public override string ToString()
        {
            return ConvertTo(this, typeof(string)).ToString();
        }

    }

    /// <summary>
    /// Give SyncMLFacade a common use of exception.
    /// </summary>
    /// 
    [Serializable()]
    public class FacadeErrorException : Exception
    {
        public FacadeErrorException()
            : base()
        {

        }

        public FacadeErrorException(string message)
            : base(message)
        {

        }

        public FacadeErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected FacadeErrorException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }

    }

}
