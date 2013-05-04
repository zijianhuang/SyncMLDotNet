using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using Fonlow.SyncML.Elements;
using Fonlow.SyncML.Common;
using Microsoft.Office.Interop.Outlook;
using System.Collections.Specialized;

namespace Fonlow.SyncML.OutlookSync
{
    public class OutlookDataSourceContacts : OutlookDataSourceBase<ContactItem>
    {
        private OutlookDataSourceContacts(Application app, string exchangeType)
            : base(app, 
            (exchangeType == "SIF") ? new OutlookToSyncMLSifC(app) as OutlookToSyncMLX<ContactItem> : new OutlookToSyncMLVcard(app) as OutlookToSyncMLX<ContactItem>,
            (exchangeType == "SIF") ? new SyncMLSifCToOutlook(app) as SyncMLXToOutlook<ContactItem> : new SyncMLVCardToOutlook(app) as SyncMLXToOutlook<ContactItem>  ,
            new OutlookContacts(app), 
            DeletionLogForContacts.Default)
        {

        }

        public OutlookDataSourceContacts(string exchangeType)
            : this(new Application(), exchangeType)
        {

        }
    }
}
