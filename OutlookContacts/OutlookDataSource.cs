using Microsoft.Office.Interop.Outlook;

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
