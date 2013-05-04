using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Fonlow.SyncML.Common;
using Fonlow.SyncML.Elements;
using System.Xml.Xsl;
using System.Collections.Specialized;
using Fonlow.VCard;

namespace Fonlow.SyncML.OpenContacts
{
    /// <summary>
    /// Convert sync command to change log XML of OC
    /// </summary>
    public sealed class SyncMLSyncCommandsToXml
    {
        private SyncMLSyncCommandsToXml()
        {

        }
        /// <summary>
        /// Generate oc change log xml from a collection of SyncML objects. 
        /// </summary>
        /// <param name="commands">External SyncMLCommand objects.</param>
        /// <param name="styleSheetFileName">Stylesheet to transform SIF-C to change log XML.</param>
        /// <param name="lastAnchor"></param>
        /// <returns>Change log XML.</returns>
        public static string GenerateXml(IList<SyncMLCommand> commands, string styleSheetFileName, string lastAnchor)
        {
            XslCompiledTransform xmlTransformer = new XslCompiledTransform();
            xmlTransformer.Load(styleSheetFileName);
            XElement sifcXml = GenerateSifCChangeLogXml(commands, lastAnchor);
            System.Diagnostics.Debug.WriteLine("Sifc Change Log:");
            System.Diagnostics.Debug.WriteLine(sifcXml.ToString());

            StringBuilder builder = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(builder))
            {
                xmlTransformer.Transform(sifcXml.CreateReader(), xmlWriter);
            }
            return builder.ToString();
        }

        private static XElement GenerateSifCChangeLogXml(IList<SyncMLCommand> commands, string lastAnchor)
        {
            XElement r = new XElement("SIFCChangeLog",
                new XElement("LastAnchor", lastAnchor),
                new XElement("Source", "SyncML Server"));

            XElement changesElement = new XElement("Changes");
            r.Add(changesElement);

            foreach (SyncMLCommand command in commands)
            {
                SyncMLAdd addCommand = command as SyncMLAdd;
                if (addCommand != null)
                {
                    WriteAddOrReplaceCommandToXml(changesElement, addCommand);
                    continue;
                }

                SyncMLReplace replaceCommand = command as SyncMLReplace;
                if (replaceCommand != null)
                {
                    WriteAddOrReplaceCommandToXml(changesElement, replaceCommand);
                    continue;
                }

                SyncMLDelete deleteCommand = command as SyncMLDelete;
                if (deleteCommand != null) // handle Delete commands
                {
                    string localID = deleteCommand.ItemCollection[0].Target.LocURI.Content;
                    changesElement.Add(new XElement("Delete",
                        new XElement("D", new XAttribute("ID", localID))));
                    continue;
                }
            }  //foreach


            return r;

        }  // using xmlWriter 


        /// <summary>
        /// 
        /// </summary>
        /// <param name="topElement">the 'Changes' element, to be manipulated</param>
        /// <param name="command"></param>
        private static void WriteAddOrReplaceCommandToXml(XElement topElement, SyncMLUpdateBase command)
        {
            if (command == null)
                return;

            bool isBase64 = false;
            ContactExchangeType contentType = ContactExchangeType.Unknown;
            bool isAddCommand = false;

            string commandTypeStr;

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
                return; //the meta element may be empty, so no need to proceed.
            }

            if (metaType.Content == "text/x-s4j-sifc")
                contentType = ContactExchangeType.Sifc;
            else if ((metaType.Content == "text/x-vcard")||(metaType.Content=="text/vcard"))
                contentType = ContactExchangeType.Vcard21;

            if (contentType == ContactExchangeType.Unknown)
                throw new LocalDataSourceException("expected data is Base64 encoded SIF-C or vCard");

            isAddCommand = command is SyncMLAdd;
            commandTypeStr = isAddCommand ? "New" : "Update";

            //Assuming there will be only one item.
            string id;
            if (isAddCommand)
                id = commandItem.Source.LocURI.Content;
            else
                id = commandItem.Target.LocURI.Content;

            XElement cItem = new XElement("C", new XAttribute("ID", id));
            XElement changItem = new XElement(commandTypeStr, cItem);
            topElement.Add(changItem);

            switch (contentType)
            {
                case ContactExchangeType.Sifc: // do not need to check isBase64, because it must be
                    cItem.Add(XElement.Parse(Utility.ConvertFromBase64(commandItem.Data.Content)));
                    break;
                case ContactExchangeType.Vcard21:
                    XElement sifcXml;
                    if (isBase64)
                        sifcXml = VCardSIFC.ConvertVCardToSifCXml(VCardReader.ParseText(
                           Utility.ConvertFromBase64(commandItem.Data.Content)));
                    else
                        sifcXml = VCardSIFC.ConvertVCardToSifCXml(VCardReader.ParseText(commandItem.Data.Content));

                    cItem.Add(sifcXml);
                    break;
                default:
                    throw new LocalDataSourceException("Can not create stream from command Data.");
            }

        }

        /// <summary>
        /// Extract from a collection of sync commands to create a list of strings. This function is generally for
        /// unit testing.
        /// </summary>
        /// <param name="commands">A set of sync commands</param>
        /// <returns>String collection. Each string represents a sync command.</returns>
        public static StringCollection ExtractSifcStrings(IList<SyncMLCommand> commands)
        {
            if (commands == null)
            {
                return null;
            } 
            
            StringCollection collection = new StringCollection();

            foreach (SyncMLCommand command in commands)
            {
                SyncMLAdd addCommand = command as SyncMLAdd;
                if (addCommand != null) // handle Add commands
                {
                    //at this stage, I would assume <Format>b64</Format><Type>text/x-s4j-sifc</Type>. In the future, use this as condition.

                    //Decode the b64 SIF-C XML
                    collection.Add(Utility.ConvertFromBase64(addCommand.ItemCollection[0].Data.Content));
                    continue;
                }
                SyncMLReplace replaceCommand = command as SyncMLReplace;
                if (replaceCommand != null) // handle Replace commands
                {

                    continue;
                }
                SyncMLDelete deleteCommand = command as SyncMLDelete;
                if (deleteCommand != null) // handle Delete commands
                {
                    continue;
                }
            }

            return collection;

        }


    }
}
