using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.Office.Interop.Outlook;
using Fonlow.SyncML.Common;
using Fonlow.SyncML.Elements;
using System.Xml.Xsl;
using System.Collections.Specialized;



namespace Fonlow.SyncML.OutlookSync
{
    public class OutlookToSyncMLSifT : OutlookToSyncMLX<TaskItem>
    {
        public OutlookToSyncMLSifT(Application app)
            : base(new OutlookTasks(app), new OutlookTasksWithSifT(app), DeletionLogForTasks.Default)
        {
        }


        /// <summary>
        /// Get contact data as Sif-C
        /// </summary>
        /// <param name="TaskItem"></param>
        /// <returns></returns>
        protected override string GetItemData(TaskItem item)
        {
            return "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + SifAgent.ReadItemToText(item);
        }

        protected override void AddContentToCommand(SyncMLUpdateBase command, TaskItem task)
        {
            command.Meta.Xml.Add(FormatOfBase64);
            command.Meta.Xml.Add(TypeOfText);

            SyncMLItem item = SyncMLItem.Create();
            item.Source.LocURI.Content = task.EntryID;

            UTF8Encoding byteConverter = new UTF8Encoding();
            byte[] buffer = byteConverter.GetBytes(GetItemData(task));
            item.Data.Content = Convert.ToBase64String(buffer);

            command.ItemCollection.Add(item);
        }

        static readonly XElement typeOfText = XElement.Parse("<Type xmlns='syncml:metinf'>text/x-s4j-sift</Type>");

        protected override XElement TypeOfText
        {
            get
            {
                return typeOfText;
            }
        }

    }

    public class OutlookToSyncMLTodo : OutlookToSyncMLX<TaskItem>
    {
        public OutlookToSyncMLTodo(Application app)
            : base(new OutlookTasks(app), new OutlookTasksWithICal(app), DeletionLogForTasks.Default)
        {
        }

        /// <summary>
        /// Get contact data as vCard.
        /// </summary>
        /// <param name="TaskItem"></param>
        /// <returns></returns>
        protected override string GetItemData(TaskItem item)
        {
            return SifAgent.ReadItemToText(item);
        }

        protected override void AddContentToCommand(SyncMLUpdateBase command, TaskItem task)
        {
            command.Meta.Xml.Add(TypeOfText);
            SyncMLItem item = SyncMLItem.Create();
            item.Source.LocURI.Content = task.EntryID;
            item.Data.Content = GetItemData(task);
            command.ItemCollection.Add(item);
        }

        static readonly XElement typeOfText = XElement.Parse("<Type xmlns='syncml:metinf'>text/x-vcalendar</Type>");

        protected override XElement TypeOfText
        {
            get
            {
                return typeOfText;
            }
        }

    }
}
