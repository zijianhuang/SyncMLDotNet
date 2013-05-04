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
    /// <summary>
    /// Apply SyncML content into Outlook to update outlook items.
    /// This abstract class provide basic logic common.
    /// </summary>
    /// <typeparam name="T">The type of Outlook items</typeparam>
    public abstract class SyncMLXToOutlook<T>
    {
        protected SyncMLXToOutlook(OutlookItems<T> outlookAgent, OutlookItemsWithSyncContent<T> sifAgent)
        {
            this.sifAgent = sifAgent;
            this.outlookAgent = outlookAgent;
        }

        OutlookItemsWithSyncContent<T> sifAgent;
        OutlookItems<T> outlookAgent;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns>localId=serverId pair</returns>
        private string ApplyAddOrReplaceCommand(SyncMLUpdateBase command)
        {
            if (command == null)
                return null;

            bool isBase64 = false;
            bool isAddCommand = false;

            SyncMLMeta commandMeta = command.Meta;
            SyncMLItem commandItem = command.ItemCollection[0]; //assuming there is always one item.
            if (String.IsNullOrEmpty(commandMeta.Content) && (!commandMeta.Xml.HasElements))
            {
                commandMeta = commandItem.Meta;
            }

            MetaParser metaParser = new MetaParser(commandMeta.Xml);
            MetaFormat metaFormat = metaParser.GetMetaFormat();
            if (metaFormat != null)
                isBase64 = metaFormat.Content == "b64";

            MetaType metaType = metaParser.GetMetaType();
            if (metaType == null)
            {
                return null; //the meta element may be empty, so no need to proceed.
            }

            //    if (contentType == ContactExchangeType.Unknown)
            //todo: add some exception        throw new LocalDataSourceException("expected data is Base64 encoded SIF-C or vCard");

            isAddCommand = command is SyncMLAdd;

            //Assuming there will be only one item.
            string serverId;
            string localId = null;

            serverId = commandItem.Source.LocURI.Content; //id of remote one

            if (!isAddCommand)
                localId = commandItem.Target.LocURI.Content; // entryId of existing contact

            string text = GetTextFromContent(isBase64, metaType.Content, commandItem.Data.Content);
            if (text != null)
            {
                localId = isAddCommand ? sifAgent.AddItem(text) :
                    sifAgent.ReplaceItem(text, localId);

                return isAddCommand ? localId + "=" + serverId : null;
            }
            else
                return null;
        }

        protected abstract string GetTextFromContent(bool isBase64, string contentType, string content);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commands"></param>
        /// <returns>A list of Id pair separated by link break</returns>
        public string ApplySyncCommands(IList<SyncMLCommand> commands)
        {
            if (commands == null)
            {
                throw new ArgumentNullException("commands");
            } 
            
            StringBuilder builder = new StringBuilder();
            foreach (SyncMLCommand command in commands)
            {
                SyncMLAdd addCommand = command as SyncMLAdd;
                if (addCommand != null)
                {
                    string idPair = ApplyAddOrReplaceCommand(addCommand);
                    if (!String.IsNullOrEmpty(idPair))
                    {
                        builder.AppendLine(idPair);
                    }
                    continue;
                }

                SyncMLReplace replaceCommand = command as SyncMLReplace;
                if (replaceCommand != null)
                {
                    string idPair = ApplyAddOrReplaceCommand(replaceCommand);
                    if (!String.IsNullOrEmpty(idPair))
                    {
                        builder.AppendLine(idPair);
                    }
                    continue;
                }

                SyncMLDelete deleteCommand = command as SyncMLDelete;
                if (deleteCommand != null)
                {
                    string localID = deleteCommand.ItemCollection[0].Target.LocURI.Content;
                    bool deletionOK = outlookAgent.DeleteItem(localID);
                    if (!deletionOK)
                    {
                        System.Diagnostics.Trace.TraceInformation(String.Format(
                            "The deletion can not be done. Contact with this id {0} can not be found.", localID));
                    }
                    continue;
                }
            }
            return builder.ToString();
        }

    }
}
