using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Fonlow.SyncML.Common;
using Fonlow.SyncML.Elements;
using System.Xml.Xsl;
using System.Diagnostics;
using Fonlow.VCard;
using System.Linq;
using System.Xml.XPath;//should be removed after changing vCardParse to accept XElement.

namespace Fonlow.SyncML.OpenContacts
{
    /// <summary>
    /// Convert change SyncML from change log xml of Open Contacts to a collection of SyncML Sync Commands.
    /// Internally it will transform OCChangeLogXML into SifCChangeLogXML using a stylesheet,
    /// then navigate through each SIF-C record to create SyncMLCommand.
    /// 
    /// In order to support vCard, a derived class called XmlToSyncMLSyncCommandsForVCard is developed.
    /// </summary>
    public class XmlToSyncMLSyncCommands : ISyncCommandsSource
    {
        protected XmlToSyncMLSyncCommands()
        {

        }

        /// <summary>
        /// Hold Sif-C change log xml for further analysis.
        /// </summary>
        private XElement sifcChangeLogXml;

        private int numberOfCommandPerBatch;

        /// <summary>
        /// Transform OC change log xml to Sif-C change log xml.
        /// </summary>
        private XslCompiledTransform xmlTransformer;

        /*      private ContactExchangeType mediumType;

              public ContactExchangeType MediumType
              {
                  get { return mediumType; }
                  set { mediumType = value; }
              }*/

        private XElement ocChangeLogXml;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml">Change log XML.</param>
        /// <param name="styleSheetFileName">Stylesheet file to convert the change log xml to SIF-C.</param>
        /// <param name="numberOfCommandPerBatch">How many sync commands can be extracted each time from the internal command buffer.</param>
        public XmlToSyncMLSyncCommands(XElement xml, string styleSheetFileName, int numberOfCommandPerBatch)
        {
            this.numberOfCommandPerBatch = numberOfCommandPerBatch;
            ocChangeLogXml = xml;
            this.styleSheetFileName = styleSheetFileName;
        }

        private string styleSheetFileName;

        private void PrepareSifcChangeLogXml()
        {
            xmlTransformer = new XslCompiledTransform();

            try
            {
                xmlTransformer.Load(styleSheetFileName);
                using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlTransformer.OutputSettings))
                    {
                        xmlTransformer.Transform(ocChangeLogXml.CreateReader(), xmlWriter);

                        stream.Position = 3;  //skip the BOM
                        sifcChangeLogXml = XElement.Load(new System.IO.StreamReader(stream));
                    }
                }
            }
            catch (XsltCompileException e)
            {
                throw new FacadeErrorException("When converting from OC Change Log XML to SIFC XML, there is error.", e);
            }
            catch (XsltException e) //it might be XslLoadException get caught, but this exception is not available to programmer?
            {
                throw new FacadeErrorException("When converting from OC Change Log XML to SIFC XML, there is error.", e);
            }

        }

        /// <summary>
        /// Internal commands buffers. 
        /// The client will get a portion each time to send to the server.
        /// </summary>
        private Collection<SyncMLCommand> commandsBuffer;

        /// <summary>
        /// The progress that client codes extract commands from the buffer.
        /// </summary>
        private int bufferPosition;


        /// <summary>
        /// Extract a batch of commands from commandsBuffer. 
        /// The number of commands is limited by NumberOfCommandPerBatch.
        /// </summary>
        /// <param name="commands">External commands collection to get command items from commandsBuffer.</param>
        /// <returns>Ture if more commands to emit; false if no more.</returns>
        public bool ExtractNextCommands(IList<SyncMLCommand> commands)
        {
            if (commands == null)
            {
                return false;
            }

            if (!commandsParepared)
            {
                if (sifcChangeLogXml == null)
                    PrepareSifcChangeLogXml();

                commandsBuffer = new Collection<SyncMLCommand>();
                PrepareCommands(commandsBuffer);
                commandsParepared = true;
            }

            int c = 0;
            while (bufferPosition < commandsBuffer.Count)
            {
                commands.Add(commandsBuffer[bufferPosition]);
                bufferPosition++;
                c++;
                if (c == numberOfCommandPerBatch)
                    break;
            }

            return (bufferPosition < commandsBuffer.Count);
        }

        private bool commandsParepared;

        /// <summary>
        /// Total number of changes indicated by the change log XML, including added, updated and deleted ones.
        /// </summary>
        public int NumberOfChanges
        {
            get
            {
                if (sifcChangeLogXml == null)
                    PrepareSifcChangeLogXml();

                int c = XmlHelpers.SafeElementsQuery(sifcChangeLogXml,
                    "Changes", "New", "C").Count() +
                    XmlHelpers.SafeElementsQuery(sifcChangeLogXml,
                    "Changes", "Update", "C").Count() +
                    XmlHelpers.SafeElementsQuery(sifcChangeLogXml,
                    "Changes", "Delete", "D").Count();
                return c;
            }
        }

        /// <summary>
        /// Analyze change log XML and transform to a set of commands.
        /// </summary>
        /// <param name="commands">External commands collection. Generally this is commandsBuffer.</param>
        private void PrepareCommands(IList<SyncMLCommand> commands)
        {
            AddAddCommandsToCollection(commands);

            AddReplaceCommandsToCollection(commands);

            AddDeleteCommandsToCollection(commands);
        }

        /// <summary>
        /// Add AddCommands from SifcChangeLogXML
        /// </summary>
        /// <param name="commands">The collections will be expanded, as result.</param>
        private void AddAddCommandsToCollection(IList<SyncMLCommand> commands)
        {
            IEnumerable<XElement> addNodes = XmlHelpers.SafeElementsQuery(sifcChangeLogXml,
                "Changes", "New", "C");

            if (forSlowSync)
            {
                foreach (XElement node in addNodes)
                {
                    SyncMLReplace replaceCommand = SyncMLReplace.Create();
                    AddContentToCommand(replaceCommand, node);
                    commands.Add(replaceCommand);
                }
            }
            else
            {
                foreach (XElement node in addNodes)
                {
                    SyncMLAdd addCommand = SyncMLAdd.Create();
                    AddContentToCommand(addCommand, node);
                    commands.Add(addCommand);
                }
            }
        }

        /// <summary>
        /// Add replace commands from SifCChangeLog
        /// </summary>
        /// <param name="commands"></param>
        private void AddReplaceCommandsToCollection(IList<SyncMLCommand> commands)
        {
            IEnumerable<XElement> replaceNodes = XmlHelpers.SafeElementsQuery(sifcChangeLogXml,
                "Changes", "Update", "C");

            foreach (XElement node in replaceNodes)
            {
                SyncMLReplace replaceCommand = SyncMLReplace.Create();
                AddContentToCommand(replaceCommand, node);
                commands.Add(replaceCommand);
            }

        }

        private void AddDeleteCommandsToCollection(IList<SyncMLCommand> commands)
        {
            IEnumerable<XElement> deleteNodes = XmlHelpers.SafeElementsQuery(sifcChangeLogXml,
                "Changes", "Delete", "D");

            foreach (XElement node in deleteNodes)
            {
                SyncMLDelete deleteCommand = SyncMLDelete.Create();
                SyncMLItem item = SyncMLItem.Create();
                item.Source.LocURI.Content = node.Attribute("ID").Value;
                deleteCommand.ItemCollection.Add(item);
                commands.Add(deleteCommand);
            }
        }

        static readonly XElement formatOfBase64 = XElement.Parse("<Format xmlns='syncml:metinf'>b64</Format>");

        protected static XElement FormatOfBase64
        {
            get
            {
                return formatOfBase64;
            }
        }

        static readonly XElement typeOfText= XElement.Parse("<Type xmlns='syncml:metinf'>text/x-s4j-sifc</Type>");

        protected static XElement TypeOfText
        {
            get
            {
                return typeOfText;
            }
        }
        /// <summary>
        /// Add content to a AddCommand or a ReplaceCommand, with currentNode of SifCChangeLogXML.
        /// </summary>
        /// <param name="command">Command object to be manipulated.</param>
        /// <param name="currentNode">A C element under either New or Update element.</param>
        protected virtual void AddContentToCommand(SyncMLUpdateBase command, XElement currentNode)
        {
            if (command == null)
            {
                Trace.TraceWarning("Hey, r u crazy? giving me null command.");
                return;
            }

            if (currentNode == null)
            {
                Trace.TraceWarning("Hey, r u crazy? giving me null currentNode.");
                return;
            }

            command.Meta.Xml.Add(FormatOfBase64);
            command.Meta.Xml.Add(TypeOfText);

            SyncMLItem item = SyncMLItem.Create();
            item.Source.LocURI.Content = currentNode.Attribute("ID").Value;

            UTF8Encoding byteConverter = new UTF8Encoding();
            byte[] buffer = byteConverter.GetBytes("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + currentNode.Element("contact").ToString(SaveOptions.DisableFormatting));
            item.Data.Content = Convert.ToBase64String(buffer);

            command.ItemCollection.Add(item);
        }

        bool forSlowSync;
        public void PrepareForSlowSync()
        {
            forSlowSync = true;
        }

    }

    public class XmlToSyncMLSyncCommandsForVcard : XmlToSyncMLSyncCommands
    {
        protected XmlToSyncMLSyncCommandsForVcard()
        {

        }

        public XmlToSyncMLSyncCommandsForVcard(XElement xml, string styleSheetFileName, int numberOfCommandPerBatch, bool useBase64)
            : base(xml, styleSheetFileName, numberOfCommandPerBatch)
        {
            this.useBase64 = useBase64;
        }

        static readonly XElement xVcardType = XElement.Parse("<Type xmlns='syncml:metinf'>text/x-vcard</Type>");
        private bool useBase64;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="currentNode">A C element under either New or Update element.</param>
        protected override void AddContentToCommand(SyncMLUpdateBase command, XElement currentNode)
        {
            if (command == null)
            {
                Trace.TraceWarning("Hey, you bad. Null command.");
                return;
            }

            if (currentNode == null)
            {
                Trace.TraceWarning("Hey, r u crazy? giving me null currentNode.");
                return;
            } 
            
            if (useBase64)
            {
                SyncMLItem item = SyncMLItem.Create();
                item.Source.LocURI.Content = currentNode.Attribute("ID").Value;
                item.Meta.Xml.Add(FormatOfBase64);
                item.Meta.Xml.Add(TypeOfText);

                UTF8Encoding byteConverter = new UTF8Encoding();
                byte[] buffer = byteConverter.GetBytes(VCardWriter.WriteToString(VCardSIFC.ConvertSifCToVCard(currentNode.Element("contact"))));
                item.Data.Content = Convert.ToBase64String(buffer);

                command.ItemCollection.Add(item);
            }
            else
            {
                SyncMLItem item = SyncMLItem.Create();
                item.Source.LocURI.Content = currentNode.Attribute("ID").Value;
                item.Meta.Xml.Add(xVcardType);

                item.Data.Content = VCardWriter.WriteToString(VCardSIFC.ConvertSifCToVCard(currentNode.Element("contact")));

                command.ItemCollection.Add(item);
            }

        }

        protected static string SafeXmlContent(string vcardText)
        {
            StringBuilder builder = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            using (XmlWriter writer = XmlWriter.Create(builder, settings))
            {
                writer.WriteString(vcardText);
            }
            return builder.ToString();

        }

    }

}
