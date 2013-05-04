using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.Office.Interop.Outlook;
using Fonlow.VCard;

namespace Fonlow.SyncML.OutlookSync
{
    public class OutlookContactsWithVCard : OutlookItemsWithSyncContent<ContactItem>
    {
        public OutlookContactsWithVCard(Application app)
            : base(app.GetNamespace("MAPI").GetDefaultFolder(OlDefaultFolders.olFolderContacts) as Folder)
        {
        }

        readonly DateTime outlookNullYear = new DateTime(4501, 1, 1);

        public override string ReadItemToText(ContactItem item)
        {
            Fonlow.VCard.VCard vcard = new Fonlow.VCard.VCard();
            vcard.FormattedName = item.FullName;//or fileAs
            vcard.GivenName = item.FirstName;
            vcard.Surname = item.LastName;
            vcard.MiddleName = item.MiddleName;
            vcard.Title = item.Title;
            vcard.Note = item.Body;
            vcard.Org = item.CompanyName;

            if ((item.Birthday != null) && (item.Birthday < outlookNullYear))
                vcard.Birthday = item.Birthday;

            vcard.Department = item.Department;
            vcard.Suffix = item.Suffix;
            vcard.Role = item.JobTitle;

            if (!String.IsNullOrEmpty(item.HomeTelephoneNumber))
            {
                vcard.Phones.Add(new PhoneNumber(item.HomeTelephoneNumber, HomeWorkTypes.HOME));
            }

            if (!String.IsNullOrEmpty(item.Home2TelephoneNumber))
            {
                vcard.Phones.Add(new PhoneNumber(item.Home2TelephoneNumber, HomeWorkTypes.HOME));
            }

            if (!String.IsNullOrEmpty(item.MobileTelephoneNumber))
            {
                vcard.Phones.Add(new PhoneNumber(item.MobileTelephoneNumber, HomeWorkTypes.None, PhoneTypes.CELL, false));
            }

            if (!String.IsNullOrEmpty(item.PagerNumber))
            {
                vcard.Phones.Add(new PhoneNumber(item.PagerNumber, HomeWorkTypes.None, PhoneTypes.PAGER, false));
            }

            if (!String.IsNullOrEmpty(item.HomeFaxNumber))
            {
                vcard.Phones.Add(new PhoneNumber(item.HomeFaxNumber, HomeWorkTypes.HOME, PhoneTypes.FAX, false));
            }
            
            if (!String.IsNullOrEmpty(item.PrimaryTelephoneNumber))
            {
                vcard.Phones.Add(new PhoneNumber(item.PrimaryTelephoneNumber, HomeWorkTypes.None, PhoneTypes.VOICE, true));
            }

            if (!String.IsNullOrEmpty(item.BusinessTelephoneNumber))
            {
                vcard.Phones.Add(new PhoneNumber(item.BusinessTelephoneNumber, HomeWorkTypes.WORK));
            }

            if (!String.IsNullOrEmpty(item.Business2TelephoneNumber))
            {
                vcard.Phones.Add(new PhoneNumber(item.Business2TelephoneNumber, HomeWorkTypes.WORK));
            }

            if (!String.IsNullOrEmpty(item.BusinessFaxNumber))
            {
                vcard.Phones.Add(new PhoneNumber(item.BusinessFaxNumber, HomeWorkTypes.WORK, PhoneTypes.FAX, false));
            }

            if (!String.IsNullOrEmpty(item.CompanyMainTelephoneNumber))
            {
                vcard.Phones.Add(new PhoneNumber(item.CompanyMainTelephoneNumber, HomeWorkTypes.WORK, PhoneTypes.VOICE, true));
            }

            if (!String.IsNullOrEmpty(item.OtherTelephoneNumber))
                vcard.Phones.Add(new PhoneNumber(item.OtherTelephoneNumber, HomeWorkTypes.None, PhoneTypes.VOICE, false));

            if (!String.IsNullOrEmpty(item.OtherFaxNumber))
                vcard.Phones.Add(new PhoneNumber(item.OtherFaxNumber, HomeWorkTypes.None, PhoneTypes.FAX, false));

            

            if (!String.IsNullOrEmpty(item.Email1Address))
            {
                vcard.Emails.Add(new EmailAddress(item.Email1Address, false));
            }

            if (!String.IsNullOrEmpty(item.Email2Address))
            {
                vcard.Emails.Add(new EmailAddress(item.Email2Address, false));
            }
            
            if (!String.IsNullOrEmpty(item.Email3Address))
            {
                vcard.Emails.Add(new EmailAddress(item.Email3Address, false));
            }

            Address homeAddress = new Address();
            homeAddress.Locality = item.HomeAddressCity;
            homeAddress.Country = item.HomeAddressCountry;
            homeAddress.Postcode = item.HomeAddressPostalCode;
            homeAddress.POBox = item.HomeAddressPostOfficeBox;
            homeAddress.Region = item.HomeAddressState;
            homeAddress.Street = item.HomeAddressStreet;
            homeAddress.HomeWorkType = HomeWorkTypes.HOME;
            if (homeAddress.HadContent)
            {
                vcard.Addresses.Add(homeAddress);
            }

            Address businessAddress = new Address();
            businessAddress.Locality = item.BusinessAddressCity;
            businessAddress.Country = item.BusinessAddressCountry;
            businessAddress.Postcode = item.BusinessAddressPostalCode;
            businessAddress.POBox = item.BusinessAddressPostOfficeBox;
            businessAddress.Region = item.BusinessAddressState;
            businessAddress.Street = item.BusinessAddressStreet;
            businessAddress.HomeWorkType = HomeWorkTypes.WORK;
            if (businessAddress.HadContent)
            {
                vcard.Addresses.Add(businessAddress);
            }

            if (!String.IsNullOrEmpty(item.PersonalHomePage))
            {
                vcard.URLs.Add(new URL(item.PersonalHomePage, HomeWorkTypes.HOME));
            }

            if (!String.IsNullOrEmpty(item.BusinessHomePage))
            {
                vcard.URLs.Add(new URL(item.BusinessHomePage, HomeWorkTypes.WORK));
            }

            return VCardWriter.WriteToString(vcard);
        }

        protected override void WriteMetaToItem(string meta, ContactItem item)
        {
            Fonlow.VCard.VCard vcard = VCardReader.ParseText(meta);
            item.LastName = vcard.Surname;
            item.FirstName = vcard.GivenName;
            item.MiddleName = vcard.MiddleName;
            item.Title = vcard.Title;
            item.Suffix = vcard.Suffix;
         //   item.FullName = vcard.FormattedName;//This must be after lastName to Suffix, otherwise, item.FullName will be recomposed by Outlook
            item.Body = vcard.Note;

            item.CompanyName = vcard.Org;
            item.Department = vcard.Department;
            item.JobTitle = vcard.Role;

            if (vcard.Birthday > DateTime.MinValue)
                item.Birthday = vcard.Birthday;

            PhoneNumber phoneNumber = vcard.Phones.GetItemsOfTypes(PhoneTypes.CELL).FirstOrDefault();
            if (phoneNumber != null)
                item.MobileTelephoneNumber = phoneNumber.Number;

            //Home phones
            IEnumerable<PhoneNumber> numbers = vcard.Phones.GetItemsOfTypes(PhoneTypes.VOICE, HomeWorkTypes.HOME);
            phoneNumber = numbers.FirstOrDefault();
            if (phoneNumber != null)
            {
                item.HomeTelephoneNumber = phoneNumber.Number;
                if (numbers.Count() > 1)
                    item.Home2TelephoneNumber = numbers.ElementAt(1).Number;
            }

            //Home fax
            numbers = vcard.Phones.GetItemsOfTypes(PhoneTypes.FAX, HomeWorkTypes.HOME);
            phoneNumber = numbers.FirstOrDefault();
            if (phoneNumber != null)
            {
                item.HomeFaxNumber = phoneNumber.Number;
            }

            //Work phones
            numbers = vcard.Phones.GetItemsOfTypes(PhoneTypes.VOICE, HomeWorkTypes.WORK);
            phoneNumber = numbers.FirstOrDefault();
            if (phoneNumber != null)
            {
                item.BusinessTelephoneNumber = phoneNumber.Number;
                if (numbers.Count() > 1)
                    item.Business2TelephoneNumber = numbers.ElementAt(1).Number;
            }

            //Work fax
            numbers = vcard.Phones.GetItemsOfTypes(PhoneTypes.FAX, HomeWorkTypes.WORK);
            phoneNumber = numbers.FirstOrDefault();
            if (phoneNumber != null)
            {
                item.BusinessFaxNumber = phoneNumber.Number;
            }

            //Other phone
            numbers = vcard.Phones.GetItemsOfTypesExactly(PhoneTypes.VOICE, HomeWorkTypes.None);
            phoneNumber = numbers.FirstOrDefault();
            if (phoneNumber != null)
            {
                item.OtherTelephoneNumber = phoneNumber.Number;
            }

            //Other fax
            numbers = vcard.Phones.GetItemsOfTypesExactly(PhoneTypes.FAX, HomeWorkTypes.None);
            phoneNumber = numbers.FirstOrDefault();
            if (phoneNumber != null)
            {
                item.OtherFaxNumber = phoneNumber.Number;
            }

            phoneNumber = vcard.Phones.GetPreferredItems().FirstOrDefault(n => (n.HomeWorkTypes == HomeWorkTypes.HOME) || (n.HomeWorkTypes == HomeWorkTypes.None));
            if (phoneNumber != null)
                item.PrimaryTelephoneNumber = phoneNumber.Number;

            phoneNumber = vcard.Phones.GetPreferredItems().FirstOrDefault(n => n.HomeWorkTypes == HomeWorkTypes.WORK);
            if (phoneNumber != null)
                item.CompanyMainTelephoneNumber = phoneNumber.Number;

            phoneNumber = vcard.Phones.GetItemsOfTypes(PhoneTypes.PAGER).FirstOrDefault();
            if (phoneNumber != null)
                item.PagerNumber = phoneNumber.Number;

            EmailAddress email = vcard.Emails.FirstOrDefault();
            if (email != null)
            {
                item.Email1Address = email.Address;
                if (vcard.Emails.Count() > 1)
                    item.Email2Address = vcard.Emails.ElementAt(1).Address;

                if (vcard.Emails.Count() > 2)
                    item.Email3Address = vcard.Emails.ElementAt(2).Address;
            }

            Address address = vcard.Addresses.FirstOrDefault(n => n.HomeWorkType == HomeWorkTypes.HOME);
            if (address != null)
            {
                item.HomeAddressCity = address.Locality;
                item.HomeAddressCountry = address.Country;
                item.HomeAddressPostalCode = address.Postcode;
                item.HomeAddressPostOfficeBox = address.POBox;
                item.HomeAddressState = address.Region;
                item.HomeAddressStreet = address.Street;
            }

            address = vcard.Addresses.FirstOrDefault(n => n.HomeWorkType == HomeWorkTypes.WORK);
            if (address != null)
            {
                item.BusinessAddressCity = address.Locality;
                item.BusinessAddressCountry = address.Country;
                item.BusinessAddressPostalCode = address.Postcode;
                item.BusinessAddressPostOfficeBox = address.POBox;
                item.BusinessAddressState = address.Region;
                item.BusinessAddressStreet = address.Street;
            }

            URL url = vcard.URLs.FirstOrDefault(n => n.HomeWorkTypes == HomeWorkTypes.HOME);
            if (url != null)
                item.PersonalHomePage = url.Address;

            url = vcard.URLs.FirstOrDefault(n => n.HomeWorkTypes == HomeWorkTypes.WORK);
            if (url != null)
                item.BusinessHomePage = url.Address;
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
