using System;
using System.Xml.Linq;
using Fonlow.SyncML.Common;

namespace DemoContact
{
    /// <summary>
    /// Conversion between a contact item and Sif-C meta.
    /// </summary>
    public class ContactWithSifC
    {

        /// <summary>
        /// Read a contact to a Sifc Xml element.
        /// </summary>
        public string ReadItemToText(Contact item)
        {
            XElement contactElement = new XElement("contact",
                new XElement("SIFVersion", "1.0"),
                     DataUtility.CreateElement("FileAs", item.Surname + "," + item.GivenName),
                DataUtility.CreateElement("FirstName", item.GivenName),
                       DataUtility.CreateElement("MiddleName", item.MiddleName),
                    DataUtility.CreateElement("LastName", item.Surname),
                DataUtility.CreateElementDate("Birthday", item.DateOfBirth),

                DataUtility.CreateElement("Email1Address", item.Email),
                DataUtility.CreateElement("HomeAddressCity", item.City),
                DataUtility.CreateElement("HomeAddressCountry", item.Country),
                DataUtility.CreateElement("HomeAddressPostalCode", item.PostalCode),
                DataUtility.CreateElement("HomeAddressState", item.State),
                DataUtility.CreateElement("HomeAddressStreet", item.Street),
                DataUtility.CreateElement("HomeTelephoneNumber", item.PhoneNumber),
                DataUtility.CreateElement("PrimaryTelephoneNumber", item.PhoneNumber)

                );
            System.Diagnostics.Debug.WriteLine("contactElement: " + contactElement.ToString());
            return contactElement.ToString(SaveOptions.DisableFormatting);
        }

        protected void WriteMetaToItem(string meta, Contact item)
        {
            XElement element = XElement.Parse(meta);

            item.DateOfBirth = DataUtility.ParseDateTime(DataUtility.GetXElementValueSafely(element, "Birthday"));
            item.Email = DataUtility.GetXElementValueSafely(element, "Email1Address");
            item.GivenName = DataUtility.GetXElementValueSafely(element, "FirstName");

            item.City = DataUtility.GetXElementValueSafely(element, "HomeAddressCity");
            item.Country = DataUtility.GetXElementValueSafely(element, "HomeAddressCountry");
            item.PostalCode = DataUtility.GetXElementValueSafely(element, "HomeAddressPostalCode");
            item.State = DataUtility.GetXElementValueSafely(element, "HomeAddressState");
            item.Street = DataUtility.GetXElementValueSafely(element, "HomeAddressStreet");
            var homePhone = DataUtility.GetXElementValueSafely(element, "HomeTelephoneNumber");
            var primaryPhone = DataUtility.GetXElementValueSafely(element, "PrimaryTelephoneNumber");
            item.PhoneNumber = String.IsNullOrEmpty(primaryPhone) ? homePhone : primaryPhone;
            item.Surname = DataUtility.GetXElementValueSafely(element, "LastName");
            item.MiddleName = DataUtility.GetXElementValueSafely(element, "MiddleName");

        }

    }
}
