using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Fonlow.SyncML.Elements
{
    /// <summary>
    /// Access different parts of the body of SyncML.
    /// Collection returned may be empty, and single command returned may be null.
    /// ((Alert | Atomic | Copy | Exec | Get | Map | Move | Put | Results | Search| Sequence | Status | Sync | Add | Replace | Delete)+, Final?)
    /// </summary>
    public sealed class AccessBody
    {
        private AccessBody()
        {

        }
        public static Collection<SyncMLAlert> GetAlertCommands(SyncMLSyncML syncml)
        {
            return AccessBodyCommand<SyncMLAlert>.GetCommands(syncml);
        }

        public static Collection<SyncMLStatus> GetStatusCommands(SyncMLSyncML syncml)
        {
            return AccessBodyCommand<SyncMLStatus>.GetCommands(syncml);
        }

        public static Collection<SyncMLResults> GetResultsCommands(SyncMLSyncML syncml)
        {
            return AccessBodyCommand<SyncMLResults>.GetCommands(syncml);
        }

        public static SyncMLSync GetSyncCommand(SyncMLSyncML syncml)
        {
            return AccessBodyCommand<SyncMLSync>.GetCommand(syncml);
        }
    }

    /// <summary>
    /// Provide basic generic function to class AccessBody.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    sealed class AccessBodyCommand<T> where T : SyncMLComplexElement
    {
        private AccessBodyCommand()
        {

        }
        /// <summary>
        /// Get a syncML command.
        /// </summary>
        /// <param name="syncml"></param>
        /// <returns>Null if no command expected.</returns>
        public static T GetCommand(SyncMLSyncML syncml)
        {
            SyncMLBody body = syncml.Body;
            foreach (SyncMLComplexElement item in body.Commands)
            {
                T r = item as T;
                if (r != null)
                    return r;
            }
            return null;

        }

        /// <summary>
        /// Get collection of command T.
        /// </summary>
        /// <param name="syncml">SyncML message</param>
        /// <returns>Collection of coimmand T. If no command, the collection is empty.</returns>
        public static Collection<T>  GetCommands(SyncMLSyncML syncml)
        {
            SyncMLBody body = syncml.Body;
            Collection<T> collection = new Collection<T>();
            foreach (SyncMLComplexElement item in body.Commands)
            {
                T r = item as T;
                if (r!=null)
                    collection.Add(r);
            }
            return collection;

        }
    }
}
