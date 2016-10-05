using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace DemoContact
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string Surname { get; set; }
        public string GivenName { get; set; }

        public string MiddleName { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public DateTime? Modified { get; set; }
    }


    [DataContract(Name ="Item")]
    public class ContactLogItem
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public DateTime Modified { get; set; }
    }


    [DataContract]
    public class ContactLog
    {
        [DataMember]
        public ContactLogItem[] Items { get; set; }
    }

    public enum UpdateType { Unchanged, Added, Updated, Deleted};
}
