using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.Office.Interop.Outlook;
using Fonlow.VCard;

namespace Fonlow.SyncML.OutlookSync
{
    /// <summary>
    /// Conversion between a contact item and Sif-C meta.
    /// </summary>
    public class OutlookContactsWithSifC : OutlookItemsWithSyncContent<ContactItem>
    {
        public OutlookContactsWithSifC(Application app)
            :base(app.GetNamespace("MAPI").GetDefaultFolder(OlDefaultFolders.olFolderContacts) as Folder)
        {
        }

        /// <summary>
        /// Read a contact to a Sifc Xml element.
        /// </summary>
        public override string ReadItemToText(ContactItem item)
        {
            XElement contactElement = new XElement("contact",
                new XElement("SIFVersion", "1.0"),
                     DataUtility.CreateElement("FileAs", item.FileAs),
                DataUtility.CreateElement("FirstName", item.FirstName),
                       DataUtility.CreateElement("MiddleName", item.MiddleName),
                    DataUtility.CreateElement("LastName", item.LastName),
           DataUtility.CreateElementDate("Anniversary", item.Anniversary),
                DataUtility.CreateElement("AssistantName", item.AssistantName),
                DataUtility.CreateElement("AssistantTelephoneNumber", item.AssistantTelephoneNumber),
                DataUtility.CreateElementDate("Birthday", item.Birthday),
                DataUtility.CreateElement("Body", item.Body),
                DataUtility.CreateElement("Business2TelephoneNumber", item.Business2TelephoneNumber),
                DataUtility.CreateElement("BusinessLabel", item.BusinessAddress),

                DataUtility.CreateElement("BusinessAddressCity", item.BusinessAddressCity),
                DataUtility.CreateElement("BusinessAddressCountry", item.BusinessAddressCountry),
                DataUtility.CreateElement("BusinessAddressPostalCode", item.BusinessAddressPostalCode),
                DataUtility.CreateElement("BusinessAddressPostOfficeBox", item.BusinessAddressPostOfficeBox),
                DataUtility.CreateElement("BusinessAddressState", item.BusinessAddressState),
                DataUtility.CreateElement("BusinessAddressStreet", item.BusinessAddressStreet),
                DataUtility.CreateElement("BusinessFaxNumber", item.BusinessFaxNumber),
                DataUtility.CreateElement("BusinessWebPage", item.BusinessHomePage),
                DataUtility.CreateElement("BusinessTelephoneNumber", item.BusinessTelephoneNumber),
                DataUtility.CreateElement("CallbackTelephoneNumber", item.CallbackTelephoneNumber),
                DataUtility.CreateElement("CarTelephoneNumber", item.CarTelephoneNumber),
                DataUtility.CreateElement("Categories", item.Categories),
                DataUtility.CreateElement("Children", item.Children),
                DataUtility.CreateElement("Companies", item.Companies),
                DataUtility.CreateElement("CompanyMainTelephoneNumber", item.CompanyMainTelephoneNumber),
                DataUtility.CreateElement("CompanyName", item.CompanyName),
                DataUtility.CreateElement("ComputerNetworkName", item.ComputerNetworkName),
                DataUtility.CreateElement("Department", item.Department),
                DataUtility.CreateElement("Email1Address", item.Email1Address),
                DataUtility.CreateElement("Email1AddressType", item.Email1AddressType),
                DataUtility.CreateElement("Email2Address", item.Email2Address),
                DataUtility.CreateElement("Email2AddressType", item.Email2AddressType),
                DataUtility.CreateElement("Email3Address", item.Email3Address),
                DataUtility.CreateElement("Email3AddressType", item.Email3AddressType),
     DataUtility.CreateElement("Gender", item.Gender.ToString()),
                DataUtility.CreateElement("Hobby", item.Hobby),
                DataUtility.CreateElement("Home2TelephoneNumber", item.Home2TelephoneNumber),
                DataUtility.CreateElement("HomeLabel", item.HomeAddress),
                DataUtility.CreateElement("HomeAddressCity", item.HomeAddressCity),
                DataUtility.CreateElement("HomeAddressCountry", item.HomeAddressCountry),
                DataUtility.CreateElement("HomeAddressPostalCode", item.HomeAddressPostalCode),
                DataUtility.CreateElement("HomeAddressPostOfficeBox", item.HomeAddressPostOfficeBox),
                DataUtility.CreateElement("HomeAddressState", item.HomeAddressState),
                DataUtility.CreateElement("HomeAddressStreet", item.HomeAddressStreet),
                DataUtility.CreateElement("HomeFaxNumber", item.HomeFaxNumber),
                DataUtility.CreateElement("HomeTelephoneNumber", item.HomeTelephoneNumber),
                //   DataUtility.CreateElement("Importance", item.Importance.ToString())
                //Remark: Funambol does not support Importance those in Sync4j document Importance is included in Sif-C.
                DataUtility.CreateElement("Initials", item.Initials),
                DataUtility.CreateElement("JobTitle", item.JobTitle),
                DataUtility.CreateElement("ManagerName", item.ManagerName),
                DataUtility.CreateElement("Mileage", item.Mileage),
                DataUtility.CreateElement("MobileTelephoneNumber", item.MobileTelephoneNumber),
                DataUtility.CreateElement("NickName", item.NickName),
                DataUtility.CreateElement("OfficeLocation", item.OfficeLocation),
                DataUtility.CreateElement("OrganizationalIDNumber", item.OrganizationalIDNumber),
                DataUtility.CreateElement("OtherLabel", item.OtherAddress),
                DataUtility.CreateElement("OtherAddressCity", item.OtherAddressCity),
                DataUtility.CreateElement("OtherAddressCountry", item.OtherAddressCountry),
                DataUtility.CreateElement("OtherAddressPostalCode", item.OtherAddressPostalCode),
                DataUtility.CreateElement("OtherAddressPostOfficeBox", item.OtherAddressPostOfficeBox),
                DataUtility.CreateElement("OtherAddressState", item.OtherAddressState),
                DataUtility.CreateElement("OtherAddressStreet", item.OtherAddressStreet),
                DataUtility.CreateElement("OtherFaxNumber", item.OtherFaxNumber),
                DataUtility.CreateElement("OtherTelephoneNumber", item.OtherTelephoneNumber),
               DataUtility.CreateElement("PagerNumber", item.PagerNumber),
                DataUtility.CreateElement("HomeWebPage", item.PersonalHomePage),
                DataUtility.CreateElement("PrimaryTelephoneNumber", item.PrimaryTelephoneNumber),
                DataUtility.CreateElement("Profession", item.Profession),
                DataUtility.CreateElement("RadioTelephoneNumber", item.RadioTelephoneNumber),
                //Sensitivity later maybe
                DataUtility.CreateElement("Spouse", item.Spouse),
                DataUtility.CreateElement("Suffix", item.Suffix),
                DataUtility.CreateElement("Title", item.Title),
                DataUtility.CreateElement("WebPage", item.WebPage),
                DataUtility.CreateElement("YomiCompanyName", item.YomiCompanyName),
                DataUtility.CreateElement("YomiFirstName", item.YomiFirstName),
                DataUtility.CreateElement("YomiLastName", item.YomiLastName)

                );
            System.Diagnostics.Debug.WriteLine("contactElement: " + contactElement.ToString());
            return contactElement.ToString(SaveOptions.DisableFormatting);
        }

        protected override void WriteMetaToItem(string meta, ContactItem item)
        {
            XElement element = XElement.Parse(meta);

            item.Anniversary = DataUtility.ParseDateTime(DataUtility.GetXElementValueSafely(element, "Anniversary"));
            item.AssistantName = DataUtility.GetXElementValueSafely(element, "AssistantName");
            item.AssistantTelephoneNumber = DataUtility.GetXElementValueSafely(element, "AssistantTelephoneNumber");
            item.Birthday = DataUtility.ParseDateTime(DataUtility.GetXElementValueSafely(element, "Birthday"));
            item.Body = DataUtility.GetXElementValueSafely(element, "Body");
            item.Business2TelephoneNumber = DataUtility.GetXElementValueSafely(element, "Business2TelephoneNumber");
            item.BusinessAddress = DataUtility.GetXElementValueSafely(element, "BusinessAddress");
            item.BusinessAddressCity = DataUtility.GetXElementValueSafely(element, "BusinessAddressCity");
            item.BusinessAddressCountry = DataUtility.GetXElementValueSafely(element, "BusinessAddressCountry");
            item.BusinessAddressPostalCode = DataUtility.GetXElementValueSafely(element, "BusinessAddressPostalCode");
            item.BusinessAddressPostOfficeBox = DataUtility.GetXElementValueSafely(element, "BusinessAddressPostOfficeBox");
            item.BusinessAddressState = DataUtility.GetXElementValueSafely(element, "BusinessAddressState");
            item.BusinessAddressStreet = DataUtility.GetXElementValueSafely(element, "BusinessAddressStreet");
            item.BusinessFaxNumber = DataUtility.GetXElementValueSafely(element, "BusinessFaxNumber");
            item.BusinessHomePage = DataUtility.GetXElementValueSafely(element, "BusinessHomePage");
            item.BusinessTelephoneNumber = DataUtility.GetXElementValueSafely(element, "BusinessTelephoneNumber");
            item.CallbackTelephoneNumber = DataUtility.GetXElementValueSafely(element, "CallbackTelephoneNumber");
            item.CarTelephoneNumber = DataUtility.GetXElementValueSafely(element, "CarTelephoneNumber");
            item.Categories = DataUtility.GetXElementValueSafely(element, "Categories");
            item.Children = DataUtility.GetXElementValueSafely(element, "Children");
            item.Companies = DataUtility.GetXElementValueSafely(element, "Companies");
            item.CompanyMainTelephoneNumber = DataUtility.GetXElementValueSafely(element, "CompanyMainTelephoneNumber");
            item.CompanyName = DataUtility.GetXElementValueSafely(element, "CompanyName");
            item.ComputerNetworkName = DataUtility.GetXElementValueSafely(element, "ComputerNetworkName");
            item.Department = DataUtility.GetXElementValueSafely(element, "Department");
            item.Email1Address = DataUtility.GetXElementValueSafely(element, "Email1Address");
            item.Email1AddressType = DataUtility.GetXElementValueSafely(element, "Email1AddressType");
            item.Email2Address = DataUtility.GetXElementValueSafely(element, "Email2Address");
            item.Email2AddressType = DataUtility.GetXElementValueSafely(element, "Email2AddressType");
            item.Email3Address = DataUtility.GetXElementValueSafely(element, "Email3Address");
            item.Email3AddressType = DataUtility.GetXElementValueSafely(element, "Email3AddressType");
            item.FileAs = DataUtility.GetXElementValueSafely(element, "FileAs");
            item.FirstName = DataUtility.GetXElementValueSafely(element, "FirstName");
            item.FTPSite = DataUtility.GetXElementValueSafely(element, "FTPSite");
            //     item.FullName = DataUtility.GetXElementValueSafely(x, "FullName");
            try
            {
                string genderText = DataUtility.GetXElementValueSafely(element, "Gender");
                if (String.IsNullOrEmpty(genderText))
                {
                    item.Gender = OlGender.olUnspecified;
                }
                else
                {
                    item.Gender = (OlGender)Enum.Parse(typeof(OlGender), genderText, true);
                }
            }
            catch (ArgumentException e)
            {
                System.Diagnostics.Trace.TraceInformation(e.Message + String.Format(" when converting Gender for {0}", item.FileAs));
            }

            item.GovernmentIDNumber = DataUtility.GetXElementValueSafely(element, "GovernmentIDNumber");
            item.Hobby = DataUtility.GetXElementValueSafely(element, "Hobby");
            item.Home2TelephoneNumber = DataUtility.GetXElementValueSafely(element, "Home2TelephoneNumber");
            item.HomeAddress = DataUtility.GetXElementValueSafely(element, "HomeAddress");
            item.HomeAddressCity = DataUtility.GetXElementValueSafely(element, "HomeAddressCity");
            item.HomeAddressCountry = DataUtility.GetXElementValueSafely(element, "HomeAddressCountry");
            item.HomeAddressPostalCode = DataUtility.GetXElementValueSafely(element, "HomeAddressPostalCode");
            item.HomeAddressPostOfficeBox = DataUtility.GetXElementValueSafely(element, "HomeAddressPostOfficeBox");
            item.HomeAddressState = DataUtility.GetXElementValueSafely(element, "HomeAddressState");
            item.HomeAddressStreet = DataUtility.GetXElementValueSafely(element, "HomeAddressStreet");
            item.HomeFaxNumber = DataUtility.GetXElementValueSafely(element, "HomeFaxNumber");
            item.HomeTelephoneNumber = DataUtility.GetXElementValueSafely(element, "HomeTelephoneNumber");
            item.IMAddress = DataUtility.GetXElementValueSafely(element, "IMAddress");
            item.Importance = (OlImportance)DataUtility.CreateEnum(element, "Importance", typeof(OlImportance));
            item.Initials = DataUtility.GetXElementValueSafely(element, "Initials");
            item.InternetFreeBusyAddress = DataUtility.GetXElementValueSafely(element, "InternetFreeBusyAddress");
            item.ISDNNumber = DataUtility.GetXElementValueSafely(element, "ISDNNumber");
            item.JobTitle = DataUtility.GetXElementValueSafely(element, "JobTitle");
            item.LastName = DataUtility.GetXElementValueSafely(element, "LastName");
            //mailing address is not supported in sif-c, and in outlook it is just a link to either home or work address.
            item.ManagerName = DataUtility.GetXElementValueSafely(element, "ManagerName");
            item.MiddleName = DataUtility.GetXElementValueSafely(element, "MiddleName");
            item.Mileage = DataUtility.GetXElementValueSafely(element, "Mileage");
            item.MobileTelephoneNumber = DataUtility.GetXElementValueSafely(element, "MobileTelephoneNumber");
            item.NetMeetingAlias = DataUtility.GetXElementValueSafely(element, "NetMeetingAlias");
            item.NetMeetingServer = DataUtility.GetXElementValueSafely(element, "NetMeetingServer");
            item.NickName = DataUtility.GetXElementValueSafely(element, "NickName");
            item.OfficeLocation = DataUtility.GetXElementValueSafely(element, "OfficeLocation");
            item.OrganizationalIDNumber = DataUtility.GetXElementValueSafely(element, "OrganizationalIDNumber");
            item.OtherAddress = DataUtility.GetXElementValueSafely(element, "OtherAddress");
            item.OtherAddressCity = DataUtility.GetXElementValueSafely(element, "OtherAddressCity");
            item.OtherAddressCountry = DataUtility.GetXElementValueSafely(element, "OtherAddressCountry");
            item.OtherAddressPostalCode = DataUtility.GetXElementValueSafely(element, "OtherAddressPostalCode");
            item.OtherAddressPostOfficeBox = DataUtility.GetXElementValueSafely(element, "OtherAddressPostOfficeBox");
            item.OtherAddressState = DataUtility.GetXElementValueSafely(element, "OtherAddressState");
            item.OtherAddressStreet = DataUtility.GetXElementValueSafely(element, "OtherAddressStreet");
            item.OtherFaxNumber = DataUtility.GetXElementValueSafely(element, "OtherFaxNumber");
            item.OtherTelephoneNumber = DataUtility.GetXElementValueSafely(element, "OtherTelephoneNumber");
            item.PagerNumber = DataUtility.GetXElementValueSafely(element, "PagerNumber");
            item.PersonalHomePage = DataUtility.GetXElementValueSafely(element, "PersonalHomePage");
            item.PrimaryTelephoneNumber = DataUtility.GetXElementValueSafely(element, "PrimaryTelephoneNumber");
            item.Profession = DataUtility.GetXElementValueSafely(element, "Profession");
            item.RadioTelephoneNumber = DataUtility.GetXElementValueSafely(element, "RadioTelephoneNumber");
            item.ReferredBy = DataUtility.GetXElementValueSafely(element, "ReferredBy");
            item.SelectedMailingAddress = (OlMailingAddress)DataUtility.CreateEnum(element, "SelectedMailingAddress", typeof(OlMailingAddress));
            item.Spouse = DataUtility.GetXElementValueSafely(element, "Spouse");
            item.Suffix = DataUtility.GetXElementValueSafely(element, "Suffix");
            item.Title = DataUtility.GetXElementValueSafely(element, "Title");
            item.YomiCompanyName = DataUtility.GetXElementValueSafely(element, "YomiCompanyName");
            item.YomiFirstName = DataUtility.GetXElementValueSafely(element, "YomiFirstName");
            item.YomiLastName = DataUtility.GetXElementValueSafely(element, "YomiLastName");

        }

        protected override ContactItem AddItemToItemsFolder()
        {
            return ItemsFolder.Items.Add(OlItemType.olContactItem) as ContactItem;
        }

        protected override string GetEntryId(ContactItem item)
        {
            return item.EntryID;
        }

        protected override void SaveItem(ContactItem item)
        {
            item.Save();
        }
    }
}
