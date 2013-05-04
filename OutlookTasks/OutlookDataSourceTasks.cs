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
    public class OutlookDataSourceTasks : OutlookDataSourceBase<TaskItem>
    {
        public OutlookDataSourceTasks(Application app, string exchangeType)
            : base(app, 
            (exchangeType=="SIF")? new OutlookToSyncMLSifT(app) as OutlookToSyncMLX<TaskItem> : new OutlookToSyncMLTodo(app),
            (exchangeType=="SIF")? new SyncMLSifTToOutlook(app) as SyncMLXToOutlook<TaskItem> : new SyncMLTodoToOutlook(app),
            new OutlookTasks(app), 
            DeletionLogForTasks.Default)
        {
        }

        public OutlookDataSourceTasks(string exchangeType)
            : this(new Application(), exchangeType)
        {

        }
    }
}
