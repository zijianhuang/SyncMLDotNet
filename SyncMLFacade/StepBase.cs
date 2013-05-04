using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Collections.Specialized;
using System.Runtime.Remoting.Messaging;
using System.Diagnostics;
using Fonlow.SyncML.Common;
using System.Collections.ObjectModel;
using Fonlow.SyncML.Elements;
using System.Linq;

namespace Fonlow.SyncML
{

    /// <summary>
    /// Authentication types
    /// </summary>
    internal enum SyncMLAuthenticationType
    {
        None,
        Base64,
        MD5
    }

    /// <summary>
    /// Indicate what status of authentication message
    /// </summary>
    internal enum SyncMLAuthenticationStatus
    {
        UnknownStatus,
        LoggedOn,
        Base64Chal,
        MD5Chal,
        FailedID,
        UnknownChal,
        FailedConnection
    }


    /// <summary>
    /// Interface of each sync message in order to encalsulate a SyncML message. 
    /// The derived classes are to implement SyncML messages
    /// for steps of a synchronization session.
    /// </summary>
    internal abstract class SyncMLMessageBase
    {
        /// <summary>
        /// Made sure the Step can use resource in SyncMLFacade
        /// </summary>
        /// <param name="syncMLFacade"></param>
        protected SyncMLMessageBase(SyncMLFacade syncMLFacade)
        {
            Facade = syncMLFacade;
            CommandAndStatusRegister = new CommandResponseRegister();
        }
        /// <summary>
        /// Send request to server. All derived functions should call Facade.CurrentMsgID++ at the end.
        /// </summary>
        public abstract void Send();

        /// <summary>
        /// Process response from server to establish SyncML models as a SyncMLSyncML object.
        /// The inherited function should call the base first, then analyze the model.
        /// If the response has fatal error, it is signaled through serverSyncML== null.
        /// </summary>
        /// <param name="text">True if success. If fail, ServerSyncML is null. </param>
        protected virtual bool ProcessResponse(string text)
        {
            Debug.WriteLine("ProcessResponse from server: " + text);

            try
            {
                ServerSyncML = SyncMLSyncML.Create(XElement.Parse(text));
            }
            catch (XmlException)
            {
                Trace.TraceInformation("The server returns no SyncML but this: " + text);
                Facade.DisplayOperationMessage("The connection fail. Please check the server URL.");
                ServerSyncML = null;
            }
            catch (ArgumentNullException)
            {
                Trace.TraceInformation("The server return invalid or incomplete SyncML message:" + text);
                ServerSyncML = null; // up to this point, SyncMLSyncML object has been created though elements are not right.
            }
            catch (Exception e)
            {
                Trace.TraceWarning("When ProcessResponse (General Exception caught): " + e.ToString());
                ServerSyncML = null; // serverSyncML might be created already. 
            }

            if (ServerSyncML == null)
            {
                Facade.SoFarOK = false;
                return false;
            }
            else
                return true;
        }

        /// <summary>
        /// Access common resources of SyncML facade as a container of syncML message objects.
        /// </summary>
        protected SyncMLFacade Facade
        {
            get;
            private set;
        }

        /// <summary>
        /// Record how many commands sent to the server and how many responses received from the server.
        /// The object is generally created at the begining of Send().
        /// This may not be used in every step.
        /// </summary>
        protected CommandResponseRegister CommandAndStatusRegister
        { get; private set; }

        /// <summary>
        /// Hold SyncML model of the message from the server in ProcessResponse.
        /// </summary>
        protected SyncMLSyncML ServerSyncML
        { get; set; }

        private SyncMLSyncML clientSyncML;

        /// <summary>
        /// Client SyncML to be constructed
        ///•  The value of the VerDTD element MUST be '1.2'.
        ///• The VerProto element MUST be included to specify the sync protocol and the version of
        ///the protocol. The value MUST be 'SyncML/1.2' when complying with this specification.
        ///• Session ID MUST be included to indicate the ID of a sync session.
        ///• MsgID MUST be used to unambiguously identify the message belonging a sync session
        ///and traveling from the client to the server.
        ///• The Target element MUST be used to identify the target device and service.
        ///• The Source element MUST be used to identify the source device.
        /// </summary>
        protected SyncMLSyncML ClientSyncML
        {
            get
            {
                if (clientSyncML == null)
                {
                    clientSyncML = SyncMLSyncML.Create();
                    clientSyncML.Hdr.VerDTD.Content = "1.2";
                    clientSyncML.Hdr.VerProto.Content = "SyncML/1.2";
                    clientSyncML.Hdr.SessionID.Content = Facade.CurrentSessionID;
                    clientSyncML.Hdr.MsgID.Content = Facade.CurrentMsgID.ToString();
                    clientSyncML.Hdr.Target.LocURI.Content = Facade.BasicUriText;
                    clientSyncML.Hdr.Source.LocURI.Content = Facade.DeviceAddress;

                    CreateCommonSyncMLWithCredential();
                }
                return clientSyncML;
            }
        }

        static readonly XElement max50KBToReceiveFromServer=XElement.Parse("<MaxMsgSize xmlns=\"syncml:metinf\">50000</MaxMsgSize>");
        /// <summary>
        /// The Cred element MUST be included if the authentication is needed.
        /// </summary>
        /// <returns>Basic XML used by other processes, with basic Cred.</returns>
        protected SyncMLSyncML CreateCommonSyncMLWithCredential()
        {
            switch (Facade.AuthenticationTypeOfNextMessage)
            {
                case SyncMLAuthenticationType.None:
                    break;
                case SyncMLAuthenticationType.Base64:
                    AddBase64Credential(ClientSyncML);
                    ClientSyncML.Hdr.Meta.Xml.Add(max50KBToReceiveFromServer);  //max 50KB to receive from server
                    Facade.AuthenticationTypeOfNextMessage = SyncMLAuthenticationType.None;
                    break;
                case SyncMLAuthenticationType.MD5:
                    AddMD5Credential(ClientSyncML);
                    ClientSyncML.Hdr.Meta.Xml.Add(max50KBToReceiveFromServer);  //max 50KB to receive from server
                    Facade.AuthenticationTypeOfNextMessage = SyncMLAuthenticationType.None;
                    break;
                default:
                    Debug.Assert(false, "Something wrong with AuthenticationTypeOfNextMessage" );
                    break;
            }

            return ClientSyncML;
        }

        static readonly XElement b64Format = XElement.Parse("<Format xmlns=\"syncml:metinf\">b64</Format>");

        static readonly XElement authBasicType = XElement.Parse("<Type xmlns=\"syncml:metinf\">syncml:auth-basic</Type>");

        protected void AddBase64Credential(SyncMLSyncML syncml)
        {
            syncml.Hdr.Cred.Meta.Xml.Add(b64Format);
            syncml.Hdr.Cred.Meta.Xml.Add(authBasicType);
            syncml.Hdr.Cred.Data.Content = UserPasswordBase64Encoded;
        }

        static readonly XElement authMd5Type = XElement.Parse("<Type xmlns=\"syncml:metinf\">syncml:auth-md5</Type>");

        protected void AddMD5Credential(SyncMLSyncML syncml)
        {
            Debug.WriteLine("Next nonce from server is: " + Facade.Md5NextNonceFromServer);
            syncml.Hdr.Source.LocName.Content = Facade.User;
            syncml.Hdr.Cred.Meta.Xml.Add(b64Format);
            syncml.Hdr.Cred.Meta.Xml.Add(authMd5Type);
            syncml.Hdr.Cred.Data.Content = CreateMD5Digest(Facade.Md5NextNonceFromServer);
        }

        /// <summary>
        /// Base64 encoded user password
        /// </summary>
        private string UserPasswordBase64Encoded
        {
            get { return Utility.ConvertUtf8TextToBase64(Facade.User + ":" + Facade.Password); }
        }


        /// <summary>
        /// Let H = the MD5 Hashing function.
        /// Let Digest = the output of the MD5 Hashing function.
        /// Let B64 = the base64 encoding function.
        /// Digest = H(B64(H(username':'password))':'nonce)
        /// </summary>
        /// <param name="nonce">Nonce from the challenger.</param>
        /// <returns>The digest.</returns>
        private string CreateMD5Digest(byte[] nonce)
        {
            return Utility.GenerateSyncMLMD5(Facade.User, Facade.Password, nonce);

        }

    }

    /// <summary>
    /// With common handlings of response from the server.
    /// And the algorithm has recursive uses of derived classes
    /// </summary>
    internal abstract class SyncMLSyncMessageBase : SyncMLMessageBase
    {
        protected SyncMLSyncMessageBase(SyncMLFacade facade)
            : base(facade)
        {

        }

        /// <summary>
        /// Move pending commands in pool into a new message.
        /// And each command will be assigned with an CmdID.
        /// When analyzing the response from server, new commands may be generated
        /// for next message to server.
        /// 
        /// This function is better to be put at the end of construction of SyncML message.
        /// </summary>
        /// <param name="syncML">New message to be amended before sending to the server.</param>
        ///<returns>Ture if commmands are added from pool; false if no command added.</returns>
        protected bool MoveCommandsInPoolToMessage(SyncMLSyncML syncML)
        {
            if (Facade.ResponseCommandPool.Count > 1)//if there's only one which is for Status with Hdr, no need to send back
            {
                foreach (SyncMLCommand command in Facade.ResponseCommandPool)
                {
                    command.CmdID = syncML.NextCmdID;
                    syncML.Body.Commands.Add(command);
                }

                Facade.ResponseCommandPool.Clear();
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// So URI of Post will likely be using session id returned by the server.
        /// </summary>
        /// <param name="syncMLFromServer">SyncML message from the server.</param>
        private void UpdateCurrentURI(SyncMLSyncML syncMLFromServer)
        {
            string newUri = syncMLFromServer.Hdr.RespURI.Content;
            if (!String.IsNullOrEmpty(newUri))
            {
                Facade.Connections.Uri = newUri;
            }
        }

        protected override bool ProcessResponse(string text)
        {
            if (!base.ProcessResponse(text))
                return false;

            // So now syncml model is created from text
            UpdateCurrentURI(ServerSyncML);

            //0: Always have a status response to the SyncHdr of the server message. However, this status may not be sent back if it is the only one in the queue.
            SyncMLStatus responseStatus = SyncMLStatus.Create();
            responseStatus.MsgRef.Content = ServerSyncML.Hdr.MsgID.Content;
            responseStatus.Data.Content = "200";
            responseStatus.Cmd.Content = "SyncHdr";
            responseStatus.CmdRef.Content = "0";
            responseStatus.TargetRefCollection.Add(SyncMLSimpleElementFactory.Create<SyncMLTargetRef>(ServerSyncML.Hdr.Target.LocURI.Content));
            responseStatus.SourceRefCollection.Add(SyncMLSimpleElementFactory.Create<SyncMLSourceRef>(ServerSyncML.Hdr.Source.LocURI.Content));
            Facade.ResponseCommandPool.Add(responseStatus);//respond in next request.

            //1: Handle returned status commands
            Collection<SyncMLStatus> serverStatusCommands = AccessBody.GetStatusCommands(ServerSyncML);
            foreach (SyncMLStatus status in serverStatusCommands)
            {
                CommandAndStatusRegister.RegisterStatus(status.CmdRef.Content, status.Data.Content);
                HandleServerStatus(status); //this fn is still abstract here. Derived classes have different ways of handling.
            }

            //2: Prepare status commands for returned Alert commands. Derived classes then handle the alerts all the same way.
            Collection<SyncMLAlert> serverAlertCommands = AccessBody.GetAlertCommands(ServerSyncML);
            foreach (SyncMLAlert alert in serverAlertCommands)
            {
                Debug.WriteLine("Alert:" + alert.Xml.ToString());
                PrepareStatusForReturnedAlert(alert);
            }

            //3: Handle returned Sync command. Derived classes handle sync commands the same way.
            SyncMLSync serverSyncCommand = AccessBody.GetSyncCommand(ServerSyncML);
            if (serverSyncCommand != null)
            {
                string numberOfChangesStr = serverSyncCommand.NumberOfChanges.Content;
                if (!String.IsNullOrEmpty(numberOfChangesStr))
                {//where numberOfChanges > 0, display progress bar in GUI.
                    Facade.totalNumberOfChangesReceiving = Convert.ToInt32(numberOfChangesStr);
                    if (Facade.totalNumberOfChangesReceiving > 0)
                    {
                        Facade.DisplayOperationMessage(String.Format("Total number of changes received from the server: {0}", numberOfChangesStr));
                        Facade.InitProgressBarReceiving(0, Facade.totalNumberOfChangesReceiving, 1);
                        Facade.DisplayStageMessageReceiving(String.Format("Receiving {0} updates ...", Facade.totalNumberOfChangesReceiving));
                    }
                }

                int numbersOfChangesThisMessage = serverSyncCommand.Commands.Count; // the server might send in multiple messages
                Facade.numberOfChangesReceived += numbersOfChangesThisMessage;
                if (Facade.numberOfChangesReceived == Facade.totalNumberOfChangesReceiving)
                    Facade.DisplayStageMessageReceiving("Receiving Done");


                GenerateStatusCommandsForSyncCommand(serverSyncCommand);

                if (Facade.GracefulStop)
                    return true; // simply return, ture of false is meaningless.

                Facade.IncrementProgressBarReceiving(numbersOfChangesThisMessage);

                ApplySyncCommandToLocal(serverSyncCommand);
            }

            //4: Verify if the server return all status codes to commands sent
            if (!CommandAndStatusRegister.IsAllCommandsReturnedWithStatus())
            {
                Trace.TraceInformation("!!!! Not all commands got status code. Please check the log for details.");
                Trace.TraceInformation("Commands without status: " + CommandAndStatusRegister.CommandsXmlWithoutStatus);
                //It is expected CommandAndStatusRegister is not used any more, otherwise, should clear it here.
            }

            //5: At the end, do what the server ask to do, likely a new SyncML message to be sent
            ProcessServerAlertCommands(serverAlertCommands);

            return true;
        }

        /// <summary>
        /// The derived class might need to call CleanUp in ProcessResponse.
        /// </summary>
        protected void CleanUp()
        {
            if ((!ServerSyncML.Body.HasFinal) && // server should has more
                (Facade.ResponseCommandPool.Count < 2) && //there's no command to send except the status for server's SyncHdr
                (ClientSyncML.Body.HasFinal)) // last client message has final
            {
                SyncMLAlert alert = SyncMLAlert.Create();
                alert.CmdID = ClientSyncML.NextCmdID;
                alert.Data.Content = "222";
                SyncMLItem item = SyncMLItem.Create();
                item.Target.LocURI.Content = Facade.ContactDataSourceAtServer;
                item.Source.LocURI.Content = Facade.LocalDataSource.DataSourceName;
                alert.ItemCollection.Add(item);
                Facade.ResponseCommandPool.Add(alert);
                CommandAndStatusRegister.Add(alert);
                Facade.clientRequestMore = true;
            }

            //7:
            while (Facade.ResponseCommandPool.Count > 1)//if there's only one which is for Status with Hdr, no need to send back
            {
                CleaningUpStep mapStep = new CleaningUpStep(Facade);
                mapStep.Send();
            }
        }

        /// <summary>
        /// Process a sync command from server. This will update the local address book, and prepare map commands to be sent back to the server
        /// </summary>
        /// <param name="syncCommand"></param>
        private void ApplySyncCommandToLocal(SyncMLSync syncCommand)
        {
            Collection<SyncMLCommand> commands = syncCommand.Commands;
            if (commands.Count > 0)
            {
                //Step1: Apply the sync command to local through LocalDataSource.
                NameValueCollection localIDServerIDCollection = Facade.LocalDataSource.ApplySyncCommands(commands, Facade.LastAnchorTimeText);

                //Step2: Report error from LocalDataSource if any
                if (!String.IsNullOrEmpty(Facade.LocalDataSource.ErrorMessage))
                    Facade.DisplayOperationMessage("PlatformProvider error: " + Facade.LocalDataSource.ErrorMessage);


                //Step3: Prepare map command for next message to notify the server
                SyncMLMap map = GenerateMapCommand(localIDServerIDCollection, syncCommand);
                if (map.MapItemCollection.Count > 0)
                    Facade.ResponseCommandPool.Add(map);

                //Step4: Report to the end user about updated names
                if (!String.IsNullOrEmpty(Facade.LocalDataSource.NamesOfAddedItems))
                {
                    Facade.DisplayOperationMessage("Items added:");
                    Facade.DisplayOperationMessage(Facade.LocalDataSource.NamesOfAddedItems);
                }

                if (!String.IsNullOrEmpty(Facade.LocalDataSource.NamesOfUpdatedItems))
                {
                    Facade.DisplayOperationMessage("Items updated:");
                    Facade.DisplayOperationMessage(Facade.LocalDataSource.NamesOfUpdatedItems);
                }

                if (!String.IsNullOrEmpty(Facade.LocalDataSource.NamesOfDeletedItems))
                {
                    Facade.DisplayOperationMessage("Items deleted:");
                    Facade.DisplayOperationMessage(Facade.LocalDataSource.NamesOfDeletedItems);
                }


            }
        }

        /// <summary>
        /// Generate status commands for received Sync command, and put the commands into the pool.
        /// </summary>
        /// <param name="syncCommand">Sync command from the server.</param>
        private void GenerateStatusCommandsForSyncCommand(SyncMLSync syncCommand)
        {
            SyncMLStatus syncStatus = SyncMLStatus.Create();
            syncStatus.Cmd.Content = "Sync";
            syncStatus.CmdRef.Content = syncCommand.CmdID.Content;
            syncStatus.Data.Content = "200";
            syncStatus.MsgRef.Content = ServerSyncML.Hdr.MsgID.Content;
            syncStatus.TargetRefCollection.Add(SyncMLSimpleElementFactory.Create<SyncMLTargetRef>(syncCommand.Target.LocURI.Content));
            syncStatus.SourceRefCollection.Add(SyncMLSimpleElementFactory.Create<SyncMLSourceRef>(syncCommand.Source.LocURI.Content));
            Facade.ResponseCommandPool.Add(syncStatus);

            Collection<SyncMLCommand> commands = syncCommand.Commands;
            if (commands != null)
            {
                foreach (SyncMLCommand command in commands)
                {
                    SyncMLAdd addCommand = command as SyncMLAdd;
                    if (addCommand != null)
                    {
                        SyncMLStatus addStatus = SyncMLStatus.Create();
                        addStatus.Cmd.Content = "Add";
                        addStatus.CmdRef.Content = command.CmdID.Content;
                        addStatus.Data.Content = "200";
                        addStatus.MsgRef.Content = syncStatus.MsgRef.Content;
                        addStatus.SourceRefCollection.Add(SyncMLSimpleElementFactory.Create<SyncMLSourceRef>(addCommand.ItemCollection[0].Source.LocURI.Content));
                        Facade.ResponseCommandPool.Add(addStatus);

                        continue;
                    }

                    SyncMLReplace replaceCommand = command as SyncMLReplace;
                    if (replaceCommand != null)
                    {
                        SyncMLStatus replaceStatus = SyncMLStatus.Create();
                        replaceStatus.Cmd.Content = "Replace";
                        replaceStatus.CmdRef.Content = command.CmdID.Content;
                        replaceStatus.Data.Content = "200";
                        replaceStatus.MsgRef.Content = syncStatus.MsgRef.Content;
                        replaceStatus.TargetRefCollection.Add(SyncMLSimpleElementFactory.Create<SyncMLTargetRef>(replaceCommand.ItemCollection[0].Target.LocURI.Content));
                        Facade.ResponseCommandPool.Add(replaceStatus);

                        continue;
                    }

                    SyncMLDelete deleteCommand = command as SyncMLDelete;
                    if (deleteCommand != null)
                    {
                        SyncMLStatus deleteStatus = SyncMLStatus.Create();
                        deleteStatus.Cmd.Content = "Delete";
                        deleteStatus.CmdRef.Content = command.CmdID.Content;
                        deleteStatus.Data.Content = "200";
                        deleteStatus.MsgRef.Content = syncStatus.MsgRef.Content;
                        deleteStatus.SourceRefCollection.Add(SyncMLSimpleElementFactory.Create<SyncMLSourceRef>(deleteCommand.ItemCollection[0].Target.LocURI.Content));
                        Facade.ResponseCommandPool.Add(deleteStatus);

                        continue;
                    }

                }
            }
        }

        /// <summary>
        /// Called by ProcessResponseForXXX in order to respond to some Status returned by server
        /// </summary>
        /// <param name="status">SyncMLStatus object which is part of syncml.</param>
        protected abstract void HandleServerStatus(SyncMLStatus status);

        /// <summary>
        /// Called by ProcessResponseForXXX in order to create status for Alert returned by server.
        /// </summary>
        /// <param name="alert"></param>
        protected void PrepareStatusForReturnedAlert(SyncMLAlert alert)
        {
            switch (alert.Data.Content)
            {
                case "200":
                case "201":
                case "202":
                case "203":
                case "204":
                case "205":
                    SyncMLStatus responseStatus = SyncMLStatus.Create();
                    //status.CmdID is not defined here, but only defined right before sending next message
                    responseStatus.MsgRef.Content = ServerSyncML.Hdr.MsgID.Content;
                    responseStatus.Data.Content = "200";
                    responseStatus.Cmd.Content = "Alert";
                    responseStatus.CmdRef.Content = alert.CmdID.Content;
                    responseStatus.TargetRefCollection.Add(SyncMLSimpleElementFactory.Create<SyncMLTargetRef>(Facade.ContactDataSourceAtServer));
                    responseStatus.SourceRefCollection.Add(SyncMLSimpleElementFactory.Create<SyncMLSourceRef>(Facade.LocalDataSource.DataSourceName));

                    //Get alert anchor next
                    SyncMLItem alertItem = alert.ItemCollection[0];//assuming there's always one.
                    SyncMLMeta alertItemMeta = alertItem.Meta;  // assuming there's always one.
                    MetaParser metaParser = new MetaParser(alertItemMeta.Xml);
                    MetaAnchor alertAnchor = metaParser.GetMetaAnchor();
                    string alertAnchorNext = alertAnchor.Next.Content;

                    SyncMLItem statusItem = SyncMLItem.Create();
                    MetaAnchor statusAnchor = MetaAnchor.Create();
                    statusAnchor.Next = SyncMLSimpleElementFactory.Create<MetaNext>(alertAnchorNext);
                    statusItem.Data.Xml.Add(statusAnchor.Xml);

                    responseStatus.ItemCollection.Add(statusItem);

                    Facade.ResponseCommandPool.Add(responseStatus);
                    break;
                case "100":
                //Show. The Data element type contains content information that should be processed and displayed through the user agent.
                    string serverStatusMessage = alert.ItemCollection[0].Data.Content;
                    Facade.DisplayOperationMessage(serverStatusMessage);

                    SyncMLStatus statusFor100 = SyncMLStatus.Create();
                    statusFor100.MsgRef.Content = ServerSyncML.Hdr.MsgID.Content;
                    statusFor100.Data.Content = "200";
                    statusFor100.Cmd.Content = "Alert";
                    statusFor100.CmdRef.Content = alert.CmdID.Content;
                    Facade.ResponseCommandPool.Add(statusFor100);

                    break;
                default:
                    Trace.TraceInformation("Do not know what to do in PrepareStatusForReturnedAlert:");
                    Trace.TraceInformation(alert.Xml.ToString());
                    break;
            }
        }


        /// <summary>
        /// Handle alert commands from the server.
        /// </summary>
        /// <param name="alertCommands"></param>
        private void ProcessServerAlertCommands(IList<SyncMLAlert> alertCommands)
        {
            if ((alertCommands == null) || (alertCommands.Count == 0))
                return;

            //RequestingNextMessage requestingNextMessage;
            foreach (SyncMLAlert alert in alertCommands)
            {
                SyncType syncType = AlertCodeToSyncType(alert.Data.Content);
                switch (syncType)
                {
                    case SyncType.TwoWay:
                        Facade.DisplayOperationMessage("Server requests for quick sync.");
                        SendSyncContent(false);
                        break;
                    case SyncType.Slow:
                        Facade.DisplayOperationMessage("Server requests for slown sync.");
                        SendSyncContent(true);
                        break;
                    case SyncType.OneWayFromClient:
                        Facade.DisplayOperationMessage("Server requests for one-way sync from client.");
                        SendSyncContent(false);  //The server will take care of the protocol, and will not send back data
                        break;
                    case SyncType.RefreshFromClient:
                        Facade.DisplayOperationMessage("Server requests for refresh from client.");
                      //  SendEmptySyncContent();
                     //   Debug.WriteLine("Finish sending empty content to server.");
                        // now the implementation is sending empty content first, then slow sync. 
                         SendSyncContent(true);  //The server will take care of the protocol, and will not send back data
                        break;

                    case SyncType.OneWayFromServer:
                        Facade.DisplayOperationMessage("Server requests for one-way sync from server.");
                        SendingEmptySyncMessage sendingEmptySyncMessageForRefresh = new SendingEmptySyncMessage(Facade);
                        sendingEmptySyncMessageForRefresh.Send();
                        break;

                    case SyncType.RefreshFromServer:
                        Facade.DisplayOperationMessage("Server requests for refresh from server.");
                            Facade.DisplayOperationMessage("deleting all contacts of local database...");
                            if (Facade.LocalDataSource.DeleteAllItems())
                            {
                                Facade.DisplayOperationMessage("All contacts is deleted in local database.");
                            }
                            else
                            {
                                Facade.SoFarOK = false;
                                Facade.DisplayOperationMessage("Local data source rejects deleting all contacts, and the operation of refreshing from server can not continue.");
                                Debug.WriteLine("Local data source rejects deleting all contacts, and the operation of refreshing from server can not continue.");
                            }
                        
                        SendingEmptySyncMessage sendingEmptySyncMessage = new SendingEmptySyncMessage(Facade);
                        sendingEmptySyncMessage.Send();
                        break;
                    default:
                        Debug.WriteLine("Do not know what to do in ProcessServerAlertCommands:");
                        Debug.WriteLine(alert.Xml);
                        break;
                }

            }
        }

        private static SyncType AlertCodeToSyncType(string alertCode)
        {
            switch (alertCode)
            {
                case "200": return SyncType.TwoWay;
                case "201": return SyncType.Slow;
                case "202": return SyncType.OneWayFromClient;
                case "203": return SyncType.RefreshFromClient;
                case "204": return SyncType.OneWayFromServer;
                case "205": return SyncType.RefreshFromServer;
                default:
                    Debug.Assert(false, "Aller code: " + alertCode);
                    return SyncType.TwoWay; //never run, just to satisfy compiler
            }
        }

        void PrepareCommandsSource(DateTime anchor)
        {
            Facade.DisplayOperationMessage("Preparing local data...");
            Facade.CommandsToSend = Facade.GenerateSyncCommandsSource(anchor);// Facade.LocalDataSource.GenerateSyncCommandsSource(anchor);
            // System.Threading.Thread.Sleep(1500000); this is only for testing timeout
            //CommandsSourceGenerated = true;
            Facade.DisplayOperationMessage("Preparation completed.");
        }

        ///// <summary>
        ///// Send dummy message to server to avoid timeout. Funambol/Apache seems to have timeout of 15 minutes by default,
        ///// though I could not alter it through C:\Program Files\Funambol\ds-server\default\config\tomcat\xml\WEB-INF\web.xml
        ///// 100 minutes should be long enough.
        ///// </summary>
        //void KeepServerBusy()
        //{
        //    for (int i = 0; i < 360; i++)// so 6 hours
        //    {
        //        if (CommandsSourceGenerated)
        //            break;
        //        System.Threading.Thread.Sleep(60000);//so ping every 1 minute
        //        if (CommandsSourceGenerated)
        //            break;

        //        string s = Facade.Connections.GetResponseText("Silly Ping");
        //        Debug.WriteLine("Server response to ping: " + s);
        //    }

        //    Debug.WriteLine("KeepServerBusy end");
        //}

      //  static object myLock = new object();

        //bool commandsSourceGenerated;
        //bool CommandsSourceGenerated
        //{
        //    get
        //    {
        //        lock (myLock)
        //        {
        //            return commandsSourceGenerated;
        //        }
        //    }

        //    set
        //    {
        //        lock (myLock)
        //        {
        //            commandsSourceGenerated = value;
        //        }
        //    }
        //}

        /// <summary>
        /// Send sync content in one or more messages.
        /// This function will spin out two thread, one for preparing commands source,
        /// and the other to keey server busy to avoid timeout, as the localDataSource
        /// might need long time to prepare data.
        /// </summary>
        /// <param name="slowSync">True to do slow sync.</param>
        private void SendSyncContent(bool slowSync)
        {
            //1: Generate SyncCommands in a buffer
            try
            {
                DateTime anchor = slowSync ? DateTime.MinValue : Facade.LastAnchorTime;
              //  CommandsSourceGenerated = false;
                Action<DateTime> prepare = PrepareCommandsSource;
                IAsyncResult resultOfPrepare = prepare.BeginInvoke(anchor, null, null);

            //    Action sillyPing = KeepServerBusy;
            //    IAsyncResult resultOfSillyPing = sillyPing.BeginInvoke(null, null);

                try
                {
                    System.Threading.WaitHandle.WaitAny(
                         new System.Threading.WaitHandle[] { resultOfPrepare.AsyncWaitHandle});//, resultOfSillyPing.AsyncWaitHandle 
                    Debug.WriteLine("WaitAny end"); //generally the preparation finishs first.

                    prepare.EndInvoke(resultOfPrepare);
                    Debug.WriteLine("Parpare End");
                 /*   if (System.Threading.WaitHandle.SignalAndWait(resultOfSillyPing.AsyncWaitHandle, resultOfPrepare.AsyncWaitHandle))
                    {
                        Debug.WriteLine("Both Preparation and SillyPing end");
                    }
                    else
                    {
                        sillyPing.EndInvoke(resultOfSillyPing);
                        Debug.WriteLine("SillyPing End");
                    }*/
                    // now 2 threads join
                }
                catch (Exception e)
                {
                    Trace.TraceWarning("wrong here: " + e.ToString());
                    Facade.SoFarOK = false;
                    throw new FacadeErrorException("Preparation or SillyPing might be wrong.", e);
                }

                if (Facade.CommandsToSend == null)
                {
                    Trace.TraceInformation("Can not get CommandsSource from thd local data source.");
                    Facade.SoFarOK = false;
                    return;
                }
            }
            catch (FacadeErrorException e) // triggered by XsltException 
            {
                Trace.TraceInformation("When SendSyncContent: "+e.Message);
                if (e.InnerException != null)
                    Trace.TraceInformation(e.InnerException.ToString() + "~" + e.InnerException.Message);
                Facade.SoFarOK = false;
                return;
            }

            Facade.totalNumberOfChangesSending = Facade.CommandsToSend.NumberOfChanges;

            if (Facade.totalNumberOfChangesSending > 0)
            {
                Facade.DisplayOperationMessage(String.Format("Total number of changes to be sent to the server: {0}", Facade.totalNumberOfChangesSending));
                Facade.DisplayOperationMessage(Facade.LocalDataSource.SummaryOfGeneratedSyncCommands);
                Facade.InitProgressBar(0, Facade.totalNumberOfChangesSending, SyncConstants.MaxCommandsPerBatch);
                Facade.DisplayStageMessage(String.Format("Sending {0} updates ...", Facade.totalNumberOfChangesSending));
            }

            if (Facade.GracefulStop)
                return;

            //2: Send the first batch of sync commands, then may be recursive call to SendinSyncMessage
            int numberOfCommandsSent = 0;
            SendingSyncMessage sendingSyncStep = new SendingSyncMessage(Facade, Facade.totalNumberOfChangesSending);
            sendingSyncStep.Send();

            numberOfCommandsSent += sendingSyncStep.NumberOfCommandsToSend;
            Facade.IncrementProgressBar(sendingSyncStep.NumberOfCommandsToSend);

            if (Facade.GracefulStop)
                return;
        }

        //private void SendEmptySyncContent()
        //{
        //    //1: Generate SyncCommands in a buffer
        //    Facade.CommandsToSend = new DummyEmptyCommandSource();

        //    Facade.totalNumberOfChangesSending = Facade.CommandsToSend.NumberOfChanges;

        //    //2: Send the first batch of sync commands.
        //    SendingSyncMessage sendingSyncStep = new SendingSyncMessage(Facade, Facade.totalNumberOfChangesSending);
        //    sendingSyncStep.Send();
        //}

        /// <summary>
        /// Generate map command through LocalID/ServerID paris.
        /// </summary>
        /// <param name="localIDServerIDPairs">The pairs is returned by local address book.</param>
        /// <param name="previousServerSyncCommand">The map command need to obtain some data from previous sync command.</param>
        /// <returns>The map command to be integrated into next SyncML message sent back to the server. CmdID is not assigned yet.</returns>
        private static SyncMLMap GenerateMapCommand(NameValueCollection localIDServerIDPairs, SyncMLSync previousServerSyncCommand)
        {
            SyncMLMap map = SyncMLMap.Create();
            map.Target.LocURI.Content = previousServerSyncCommand.Source.LocURI.Content;
            map.Source.LocURI.Content = previousServerSyncCommand.Target.LocURI.Content;

            for (int i = 0; i < localIDServerIDPairs.Count; i++)
            {
                SyncMLMapItem mapItem = SyncMLMapItem.Create();
                mapItem.Target.LocURI.Content = localIDServerIDPairs.Get(i);
                mapItem.Source.LocURI.Content = localIDServerIDPairs.GetKey(i);
                map.MapItemCollection.Add(mapItem);
            }

            return map;
        }

        protected string GetStatusReport(SyncMLStatus status)
        {
            return String.Format("Status: {0}  CmdRef: {1}  Cmd: {2} " + Environment.NewLine + "{3}",
                status.Data.Content, status.CmdRef.Content, status.Cmd.Content,
                Facade.StatusMessages.GetMessage(status.Data.Content));
        }

    }

}
