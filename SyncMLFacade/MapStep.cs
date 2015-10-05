using System;
using System.Xml.Linq;
using System.Diagnostics;
using Fonlow.SyncML.Elements;


namespace Fonlow.SyncML
{
    /// <summary>
    ///  After applying changes to local, generate SyncML message from the commands pool.
    /// The Map command might be already stored in Facade.ResponseCommandPool,
    /// if the changes including adding contacts.
    /// </summary>
    internal class CleaningUpStep : SyncMLSyncMessageBase
    {
        public CleaningUpStep(SyncMLFacade facade)   : base(facade)
        {

        }

        protected override void HandleServerStatus(SyncMLStatus status)
        {
            //do nothing, as not more work needs to be done.
        }
        /// <summary>
        /// Send pending commands to the server. If no pending commands, do nothing.
        /// </summary>
        public override void Send()
        {
            SyncMLSyncML syncml = CreateMessageFromCommandsPool(); // the returned message might contain responses to previous server message.

            if (syncml == null) // If no more command to send, don't do any thing.
                return;

            syncml.Hdr.Target.LocURI.Content = Facade.BasicUriText; //Facade.CurrentRespUri;

            if (!Facade.clientRequestMore)
            {
                syncml.Body.MarkFinal();// This is to create Final in body.
            }
            else
            {
                Facade.clientRequestMore = false;
            }


            Debug.WriteLine("Sending Cleaning up:" + syncml.Xml.ToString());
            string responseText = Facade.Connections.GetResponseText(syncml.Xml.ToString(SaveOptions.DisableFormatting));
            if (String.IsNullOrEmpty(responseText))
                return;

            // Increase CurrentMsgID for next message sent to the server
            Facade.CurrentMsgID++;

            ProcessResponse(responseText);
        }

        /// <summary>
        /// Create SyncML if there's any command pending to sent from the pool.
        /// </summary>
        /// <returns>Null if no more command to send.</returns>
        private SyncMLSyncML CreateMessageFromCommandsPool()
        {
            if (MoveCommandsInPoolToMessage(ClientSyncML))
                return ClientSyncML;
            else
                return null;
        }

        protected override bool ProcessResponse(string text)
        {
            if (!base.ProcessResponse(text))
                return false;

            CleanUp();
            return true;
        }
    }

}

