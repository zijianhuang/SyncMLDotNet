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
    public class OutlookDataSourceNotes : OutlookDataSourceBase<NoteItem>
    {
        public OutlookDataSourceNotes(Application app, string exchangeType)
            : base(app, 
            new OutlookToSyncMLSifN(app),
            new SyncMLNotesToOutlook(app),
            new OutlookNotes(app), 
            DeletionLogForNotes.Default)
        {
            //exchangeType is not used for Notes, however, keeping it is for consistency and flexibility if more than one
            //exhnageType is needed.
        }

        public OutlookDataSourceNotes(string exchangeType)
            : this(new Application(), exchangeType)
        {

        }
    }
}
